using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.Stock;

/// <summary>
/// Interaction logic for StockPage.xaml
/// </summary>
public partial class StockPage : Page
{
    public StockPage(ShopsPage shopsPage)
    {
      
        InitializeComponent();
        
        
        this.shopsPage = shopsPage;Load();

    }

    public ShopsPage shopsPage;

    public void Load()
    {
        var db = new AppDbContext();


        var items = new List<Entities.Stock>();
        var products = db.Products.Where(p => p.ShopId == shopsPage.ShopId).OrderByDescending(p => p.CreatedDate).ToList();
        int i = 1;

        if (products.Count > 0)
        {
            foreach (var product in products)
            {
                var childCategory = db.ChildCategories.FirstOrDefault(p => p.Id == product.ChildCategoryId);
                if (childCategory != null)
                {
                    var category = db.Categories.FirstOrDefault(p => p.Id == product.CategoryId);
                    

                    var item = new Entities.Stock
                    {

                        Number = i,
                        ProductName = product.Name, 
                        Barcode = product.Barcode,
                        ChildCategory = childCategory.Name,
                        Category = category!.Name,
                        OriginalPrice = product.OriginalPrice.ToString(),
                        SellingPrice = product.SellingPrice.ToString(),
                        Count = (product.Count ?? 1).ToString(),
                    };

                    items.Add(item);
                    i++;
                }

            }

            stockData.ItemsSource = items;
        }


        //var dbContext = new AppDbContext();
        //var stocks = new List<Entities.Stock>();
        //int numberStock = 1;
        //if (dbContext.Shops.Any(sh => sh.Id == shopsPage.ShopId))
        //{
        //    var shop = dbContext.Shops.First(sh => sh.Id == shopsPage.ShopId);
        //    var products = dbContext.Products.Where(p => p.ShopId == shop.Id).ToList().OrderByDescending(p => p.CreatedDate);



        //        foreach (var product in products)
        //        {
        //            var stock = new Entities.Stock()
        //            {
        //                Number = numberStock,
        //                ProductName = product.Name,
        //                Barcode = product.Barcode,
        //                Category = dbContext.Categories.First(c => c.Id == product.CategoryId).Name,
        //                ChildCategory = dbContext.ChildCategories.First(c => c.Id == product.ChildCategoryId).Name,
        //                OriginalPrice = product.OriginalPrice + " USZ",
        //                Count = product.Count ?? 1,
        //                SellingPrice = product.SellingPrice + " USZ"
        //            };

        //            stocks.Add(stock);
        //            numberStock++;
        //        }

        //        stockData.ItemsSource = stocks;

        //}
    }

    private void AddProductForStockBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var addProductStock = new AddProductStock(this);
        addProductStock.ShowDialog();
    }

    private void UpdateProductForStockBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var indexItem = stockData.SelectedIndex;
        if (indexItem == -1)
        {
            MessageBox.Show("tanglang");
            return;
        }

        var stocks = (List<Entities.Stock>)stockData.ItemsSource;
        var selectedStock = stocks[indexItem];
        var updateProductStock = new UpdateProductStock(this, selectedStock);
        updateProductStock.ShowDialog();
    }
}