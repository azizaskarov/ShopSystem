using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfForM_CRM.Pages.User;

namespace WpfForM_CRM;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        
        InitializeComponent();
        mainframe.Navigate(new MainMenuPage(this));
        
        
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
        exit_btn.Background = Brushes.CadetBlue;
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

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
}