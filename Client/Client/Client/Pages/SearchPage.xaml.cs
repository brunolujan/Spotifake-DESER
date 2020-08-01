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
    /// Lógica de interacción para SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page {
        public SearchPage() {
            InitializeComponent();
        }

        private void HiddenLists() {
            ListViewTracks.Visibility = Visibility.Hidden;
        }

        public async void GetTrackByQuery() {
            try
            {
                ListViewTracks.Visibility = Visibility.Visible;
                List<Track> tracks = await Session.serverConnection.trackService.GetTrackByQueryAsync(TextBox_search.Text);
                ListViewTracks.ItemsSource = tracks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        private bool ValidateEmptyField() {
            bool isValid = true;
            if (String.IsNullOrEmpty(TextBox_search.Text))
            {
                MessageBox.Show("Empty field");
                isValid = false;
            }
            return isValid;
        }

        private void Button_search_Click(object sender, RoutedEventArgs e) {

            string opcion = ComboBox_filter.Text;
            Console.WriteLine(opcion);

            if (ValidateEmptyField())
            {
                switch (opcion)
                {
                    case "Tracks":
                        HiddenLists();
                        GetTrackByQuery();
                        break;
                }
            }

        }
    }
}
