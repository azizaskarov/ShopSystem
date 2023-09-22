using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages;

/// <summary>
/// Interaction logic for ShopsPage.xaml
/// </summary>
public partial class ShopsPage : Page
{
    private MainWindow window;
    public Guid userId;
    private AppDbContext appDbContext;
    public ShopsPage(MainWindow window, Guid userId)
    {
        ;
        this.window = window;
        this.userId = userId;
        this.appDbContext = new AppDbContext();
        InitializeComponent();

    }

    public void UserShopsCount(Guid userId)
    {
        var shops = appDbContext.Shops.Where(u => u.UserId == userId);
        MessageBox.Show($"{shops.Count()}");
    }

    public Guid ShopId { get; set; }
    public Guid? CategoryId { get; set; }

    public void Load()
    {
        var db = new AppDbContext();

        var shops = db.Shops
            .Where(shop => shop.UserId == userId)
            .OrderByDescending(shop => shop.CreatedDate)
            .ToList();

        var list = new List<ShopControl>();

        foreach (var shop in shops)
        {
            var model = new ShopControl(this);
            model.ShopId = shop.Id;
            model.Name = shop.Name;
            list.Add(model);
        }

        addShopButton.Visibility = Visibility.Visible;
        shopsFrame.ItemsSource = list;
    }

    private void Button_ReadShops(object sender, RoutedEventArgs e)
    {
        TitleShop.Visibility = Visibility.Visible;
        addShopButton.Visibility = Visibility.Visible;
        SearchText.Visibility = Visibility.Visible;
        Load();
    }

    private void Button_AddShop(object sender, RoutedEventArgs e)
    {
        Add add = new Add(mainWindow: window, this);
        add.ShowDialog();
    }


    private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchTxt = SearchText.Text;

        var dbContext = new AppDbContext();

        var matchingShops = dbContext.Shops
            .Where(shop => shop.Name.Contains(searchTxt) && shop.UserId == userId)
            .ToList();

        var list = new List<ShopControl>();

        foreach (var shop in matchingShops)
        {
            var model = new ShopControl(this);
            //model.Width = 200;
            //model.Height = 40;
            model.Name = shop.Name;
            list.Add(model);
        }

        shopsFrame.ItemsSource = list;

    }
}