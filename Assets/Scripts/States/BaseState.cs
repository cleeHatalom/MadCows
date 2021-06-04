using System;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseState<T> where T : System.Enum
{
    protected Dictionary<InputCommandEnum, T> transitions;

    protected abstract void PopulateTransitions(out Dictionary<InputCommandEnum, T> transitions);

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void UpdateState();

    /// <summary>
    /// Returns the enum of the next possible state in reaction to the command.
    /// Returns an invalid enum if the transition is not possible.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public abstract T ProcessCommand(InputCommandEnum command);
}

public abstract class GameState : BaseState<GameStateEnum>
{ 

}

public abstract class PlayerState : BaseState<PlayerStateEnum>
{

}

public enum GameStateEnum
{
    StartScreen,
    MainMenu,
    Settings,
    StageSelect,
    InGame_Infiltration,
    InGame_Siege,
    GameResults,
    BaseBuilding,
    Invalid
}

public enum PlayerStateEnum
{
    PlayerIdle,
    PlayerUnitPanel,
    PlayerViewInformation,
    PlayerCannon,
    PlayerPaused,
    Invalid
}

public enum InputCommandEnum
{

}