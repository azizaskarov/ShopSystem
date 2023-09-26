using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.ChildCategory;

/// <summary>
/// Interaction logic for UpdateChildcategory.xaml
/// </summary>
public partial class UpdateChildCategory : Window
{
    public UpdateChildCategory(ShopsPage shopsPage, Guid childCategoryId, string childCurrentName, Guid? categoryId)
    {
        this.shopsPage = shopsPage;
        this.childCurrentName = childCurrentName;
        this.categoryId = categoryId;
        this.appDbContext = new AppDbContext();
        InitializeComponent();
        childCategoryName.Text = childCurrentName;
        this.childCategoryId = childCategoryId;
    }

    ShopsPage shopsPage;
    private Guid childCategoryId;
    private string childCurrentName;
    Guid? categoryId;
    AppDbContext appDbContext;
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var childCategory = appDbContext.ChildCategories.FirstOrDefault(c => c.Id == childCategoryId);
        if (childCategoryName.Text.Length == 0)
        {

            MessageBox.Show("Значение не указано", "",
                MessageBoxButton.OK, MessageBoxImage.Warning);

            return;
        }

        if (appDbContext.ChildCategories.Where(ch => ch.CategoryId == categoryId).ToList().Any(c => c.Name == childCategoryName.Text))
        {
            MessageBox.Show("Это имя уже существует");
            return;
        }

        var categoryName1 = childCategoryName.Text;
        categoryName1 = char.ToUpper(categoryName1[0]) + categoryName1.Substring(1);

        childCategory.Name = categoryName1;
        appDbContext.ChildCategories.Update(childCategory);
        appDbContext.SaveChanges();
        shopsPage.ReadChildCategories();
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

    private void UpdateChildcategory_OnKeyDown(object sender, KeyEventArgs e)
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