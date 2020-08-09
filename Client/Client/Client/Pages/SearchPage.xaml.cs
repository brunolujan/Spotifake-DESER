using System;
using System.Collections.Generic;
using System.IO;
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
                Button_AddToPlaylist.Visibility = Visibility.Visible;
                Button_AddToLibrary.Visibility = Visibility.Visible;
                List<Track> tracks = await Session.serverConnection.trackService.GetTrackByQueryAsync(TextBox_search.Text);
                datagrid_SearchTracks.ItemsSource = tracks;
                
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
                Button_AddToPlaylist.Visibility = Visibility.Hidden;
                Button_AddToLibrary.Visibility = Visibility.Visible;
                List<Album> albums = await Session.serverConnection.albumService.GetAlbumByQueryAsync(TextBox_search.Text);
                foreach (var album in albums)
                {
                    album.AlbumImage = await GetImageAlbum(album.CoverPath);
                }
                datagrid_SearchAlbums.ItemsSource = albums;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        private async Task<BitmapImage> GetImageAlbum(String CoverPath) {
            try
            {
                var imageBytes = await Session.serverConnection.albumService.GetImageToMediaAsync(CoverPath);
                MemoryStream ms = new MemoryStream(imageBytes);
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.StreamSource = ms;
                src.EndInit();
                return src;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " in AddAlbum LoadImage");
                return null;
            }
        }

        public async void GetContentCreatorByQuery() {
            try
            {
                datagrid_SearchContentCreators.Visibility = Visibility.Visible;
                Button_AddToPlaylist.Visibility = Visibility.Hidden;
                Button_AddToLibrary.Visibility = Visibility.Visible;
                List<ContentCreator> contentCreators = await Session.serverConnection.contentCreatorService.GetContentCreatorByQueryAsync(TextBox_search.Text);
                datagrid_SearchContentCreators.ItemsSource = contentCreators;
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
                Button_AddToPlaylist.Visibility = Visibility.Hidden;
                Button_AddToLibrary.Visibility = Visibility.Visible;
                List<Playlist> playlists = await Session.serverConnection.playlistService.GetPlaylistByQueryAsync(TextBox_search.Text);
                foreach (var playlist in playlists)
                {
                    playlist.PlaylistImage = await GetImagePlaylist(playlist.CoverPath);
                }
                datagrid_SearchPlaylists.ItemsSource = playlists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        private async Task<BitmapImage> GetImagePlaylist(String CoverPath) {
            try
            {
                var imageBytes = await Session.serverConnection.playlistService.GetImageToMediaAsync(CoverPath);
                MemoryStream ms = new MemoryStream(imageBytes);
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.StreamSource = ms;
                src.EndInit();
                return src;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " in AddAlbum LoadImage");
                return null;
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

        private void datagrid_SearchAlbums_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var albumAux = (Album)datagrid_SearchAlbums.SelectedItem;
            if (albumAux != null)
            {
                NavigationService.Navigate(new TrackAlbum(albumAux));
            }
        }

        private void datagrid_SearchPlaylists_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var playlistAux = (Playlist)datagrid_SearchPlaylists.SelectedItem;
            if (playlistAux != null)
            {
                NavigationService.Navigate(new TracksPlaylistPage(playlistAux));
            }
        }

        private void datagrid_SearchContentCreators_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var contentCreatorAux = (ContentCreator)datagrid_SearchContentCreators.SelectedItem;
            if (contentCreatorAux != null)
            {
                NavigationService.Navigate(new AlbumsContentCreatorPage(contentCreatorAux));
            }
        }

        private void Button_AddToPlaylist_Click(object sender, RoutedEventArgs e) {
            var trackAux = (Track)datagrid_SearchTracks.SelectedItem;
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
            if (datagrid_SearchAlbums.SelectedItem != null)
            {
                Album albumAux = (Album)datagrid_SearchAlbums.SelectedItem;
                await Session.serverConnection.albumService.AddAlbumToLibraryAsync(Session.library.IdLibrary, albumAux.IdAlbum);
            }
            else if (datagrid_SearchContentCreators.SelectedItem != null)
            {
                ContentCreator contentCreatorAux = (ContentCreator)datagrid_SearchContentCreators.SelectedItem;
                await Session.serverConnection.contentCreatorService.AddContentCreatorToLibraryAsync(Session.library.IdLibrary, contentCreatorAux.IdContentCreator);
            }
            else if (datagrid_SearchPlaylists.SelectedItem != null)
            {
                Playlist playlistAux = (Playlist)datagrid_SearchPlaylists.SelectedItem;
                await Session.serverConnection.playlistService.AddPlaylistToLibraryAsync(Session.library.IdLibrary, playlistAux.IdPlaylist);
            }
            else if (datagrid_SearchTracks.SelectedItem != null)
            {
                Track trackAux = (Track)datagrid_SearchTracks.SelectedItem;
                await Session.serverConnection.trackService.AddTrackToLibraryAsync(Session.library.IdLibrary, trackAux.IdTrack);
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
