using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CcTimer.Views;

public partial class TimerWindow : Window
{
    public TimerWindow()
    {
        InitializeComponent();
        Topmost = true;
        WindowToolPanel.Visibility = Visibility.Hidden;
    }
    
    /// <summary>
    /// Callback method for the Deactivated event of the window.
    /// Keeps the window on top of other windows.
    /// </summary>
    private void OnWindowDeactivated(object sender, EventArgs e)
    {
        Window window = (Window)sender;
        window.Topmost = true;
    }

    /// <summary>
    /// Callback method.
    /// Allows users to move the window by dragging on the drag control.
    /// </summary>
    private void OnDragControlMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            DragMove();
    }

    /// <summary>
    /// Callback method.
    /// Closes the window.
    /// </summary>
    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Callback method.
    /// Shows the window controls when the mouse enters the window.
    /// </summary>
    private void OnWindowMouseEnter(object sender, MouseEventArgs e)
    {
        WindowToolPanel.Visibility = Visibility.Visible;
        BorderBrush = new SolidColorBrush(Colors.LightGray);
    }

    /// <summary>
    /// Callback method.
    /// Hides the window controls when the mouse exits the window.
    /// </summary>
    private void OnWindowMouseLeave(object sender, MouseEventArgs e)
    {
        WindowToolPanel.Visibility = Visibility.Hidden;
        BorderBrush = new SolidColorBrush(Colors.Transparent);
    }

    private void OnResizerDrag(object sender, DragDeltaEventArgs e)
    {
        var totalChange = e.HorizontalChange + e.VerticalChange;
        var ratio = Width / Height;
        var multiplier = 0.3;

        var targetWidth = Math.Clamp(Width + totalChange * multiplier * ratio, 50, 1900);
        var targetHeight = Math.Clamp(Height + totalChange * multiplier, 20, 800);
        
        Width = targetWidth;
        Height = targetHeight;
    }
}