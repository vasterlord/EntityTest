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
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using Microsoft.Win32;

namespace EntityTest
{
    /// <summary>
    /// Interaction logic for CarBrandView.xaml
    /// </summary>
    public partial class CarBrandView : Window
    {
        public CarBrandView()
        {
            InitializeComponent();
        }
        public static String ImageLoad { get; set; }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            {
                List<string> countries = context.CountryProducings.AsEnumerable().Select(x => x.CountryName).ToList();
                comboBox.ItemsSource = countries;
                comboBox.IsReadOnly = true;
            }
            SelectData();
        }

        private void SelectData()
        {
            using (var context = new Context())
            {
                var responses = context.CarBrands
                    .Join(context.CountryProducings,
                          c => c.CountryProducingId,
                          o => o.Id,
                          (c, o) => new { c, o })
                    .OrderBy(x => x.c.Id)
                    .Select(x => new { x.c.Id, x.c.Brand, x.o.CountryName, x.c.Logo }).ToList();
                dataGrid.ItemsSource = responses;
            }
        }

        private void Car_Model_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Context())
            { 
                List<string> idList = context.CountryProducings
                    .Where(x=>x.CountryName == comboBox.Text) 
                    .AsEnumerable() 
                    .Select(x=>x.Id.ToString()).ToList();
                int id = Convert.ToInt32(idList[0]);
                context.CarBrands.Add(new CarBrand()
                {
                    Brand = textBox1.Text,
                    CountryProducingId = id,
                    Logo = GetPhoto(ImageLoad)
                });
                context.CarBrands.Add(new CarBrand()
                {
                    Brand = textBox1.Text+ "new",
                    CountryProducingId = id
                });
                context.SaveChanges();
                SelectData();
            }
        }

        public  byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(
            filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        } 

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox.Clear();
            textBox1.Clear();
            image.Source = null;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oDialog = new OpenFileDialog();
            oDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            Nullable<bool> result = oDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    string fileName = oDialog.FileName;
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bi.UriSource = new Uri(fileName);
                    bi.EndInit();
                    image.Source = bi; 
                    ImageLoad = oDialog.FileName; ;
                }
                catch
                {
                    MessageBox.Show("Сan not open the selected file",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
    }
}
