using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using WpfForM_CRM.Context;

namespace WpfForM_CRM.Pages.Stock
{
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
            categoryName.Text = selectedStock.Category;
            childCategoryName.Text = selectedStock.ChildCategory;
            productName.Text = selectedStock.ProductName;
            productOriginalPrice.Text = selectedStock.OriginalPrice;
            productSellingPrice.Text = selectedStock.SellingPrice;
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
}
