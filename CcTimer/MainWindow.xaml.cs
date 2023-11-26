using System;
using System.Windows;
using CcTimer.Services;
using CcTimer.ViewModels;
using CcTimer.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CcTimer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    private readonly MainViewModel _vm;
    
    public MainWindow()
    {
        _vm = new MainViewModel(new GameStateService(), DialogCoordinator.Instance);
        DataContext = _vm;
        InitializeComponent();
    }

    /// <summary>
    /// Callback.
    /// When main window is closed, shutdown the application.
    /// </summary>
    private void OnMainWindowClosed(object sender, EventArgs e) => Application.Current.Shutdown();

    /// <summary>
    /// Click callback for the overlay button.
    /// </summary>
    private void OnPopOutClicked(object sender, RoutedEventArgs e)
    {
        var overlayWindow = new TimerWindow
        {
            DataContext = _vm.TimerVm
        };
        overlayWindow.Show();
    }
}