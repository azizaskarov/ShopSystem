using System.Linq;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfForM_CRM.Context;
using WpfForM_CRM.Pages.Shop;

namespace WpfForM_CRM.Pages.ChildCategory;

/// <summary>
/// Interaction logic for AddChildCategory.xaml
/// </summary>
public partial class AddChildCategory : Window
{
    private ShopsPage shopsPage;
    public AddChildCategory(ShopsPage shopsPage)
    {
        this.shopsPage = shopsPage;
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var db = new AppDbContext();

        if (childCategoryName.Text.Length == 0)
        {

            MessageBox.Show("Значение не указано", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;

        }

        var childCategories = db.ChildCategories
            .ToList();

        if (childCategories.Any(childCategory => childCategory.Name == childCategoryName.Text))
        {
            MessageBox.Show("Название магазина уже существует");
            return;
        }

        var categoryChildName = childCategoryName.Text;
        categoryChildName = char.ToUpper(categoryChildName[0]) + categoryChildName.Substring(1);

        var childCategory = new Entities.ChildCategory()
        {
            Name = categoryChildName,
            CategoryId = shopsPage.CategoryId,
            ShopId = shopsPage.ShopId,
        };

        db.ChildCategories.Add(childCategory);

        var category = db.Categories.First(c => c.Id == shopsPage.CategoryId);
        category.ChildCategories.Add(childCategory);
        db.Categories.Update(category);
        db.SaveChanges();
        shopsPage.ReadChildCategories();
        Close();
    }

    private void CategoryName_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var pattern = @"^[а-яА-Яa-zA-Z0-9]+$";
        var regex = new Regex(pattern);

        if (!regex.IsMatch(e.Text))
        {
            e.Handled = true;
        }
    }
}