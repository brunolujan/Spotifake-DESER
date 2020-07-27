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

    public partial class MainWindow
    {

        Consumer thisConsumer;
        short thisIdLibrary;

        public MainWindow(Consumer consumer, short idLibrary)
        {
            thisIdLibrary = idLibrary;
            thisConsumer = consumer;
            InitializeComponent();
            if (consumer.ImageStoragePath == null) {
                image.Fill = LoadImage("C:\\Users\\Bruno\\Desktop\\IMAGES\\DefaultCover.jpg");
            } else {
                image.Fill = LoadImage(consumer.ImageStoragePath);
            }
            textBlock_NameUser.Text = "Hi, " + thisConsumer.GivenName;
        }

        private void button_Settings_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = true;
        }

        private void button_Logout_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }

        private void button_Configuration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            flyout.IsOpen = false;
        }

        private async void button_Albums_Click(object sender, RoutedEventArgs e) {
            List<Album> albums = await Session.serverConnection.albumService.GetAlbumByLibraryIdAsync(thisIdLibrary);
            datagrid_Track.ItemsSource = albums;
        }

        private async void button_Tracks_Click(object sender, RoutedEventArgs e) {
            List<Track> tracks = await Session.serverConnection.trackService.GetTrackByLibraryIdAsync(thisIdLibrary);
            datagrid_Track.ItemsSource = tracks.Select(x => new { TITLE = x.Title, SECONDS = x.DurationSeconds });
            datagrid_Track.Items.Refresh();


        }

        private async void button_Playlists_Click(object sender, RoutedEventArgs e) {
            List<Playlist> playlists = await Session.serverConnection.playlistService.GetPlaylistByLibraryIdAsync(thisIdLibrary);
            datagrid_Track.ItemsSource = playlists;
        }

        private async void button_ContentCreators_Click(object sender, RoutedEventArgs e) {
            List<ContentCreator> contentCreators = await Session.serverConnection.contentCreatorService.GetContentCreatorByLibraryIdAsync(thisIdLibrary);
            datagrid_Track.ItemsSource = contentCreators;
        }


        private void button_Search_Click(object sender, RoutedEventArgs e) {

        }

        private ImageBrush LoadImage(string path) {
            try {
                Image imageX = new Image();
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(path);
                src.EndInit();
                imageX.Source = src;

                ImageBrush ib = new ImageBrush();
                ib.ImageSource = src;
                return ib;
            } catch (Exception ex) {
                Console.WriteLine(ex + " in AddAlbum LoadImage");
                return null;
            }
        }
    }
}
