using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

    public Guid? userId;
    private AppDbContext appDbContext;
    public ShopsPage(MainWindow window, Guid? userId = null,Guid? shopId = null)
    {
        ;
        this.window = window;
        this.userId = userId;
        this.appDbContext = new AppDbContext();
        InitializeComponent();

    }

    private string AddText { get; set; }

    public void UserShopsCount(Guid userId)
    {
        var shops = appDbContext.Shops.Where(u => u.UserId == userId);
        MessageBox.Show($"{shops.Count()}");
    }

    public Guid? ShopId { get; set; }
    public Guid? CategoryId { get; set; }

    public void Load()
    {
        Title.Text = "Мои магазины";
        AddText = "shop";
        var shops = appDbContext.Shops
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

    public void ReadCategories()
    {
        SearchText.Visibility = Visibility.Collapsed;
        AddText = "category";
        Title.Text = "Категории";
        var categories = appDbContext.Categories
            .Where(category => category.ShopId == ShopId)
            .OrderByDescending(category => category.CreatedDate).ToList();

        ShopNameTitle.Text = appDbContext.Shops.FirstOrDefault(shop => shop.Id == ShopId)!.Name;
        ShopNameTitle.Visibility = Visibility.Visible;

        var categoryControls = new List<CategoryControl>();
        foreach (var category in categories)
        {
            var categoryControl = new CategoryControl(this);
            categoryControl.Name = category.Name;
            categoryControl.CategoryId = category.Id;
            categoryControl.ShopId = ShopId;
            categoryControls.Add(categoryControl);
        }

        shopsFrame.ItemsSource = categoryControls;
    }

    private void Button_ReadShops(object sender, RoutedEventArgs e)
    {
        Title.Visibility = Visibility.Visible;
        addShopButton.Visibility = Visibility.Visible;
        SearchText.Visibility = Visibility.Visible;
        ShopNameTitle.Visibility = Visibility.Hidden;
        Load();
    }

    private void Button_Add(object sender, RoutedEventArgs e)
    {
        if (AddText =="shop")
        {
            AddShop();
        }

        if (AddText == "category")
        {
            AddCategory();
        }
    }

    private void AddShop()
    {
        Add add = new Add(mainWindow: window, this);
        add.ShowDialog();
    }
    private void AddCategory()
    {
        var addCategory = new AddCategory(this);
        addCategory.ShowDialog();
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

    private void AddCategoryName_OnClick(object sender, RoutedEventArgs e)
    {
        var addCategory = new AddCategory(this);
        addCategory.ShowDialog();
    }
}