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
    /// Логика взаимодействия для PageReg.xaml
    /// </summary>
    public partial class PageReg : Page
    {
        public PageReg()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.framemain.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AppConnect.model0db.Authors.Count(x => x.Login == TextLogin.Text) > 0)
                {
                    MessageBox.Show("Пользователь с таким логином есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (String.IsNullOrEmpty(TextLogin.Text) || String.IsNullOrEmpty(TextName.Text) ||
                    String.IsNullOrEmpty(TextPassword.Password) || 
                    String.IsNullOrWhiteSpace(TextPassword.Password) || 
                    String.IsNullOrWhiteSpace(TextName.Text) || 
                    String.IsNullOrWhiteSpace(TextLogin.Text))
                {
                    MessageBox.Show("Не заполнены все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                Authors userObj = new Authors()
                {
                    Login = TextLogin.Text,
                    AuthorName = TextName.Text,
                    Password = TextPassword.Password,
                    ByDay = Bid.SelectedDate.Value,
                    Stoge = int.Parse(TextStag.Text),
                    Telefon = TextFon.Text,
                    Email = TextEmali.Text
                };
                AppConnect.model0db.Authors.Add(userObj);
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppData.AppFrame.framemain.GoBack();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TextPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (TextPassword.Password != TextPasswordd.Password)
            {
                btnReg.IsEnabled = false;
                TextPasswordd.Background = Brushes.LightCoral;
                TextPasswordd.BorderBrush = Brushes.Red;
            }
            else
            {
                btnReg.IsEnabled = true;
                TextPasswordd.Background = Brushes.LightGreen;
                TextPasswordd.BorderBrush = Brushes.Green;
            }
        }
    }
}
