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

    public partial class ViewHistory : Page {

        public ViewHistory() {
            InitializeComponent();
            LoadTracks();
        }

        public void LoadTracks() {
            try {
                datagrid_Track.ItemsSource = StreamingPlayer.historyTracks;
                datagrid_Track.Items.Refresh();
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        private void datagrid_Track_MouseDoubleClick(object sender, MouseButtonEventArgs e) {

        }
    }
}
