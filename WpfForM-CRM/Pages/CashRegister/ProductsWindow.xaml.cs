using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.CashRegister
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        public ProductsWindow(MainWindow mainWindow, ShopsPage shopsPage, KassaPage kassaPage, string header)
        {
            InitializeComponent();
            this.shopsPage = shopsPage;
            this.kassaPage = kassaPage;
            Header = header;
            this.mainWindow = mainWindow;
            
            Load();
        }

        ShopsPage shopsPage;
        KassaPage kassaPage;
        private MainWindow mainWindow;
        string Header;

        public void Load()
        { 
            var db = new AppDbContext();
            var items = new List<Entities.Stock>();
            var products = db.Products.Where(p => p.TabName == null).ToList();
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
                            Номер = i,
                            Магазин = shopsPage.ShopName,
                            Продукт = product.Name,
                            Штрихкод = product.Barcode,
                            Подкатегория = childCategory.Name,
                            Категория = category!.Name,
                            Текущая = product.OriginalPrice.ToString(),
                            Прибывшая = product.SellingPrice.ToString(),
                            Количство = (product.Count ?? 1).ToString(),
                        };

                        items.Add(item);
                        i++;
                    }
                }
                storage_data.ItemsSource = items;
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var index = storage_data.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }
            var db = new AppDbContext();

            var items = storage_data.ItemsSource as List<Entities.Stock>;
            var selectedData = items![index];
            var product2 = db.Products.First(p => p.Barcode == selectedData.Штрихкод);
            product2.TabName = Header;
            db.SaveChanges();
            if (Header == kassaPage.tab1.Header.ToString()) kassaPage.TabFoodLoad();
            //if (Header == kassaPage.tab_kiyimlar.Header.ToString()) kassaPage.LoadTabKiyim();
            //if (Header == kassaPage.tab_texnika.Header.ToString()) kassaPage.LoadTabTexnika();
            //if (Header == kassaPage.tab_animal.Header.ToString()) kassaPage.LoadTabAnimal();
            //if (Header == kassaPage.tab_ichimlik.Header.ToString()) kassaPage.LoadTabIchimlik();
            //if (Header == kassaPage.tab_maishiy.Header.ToString()) kassaPage.LoadTabMaishiy();

            Close();
        }
    }
}
