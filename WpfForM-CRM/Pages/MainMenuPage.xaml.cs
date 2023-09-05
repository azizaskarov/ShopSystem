using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages;

/// <summary>
/// Interaction logic for MainMenuPage.xaml
/// </summary>
public partial class MainMenuPage : Page
{
    private int eye_counter = 0;
    private string password_change = string.Empty;

    public MainMenuPage(bool enter = false, string login = "", string parol = "")
    {
        InitializeComponent();
        EnterTextBox(enter, login, parol);
    }

    private void EnterTextBox(bool enter = false, string login = "", string parol = "")
    {
        if (enter)
        {
            parol_txt.Password = parol;
            register_txt.Text = login;
        }
        else
        {
            if (Properties.Settings.Default.RememberMe)
            {
                register_txt.Text = Properties.Settings.Default.Owner;
                parol_txt.Password = Properties.Settings.Default.Password;
            }
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MainPage.NavigationService.Navigate(new RegistrationPage());
    }

    private void enter_btn_Click(object sender, RoutedEventArgs e)
    {
        var username = register_txt.Text;
        var password = parol_txt.Password;
        var invisiblePassword = parol_tx.Text;

        if (username.Length == 0 || password.Length == 0)
        {
            MessageBox.Show("Enter your login and password");
            return;
        }

        var dbContext = new AppDbContext();

        if (dbContext.Users.Any(u => u.UserName == username))
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(u => u.UserName == username);
                var page = new RegistrationPage();
                var hash = page.GenerateHash(password);
                var invisibleHash = page.GenerateHash(invisiblePassword);

                if (hash == user.Password || invisibleHash == user.Password)
                {
                    if ((bool)checkMe.IsChecked!)
                    {
                        Properties.Settings.Default.Password = password;
                        Properties.Settings.Default.Owner = username;
                        Properties.Settings.Default.RememberMe = true;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Name = username;
                    }
                    else
                    {
                        Properties.Settings.Default.Name = username;
                    }
                    MainPage.NavigationService.Navigate(new ShopsPage());
                }
                else
                {
                    MessageBox.Show("Invalid password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        else
        {
            MessageBox.Show("User not found");
        }
    }

    private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (eye_counter == 2)
        {
            eye_counter = 0;
        }
        if (eye_counter == 0)
        {
            parol_tx.Visibility = Visibility.Visible;
            parol_txt.Visibility = Visibility.Hidden;
            eye.Source =
                new BitmapImage(new Uri("/Pages/eye close.png", UriKind.Relative));
            eye_counter = 1;
        }
        else if (eye_counter == 1)
        {
            parol_tx.Visibility = Visibility.Hidden;
            parol_txt.Visibility = Visibility.Visible;
            eye.Source =
                new BitmapImage(new Uri("/Pages/eye open.png", UriKind.Relative));
            eye_counter = 2;
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
}