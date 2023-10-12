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
using WpfForM_CRM.Entities.Cashed;
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
            var products = db.Products.Where(p =>  p.ShopId.Equals(shopsPage.ShopId)).OrderByDescending(p => p.CreatedDate).ToList();
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

            var items = (List<Entities.Stock>)storage_data.ItemsSource;
            var selectedData = items![index];
            var product2 = db.Products.First(p => p.Barcode == selectedData.Штрихкод);

            //var cashedFood = new CashedFood()
            //{
            //    Name = product2.Name,
            //    Price = product2.SellingPrice,
            //    TabName = Header
            //};

            //db.CashedFoods.Add(cashedFood);
            //db.SaveChanges();
            //kassaPage
            product2.TabName = Header;
            db.Products.Update(product2);
            db.SaveChanges();
            if (Header == kassaPage.tab1.Header.ToString())
                kassaPage.TabFoodLoad();
            if (Header == kassaPage.tab2.Header.ToString())
                kassaPage.TabClothesLoad();
            if (Header == kassaPage.tab3.Header.ToString()) 
                kassaPage.TabTechLoad();
           

            Close();
        }
    }
}
