using System;
using MindControl;

namespace CcTimer.Tracking;

/// <summary>
/// Tracker for Command & Conquer: Red Alert 3 (Steam version).
/// </summary>
public class Ra3SteamGameTracker : ProcessTracker, IGameTracker
{
    private static readonly PointerPath ElapsedTimePointerPath = "RA3_1.12.game+001FEBE8,0";
    private static readonly PointerPath IsInGamePointerPath = "RA3_1.12.game+001FEBE8,-D";
    
    /// <summary>
    /// Builds the tracker.
    /// </summary>
    public Ra3SteamGameTracker() : base("ra3_1.12.game") { }

    /// <summary>
    /// Gets the current state of the tracked game.
    /// </summary>
    public GameState GetCurrentState()
    {
        var processMemory = GetProcessMemory();
        if (processMemory == null)
            return GameState.Detached;

        bool isInGame = processMemory.ReadInt(IsInGamePointerPath).GetValueOrDefault() > 0;
        
        return new GameState
        {
            IsAttached = true,
            IsInGame = isInGame,
            InGameTime = isInGame ?
                TimeSpan.FromMilliseconds(processMemory.ReadInt(ElapsedTimePointerPath).GetValueOrDefault())
                : null
        };
    }
}