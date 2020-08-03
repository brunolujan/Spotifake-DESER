using Client.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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
            LoadImageBytes();
            textBlock_NameUser.Text = "Hi, " + Session.consumer.GivenName;
        }

        private async void LoadImageBytes() {
            image_Consumer.Source = LoadImage(await Session.serverConnection.consumerService.GetImageToMediaAsync(Session.consumer.ImageStoragePath));
            image_Consumer.Stretch = Stretch.Uniform;
        }

        private BitmapImage LoadImage(byte[] bytes) {
            try {
                MemoryStream ms = new MemoryStream(bytes);
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.StreamSource = ms;
                src.EndInit();
                return src;
            } catch (Exception ex) {
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
            centralFrame.Navigate(new ConfigurationConsumerPage());
            flyout.IsOpen = false;
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
