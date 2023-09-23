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

namespace WpfForM_CRM.Pages
{
    /// <summary>
    /// Interaction logic for UpdateCategory.xaml
    /// </summary>
    public partial class UpdateCategory : Window
    {
        public UpdateCategory(ShopsPage shopsPage, Guid categoryId, string currentCategoryName)
        {
            this.shopsPage = shopsPage;
            this.categoryId = categoryId;
            //categoryName.Text = currentCategoryName;
            InitializeComponent();
            this.appDbContext = new AppDbContext();
        }

        ShopsPage shopsPage;
        private Guid categoryId;
        private string Name;
        AppDbContext appDbContext;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var category = appDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (categoryName.Text.Length == 0)
            {

                MessageBox.Show("Значение не указано", "",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (appDbContext.Categories.Any(c => c.Name == categoryName.Text))
            {
                MessageBox.Show("Это имя уже существует");
                return;
            }


            category.Name = categoryName.Text;
            appDbContext.Categories.Update(category);
            appDbContext.SaveChanges();
            shopsPage.ReadCategories();
            Close();
        }
    }
}
