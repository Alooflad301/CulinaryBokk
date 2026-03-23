using CulinaryBokk.AppData;
using CulinaryBokk.Pages;
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

namespace CulinaryBokk
{
    /// <summary>
    /// Логика взаимодействия для PageTask.xaml
    /// </summary>
    public partial class PageTask : Page
    {
        public PageTask()
        {
            InitializeComponent();
            listProduct.ItemsSource = AppConnect.model0db.Recipes.ToList();
            Fill();
        }

        public void Fill()
        {
            ComdoSort.Items.Add("Время");
            ComdoSort.Items.Add("По возрастанию времени приготовления");
            ComdoSort.Items.Add("По убыванию времяни выполнения");
            ComdoSort.SelectedIndex = 0;
            ComboFilter.SelectedIndex = 0;
            var category = AppConnect.model0db.Categories;
            ComboFilter.Items.Add("Категория");
            foreach (var c in category)
            {
                ComboFilter.Items.Add(c.CategoryName);
            }
        }
        Recipes[] RecipesList()
        {
            try
            {
                List<Recipes> recipes = AppConnect.model0db.Recipes.ToList();
                if (TextSearch != null)
                {
                    recipes = recipes.Where(x => x.RecipeName.ToLower().Contains(TextSearch.Text.ToLower())).ToList();
                }
                if (ComboFilter.SelectedIndex > 0)
                {
                    switch (ComboFilter.SelectedIndex)
                    {
                        case 1:
                            recipes = recipes.Where(x => x.CategoryID == 1).ToList();
                            break;
                        case 2:
                            recipes = recipes.Where(x => x.CategoryID == 2).ToList();
                            break;
                        case 3:
                            recipes = recipes.Where(x => x.CategoryID == 3).ToList();
                            break;
                        case 4:
                            recipes = recipes.Where(x => x.CategoryID == 4).ToList();
                            break;
                        case 5:
                            recipes = recipes.Where(x => x.CategoryID == 5).ToList();
                            break;
                        case 6:
                            recipes = recipes.Where(x => x.CategoryID == 6).ToList();
                            break;
                        case 7:
                            recipes = recipes.Where(x => x.CategoryID == 7).ToList();
                            break;
                        case 8:
                            recipes = recipes.Where(x => x.CategoryID == 8).ToList();
                            break;
                        case 9:
                            recipes = recipes.Where(x => x.CategoryID == 9).ToList();
                            break;
                        case 10:
                            recipes = recipes.Where(x => x.CategoryID == 10).ToList();
                            break;
                    }
                }
                if (ComdoSort.SelectedIndex > 0)
                {
                    switch (ComdoSort.SelectedIndex)
                    {
                        case 1:
                            recipes = recipes.OrderBy(x => x.CookingTime).ToList();
                            break;
                         case 2:
                            recipes = recipes.OrderByDescending(x => x.CookingTime).ToList();
                            break;
                    }
                }
                if (recipes.Count > 0)
                {
                    tbCounter.Text = "Найдено " + recipes.Count + " рец.";

                }
                else
                {
                    tbCounter.Text = "Не найдено";
                }
                return recipes.ToArray();
            }
            catch
            {
                MessageBox.Show("Повторите попытку позже");
                return null;
            }
        }

        private void ComdoSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProduct.ItemsSource = RecipesList();
        }

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listProduct.ItemsSource = RecipesList();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProduct.ItemsSource = RecipesList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framemain.Navigate(new AddRecipes(null));
        }

        private void listProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listProduct.SelectedItem is Recipes selectedRecipe)
            {
                NavigationService.Navigate(new AddRecipes(selectedRecipe));
                listProduct.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Выделите рецепт!");
            }
        }
    }
}
