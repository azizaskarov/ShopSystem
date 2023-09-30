using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.Stock;

/// <summary>
/// Interaction logic for FastArrivalProductStock.xaml
/// </summary>
public partial class FastArrivalProductStock : Window
{
    public FastArrivalProductStock(StockPage stockPage, Entities.Stock selectedStock)
    {
        InitializeComponent();
        this.stockPage = stockPage;
        this.selectedStock = selectedStock;
        categoryName.Text = "Категория: " + selectedStock.Category;
        childCategoryName.Text = "Под категория: " + selectedStock.ChildCategory;
        productName.Text = selectedStock.ProductName;
        productOriginalPrice.Text = "Изначальная цена - " + selectedStock.OriginalPrice;
        productSellingPrice.Text = "Цена продажи - " + selectedStock.SellingPrice;
        productCount.Text = selectedStock.Count;

    }

    StockPage stockPage;
    Entities.Stock selectedStock;
        
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var db = new AppDbContext();

        if (productCount.Text.Length == 0)
        {
            MessageBox.Show("Пустое поле");
            return;
        }

        var product = db.Products.First(p => p.Barcode == selectedStock.Barcode);
        product.Count += product.Count;
        db.Products.Update(product);
        db.SaveChanges();
        stockPage.Load();
        Close();

    }

    private void ProductPrice_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {

        string pattern = @"^[0-9]+$";

        // Create a regular expression object with the pattern
        Regex regex = new Regex(pattern);

        // Check if the entered text matches the pattern
        if (!regex.IsMatch(e.Text))
        {
            e.Handled = true; // Ignore the input
        }
    }

        
}