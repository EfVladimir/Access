using System;
using System.Collections.Generic;
using System.Configuration;
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
using AccessHW.BLL;

namespace AccessHW
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string connectionString =
        ConfigurationManager
        .ConnectionStrings["AccessHW.BLL.Properties.Settings.ADOConnectionString"]
        .ConnectionString;
        Class1 class1 = new Class1(connectionString);
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            TableManuf.Visibility = Visibility.Hidden;
            TableModel.Visibility = Visibility.Hidden;
            DataModel.Visibility = Visibility.Hidden;
            DataManufacture.Visibility = Visibility.Hidden;
            Main.Visibility = Visibility.Visible;
        }
        
        public void Button_Click_Status(object sender, RoutedEventArgs e)
        {
            string message;
            if(Status.Content == "Status: Connect")
            {
                class1.Disconnect(out message);
                MessageBox.Show(message);
                Status.Content = "Status: Disconnect";
                Status.Background = Brushes.Red;
            }
            else
            {
                class1.Connect(out message);
                MessageBox.Show(message);
                Status.Content = "Status: Connect";
                Status.Background = Brushes.Green;
            }
        }


        #region Работа с model
        private void Button_Click_model(object sender, RoutedEventArgs e)
        {
            string message;
            class1 = new Class1(connectionString);
            class1.Connect(out message);
            List<TablesModel> data = class1.dataModel;
            int i = 0;
            if (Status.Content == "Status: Connect")
            {
                Main.Visibility = Visibility.Hidden;
                TableModel.Visibility = Visibility.Visible;
                while (stackP1.Children.Count > 0)
                {
                    stackP1.Children.RemoveAt(stackP1.Children.Count - 1);
                }
                while (i < data.Count())
                {
                    TextBlock hstr = new TextBlock()
                    { Name = "txt", Text = data[i].intModelID +
                    "|\t|" + data[i].strName +
                    "|\t|" + data[i].intManufacturerID +
                    "|\t|" + data[i].intSMCSFamilyID +
                    "|\t|" + data[i].strImage };
                    hstr.Margin = new Thickness(20, 10, 20, 10);
                    hstr.MinHeight = 30;
                    hstr.FontSize = 16;
                    hstr.MaxWidth = 500;
                    hstr.TextWrapping = TextWrapping.WrapWithOverflow;
                    hstr.Padding = new Thickness(5);
                    hstr.HorizontalAlignment = HorizontalAlignment.Left;
                    stackP1.Children.Add(hstr);
                    stackP1.Children.Add(new Separator());
                    i++;
                }
            }
            else MessageBox.Show("Подключитесь к базе!");
        }
        public void Button_Click_AddModel(object sender, RoutedEventArgs e)
        {
            TableModel.Visibility = Visibility.Hidden;
            DataModel.Visibility = Visibility.Visible;
        }
        public void Button_Click_DeleteDataModel(object sender, RoutedEventArgs e)
        {
            string message;
            class1.RemoveDataModel(out message, Convert.ToInt32(ModelDataRemove.Text));
            MessageBox.Show(message);
            TableModel.Visibility = Visibility.Hidden;
            Main.Visibility = Visibility.Visible;
            ModelDataRemove.Text = "";
        }
        public void Button_Click_SaveAddModel(object sender, RoutedEventArgs e)
        {
            string message;
            try
            {
                TablesModel model = new TablesModel(intModelId.Text, strNameModel.Text, intManufacturerID.Text, intSMCSFamilyID.Text, strImage.Text);
                class1.AddDataModel(out message, model);
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                MessageBox.Show(message);
            }

        }
        #endregion
        #region Работа с Manuf
        private void Button_Click_manuf(object sender, RoutedEventArgs e)
        {
            string message;
            class1 = new Class1(connectionString);
            class1.Connect(out message);
            List<TablesManufacturer> data = class1.dataManuf;
            int i = 0;
            if (Status.Content == "Status: Connect")
            {
                Main.Visibility = Visibility.Hidden;
                TableManuf.Visibility = Visibility.Visible;
                while (stackP2.Children.Count > 0)
                {
                    stackP2.Children.RemoveAt(stackP2.Children.Count - 1);
                }
                while (i < data.Count())
                {
                    TextBlock hstr = new TextBlock()
                    { Name = "txt", Text = data[i].intManufacturer + "\t" + data[i].strName };
                    hstr.Margin = new Thickness(20, 10, 20, 10);
                    hstr.MinHeight = 30;
                    hstr.FontSize = 16;
                    hstr.MaxWidth = 500;
                    hstr.TextWrapping = TextWrapping.WrapWithOverflow;
                    hstr.Padding = new Thickness(5);
                    hstr.HorizontalAlignment = HorizontalAlignment.Left;
                    stackP2.Children.Add(hstr);
                    stackP2.Children.Add(new Separator());
                    i++;
                }
            }
            else MessageBox.Show("Подключитесь к базе!");
        }
        public void Button_Click_AddManuf(object sender, RoutedEventArgs e)
        {
            TableManuf.Visibility = Visibility.Hidden;
            DataManufacture.Visibility = Visibility.Visible;
        }
        public void Button_Click_DeleteDataManuf(object sender, RoutedEventArgs e)
        {
            string message;
            class1.RemoveDataManufacturer(out message, Convert.ToInt32(ManufDataRemove.Text));
            MessageBox.Show(message);
            TableManuf.Visibility = Visibility.Hidden;
            Main.Visibility = Visibility.Visible;
            ManufDataRemove.Text = "";
        }
        public void Button_Click_SaveAddManuf(object sender, RoutedEventArgs e)
        {

            string message;
            try
            {
                TablesManufacturer manufacturer = new TablesManufacturer(Convert.ToInt32(intManufId.Text), strName.Text);
                class1.AddDataManufacturer(out message, manufacturer);
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                MessageBox.Show(message);
            }

        }
        #endregion
    }
}
