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

namespace Client.Pages {

    public partial class AddPlaylistPage : Page {

        byte[] imageBytes;
        Random random;

        public AddPlaylistPage() {
            InitializeComponent();
            LoadImageBytes();
            imageBytes = null;
        }

        private byte[] GetImageBytes(string filePath) {
            return File.ReadAllBytes(filePath);
        }

        private async void LoadImageBytes() {
            image_Consumer.Source = LoadImage(await Session.serverConnection.consumerService.GetImageToMediaAsync("DefaultAlbumCover"));
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

        private void button_Create_Click(object sender, RoutedEventArgs e) {
            if (imageBytes != null) {
                Playlist newPlaylist = new Playlist();
                Date date = new Date();
                DateTime today = DateTime.Today;
                int n = random.Next();
                newPlaylist.Name = textBox_Name.Text;
                newPlaylist.Description = textBox_Description.Text;
                string fileName = String.Concat("Playlist", textBox_Name.Text, n);
                date.Day = Convert.ToInt16(today.Day);
                date.Month = Convert.ToInt16(today.Month);
                date.Year = Convert.ToInt16(today.Year);
                newPlaylist.CreationDate = date;
                newPlaylist.CoverPath = fileName;
                Session.serverConnection.playlistService.AddImageToMediaAsync(fileName, imageBytes);
                Window.GetWindow(this).Close();
            } else {
                textBlock_Message.Text = "*Select a pic file";
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
    }
}
