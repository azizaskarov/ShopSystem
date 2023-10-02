using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages.User;

/// <summary>
/// Interaction logic for RegistrationPage.xaml
/// </summary>
public partial class RegistrationPage : Page
{
    private int _eyeCounter = 0;
    private int _eyeCounter2 = 0;

    private MainWindow mainWindow;
    public RegistrationPage(MainWindow mainWindow)
    {
        this.mainWindow = mainWindow;
        InitializeComponent();
        mainWindow.Background = new SolidColorBrush(Colors.White);
    }


    private void back_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.DockPanel.Width = 800;
        mainWindow.Window.Width = 800;
        mainWindow.Window.Height = 500;
        RegisterPage.NavigationService!.Navigate(new MainMenuPage(mainWindow));
    }

    private void reg_btn_Click(object sender, RoutedEventArgs e)
    {
        var username = login_txt.Text;
        var password = pass.Password;
        var confirmPassword = con_pass.Password;
          
        var dbContext = new AppDbContext();

        if (username.Length == 0 || (password.Length == 0 && pass2.Text.Length == 0) || (confirmPassword.Length == 0 && con_pass2.Text.Length == 0))
        {
            MessageBox.Show("Заполните необходимые поля");
            return;
        }

        if (dbContext.Users.Any(u => u.UserName == username))
        {
            MessageBox.Show("Пользователь уже существует");
            return;
        }

        if (!IsValidPassword(password))
        {
            // valid_lbl.Content = "Минимум 8 символов, хотя бы одна заглавная буква, одна строчная буква и одна цифра.";
            MessageBox.Show("Минимум 8 символов, хотя бы одна заглавная буква, одна строчная буква и одна цифра.");
            return;
        }

        if ((password != confirmPassword))
        {
            MessageBox.Show("Пароль неправильный");
            return;
        }

        var hashedPassword = GenerateHash(password);

        var user = new Entities.User()
        {
            UserName = username,
            Password = hashedPassword
        };

        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        NavigationService!.Navigate(new MainMenuPage(mainWindow,isRegistered: true,currentRegisterUser: username,currentPasswordUser: password));

    }

    public string GenerateHash(string password)
    {
        var hasher = new SHA256Managed();
        var unhashed = System.Text.Encoding.Unicode.GetBytes(password);
        var hashed = hasher.ComputeHash(unhashed);

        var hashedPassword = Convert.ToBase64String(hashed);
        return hashedPassword;
    }

    private bool IsValidPassword(string password)
    {
        var pattern = @"^(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*\d)[А-яa-zA-Z\d]{8,}$";
        if (string.IsNullOrWhiteSpace(password))
            return false;

        var result = Regex.Match(password, pattern);

        if (result.Success)
            return true;

        return false;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        RegisterPage.NavigationService.Navigate(new MainMenuPage(mainWindow));
    }

    private void pass_name_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (_eyeCounter == 2)
        {
            _eyeCounter = 0;
        }
        if (_eyeCounter == 0)
        {
            pass2.Visibility = Visibility.Visible;
            pass.Visibility = Visibility.Hidden;
            pass_name.Source =
                new BitmapImage(new Uri("../../IconImages/eye open.png", UriKind.Relative));
            _eyeCounter = 1;
        }
        else if (_eyeCounter == 1)
        {
            pass2.Visibility = Visibility.Hidden;
            pass.Visibility = Visibility.Visible;
            pass_name.Source =
                new BitmapImage(new Uri("../../IconImages/eye open.png", UriKind.Relative));
            _eyeCounter = 2;
        }
    }

    private void conpassname_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (_eyeCounter2 == 2)
        {
            _eyeCounter2 = 0;
        }
        if (_eyeCounter2 == 0)
        {
            con_pass2.Visibility = Visibility.Visible;
            con_pass.Visibility = Visibility.Hidden;
            conpassname.Source =
                new BitmapImage(new Uri("/Pages/eye close.png", UriKind.Relative));
            _eyeCounter2 = 1;
        }
        else if (_eyeCounter2 == 1)
        {
            con_pass2.Visibility = Visibility.Hidden;
            con_pass.Visibility = Visibility.Visible;
            conpassname.Source =
                new BitmapImage(new Uri("/Pages/eye open.png", UriKind.Relative));
            _eyeCounter2 = 2;
        }
    }

    private void pass2_TextChanged(object sender, TextChangedEventArgs e)
    {
        pass.Password = pass2.Text;
    }

    private void con_pass2_TextChanged(object sender, TextChangedEventArgs e)
    {
        con_pass.Password = con_pass2.Text;
    }

    private void pass_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (!pass.IsVisible)
        {
            pass2.Text = pass.Password;
        }
    }

    private void con_pass_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (!con_pass.IsVisible)
        {
            con_pass2.Text = con_pass.Password;
        }
    }

    private void login_txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        string pattern = @"^[ЁёА-яa-zA-Z0-9]+$";

        Regex regex = new Regex(pattern);

        if (!regex.IsMatch(e.Text))
        {
            e.Handled = true; 
        }
    }

    private void pass2_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        login_txt_PreviewTextInput(sender, e);
    }

    private void con_pass2_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        login_txt_PreviewTextInput(sender, e);
    }

    private void pass_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        login_txt_PreviewTextInput(sender, e);
    }

    private void con_pass_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        login_txt_PreviewTextInput(sender, e);
    }


    private void RegistrationPage_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            reg_btn_Click(sender, e);
        }
    }
}