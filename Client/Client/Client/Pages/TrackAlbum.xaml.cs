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
                datagrid_TrackAlbum.ItemsSource = tracks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        private void Button_AddToPlaylist_Click(object sender, RoutedEventArgs e) {
            var trackAux = (Track)datagrid_TrackAlbum.SelectedItem;
            if (trackAux != null)
            {
                PopUpWindow popUpWindow = new PopUpWindow(new AddTrackToPlayist(trackAux));
                popUpWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a track");
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
            if (datagrid_TrackAlbum.SelectedItem != null)
            {
                Track trackAux = (Track)datagrid_TrackAlbum.SelectedItem;
                await Session.serverConnection.trackService.AddTrackToLibraryAsync(Session.library.IdLibrary, trackAux.IdTrack);
            }
            return result; 
  
        }

        private void Button_AddToQueue_Click(object sender, RoutedEventArgs e) {
            textBlock_Message.Text = "";
            var trackAux = (Track)datagrid_TrackAlbum.SelectedItem;
            if (trackAux != null) {
                StreamingPlayer.AddTrackToQueue(trackAux);
                textBlock_Message.Text = "*Track added to Queue";
            } else {
                textBlock_Message.Text = "*Select a track";
            }
        }

        private async void button_LetsPlay_Click(object sender, RoutedEventArgs e) {
            textBlock_Message.Text = "";
            var trackAux = (Track)datagrid_TrackAlbum.SelectedItem;
            if (trackAux != null) {
                await StreamingPlayer.UploadTrackAsync(trackAux);
            } else {
                textBlock_Message.Text = "*Select a track";
            }
        }
    }
}
