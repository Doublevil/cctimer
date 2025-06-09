using System;
using MindControl;

namespace CcTimer.Tracking;

public abstract class Ra3GameTracker(string processName) : ProcessTracker(processName), IGameTracker
{
    protected abstract PointerPath ElapsedTimePointerPath { get; }
    protected abstract PointerPath IsInGamePointerPath { get; }

    /// <summary>
    /// Gets the current state of the tracked game.
    /// </summary>
    public GameState GetCurrentState()
    {
        var processMemory = GetProcessMemory();
        if (processMemory == null)
            return GameState.Detached;

        bool isInGame = processMemory.Read<int>(IsInGamePointerPath).ValueOrDefault() > 0;
        
        return new GameState
        {
            IsAttached = true,
            IsInGame = isInGame,
            InGameTime = isInGame ?
                TimeSpan.FromMilliseconds(processMemory.Read<int>(ElapsedTimePointerPath).ValueOrDefault())
                : null
        };
    }
}

/// <summary>
/// Tracker for Command & Conquer: Red Alert 3 version 1.12 (Steam version).
/// </summary>
public class Ra3V112SteamGameTracker() : Ra3GameTracker("ra3_1.12.game")
{
    protected override PointerPath ElapsedTimePointerPath => "RA3_1.12.game+8D8C78";
    protected override PointerPath IsInGamePointerPath => "RA3_1.12.game+8D7AD8";
}

/// <summary>
/// Tracker for Command & Conquer: Red Alert 3 version 1.13 (Steam version).
/// </summary>
public class Ra3V113SteamGameTracker() : Ra3GameTracker("ra3_1.13.game")
{
    protected override PointerPath ElapsedTimePointerPath => "RA3_1.13.game+8E0F98";
    protected override PointerPath IsInGamePointerPath => "RA3_1.13.game+8B6040";
}
