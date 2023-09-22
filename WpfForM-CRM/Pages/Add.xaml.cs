using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Entities;

namespace WpfForM_CRM.Pages;

/// <summary>
/// Interaction logic for Add.xaml
/// </summary>
public partial class Add : Window
{
    private ShopsPage shopsPage;
    public Add(MainWindow mainWindow, ShopsPage shopsPage)
    {
        this.shopsPage = shopsPage;
        InitializeComponent();
    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {


        var db = new AppDbContext();

        if ((db.Shops.Any(sh => sh.Name == shopname.Text)))
        {
            MessageBox.Show("Название магазина уже существует");
            return;
        }

        if (shopname.Text.Length == 0)
        {

            MessageBox.Show("Значение не указано", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;

        }

        var shop = new Shop()
        {
            Name = shopname.Text,
            UserId =(Guid)shopsPage.userId,
        };

        //var user = db.Users.FirstOrDefault(user => user.Id == shopsPage.userId);
        //if (user != null)
        //{
        //    user.Shops.Add(shop);
        //    db.Users.Update(user);
        //}


        //shopsPage.ShopId = shop.Id;
        db.Shops.Add(shop);

        var user = db.Users.FirstOrDefault(u => u.Id == shopsPage.userId);
        user.Shops.Add(shop);
        db.Users.Update(user);

        db.SaveChanges();
        shopsPage.Load();
        Close();
    }

    private void shopNamePreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var pattern = @"^[а-яА-Яa-zA-Z0-9]+$";
        var regex = new Regex(pattern);

        if (!regex.IsMatch(e.Text))
        {
            e.Handled = true;
        }
    }

    private void CreateShopWithEnterButton(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Button_Click(sender, e);
        }
    }
}