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

namespace NatalyaYakovenko.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrganizerPage.xaml
    /// </summary>
    public partial class OrganizerPage : Page
    {
        public OrganizerPage()
        {
            InitializeComponent();
            string helloText = "";
            var hour = DateTime.Now.Hour;
            if (hour > 9)
                helloText = "Доброе утро!";
            if (hour > 11)
                helloText = "Добрый день!";
            if (hour > 19)
                helloText = "Добрый вечер!";

            TBlockTimeHello.Text = helloText;
            var nameText = App.AuthUser.IDGender == 1 ? "Mr. " : "Mrs. ";
            nameText += App.AuthUser.Surname + " " + App.AuthUser.Name + " " + App.AuthUser.Patronymic;
            TBlockFullName.Text = nameText;
            ImgPhoto.DataContext = App.AuthUser.Photo;
        }

        private void BtnEvents_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функционал находится в разработке", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnPartipants_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функционал находится в разработке", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnJuries_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void BtnMyProfile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функционал находится в разработке", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
