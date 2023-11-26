using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using NatalyaYakovenko.Entities;

namespace NatalyaYakovenko.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private byte[] _data;
        public RegistrationPage()
        {
            InitializeComponent();
            ImgPhoto.DataContext = null;
            TBoxId.Text = (App.Context.User.ToList().Max(x => x.ID) + 20).ToString();
            CBoxGender.ItemsSource = App.Context.Gender.ToList();
            CBoxRole.ItemsSource = App.Context.Role.ToList().Where(x => x.ID == 1 || x.ID == 3).ToList();
            CBoxDirection.ItemsSource = App.Context.Direction.ToList();
            CBoxEvent.ItemsSource = App.Context.Event.ToList();
            CBoxCountry.ItemsSource = App.Context.Country.ToList();
        }

        private void ImgPhoto_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файлы изображений (*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg";
            if (dialog.ShowDialog() == true)
            {
                ImgPhoto.DataContext = _data = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void ChBoxShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if(ChBoxShowPassword.IsChecked.Value)
            {
                TBoxRePassword.Text = PBoxRePassword.Password;
                PBoxRePassword.Visibility = Visibility.Collapsed;
                TBoxRePassword.Visibility = Visibility.Visible;
            }
            else
            {
                PBoxRePassword.Password = TBoxRePassword.Text;
                PBoxRePassword.Visibility = Visibility.Visible;
                TBoxRePassword.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var errors = "";
                if (TBoxFullName.Text == "") errors += "Заполните ФИО\n";
                if (CBoxGender.SelectedItem == null) errors += "Выберите пол\n";
                if (CBoxRole.SelectedItem == null) errors += "Выберите роль\n";
                if (TBoxEmail.Text == "") errors += "Заполните Email\n";
                if (TBoxEmail.Text != "" && (!TBoxEmail.Text.Contains("@") || !TBoxEmail.Text.Contains("."))) errors += "Неверный формат Email'a\n";
                if (TBoxPhoneNumber.Text.ToLower().Contains(" ")) errors += "Введите номер телефона\n";
                if (CBoxDirection.Text == "") errors += "Введите направление\n";
                if (ChBoxAttachToEvent.IsChecked == true && CBoxEvent.SelectedItem == null) errors += "Выберите мероприятие\n";
                if (PBoxPassword.Password == "") errors += "Введите пароль\n";
                if (PBoxPassword.Password != "" && !CheckPassword(PBoxPassword.Password)) errors += "Неверный формат пароля\n";
                if (PBoxPassword.Password != "" && !(PBoxRePassword.Password == PBoxPassword.Password || TBoxRePassword.Text == PBoxPassword.Password)) errors += "Пароли не совпадают\n";

                if (errors == "")
                {
                    Direction direction;
                    if (CBoxDirection.SelectedItem == null)
                    {
                        direction = new Direction();
                        direction.Name = CBoxDirection.Text;
                        App.Context.Direction.Add(direction);
                        App.Context.SaveChanges();
                    }
                    else
                    {
                        direction = CBoxDirection.SelectedItem as Direction;
                    }

                    User user = new User();
                    var fullName = TBoxFullName.Text.Split(' ');
                    user.Surname = fullName[0];
                    user.Name = fullName[1];
                    user.Patronymic = fullName[2];
                    user.E_mail = TBoxEmail.Text;
                    user.Number_Phone = TBoxPhoneNumber.Text;
                    user.Direction = direction;
                    if (ChBoxAttachToEvent.IsChecked.Value)
                        user.Event = CBoxEvent.SelectedItem as Event;
                    user.Photo = _data;
                    user.Password = PBoxPassword.Password;
                    user.Role = CBoxRole.SelectedItem as Role;
                    user.Gender = CBoxGender.SelectedItem as Gender;
                    user.Country = CBoxCountry.SelectedItem as Country; 
                    App.Context.User.Add(user);
                    App.Context.SaveChanges();

                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Проверьте правильность заполнения полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CheckPassword(string password)
        {

            if (password.Length < 6)
                return false;
            if (password.FirstOrDefault(x => char.IsLower(x)) == 0)
                return false;
            if (password.FirstOrDefault(x => char.IsUpper(x)) == 0)
                return false;
            if (password.FirstOrDefault(x => char.IsDigit(x)) == 0)
                return false;
            var symbols = "!@#$%^&*()_-+=";
            foreach (var symbol in symbols)
            {
                if (password.FirstOrDefault(x => x == symbol) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ChBoxAttachToEvent_Click(object sender, RoutedEventArgs e)
        {
            CBoxEvent.IsEnabled = ChBoxAttachToEvent.IsChecked.Value;
        }
    }
}
