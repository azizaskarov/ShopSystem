using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.User;

/// <summary>
/// Interaction logic for MainMenuPage.xaml
/// </summary>
public partial class MainMenuPage : Page
{
    private int _eyeCounter = 0;
    private string _passwordChange = string.Empty;
    private readonly MainWindow _mainWindow;
    public MainMenuPage(MainWindow mainWindow, bool isRegistered = false, string currentRegisterUser = "", string currentPasswordUser = "")
    {
        this._mainWindow = mainWindow;
        mainWindow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0F9FF"));
        InitializeComponent();
        EnterTextBox(isRegistered, currentRegisterUser, currentPasswordUser);
    }


    private void EnterTextBox(bool isRegistered = false, string currentRegisterUser = "", string currentPasswordUser = "")
    {
        if (isRegistered)
        {
            parol_txt.Password = currentPasswordUser;
            register_txt.Text = currentRegisterUser;
        }
        else
        {
            if (Properties.Settings.Default.RememberMe)
            {
                register_txt.Text = Properties.Settings.Default.Name;
                parol_txt.Password = Properties.Settings.Default.Password;
            }
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MainPage.NavigationService!.Navigate(new RegistrationPage(_mainWindow));
    }

    private void enter_btn_Click(object sender, RoutedEventArgs e)
    {
        var username = register_txt.Text;
        var password = parol_txt.Password;
        var invisiblePassword = parol_tx.Text;

        if (username.Length == 0 || password.Length == 0)
        {
            MessageBox.Show("Введите логин и пароль");
            return;
        }

        var dbContext = new AppDbContext();

        if (dbContext.Users.Any(u => u.UserName == username))
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(u => u.UserName == username);
                var page = new RegistrationPage(_mainWindow);
                var hash = page.GenerateHash(password);
                var invisibleHash = page.GenerateHash(invisiblePassword);

                if (hash == user.Password || invisibleHash == user.Password)
                {
                    if ((bool)checkMe.IsChecked!)
                    {
                        Properties.Settings.Default.Password = password;
                        Properties.Settings.Default.RememberMe = true;
                        Properties.Settings.Default.Name = username;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.Password = "";
                        Properties.Settings.Default.RememberMe = false;
                        Properties.Settings.Default.Name = "";
                        Properties.Settings.Default.Save();
                    }

                    

                    MainPage.NavigationService!.Navigate(new ShopsPage(_mainWindow, user.Id));
                }
                else
                {
                    MessageBox.Show("Неправильный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        else
        {
            MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (_eyeCounter == 2)
        {
            _eyeCounter = 0;
        }
        if (_eyeCounter == 0)
        {
            parol_tx.Visibility = Visibility.Visible;
            parol_txt.Visibility = Visibility.Hidden;
            eye.Source =
                new BitmapImage(new Uri("../../IconImages/eye close.png", UriKind.Relative));
            _eyeCounter = 1;
        }
        else if (_eyeCounter == 1)
        {
            parol_tx.Visibility = Visibility.Hidden;
            parol_txt.Visibility = Visibility.Visible;
            eye.Source =
                new BitmapImage(new Uri("../../IconImages/eye open.png", UriKind.Relative));
            _eyeCounter = 2;
        }
    }

    private void parol_tx_TextChanged(object sender, TextChangedEventArgs e)
    {
        parol_txt.Password = parol_tx.Text;
    }

    private void parol_txt_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (!parol_txt.IsVisible)
        {
            parol_tx.Text = parol_txt.Password;
        }
    }

    private void MainMenuPage_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            enter_btn_Click(sender, e);
        }
        else if (e.Key == Key.Escape)
        {
            Application.Current.Shutdown();
        }
    }


}