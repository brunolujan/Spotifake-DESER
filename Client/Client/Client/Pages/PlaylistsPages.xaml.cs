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

namespace Client.Pages {

    public partial class PlaylistsPages : Page {

        public PlaylistsPages() {
            InitializeComponent();
            LoadPlaylists();
        }

        public async void LoadPlaylists() {
            List<Playlist> playlists = await Session.serverConnection.playlistService.GetPlaylistByLibraryIdAsync(Session.library.IdLibrary);
            datagrid_Playlist.ItemsSource = playlists;
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
