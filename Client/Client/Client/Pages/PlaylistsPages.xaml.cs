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

    public partial class PlaylistsPages : Page {

        public PlaylistsPages() {
            InitializeComponent();
            LoadPlaylists();
        }

        public async void LoadPlaylists() {
            List<Playlist> playlists = await Session.serverConnection.playlistService.GetPlaylistByLibraryIdAsync(Session.library.IdLibrary);
            foreach (var playlist in playlists)
            {
                playlist.PlaylistImage = await GetImage(playlist.CoverPath);     
            }
            datagrid_Playlist.ItemsSource = playlists;

        }

        private async Task<BitmapImage> GetImage(String CoverPath) {
            try
            {
                var imageBytes = await Session.serverConnection.playlistService.GetImageToMediaAsync(CoverPath);
                MemoryStream ms = new MemoryStream(imageBytes);
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.StreamSource = ms;
                src.EndInit();
                return src;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " in AddAlbum LoadImage");
                return null;
            }
        }


        private void datagrid_Playlist_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var playlistAux = (Playlist)datagrid_Playlist.SelectedItem;
            if (playlistAux != null)
            {
                NavigationService.Navigate(new TracksPlaylistPage(playlistAux));
            }
        }
    }
}
