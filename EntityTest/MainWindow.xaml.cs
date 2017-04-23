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
using EntityTest.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Windows.Controls.DataVisualization.Charting;

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
            using (var context = new Context())
            { 
                var countryProducing = context.CountryProducings.Include(car=>car.CarBrands).OrderBy(val => val.Id).ToList();
                dataGrid.ItemsSource = countryProducing;
            }
        }

        private void Car_Model_Click(object sender, RoutedEventArgs e)
        {
            // try
            // {  
            using (var context = new Context())
            {
                List<CountryProducing> temp = new List<CountryProducing>(); 
                var countryOne = new CountryProducing();
                countryOne.CountryName = "Japan";
                countryOne.Price = 22.4;

                var countryTwo = new CountryProducing()
                {
                    CountryName = "Germany",
                    Price = 10.1
                };
                var countryThird = new CountryProducing()
                {
                    CountryName = "France", 
                    Price = 31.3
                };
                temp.Add(countryOne);
                temp.Add(countryTwo);
                temp.Add(countryThird);
                foreach (var item in temp)
                {
                    context.CountryProducings.Add(item);
                }
                foreach (var item in context.CountryProducings)
                {
                    item.CarBrands = context.CarBrands.Where(s => s.CountryProducingId == item.Id).ToList();
                }
                context.SaveChanges();
                //int id = Convert.ToInt32(textBox.Text);
                //var updatedcountry = context.CountryProducings.FirstOrDefault(country => country.Id == id);
                //context.CountryProducings.Attach(updatedcountry);
                //context.Entry(updatedcountry).State = EntityState.Modified;
                //updatedcountry.CountryName = textBox1.Text;
                //context.SaveChanges();
                var countryProducing = context.CountryProducings.Include(car => car.CarBrands).OrderBy(val => val.Id).ToList();
                dataGrid.ItemsSource = countryProducing;
            }
            //}
            //catch (Exception ex)
            //{
            //     MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                var countryThird = new CountryProducing()
                {
                    CountryName = textBox1.Text
                   // Mark = 12 
                };
                context.CountryProducings.Add(countryThird);
                context.SaveChanges(); 
                var countryProducing = context.CountryProducings.OrderBy(val => val.Id).ToList();
                dataGrid.ItemsSource = countryProducing;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox.Clear();
            textBox1.Clear();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                var deletedcountry = context.CountryProducings.FirstOrDefault(country => country.CountryName == textBox1.Text);
                if (deletedcountry != null)
                {
                    context.CountryProducings.Attach(deletedcountry);
                    context.CountryProducings.Remove(deletedcountry);
                    context.SaveChanges();
                    var countryProducing = context.CountryProducings.OrderBy(val => val.Id).ToList();
                    dataGrid.ItemsSource = countryProducing;
                    dataGrid.Items.Refresh();
                }
            }
           
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            CarBrandView car = new CarBrandView();
            car.ShowDialog();
        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            
       }

        private void dataGrid_Copy_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void dataGrid_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountryProducing country = new CountryProducing();
            if (dataGrid.SelectedItem != null)
            {
                country = (CountryProducing)dataGrid.SelectedItem;
                using (var context = new Context())
                {
                    var responses = context.CarBrands
                       .Join(context.CountryProducings,
                             c => c.CountryProducingId,
                             o => o.Id,
                             (c, o) => new { c, o })
                             .Where(t => t.c.CountryProducingId == country.Id)
                       .OrderBy(x => x.c.Id)
                       .Select(x => new { x.c.Id, x.c.Brand, x.o.CountryName, x.c.Logo }).ToList();
                    dataGrid_Copy.ItemsSource = responses;
                }
            }
           
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ((BubbleSeries)mChart.Series[0]).ItemsSource =
         new KeyValuePair<string, int>[]{
        new KeyValuePair<string,int>("Project Manager", 12),
        new KeyValuePair<string,int>("CEO", 25),
        new KeyValuePair<string,int>("Software Engg.", 5),
        new KeyValuePair<string,int>("Team Leader", 6),
        new KeyValuePair<string,int>("Project Leader", 10),
        new KeyValuePair<string,int>("Developer", 5) };
        } 
        
        
    } 

 }


