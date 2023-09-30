using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages.Stock;

/// <summary>
/// Interaction logic for UpdateProductStock.xaml
/// </summary>
public partial class UpdateProductStock : Window
{
    public UpdateProductStock(StockPage stockPage, Entities.Stock selectedStock)
    {
            
        InitializeComponent();
        categoryName.Text = selectedStock.Category;
        childCategoryName.Text = selectedStock.ChildCategory;
        this.stockPage = stockPage;
        this.selectedStock = selectedStock;
        productName.Text = selectedStock.ProductName;
        productOriginalPrice.Text = selectedStock.OriginalPrice;
        productSellingPrice.Text = selectedStock.OriginalPrice;
        productCount.Text = selectedStock.Count;

    }

    private string SubstrUzs(string productPrice)
    {
        if (productPrice.ToLower().EndsWith("uzs"))
        {
            productPrice = productPrice.Substring(0, productPrice.Length - 3).Trim(); // "uzs" so'zini olib tashayapmiz va bo'shlikni olib tashayapmiz
        }

        return productPrice;
    }
    StockPage  stockPage;
    private Entities.Stock selectedStock;

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (productName.Text.Length == 0 || productOriginalPrice.Text.Length == 0 || productSellingPrice.Text.Length == 0)
        {
            MessageBox.Show("Пустое поле");
            return;
        }

        int productPrice; 
        var price = Console.ReadLine(); // 12 uzs 

        productPrice = Convert.ToInt32(price);


        var db = new AppDbContext();
        var product = db.Products.First(p => p.Barcode == selectedStock.Barcode);
        product.Name = productName.Text;
        product.OriginalPrice = (long.Parse(productOriginalPrice.Text));
        product.SellingPrice = (long.Parse(productSellingPrice.Text));
        product.Count = int.Parse(productCount.Text);

        db.Products.Update(product);
        db.SaveChanges();
        stockPage.Load();
        Close();
    }
    private void ProductName_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {

        string pattern = @"^[а-яА-Яa-zA-Z0-9]+$";

        // Create a regular expression object with the pattern
        Regex regex = new Regex(pattern);

        // Check if the entered text matches the pattern
        if (!regex.IsMatch(e.Text))
        {
            e.Handled = true; // Ignore the input
        }

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