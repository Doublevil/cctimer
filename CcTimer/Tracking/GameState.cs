using System;

namespace CcTimer.Tracking;

/// <summary>
/// Represents the tracking state of a game.
/// </summary>
public struct GameState
{
    /// <summary>
    /// Default game state, in a detached state.
    /// </summary>
    public static readonly GameState Detached = new();
    
    /// <summary>
    /// Gets or sets a value indicating if a game is currently being tracked.
    /// </summary>
    public bool IsAttached { get; init; }
    
    /// <summary>
    /// Gets or sets a value indicating if an actual game (a match) is ongoing in the game.
    /// </summary>
    public bool IsInGame { get; init; }
    
    /// <summary>
    /// Gets or sets the current in-game time.
    /// </summary>
    public TimeSpan? InGameTime { get; init; }
}