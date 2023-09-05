using System.Windows;
using WpfForM_CRM.Context;
using WpfForM_CRM.Entities;

namespace WpfForM_CRM.Pages;

/// <summary>
/// Interaction logic for Add.xaml
/// </summary>
public partial class Add : Window
{
    ShopsPage shopsPage;
    public Add(ShopsPage model)
    {
        InitializeComponent();
        shopsPage = model;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

        var db = new AppDbContext();

        var shop = new Shop()
        {
            Name = shopname.Text,
            Owner = Properties.Settings.Default.Name
        };

        db.Shops.Add(shop);
        db.SaveChanges();
        shopsPage.Load();
        Close();
    }
}