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
    /// Lógica de interacción para TrackAlbum.xaml
    /// </summary>
    public partial class TrackAlbum : Page {
        private Album album;
        public TrackAlbum(Album album) {
            this.album = album;
            InitializeComponent();
            GetTrackByAlbumId();
        }

        public async void GetTrackByAlbumId() {
            try
            {
                List<Track> tracks = await Session.serverConnection.trackService.GetTrackByAlbumIdAsync(album.IdAlbum);
                datagrid_TrackAlbum.ItemsSource = tracks.Select(x => new { NUM = x.TrackNumber, TITLE = x.Title, DURATION = x.DurationSeconds});
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }
    }
}
