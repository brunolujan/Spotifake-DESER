    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Client.Pages {
   
    public partial class TracksPages : Page {

        public TracksPages() {
            InitializeComponent();
            LoadTracks();
        }

        public async void LoadTracks() {
            List<Track> tracks = await Session.serverConnection.trackService.GetTrackByLibraryIdAsync(Session.library.IdLibrary);
            datagrid_Track.ItemsSource = tracks;
            datagrid_Track.Items.Refresh();
        }

        private void Button_AddToPlaylist_Click(object sender, RoutedEventArgs e) {
            var trackAux = (Track)datagrid_Track.SelectedItem;
            if (trackAux != null)
            {
                PopUpWindow popUpWindow = new PopUpWindow(new AddTrackToPlayist(trackAux));
                popUpWindow.ShowDialog();
            }
            else
            {
                textBlock_Message.Text = "*Select a track";
            }
        }

        private void Button_AddToQueue_Click(object sender, RoutedEventArgs e) {
            textBlock_Message.Text = "";
            var trackAux = (Track)datagrid_Track.SelectedItem;
            if (trackAux != null) {
                StreamingPlayer.AddTrackToQueue(trackAux);
                textBlock_Message.Text = "*Track added to Queue";
            } else {
                textBlock_Message.Text = "*Select a track";
            }
        }

        private async void button_LetsPlay_Click(object sender, RoutedEventArgs e) {
            textBlock_Message.Text = "";
            var trackAux = (Track)datagrid_Track.SelectedItem;
            if (trackAux != null) {
                await StreamingPlayer.UploadTrackAsync(trackAux);
            } else {
                textBlock_Message.Text = "*Select a track";
            }
        }
    }
}
