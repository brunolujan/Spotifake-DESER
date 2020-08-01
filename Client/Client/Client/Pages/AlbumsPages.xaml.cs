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

    public partial class AlbumsPages : Page {

        public AlbumsPages() {
            InitializeComponent();
            LoadAlbums();
        }

        public async void LoadAlbums() {
            List<Album> albums = await Session.serverConnection.albumService.GetAlbumByLibraryIdAsync(Session.library.IdLibrary);
            datagrid_Album.ItemsSource = albums;
        }
    }
}
