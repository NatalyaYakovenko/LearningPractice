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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            List<Entities.Direction> directions = App.Context.Direction.ToList();
            directions.Insert(0, new Entities.Direction() { Name = "Все" });
            CBoxDirection.ItemsSource = directions;
            CBoxDirection.SelectedIndex = 0;
        }

        private void DPickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void DPickerEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void CBoxDirection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
        private void UpdateList()
        {
            var meets = App.Context.Meet.ToList();

            if (DPickerStart.SelectedDate.HasValue)
                meets = meets.Where(o => DPickerStart.SelectedDate.Value <= o.Date).ToList();

            if (DPickerEnd.SelectedDate.HasValue)
                meets = meets.Where(o => DPickerEnd.SelectedDate.Value <= o.Date).ToList();

            if (CBoxDirection.SelectedIndex != 0)
            {
                meets = meets.Where(o => o.Direction == CBoxDirection.SelectedItem).ToList();
            }

            LViewMeets.ItemsSource = meets;
        }
    }
}
