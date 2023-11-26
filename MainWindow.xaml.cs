using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NatalyaYakovenko.Pages;

namespace NatalyaYakovenko
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.UserID == -1)
            {
                FrameMain.Navigate(new Pages.MainPage());
            }
            else
            {
                switch (App.AuthUser.IDRole)
                {
                    case 1:
                        FrameMain.Navigate(new JuryPage());
                        break;
                    case 2:
                        FrameMain.Navigate(new ParticipantPage());
                        break;
                    case 3:
                        FrameMain.Navigate(new ModeratorPage());
                        break;
                    case 4:
                        FrameMain.Navigate(new OrganizerPage());
                        break;
                    default:
                        break;
                }
            }
        }

        private void FrameMain_ContentRendered(object sender, EventArgs e)
        {
            if (FrameMain.Content is MainPage)
            {
                BtnReg.Visibility = Visibility.Visible;
                BtnAuth.Visibility = Visibility.Visible;
                BtnBack.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnReg.Visibility = Visibility.Collapsed;
                BtnAuth.Visibility = Visibility.Collapsed;
                BtnBack.Visibility = Visibility.Visible;
            }

            if (FrameMain.Content is IRolePage)
            {
                BtnExit.Visibility = Visibility.Visible;
            }
            else
            {
                BtnExit.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameMain.GoBack();
            }
            catch
            {
                FrameMain.Navigate(new Pages.MainPage());            
            }
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new AuthPage());
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new RegistrationPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new MainPage());
            Properties.Settings.Default.UserID = -1;
            Properties.Settings.Default.Save();
        }
    }
}
