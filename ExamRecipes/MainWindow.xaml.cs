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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

public class Recipe
{
    public string Name { get; set; }
    public List<string> Ingredients { get; set; } = new List<string>();
    public string Category { get; set; }
    public string Instructions { get; set; }
}
namespace Моя_книга_рецептов
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var recipe = new Recipe
            {
                Name = Recipe.Text,
                Ingredients = Ingredient.Text.Split(',').Select(i => i.Trim()).ToList(),
                Category = (Categories.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Instructions = Instruction.Text
            };
            recipes.Add(recipe);
            UpdateRecipe();
            ClearInputFields();
        }
        private void UpdateRecipe()
        {
            var filterRecipe = recipes.AsEnumerable();

            if (!string.IsNullOrEmpty(Ingredient.Text))
            {
                var search = Search.Text.ToLower();
                filterRecipe = filterRecipe.Where(j => j.Ingredients.Any(i => i.ToLower().Contains(search)));
            }

            if (CategoriesF.SelectedItem is ComboBoxItem selectCategory)
            {
                var category = selectCategory.Content.ToString();
                filterRecipe = filterRecipe.Where(r => r.Category == category);
            }

            RecipeLB.ItemsSource = filterRecipe.ToList();
        }

        private void RecipesLB(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeLB.SelectedItem is Recipe selectedRecipe)
            {
                SName.Text = selectedRecipe.Name;
                SIngredient.Text = "Ингредиенты: " + string.Join(", ", selectedRecipe.Ingredients);
                SCategories.Text = "Вид блюда: " + selectedRecipe.Category;
                SInstructions.Text = "Рецепт: " + selectedRecipe.Instructions;
            }
        }
        private void CategoryF(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecipe();
        }

        private void ClearInputFields()
        {
            Recipe.Clear();
            Ingredient.Clear();
            Instruction.Clear();
            Categories.SelectedIndex = -1;
        }
    }

}