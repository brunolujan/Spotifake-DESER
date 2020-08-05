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
    /// <summary>
    /// Lógica de interacción para AddTrackToPlayist.xaml
    /// </summary>
    public partial class AddTrackToPlayist : Page {

        private Track track;
        public AddTrackToPlayist(Track track) {
            this.track = track;
            InitializeComponent();
            LoadPlaylists();
        }

        public async void LoadPlaylists() {
            List<Playlist> playlists = await Session.serverConnection.playlistService.GetPlaylistByLibraryIdAsync(Session.library.IdLibrary);
            datagrid_Playlist.ItemsSource = playlists;
        }

        private async void datagrid_Playlist_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var playlistAux = (Playlist)datagrid_Playlist.SelectedItem;
            if (playlistAux != null)
            {
                var result = await Session.serverConnection.trackService.AddTrackToPlaylistAsync(playlistAux.IdPlaylist, track.IdTrack);
                if (result)
                {
                    MessageBox.Show("Canción Agregada");
                    Window.GetWindow(this).Close();
                }
                else
                {
                    MessageBox.Show("Inténtalo de nuevo");
                }
               
            }
        }
    }
}
