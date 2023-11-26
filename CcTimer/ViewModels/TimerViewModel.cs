using System.ComponentModel;
using System.Runtime.CompilerServices;
using CcTimer.Tracking;
using PropertyChanged.SourceGenerator;

namespace CcTimer.ViewModels;

/// <summary>
/// View model for timer views.
/// </summary>
public partial class TimerViewModel
{
    [Notify] private GameState _currentState = GameState.Detached;
}