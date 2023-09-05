using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using WpfForM_CRM.Pages;

namespace WpfForM_CRM;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int second = 0;
    private int hour = 0;
    private int minut = 0;
    public MainWindow()
    {
        InitializeComponent();
        mainframe.Navigate(new MainMenuPage());
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval += TimeSpan.FromSeconds(1);
        timer.Start();
        exit_btn.Background = Brushes.Blue;
    }

    

    private void Page1_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void exit_btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        exit_btn.Background = Brushes.Red;
    }

    private void exit_btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        exit_btn.Background = Brushes.Blue;
    }

    private void DockPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        DragMove();
    }
}