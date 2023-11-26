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
using NatalyaYakovenko.Entities;
using NatalyaYakovenko;

namespace NatalyaYakovenko.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private Random _random = new Random();
        private byte[][] _backgrounds = new[] {
            Properties.Resources.capcha1,
            Properties.Resources.capcha2,
            Properties.Resources.capcha3,
            Properties.Resources.capcha4,
        };
        private string _capchaText;
        private int _attemptCount = 0;
        public AuthPage()
        {
            InitializeComponent();
        }

        private void UpdateCapcha()
        {
            _capchaText = "";
            var charset = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            for (int i = 0; i < 4; i++)
                _capchaText += charset[_random.Next(charset.Length)];

            TBlockCapcha.Text = _capchaText;
            ImgCapchaBack.DataContext = _backgrounds[_random.Next(_backgrounds.Length)];
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateCapcha();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (App.Context.User.ToList().FirstOrDefault(u => u.ID.ToString() == TBoxLogin.Text &&
            u.Password == PBoxPassword.Password)
           is User user)
            {
                if (TBoxCapcha.Text == _capchaText) // Успешная авторизация
                {
                    Properties.Settings.Default.UserID = user.ID;
                    if (ChBoxRememberMe.IsChecked == true) // Стоит галочка "Запомнить меня"
                        Properties.Settings.Default.Save();

                    switch (App.AuthUser.IDRole)
                    {
                        case 1:
                            NavigationService.Navigate(new JuryPage());
                            break;
                        case 2:
                            NavigationService.Navigate(new ParticipantPage());
                            break;
                        case 3:
                            NavigationService.Navigate(new ModeratorPage());
                            break;
                        case 4:
                            NavigationService.Navigate(new OrganizerPage());
                            break;
                        default:
                            break;
                    }
                }
                else // Неверная капча
                {
                    MessageBox.Show("Неверная капча",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    _attemptCount++;
                }
            }
            else // Неверные учетные данные
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _attemptCount++;
            }

            if (_attemptCount == 3)
            {
                _attemptCount = 0;
                GridBlock.Visibility = Visibility.Visible;
                await Task.Delay(10000);
                GridBlock.Visibility = Visibility.Collapsed;
            }
        }
    }
}
