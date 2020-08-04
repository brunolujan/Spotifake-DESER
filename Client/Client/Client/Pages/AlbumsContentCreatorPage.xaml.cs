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
    /// Lógica de interacción para AlbumsContentCreatorPage.xaml
    /// </summary>
    public partial class AlbumsContentCreatorPage : Page {
        private ContentCreator contentCreator;
        public AlbumsContentCreatorPage(ContentCreator contentCreator) {
            this.contentCreator = contentCreator;
            InitializeComponent();
            GetAlbumByContentCreatorId();
        }

        public async void GetAlbumByContentCreatorId() {
            try
            {
                List<Album> albums = await Session.serverConnection.albumService.GetAlbumsByContentCreatorIdAsync(contentCreator.IdContentCreator);
                datagrid_AlbumContentCreator.ItemsSource = albums;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        private void datagrid_AlbumContentCreator_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var albumAux = (Album)datagrid_AlbumContentCreator.SelectedItem;
            if (albumAux != null)
            {
                NavigationService.Navigate(new TrackAlbum(albumAux));
            }
        }
    }
}
