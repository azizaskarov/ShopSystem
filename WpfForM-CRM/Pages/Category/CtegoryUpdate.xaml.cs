using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.Category
{
    /// <summary>
    /// Interaction logic for CtegoryUpdate.xaml
    /// </summary>
    public partial class CtegoryUpdate : Window
    {
        public CtegoryUpdate(ShopsPage shopsPage, Guid categoryId, string currentCategoryName)
        {
            InitializeComponent();
            this.shopsPage = shopsPage;
            this.categoryId = categoryId;
            categoryName.Text = currentCategoryName;
            this.appDbContext = new AppDbContext();
        }

        ShopsPage shopsPage;
        private Guid categoryId;
        private string Name;
        AppDbContext appDbContext;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var category = appDbContext.Categories.First(c => c.Id == categoryId);
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

            var categoryName1 = categoryName.Text;
            categoryName1 = char.ToUpper(categoryName1[0]) + categoryName1.Substring(1);

            category.Name = categoryName1;
            appDbContext.Categories.Update(category);
            appDbContext.SaveChanges();
            shopsPage.ReadCategories();
            Close();
        }

        private void addtxt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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

        private void UpdateCategory_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }

            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
