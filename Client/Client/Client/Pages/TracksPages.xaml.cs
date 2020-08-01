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
   
    public partial class TracksPages : Page {

        public TracksPages() {
            InitializeComponent();
            LoadTracks();
        }

        public async void LoadTracks() {
            List<Track> tracks = await Session.serverConnection.trackService.GetTrackByLibraryIdAsync(Session.library.IdLibrary);
            datagrid_Track.ItemsSource = tracks.Select(x => new { TITLE = x.Title, SECONDS = x.DurationSeconds });
            datagrid_Track.Items.Refresh();
        }
    }
}
