using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Image = System.Windows.Controls.Image;

namespace Client
{
    public partial class MainWindowContentCreator
    {

        ContentCreator thisContentCreator;

        public MainWindowContentCreator(ContentCreator contentCreator)
        {
            thisContentCreator = contentCreator;
            InitializeComponent();
            textBlock_StageName.Text = "Hi, " + thisContentCreator.StageName;
        }

        private void button_Albums_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Singles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Settings_Click(object sender, RoutedEventArgs e)
        {
            flyout.IsOpen = true;
        }

        private void button_Configuration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Logout_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            flyout.IsOpen = false;
        }

        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            Image imageX = new Image();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri("C:\\Users\\Bruno\\Documents\\BRUNO\\oregon-coast-3840x2400-sunset-beach-purple-sky-4k-17946.jpg");
            src.EndInit();
            imageX.Source = src;
            imageX.Stretch = Stretch.Uniform;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = src;
            image.Fill = ib;
        }
    }
}
