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
                datagrid_TrackPlaylist.ItemsSource = tracks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        private async void Button_AddToLibrary_Click(object sender, RoutedEventArgs e) {
            if (await AddToLibrary())
            {

                MessageBox.Show("Item added to library");
            }
            else
            {
                MessageBox.Show("Please select an item");
            }
        }

        private async Task<bool> AddToLibrary() {
            bool result = true;
            if (datagrid_TrackPlaylist.SelectedItem != null)
            {
                Track trackAux = (Track)datagrid_TrackPlaylist.SelectedItem;
                await Session.serverConnection.trackService.AddTrackToLibraryAsync(Session.library.IdLibrary, trackAux.IdTrack);
            }
            return result;

        }

        private void Button_AddToQueue_Click(object sender, RoutedEventArgs e) {
            textBlock_Message.Text = "";
            var trackAux = (Track)datagrid_TrackPlaylist.SelectedItem;
            if (trackAux != null) {
                StreamingPlayer.AddTrackToQueue(trackAux);
                textBlock_Message.Text = "*Track added to Queue";
            } else {
                textBlock_Message.Text = "*Select a track";
            }
        }

        private async void Button_GenerateRadioStation_Click(object sender, RoutedEventArgs e) {
            var trackAux = (Track)datagrid_TrackPlaylist.SelectedItem;
            if (trackAux != null)
            {
                int idGender = (int)Enum.Parse(typeof(MusicGender), trackAux.Gender.ToString());
                var trackList = await Session.serverConnection.trackService.GenerateRadioStationAsync((short)idGender);
                StreamingPlayer.AddListTracksToQueue(trackList);
                MessageBox.Show("Radio station has been generated: " + trackAux.Gender);
            }
            else
            {
                textBlock_Message.Text = "*Select a track";
            }
        }

        private async void datagrid_TrackPlaylist_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            textBlock_Message.Text = "";
            var trackAux = (Track)datagrid_TrackPlaylist.SelectedItem;
            if (trackAux != null)
            {
                await StreamingPlayer.UploadTrackAsync(trackAux);
            }
            else
            {
                textBlock_Message.Text = "*Select a track";
            }
        }
    }
}

