using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfForM_CRM.Context;
using WpfForM_CRM.Entities;

namespace WpfForM_CRM.Pages;

/// <summary>
/// Interaction logic for ShopsPage.xaml
/// </summary>
public partial class ShopsPage : Page
{
    private MainWindow window;
    public ShopsPage(MainWindow window)
    {
        this.window = window;
        InitializeComponent();
    }

    public Guid ShopId { get; set; }
    public Guid? CategoryId { get; set; }

    public void Load()
    {
        var db = new AppDbContext();
        var shops = db.Shops.ToList();
        var list = new List<ShopControl>();

        foreach (var shop in shops)
        {
            var model = new ShopControl(this);
            //model.Width = 200;
            //model.Height = 40;
            model.ShopId = shop.Id;
            model.Name = shop.Name;
            list.Add(model);
        }

        shopsFrame.ItemsSource = list;
    }

    private void Button_ReadShops(object sender, RoutedEventArgs e)
    {
        SearchText.Visibility = Visibility.Visible;
        poisk.Visibility = Visibility.Visible;
        Load();
    }

    private void Button_AddShop(object sender, RoutedEventArgs e)
    {
        Add add = new Add(mainWindow:window, this);
        add.ShowDialog();
    }

    //public void ReadCategories()
    //{
    //    SearchText.Visibility = Visibility.Collapsed;
    //    poisk.Visibility = Visibility.Collapsed;

    //    AddButton.Visibility = Visibility.Visible;
        
    //    var db = new AppDbContext();
    //    var categories = db.Categories.Where(category => category.ShopId == ShopId)
    //        .OrderByDescending(category => category.CreatedDate).ToList();
    //    var categoryControls = new List<CategoryControl>();

    //    foreach (var category in categories)
    //    {
    //        var categoryControl = new CategoryControl(this);
    //        //categoryControl.Width = 200;
    //        //categoryControl.Height = 40;
    //        categoryControl.CategoryTitle = category.Title;
    //        categoryControl.CategoryId = category.Id;
    //        categoryControls.Add(categoryControl);
    //    }

    //    shopsFrame.ItemsSource = categoryControls;
    //}

    //public void AddCategoryButton(object sender, RoutedEventArgs e)
    //{
    //    var category = new AddCategory(window, this);
    //    category.ShowDialog();
    //}

    private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchTxt = SearchText.Text;

        var dbContext = new AppDbContext();

        var matchingShops = dbContext.Shops
            .Where(shop => shop.Name.Contains(searchTxt))
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