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

    public partial class AlbumsPage : Page {

        public AlbumsPage() {
            InitializeComponent();
            LoadAlbums();
        }

        public async void LoadAlbums() {
            List<Album> albums = await Session.serverConnection.albumService.GetAlbumsByContentCreatorIdAsync(Session.contentCreator.IdContentCreator);
            foreach (var album in albums) {
                album.AlbumImage = await GetImage(album.CoverPath);
                album.AlbumYear = album.ReleaseDate.Year.ToString();
            }
            datagrid_Album.ItemsSource = albums;
        }

        private async Task<BitmapImage> GetImage(String CoverPath) {
            try {
                var imageBytes = await Session.serverConnection.albumService.GetImageToMediaAsync(CoverPath);
                MemoryStream ms = new MemoryStream(imageBytes);
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

        private void button_AddAlbum_Click(object sender, RoutedEventArgs e) {
            PopUpWindow popUpWindow = new PopUpWindow(new AddAlbumPage());
            popUpWindow.ShowDialog();
        }
    }
}
