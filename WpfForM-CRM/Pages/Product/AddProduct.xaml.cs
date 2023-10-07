using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.Product;

/// <summary>
/// Interaction logic for AddProduct.xaml
/// </summary>
public partial class AddProduct : Window
{
    public AddProduct(ShopsPage shopsPage)
    {
        this.shopsPage = shopsPage;
        InitializeComponent();
        categoryName.Text = "Категория: " + shopsPage.CategoryName;
        childCategoryName.Text = "Под категория: " + shopsPage.ChildCategoryName;
        productName.Focus();
    }

    private ShopsPage shopsPage;
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var db = new AppDbContext();

        if (productName.Text.Length == 0 || productOriginalPrice.Text.Length == 0 || productSellingPrice.Text.Length == 0)
        {
            MessageBox.Show("Пустое поле");
            return;
        }
        if (productName.Text == 0.ToString() || productOriginalPrice.Text == 0.ToString() || productSellingPrice.Text == 0.ToString() || productCount.Text == 0.ToString())
        {
            MessageBox.Show("not variable");
            return;
        }

        var products = db.Products.Where(p => p.ChildCategoryId == shopsPage.ChildCategoryId).ToList();


        try
        {

            var product = new Entities.Product()
            {
                Name = Helper.Helper.ToUpperNamesOneChar(productName.Text),
                OriginalPrice = double.Parse(productOriginalPrice.Text.Replace(" ", "")),
                SellingPrice = double.Parse(productSellingPrice.Text.Replace(" ", "")),
                Barcode = Helper.Helper.GenerateBarcode(),
                Count = int.Parse(productCount.Text),
                ChildCategoryId = shopsPage.ChildCategoryId,
                UserId = shopsPage.UserId,
                CategoryId = shopsPage.CategoryId,
                ShopId = shopsPage.ShopId
            };

            db.Products.Add(product);

            var childCategory = db.ChildCategories.First(p => p.Id == shopsPage.ChildCategoryId);
            childCategory.Products!.Add(product);
            db.ChildCategories.Update(childCategory);

            db.SaveChanges();
            shopsPage.ReadProducts();
            Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"{exception.Message}");
            throw;
        }

        
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
        PriceSpace(productOriginalPrice);
    }

    private void ProductSellingPrice_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        PriceSpace(productSellingPrice);
    }

    public void PriceSpace(TextBox textBox)
    {
        // Kiritilgan matni olish
        string inputText = textBox.Text;

        // Probellarni olib tashlash va sonlarni ajratib olish
        string cleanedText = string.Join("", inputText.Split(' '));

        // Formatlangan natijani tuzish
        string formattedText = "";

        int length = cleanedText.Length;
        int groupSize = 3; // Gruplar o'lchami

        for (int i = 0; i < length; i++)
        {
            formattedText += cleanedText[i];
            if ((length - i - 1) % groupSize == 0 && i != length - 1)
            {
                formattedText += " ";
            }
        }

        // Formatlangan natijani matnga qaytaramiz
        textBox.Text = formattedText;

        // Kursorning pozitsiyasini oxiriga qo'yamiz
        textBox.SelectionStart = textBox.Text.Length;
    }
}