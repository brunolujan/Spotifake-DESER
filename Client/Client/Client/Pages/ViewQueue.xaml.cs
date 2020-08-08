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

    public partial class ViewQueue : Page {

        public ViewQueue() {
            InitializeComponent();
            LoadTracks();
        }

        public void LoadTracks() {
            try {
                datagrid_Track.ItemsSource = StreamingPlayer.queueTracks;
                datagrid_Track.Items.Refresh();
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        private void Button_go_back_Click(object sender, RoutedEventArgs e) {
            if (NavigationService.CanGoBack) {
                NavigationService.GoBack();
            }
        }

        private void Button_clear_queue_Click(object sender, RoutedEventArgs e) {
            StreamingPlayer.queueTracks.Clear();
            LoadTracks();
        }

    }
}
