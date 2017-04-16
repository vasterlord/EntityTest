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
using EntityTest.Model;
using System.Data.SqlClient;

namespace EntityTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Car_Model_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            using (var context = new Context())
            {
                var countryOne = new CountryProducing()
                {
                    Country = "Japan"
                };
                var countryTwo = new CountryProducing()
                {
                    Country = "Germany"
                };
                var countryThird = new CountryProducing()
                {
                    Country = "France"
                }; 
                context.CountryProducings.Add(countryOne);
                context.CountryProducings.Add(countryTwo);
                context.CountryProducings.Add(countryThird);
                context.SaveChanges();
                var countryProducing = context.CountryProducings.OrderBy(val => val.Id).ToList();
                dataGrid.ItemsSource = countryProducing;
            }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                var countryThird = new CountryProducing()
                {
                    Country = "Chinese"
                };
                context.CountryProducings.Add(countryThird);
                context.SaveChanges(); 
                var countryProducing = context.CountryProducings.OrderBy(val => val.Id).ToList();
                dataGrid.ItemsSource = countryProducing;
            }
        }
     }
    }


