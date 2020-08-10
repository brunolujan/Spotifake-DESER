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

    public partial class MyOwnTracksPage : Page {
        public MyOwnTracksPage() {
            InitializeComponent();
            LoadLocalTracks();
        }

        public async void LoadLocalTracks() {
            try
            {
                List<LocalTrack> localTracks = await Session.serverConnection.trackService.GetLocalTracksByIdConsumerAsync(Session.consumer.IdConsumer);
                datagrid_MyOwnTracks.ItemsSource = localTracks;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private async void datagrid_MyOwnTracks_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            textBlock_Message.Text = "";
            var trackAux = (LocalTrack)datagrid_MyOwnTracks.SelectedItem;
            if (trackAux != null) {
                Track localTrack = new Track();
                localTrack.Title = trackAux.Title;
                localTrack.StoragePath = trackAux.FileName;
                localTrack.DurationSeconds = 200;
                await StreamingPlayer.UploadTrackAsync(localTrack);
            } else {
                textBlock_Message.Text = "*Select a track";
            }
        }

        private void Button_AddLocalTrack_Click(object sender, RoutedEventArgs e) {
            PopUpWindow popUpWindow = new PopUpWindow(new UploadLocalTrackPage());
            popUpWindow.ShowDialog();
            LoadLocalTracks();
        }

        private void Button_AddToQueue_Click(object sender, RoutedEventArgs e) {
            textBlock_Message.Text = "";
            var trackAux = (LocalTrack)datagrid_MyOwnTracks.SelectedItem;
            if (trackAux != null) {
                Track localTrack = new Track();
                localTrack.Title = trackAux.Title;
                localTrack.StoragePath = trackAux.FileName;
                localTrack.DurationSeconds = 200;
                StreamingPlayer.AddTrackToQueue(localTrack);
                textBlock_Message.Text = "*Track added to Queue";
            } else {
                textBlock_Message.Text = "*Select a track";
            }
        }
    }
}
