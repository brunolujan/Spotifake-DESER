using ControlzEx.Standard;
using System;
using System.Activities.Expressions;
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

namespace Client {

    public partial class MainWindowContentCreator {

        ContentCreator thisContentCreator;
        int thisVar;

        public MainWindowContentCreator(ContentCreator contentCreator, int var) {
            thisContentCreator = contentCreator;
            thisVar = var;
            InitializeComponent();
            LoadContentCreatorImage("C:\\Users\\Bruno\\Documents\\BRUNO\\oregon-coast-3840x2400-sunset-beach-purple-sky-4k-17946.jpg");
            textBlock_StageName.Text = "Hi, " + thisContentCreator.StageName;
            if (var == 0) {
                label_Title.Text = "MY ALBUMS";
                button_Add.Content = "+ Add Album";
            } else {
                label_Title.Text = "MY SINGLES";
                button_Add.Content = "+ Add Single";
            }
        }

        private async void button_Albums_Click(object sender, RoutedEventArgs e) {
            label_Title.Text = "MY ALBUMS";
            button_Add.Content = "+ Add Album";
            List<Album> albums = await Session.serverConnection.albumService.GetAlbumsByContentCreatorIdAsync(thisContentCreator.IdContentCreator);
        }

        private async void button_Singles_Click(object sender, RoutedEventArgs e) {
            label_Title.Text = "MY SINGLES";
            button_Add.Content = "+ Add Single";
            List<Album> singles = await Session.serverConnection.albumService.GetSinglesByContentCreatorIdAsync(thisContentCreator.IdContentCreator);
        }

        private void button_Settings_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = true;
        }

        private void button_Configuration_Click(object sender, RoutedEventArgs e) {

        }

        private void button_Logout_Click(object sender, RoutedEventArgs e) {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = false;
        }

        private void LoadContentCreatorImage(string path) {
            Image imageX = new Image();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(path);
            src.EndInit();
            imageX.Source = src;
            imageX.Stretch = Stretch.Uniform;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = src;
            image.Fill = ib;
        }

        private void button_Search_Click(object sender, RoutedEventArgs e) {

        }

        private void button_Add_Click(object sender, RoutedEventArgs e) {
            if (label_Title.Text == "MY SINGLES") {
                thisVar = 1;
            }
            AddAlbum addAlbumWindow = new AddAlbum(thisContentCreator, thisVar);
            addAlbumWindow.Show();
            this.Close();
        }
    }
}
