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
            datagrid_SearchTracks.Visibility = Visibility.Hidden;
            datagrid_SearchAlbums.Visibility = Visibility.Hidden;
            datagrid_SearchContentCreators.Visibility = Visibility.Hidden;
            datagrid_SearchPlaylists.Visibility = Visibility.Hidden;
        }

        public async void GetTrackByQuery() {
            try
            {
                datagrid_SearchTracks.Visibility = Visibility.Visible;
                List<Track> tracks = await Session.serverConnection.trackService.GetTrackByQueryAsync(TextBox_search.Text);
                datagrid_SearchTracks.ItemsSource = tracks.Select(x => new { NUM = x.TrackNumber, TITLE = x.Title, SECONDS = x.DurationSeconds });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        public async void GetAlbumByQuery() {
            try
            {
                datagrid_SearchAlbums.Visibility = Visibility.Visible;
                List<Album> albums = await Session.serverConnection.albumService.GetAlbumByQueryAsync(TextBox_search.Text);
                datagrid_SearchAlbums.ItemsSource = albums;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        public async void GetContentCreatorByQuery() {
            try
            {
                datagrid_SearchContentCreators.Visibility = Visibility.Visible;
                List<ContentCreator> contentCreators = await Session.serverConnection.contentCreatorService.GetContentCreatorByQueryAsync(TextBox_search.Text);
                datagrid_SearchContentCreators.ItemsSource = contentCreators.Select(x => new { NAME = x.StageName, DESCRIPTION = x.Description });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        public async void GetPlaylistByQuery() {
            try
            {
                datagrid_SearchPlaylists.Visibility = Visibility.Visible;
                List<Playlist> playlists = await Session.serverConnection.playlistService.GetPlaylistByQueryAsync(TextBox_search.Text);
                datagrid_SearchPlaylists.ItemsSource = playlists.Select(x => new { NAME = x.Name, DESCRIPTION = x.Description });
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
                    case "Albums":
                        HiddenLists();
                        GetAlbumByQuery();
                        break;
                    case "Artists":
                        HiddenLists();
                        GetContentCreatorByQuery();
                        break;
                    case "Playlists":
                        HiddenLists();
                        GetPlaylistByQuery();
                        break;
                }
            }

        }

    }
}
