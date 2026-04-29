using CulinaryBokk.AppData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CulinaryBokk.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddRecipes.xaml
    /// </summary>
    public partial class AddRecipes : Page
    {
        public Recipes recipes = new Recipes();
        private int _currentIndex;


        public AddRecipes(Recipes recipe)
        {
            InitializeComponent();
            FillAuthors();
            FillCategory();
            if (recipe != null)
            {
                recipes = recipe;
            }
            DataContext = recipes;

        }
        public void FillCategory()
        {
            CategoryCombo.Items.Add("Выбор");
            CategoryCombo.SelectedIndex = 0;
            var category = AppConnect.model0db.Categories;
            foreach (var c in category)
            {
                CategoryCombo.Items.Add(c.CategoryName);
            }
        }
        public void FillAuthors()
        {
            AuyhorCombo.Items.Add("Выбор");
            AuyhorCombo.SelectedIndex = 0;
            var authore = AppConnect.model0db.Authors;
            foreach (var a in authore)
            {
                AuyhorCombo.Items.Add(a.AuthorName);
            }
        }

        private void AddRecep_Click(object sender, RoutedEventArgs e)
        {
            
                try
                {


                    if (String.IsNullOrEmpty(NameRecepis.Text) || String.IsNullOrEmpty(NameRecepis.Text) ||
                        String.IsNullOrEmpty(DescRecipes.Text) ||
                        String.IsNullOrWhiteSpace(DescRecipes.Text) ||
                        String.IsNullOrWhiteSpace(TextTime.Text) ||
                        String.IsNullOrWhiteSpace(TextPage.Text))
                    {
                        MessageBox.Show("Не заполнены все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    Recipes userObj = new Recipes()
                    {
                    RecipeName = NameRecepis.Text,
                    Description = DescRecipes.Text,
                    CategoryID = CategoryCombo.SelectedIndex,
                    AuthorID = AuyhorCombo.SelectedIndex,
                    CookingTime = int.Parse(TextTime.Text),
                    image = TextPage.Text,
                    };
                    AppConnect.model0db.Recipes.Add(userObj);
                    AppConnect.model0db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppData.AppFrame.framemain.GoBack();
                }
                catch
                {
                    MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        }

        private void HomeWorld_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framemain.Navigate(new PageTask());
        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";
            dialog.Title = "Выберите изображение";
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string photoName = System.IO.Path.GetFileName(dialog.FileName);
                    string imagesDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Images\\");
                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }
                    string destinationPath = System.IO.Path.Combine(imagesDirectory, photoName);
                    File.Copy(dialog.FileName, destinationPath, true);
                    recipes.image = photoName;
                    TextPage.Text = photoName;
                    LoadImageToPictureBox(destinationPath);
                    MessageBox.Show("Изображение загружено: " + photoName, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Оштбка при загрузки изображения: {ex.Message}","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Изображение не выбрано.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void LoadImageToPictureBox(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                try 
                {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();

                pictureBox.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении изображения: {ex.Message}");
                }
            }
        }

        private void FazaButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framemain.Navigate(new PageFaza());
        }
    }
}
