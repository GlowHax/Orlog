using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public enum GameState
{
    MainMenu,
    FavorSelection,
    Starting,
    RollPhase,
    FavorPhase,
    ResolutionPhase,
    Pause
}

public class GameManager : StaticInstance<GameManager>
{
    [SerializeField] private GameState State;
    public static event Action<GameState> OnGameStateChanged;

    public Player Player1;
    public Player Player2;

    public int RoundCounter = 1;
    public LinkedList<Player> PlayerOrder;
    public List<GodFavor> FavorsInResolvingOrder;

    public DieData[] DiceData = new DieData[6];

    public Player BeginningPlayer;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.FavorSelection:
                Player1 = new Player("Player 1");
                Player2 = new Player("Player 2");
                break;
            case GameState.Starting:
                PlayerOrder = new LinkedList<Player>();
                FavorsInResolvingOrder = new List<GodFavor>();
                RoundCounter = 0;
                PlayerOrder.Clear();
                if (Convert.ToBoolean(new System.Random().Next(2)))
                {
                    PlayerOrder.AddFirst(Player2);
                    PlayerOrder.AddLast(Player1);
                }
                else
                {
                    PlayerOrder.AddFirst(Player1);
                    PlayerOrder.AddLast(Player2);
                }
                ChangeState(GameState.RollPhase);
                break;
            case GameState.RollPhase:
                RoundCounter++;
                Player1.TurnCounter = 1;
                Player2.TurnCounter = 1;
                Player1.PickedResults = new List<DieResult>();
                Player2.PickedResults = new List<DieResult>();
                Player1.RemainingDice = DiceData.ToList();
                Player2.RemainingDice = DiceData.ToList();
                Player1.selectedGodfavor = null;
                Player2.selectedGodfavor = null;

                if(RoundCounter > 1 && BeginningPlayer == PlayerOrder.First.Value)
                {
                    SwitchActivePlayer();
                }
                BeginningPlayer = PlayerOrder.First.Value;

                break;
            case GameState.FavorPhase:
                break; 
            case GameState.ResolutionPhase:
                break;
            case GameState.Pause:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

        OnGameStateChanged?.Invoke(State);
    }

    public void SwitchActivePlayer()
    {
        Player temp = PlayerOrder.Last.Value;
        PlayerOrder.RemoveLast();
        PlayerOrder.AddFirst(temp);
    }

    public void EndRound()
    {
        Player losingPlayer = null;
        foreach (Player player in PlayerOrder)
        {
            if (player.Health <= 0)
            {
                losingPlayer = player;
            }
        }
        if (losingPlayer != null)
        {
            PlayerOrder.Remove(losingPlayer);
            UIManager.Instance.ShowText($"{PlayerOrder.First.Value.Name} WON THE GAME!");
        }
        else
        {
            ChangeState(GameState.RollPhase);
        }
    }

    public void ResolveNextGodFavor()
    {
        if(State == GameState.FavorPhase)
        {
            foreach (GodFavor favor in FavorsInResolvingOrder)
            {
                if (favor.Behaviour.EffectResolutionTime == ResolveEffect.BeforeResolutionPhase)
                {
                    FavorsInResolvingOrder.Remove(favor);
                    favor.ResolveEffect();
                    return;
                }
            }
            ChangeState(GameState.ResolutionPhase);
        }
        else if(State == GameState.ResolutionPhase)
        {
            foreach (GodFavor favor in FavorsInResolvingOrder)
            {
                if (favor.Behaviour.EffectResolutionTime == ResolveEffect.AfterResolutionPhase)
                {
                    FavorsInResolvingOrder.Remove(favor);
                    favor.ResolveEffect();
                    return;
                }
            }
            UIManager.Instance.ShowView("EndOfRoundView");
        } 
    }

    public Player ApplyRoundResults()
    {
        Player otherPlayer;
        if (BeginningPlayer == Player1)
        {
            otherPlayer = Player2;
        }
        else
        {
            otherPlayer= Player1;
        }

        //Apply Gold
        foreach(DieResult result in BeginningPlayer.PickedResults)
        {
            if (result.face.isGolden)
            {
                BeginningPlayer.FavorTokens++;
            }
        }
        foreach (DieResult result in otherPlayer.PickedResults)
        {
            if (result.face.isGolden)
            {
                otherPlayer.FavorTokens++;
            }
        }

        //Apply Arrows & Axes
        if (ExecuteAttack(BeginningPlayer, otherPlayer))
        {
            //beginningPlayer WON THE GAME
            return BeginningPlayer;
        }
        else if(ExecuteAttack(otherPlayer,BeginningPlayer))
        {
            //otherPlayer WON THE GAME
            return otherPlayer;
        }

        //Apply Hands
        foreach (DieResult result in BeginningPlayer.PickedResults)
        {
            if (result.face.name == "hand" && otherPlayer.FavorTokens > 0)
            {
                otherPlayer.FavorTokens--;
                BeginningPlayer.FavorTokens++;
            }
        }
        foreach (DieResult result in otherPlayer.PickedResults)
        {
            if (result.face.name == "hand" && BeginningPlayer.FavorTokens > 0)
            {
                BeginningPlayer.FavorTokens--;
                otherPlayer.FavorTokens++;
            }
        }

        return null;
    }

    private bool ExecuteAttack(Player attacker, Player defender)
    {
        int axes = 0;
        int arrows = 0;
        int shields = 0;
        int armours = 0;
        foreach (DieResult result in attacker.PickedResults)
        {
            if (result.face.name == "axe")
            {
                axes++;
            }
            else if (result.face.name == "arrow")
            {
                arrows++;
            }
        }

        foreach (DieResult result in defender.PickedResults)
        {
            if (result.face.name == "armour")
            {
                armours++;
            }
            else if (result.face.name == "shield")
            {
                shields++;
            }
        }

        defender.Health -= Math.Max(0, axes-armours) + Math.Max(0, arrows - shields);
        if(defender.Health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddGodFavorToResolutionList(GodFavor favor)
    {
        FavorsInResolvingOrder.Add(favor);
        FavorsInResolvingOrder.OrderBy(o => o.Behaviour.Priority);
    }

    public void EndTurn()
    {
        PlayerOrder.First.Value.TurnCounter++;

        bool activePlayerNoPicksLeft = false;
        bool nextPlayerNoPicksLeft = false;
        
        if(PlayerOrder.First.Value.RemainingDice.Count == 0)
        {
            activePlayerNoPicksLeft = true;
        }

        if(PlayerOrder.Last.Value.RemainingDice.Count == 0)
        {
            nextPlayerNoPicksLeft= true;
        }

        if (activePlayerNoPicksLeft && nextPlayerNoPicksLeft)
        {
            ChangeState(GameState.FavorPhase);
        }
        else if(!nextPlayerNoPicksLeft)
        {
            SwitchActivePlayer();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

[Serializable]
public class DieResult
{
    public Face face;

    public DieResult(Face face)
    {
        this.face = face;
    }
}