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
            random = new Random();
        }

        private byte[] GetImageBytes(string filePath) {
            return File.ReadAllBytes(filePath);
        }

        private void LoadImage(string path) {
            try {
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(path);
                src.EndInit();
                image_Consumer.Source = src;
            } catch (Exception ex) {
                Console.WriteLine(ex + " in AddAlbum LoadImage");
            }
        }

        private void button_SelectFile_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            var resultado = ofd.ShowDialog();
            string path = "";
            if (resultado == true) {
                path = ofd.FileName;
                LoadImage(path);
                image_Consumer.Stretch = Stretch.Uniform;
                imageBytes = GetImageBytes(path);
            }
        }

        private async void button_SetConfiguration_Click(object sender, RoutedEventArgs e) {
            int n = random.Next();
            string fileName = String.Concat(Session.consumer.GivenName.ToString(), Session.consumer.LastName.ToString(), n);
            await Session.serverConnection.consumerService.UpdateConsumerImageAsync(Session.consumer.Email, fileName);
            await Session.serverConnection.consumerService.AddImageToMediaAsync(fileName, imageBytes);
        }
    }
}
