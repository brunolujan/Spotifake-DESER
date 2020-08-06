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
    /// Lógica de interacción para MyOwnTracksPage.xaml
    /// </summary>
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

        private void datagrid_MyOwnTracks_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

        }
    }
}
