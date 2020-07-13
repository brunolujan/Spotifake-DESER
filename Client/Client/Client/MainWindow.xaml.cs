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
using System.Windows.Shapes;

namespace Client
{

    public partial class MainWindow : Window
    {

        Consumer thisConsumer;

        public MainWindow(Consumer consumer)
        {
            thisConsumer = consumer;
            InitializeComponent();
            textBlock_NameUser.Text = thisConsumer.GivenName + " " + thisConsumer.LastName;
        }

        private void button_Logout_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }

        private void button_Singles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Albums_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Singles_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void button_Configuration_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
