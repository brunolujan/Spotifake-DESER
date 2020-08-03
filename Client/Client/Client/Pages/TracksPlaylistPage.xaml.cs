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
    /// Lógica de interacción para TracksPlaylistPage.xaml
    /// </summary>
    public partial class TracksPlaylistPage : Page {
        private Playlist playlist;
        public TracksPlaylistPage(Playlist playlist) {
            this.playlist = playlist;
            InitializeComponent();
            GetTrackByPlaylistId();
        }

        public async void GetTrackByPlaylistId() {
            try
            {
                List<Track> tracks = await Session.serverConnection.trackService.GetTrackByPlaylistIdAsync(playlist.IdPlaylist);
                datagrid_TrackPlaylist.ItemsSource = tracks.Select(x => new { NUM = x.TrackNumber, TITLE = x.Title, DURATION = x.DurationSeconds });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

    }
}
