using CulinaryBokk.AppData;
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

namespace CulinaryBokk.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageFaza.xaml
    /// </summary>
    public partial class PageFaza : Page
    {
        public PageFaza(Recipes recipe)
        {
            InitializeComponent();
            this.DataContext = recipe;
            if (recipe.CookingSteps != null)
            {
                ListFaza.ItemsSource = recipe.CookingSteps.OrderBy(x => x.StepNumber).ToList();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framemain.GoBack();
        }
    }
}
