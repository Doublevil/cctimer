using System;
using MindControl;

namespace CcTimer.Tracking;

internal interface IKwVersion
{
    PointerPath GetElapsedTimePointerPath();
    PointerPath GetIsInGamePointerPath();
    PointerPath GetVersionSignaturePath();
}

internal class KwVersion112Steam : IKwVersion
{
    public PointerPath GetElapsedTimePointerPath() => "cnc3ep1.dat+7FB1B0";
    public PointerPath GetIsInGamePointerPath() => "cnc3ep1.dat+7F6818";
    public PointerPath GetVersionSignaturePath() => "cnc3ep1.dat+809667";
}

internal class KwVersion113Steam : IKwVersion
{
    public PointerPath GetElapsedTimePointerPath() => "cnc3ep1.dat+807C88";
    public PointerPath GetIsInGamePointerPath() => "cnc3ep1.dat+8032F0";
    public PointerPath GetVersionSignaturePath() => "cnc3ep1.dat+76D30C";
}

internal class KwVersion112SteamWith4K : IKwVersion
{
    public PointerPath GetElapsedTimePointerPath() => "cnc3ep1.dat+7F0A18";
    public PointerPath GetIsInGamePointerPath() => "cnc3ep1.dat+7EC080";
    public PointerPath GetVersionSignaturePath() => "cnc3ep1.dat+7E0B7F";
}

/// <summary>
/// Tracker for Command & Conquer 3: Kane's Wrath (Steam version).
/// </summary>
public class KwSteamGameTracker : ProcessTracker, IGameTracker
{
    private static readonly IKwVersion[] Versions =
    [
        new KwVersion112Steam(),
        new KwVersion113Steam(),
        new KwVersion112SteamWith4K()
    ];
    
    /// <summary>
    /// Builds the tracker.
    /// </summary>
    public KwSteamGameTracker() : base("cnc3ep1.dat") { }

    private IKwVersion? GetVersion(ProcessMemory processMemory)
    {
        foreach (var version in Versions)
        {
            var signature = processMemory.Read<byte>(version.GetVersionSignaturePath());
            if (signature.ValueOrDefault() is 5 or 6) // 5 is for vanilla, 6 is when Tacitus is installed
                return version;
        }
        
        return null;
    }
    
    /// <summary>
    /// Gets the current state of the tracked game.
    /// </summary>
    public GameState GetCurrentState()
    {
        var processMemory = GetProcessMemory();
        if (processMemory == null)
            return GameState.Detached;

        var version = GetVersion(processMemory);
        if (version == null)
            return GameState.Detached;
        
        bool isInGame = processMemory.Read<bool>(version.GetIsInGamePointerPath()).ValueOrDefault();
        
        return new GameState
        {
            IsAttached = true,
            IsInGame = isInGame,
            InGameTime = isInGame ?
                TimeSpan.FromMilliseconds(processMemory.Read<int>(version.GetElapsedTimePointerPath()).ValueOrDefault())
                : null
        };
    }
}