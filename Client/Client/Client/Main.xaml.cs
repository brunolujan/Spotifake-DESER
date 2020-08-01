using Client.Pages;
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

namespace Client {
    
    public partial class Main {

        public Main() {
            InitializeComponent();
            centralFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            if (Session.consumer.ImageStoragePath == null)
            {
                image.Fill = LoadImage("C:\\Users\\Bruno\\Desktop\\IMAGES\\DefaultCover.jpg");
            }
            else
            {
                image.Fill = LoadImage(Session.consumer.ImageStoragePath);
            }
            textBlock_NameUser.Text = "Hi, " + Session.consumer.GivenName;

        }

        private ImageBrush LoadImage(string path) {
            try
            {
                Image imageX = new Image();
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(path);
                src.EndInit();
                imageX.Source = src;

                ImageBrush ib = new ImageBrush();
                ib.ImageSource = src;
                return ib;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " in AddAlbum LoadImage");
                return null;
            }
        }


        private void button_Settings_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = true;
        }

        private void button_Logout_Click(object sender, RoutedEventArgs e) {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }

        private void button_Configuration_Click(object sender, RoutedEventArgs e) {

        }

        private void button_Back_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = false;
        }

        private void button_Tracks_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new TracksPages());
        }

        private void button_ContentCreators_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new ContentCreatorsPages());
        }

        private void button_Albums_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new AlbumsPages());

        }

        private void button_Playlists_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new PlaylistsPages());
        }
        private void button_Search_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new SearchPage());
        }
    }
}
