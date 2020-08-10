using Microsoft.Win32;
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

namespace Client.ContentCreatorPages {

    public partial class ConfigurationContentCreatorPage : Page {

        byte[] imageBytes = null;
        Random random;

        public ConfigurationContentCreatorPage() {
            InitializeComponent();
            imageBytes = null;
            random = new Random();
            LoadImageBytes();
        }

        private byte[] GetImageBytes(string filePath) {
            return File.ReadAllBytes(filePath);
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

        private void button_SelectFile_Click(object sender, RoutedEventArgs e) {
            textBlock_Message.Text = "";
            OpenFileDialog ofd = new OpenFileDialog();
            var resultado = ofd.ShowDialog();
            string path = "";
            if (resultado == true) {
                path = ofd.FileName;
                imageBytes = GetImageBytes(path);
                image_ContentCreator.Source = LoadImage(imageBytes);
                image_ContentCreator.Stretch = Stretch.Uniform;
            }
        }

        private async void button_SetConfiguration_Click(object sender, RoutedEventArgs e) {
            if (imageBytes != null) {
                int n = random.Next();
                string fileName = String.Concat(Session.contentCreator.StageName.ToString(), n);
                await Session.serverConnection.contentCreatorService.UpdateContentCreatorImageAsync(Session.contentCreator.Email, fileName);
                if (Session.contentCreator.ImageStoragePath != "DefaultCover") {
                    await Session.serverConnection.contentCreatorService.DeleteImageToMediaAsync(Session.contentCreator.ImageStoragePath);
                }
                Session.contentCreator.ImageStoragePath = fileName;
                await Session.serverConnection.contentCreatorService.AddImageToMediaAsync(fileName, imageBytes);
                Window.GetWindow(this).Close();
            } else {
                textBlock_Message.Text = "*Select a pic file";
            }
        }
    }
}
