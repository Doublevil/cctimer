using System;
using MindControl;

namespace CcTimer.Tracking;

/// <summary>
/// Tracker for Command & Conquer 3: Kane's Wrath (Steam version).
/// </summary>
public class KwSteamGameTracker : ProcessTracker, IGameTracker
{
    private static readonly PointerPath ElapsedTimePointerPath = "cnc3ep1.dat+7F0A18";
    private static readonly PointerPath IsInGamePointerPath = "cnc3ep1.dat+7EC080";
    
    /// <summary>
    /// Builds the tracker.
    /// </summary>
    public KwSteamGameTracker() : base("cnc3ep1.dat") { }

    /// <summary>
    /// Gets the current state of the tracked game.
    /// </summary>
    public GameState GetCurrentState()
    {
        var processMemory = GetProcessMemory();
        if (processMemory == null)
            return GameState.Detached;

        bool isInGame = processMemory.ReadBool(IsInGamePointerPath).GetValueOrDefault();
        
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