using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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
        categoryName.Text = "Категория: " + selectedStock.Категория;
        childCategoryName.Text = "Под категория: " + selectedStock.Подкатегория;
        this.stockPage = stockPage;
        this.selectedStock = selectedStock;
        productName.Text = selectedStock.Продукт; // productPrice.Text.Replace("UZS", "").Trim()
        productOriginalPrice.Text = selectedStock.Текущая!.Replace("UZS", "");
        productSellingPrice.Text = selectedStock.Прибывшая!.Replace("UZS", ""); ;
        productCount.Text = selectedStock.Количство;

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
        if (productName.Text.Length == 0 || productOriginalPrice.Text.Length == 0 || productSellingPrice.Text.Length == 0 || productCount.Text.Length == 0)
        {
            MessageBox.Show("Пустое поле");
            return;
        }
        if (productName.Text == 0.ToString() || productOriginalPrice.Text == 0.ToString() || productSellingPrice.Text == 0.ToString() || productCount.Text == 0.ToString())
        {
            MessageBox.Show("not variable");
            return;
        }

        var db = new AppDbContext();
        var product = db.Products.First(p => p.Barcode == selectedStock.Штрихкод);
        product.Name = productName.Text;
        product.OriginalPrice = (double.Parse(productOriginalPrice.Text.Replace(",","")));
        product.SellingPrice = (double.Parse(productSellingPrice.Text.Replace(",", "")));
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


    private void ProductOriginalPrice_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var formatted = (String.Format("{0:N}", double.Parse(productOriginalPrice.Text)));
        productOriginalPrice.Text = formatted.Remove(formatted.Length - 3);
        productOriginalPrice.Select(productOriginalPrice.Text.Length, 0);
    }

    private void ProductSellingPrice_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var formatted = (String.Format("{0:N}", double.Parse(productSellingPrice.Text)));
        productSellingPrice.Text = formatted.Remove(formatted.Length - 3);
        productSellingPrice.Select(productSellingPrice.Text.Length, 0);
    }
}