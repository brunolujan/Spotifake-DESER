using Microsoft.Win32;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Client.Pages {

    public partial class ConfigurationConsumerPage : Page {

        byte[] imageBytes;
        Random random;

        public ConfigurationConsumerPage() {
            InitializeComponent();
            imageBytes = null;
            random = new Random();
            LoadImageBytes();
        }

        private byte[] GetImageBytes(string filePath) {
            return File.ReadAllBytes(filePath);
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

        private void button_SelectFile_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            var resultado = ofd.ShowDialog();
            string path = "";
            if (resultado == true) {
                path = ofd.FileName;
                imageBytes = GetImageBytes(path);
                image_Consumer.Source = LoadImage(imageBytes);
                image_Consumer.Stretch = Stretch.Uniform;
            }
        }

        private async void button_SetConfiguration_Click(object sender, RoutedEventArgs e) {
            if (imageBytes != null) {
                int n = random.Next();
                string fileName = String.Concat(Session.consumer.GivenName.ToString(), Session.consumer.LastName.ToString(), n);
                await Session.serverConnection.consumerService.UpdateConsumerImageAsync(Session.consumer.Email, fileName);
                await Session.serverConnection.consumerService.AddImageToMediaAsync(fileName, imageBytes);
            }
            textBlock_Message.Text = "*Select a pic file";
        }
    }
}
