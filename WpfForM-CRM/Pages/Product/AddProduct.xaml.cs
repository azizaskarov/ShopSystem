﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
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

        var products = db.Products.Where(p => p.ChildCategoryId == shopsPage.ChildCategoryId).ToList();

        var name = productName.Text;
        name = char.ToUpper(name[0]) + name.Substring(1);

        var product = new Entities.Product()
        {
            Name = name,
            OriginalPrice = long.Parse(productOriginalPrice.Text),
            SellingPrice = long.Parse(productSellingPrice.Text),
            Barcode = GenerateBarcode(),
            Count = int.Parse(productCount.Text),
            ChildCategoryId = shopsPage.ChildCategoryId,
            UserId = shopsPage.userId,
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

    private string GenerateBarcode()
    {
        Random random = new Random();
        const int barcodeLength = 11; // 11 ta raqamdan keyin 637 qo'shiladi

        // Random raqamlardan tuzilgan barcode generatsiya qilish
        string barcode = "637" + new string(Enumerable.Range(0, barcodeLength - 3)
            .Select(_ => random.Next(10).ToString()[0])
            .ToArray());

        return barcode;
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