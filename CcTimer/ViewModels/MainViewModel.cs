using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using CcTimer.Services;
using CcTimer.Tracking;
using MahApps.Metro.Controls.Dialogs;
using PropertyChanged.SourceGenerator;

namespace CcTimer.ViewModels;

/// <summary>
/// View model for the main window.
/// </summary>
public partial class MainViewModel
{
    private readonly IDialogCoordinator _dialogCoordinator;
    private readonly GameStateService _gameStateService;
    private readonly DispatcherTimer _refreshTimer;
    
    /// <summary>
    /// Gets or sets the current game state.
    /// </summary>
    [Notify] private GameState _currentState = GameState.Detached;
    
    /// <summary>
    /// Gets the version number of the application.
    /// </summary>
    public string VersionNumber => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown version";
    
    /// <summary>
    /// Gets the main window timer view model.
    /// </summary>
    public TimerViewModel TimerVm { get; }

    /// <summary>
    /// Gets a boolean indicating if the game is tracked but is currently in a menu.
    /// </summary>
    public bool IsInMenu => CurrentState.IsAttached && !CurrentState.IsInGame;

    /// <summary>
    /// Gets a boolean indicating if the game is tracked and in game.
    /// </summary>
    public bool IsInGame => CurrentState.IsAttached && CurrentState.IsInGame;

    public MainViewModel(GameStateService gameStateService, IDialogCoordinator dialogCoordinator)
    {
        _gameStateService = gameStateService;
        _dialogCoordinator = dialogCoordinator;
        TimerVm = new TimerViewModel();

        // Use a refresh rate superior to the game's 30FPS. If we set it to 30, it will miss some frames
        // (because of timer precision) and the timer will feel laggy.
        _refreshTimer = new DispatcherTimer(TimeSpan.FromSeconds(1f / 60), DispatcherPriority.Normal, OnRefreshTick,
            Application.Current.Dispatcher);
        _refreshTimer.Start();
    }

    /// <summary>
    /// Callback. Called when the refresh timer ticks.
    /// </summary>
    private void OnRefreshTick(object? sender, EventArgs e)
    {
        CurrentState = _gameStateService.GetCurrentState();
        TimerVm.CurrentState = CurrentState;
    }
}