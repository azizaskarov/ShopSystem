using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;
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
        exit_btn.Background = Brushes.Blue;
    }

    

    private void Page1_Click(object sender, RoutedEventArgs e)
    {
        var xButton = MessageBox.Show("Вы хотите выйти из приложения?", "Выйти", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (xButton == MessageBoxResult.Yes)
        {
            Application.Current.Shutdown();
        }

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


    private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Page1_Click(sender,e);
        }
    }

   
}