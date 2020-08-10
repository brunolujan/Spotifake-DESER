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

namespace Client.ContentCreatorPages {

    public partial class SinglesPage : Page {

        public SinglesPage() {
            InitializeComponent();
            LoadSingles();
        }

        public async void LoadSingles() {
            List<Album> singles = await Session.serverConnection.albumService.GetSinglesByContentCreatorIdAsync(Session.contentCreator.IdContentCreator);
            foreach (var single in singles) {
                single.AlbumImage = await GetImage(single.CoverPath);
                single.AlbumYear = single.ReleaseDate.Year.ToString();
            }
            datagrid_Single.ItemsSource = singles;
        }

        private async Task<BitmapImage> GetImage(String CoverPath) {
            try {
                var imageBytes = await Session.serverConnection.albumService.GetImageToMediaAsync(CoverPath);
                MemoryStream ms = new MemoryStream(imageBytes);
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.StreamSource = ms;
                src.EndInit();
                return src;
            } catch (Exception ex) {
                Console.WriteLine(ex + " in AddAlbum LoadImage");
                return null;
            }
        }

        private void button_AddSingle_Click(object sender, RoutedEventArgs e) {
            PopUpWindow popUpWindow = new PopUpWindow(new AddSinglePage());
            popUpWindow.ShowDialog();
        }

        private void datagrid_Single_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var singleAux = (Album)datagrid_Single.SelectedItem;
            if (singleAux != null) {
                PopUpWindow popUpWindow = new PopUpWindow((new TrackAlbum(singleAux)));
                popUpWindow.ShowDialog();
            }
        }
    }
}
