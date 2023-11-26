namespace CcTimer.Tracking;

/// <summary>
/// Interface for all game trackers.
/// </summary>
public interface IGameTracker
{
    /// <summary>
    /// Gets the current state of the tracked game.
    /// </summary>
    GameState GetCurrentState();
}