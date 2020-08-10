using Client.ContentCreatorPages;
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
using System.Windows.Shapes;

namespace Client {

    public partial class ContentCreatorMain {
        public ContentCreatorMain() {
            InitializeComponent();
            LoadImageBytes();
            textBlock_StageName.Text = "Hi, " + Session.contentCreator.StageName;
        }

        private void button_Albums_Click(object sender, RoutedEventArgs e) {
            centralFrame.Navigate(new AlbumsPage());
        }

        private void button_Singles_Click(object sender, RoutedEventArgs e) {
            centralFrame.Navigate(new SinglesPage());
        }

        private void button_Settings_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = true;
        }

        private void button_Back_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = false;
        }

        private void button_Configuration_Click(object sender, RoutedEventArgs e) {
            PopUpWindow popUpWindow = new PopUpWindow(new ConfigurationContentCreatorPage());
            flyout.IsOpen = false;
            popUpWindow.ShowDialog();
        }

        private void button_Logout_Click(object sender, RoutedEventArgs e) {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }

        private async void LoadImageBytes() {
            image_ContentCreator.Source = LoadImage(await Session.serverConnection.contentCreatorService.GetImageToMediaAsync(Session.contentCreator.ImageStoragePath));
            image_ContentCreator.Stretch = Stretch.Uniform;
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
    }
}
