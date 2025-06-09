using CcTimer.Tracking;

namespace CcTimer.Services;

public class GameStateService
{
    private readonly IGameTracker[] _trackers = {
        new Ra3V112SteamGameTracker(),
        new Ra3V113SteamGameTracker(),
        new KwSteamGameTracker()
    };

    /// <summary>
    /// Gets the current tracking state by polling the trackers.
    /// If several trackers are attached to a game and in-game, an arbitrary one will be used.
    /// </summary>
    public GameState GetCurrentState()
    {
        GameState? attachedState = null;
        foreach (var tracker in _trackers)
        {
            var state = tracker.GetCurrentState();
            if (state is { IsAttached: true, IsInGame: true })
                return state;
            if (state.IsAttached)
                attachedState = state;
        }

        // No attached + in-game state. Return any attached state when available, or the Detached state otherwise.
        return attachedState ?? GameState.Detached;
    }
}