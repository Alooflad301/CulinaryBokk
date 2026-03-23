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
    /// Логика взаимодействия для PageAftoriz.xaml
    /// </summary>
    public partial class PageAftoriz : Page
    {
        public PageAftoriz()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = AppData.AppConnect.model0db.Authors.FirstOrDefault(x => x.Login == TextLogin.Text && x.Password == TextPassword.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя нет", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Здравствуйте, Автор " + userObj.AuthorName + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.framemain.Navigate(new PageTask());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString(), "Критическая ошибка приложения", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AppFrame.framemain.Navigate(new PageReg());
        }
    }
}
