using Microsoft.Win32;
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
using ListItem = System.Web.UI.WebControls.ListItem;

namespace Client.ContentCreatorPages {

    public partial class AddSinglePage : Page {

        int duration;
        Random random;
        byte[] imageBytes;
        byte[] fileBytes;

        public AddSinglePage() {
            InitializeComponent();
            LoadContent();
            duration = -1;
            random = new Random();
            imageBytes = null;
            fileBytes = null;
        }

        private async void LoadContent() {
            image_AlbumCover.Source = LoadImage(await Session.serverConnection.albumService.GetImageToMediaAsync("DefaultAlbumCover"));
            image_AlbumCover.Stretch = Stretch.Uniform;

            foreach (int r in Enum.GetValues(typeof(MusicGender))) {
                var gender = new ListItem(Enum.GetName(typeof(MusicGender), r), r.ToString());
                comboBox_Gender.Items.Add(gender);
                comboBox_SingleTrackGender.Items.Add(gender);
            }

            List<ContentCreator> contentCreators = await Session.serverConnection.contentCreatorService.GetContentCreatorsAsync();
            foreach (ContentCreator contentCreatorAux in contentCreators) {
                comboBox_Featuring.Items.Add(contentCreatorAux.StageName);
                comboBox_SingleFeaturing.Items.Add(contentCreatorAux.StageName);
            }
        }

        private BitmapImage LoadImage(byte[] bytes) {
            try {
                MemoryStream ms = new MemoryStream(bytes);
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

        private bool ValidateBlankSpacesSingle() {
            if (textBox_AlbumTitle.Text == "" || comboBox_Gender.SelectedIndex == -1 ||
                textBox_SingleTrackTitle.Text == "" || comboBox_SingleTrackGender.SelectedIndex == -1) {
                return true;
            } else {
                return false;
            }
        }

        private byte[] GetImageBytes(string filePath) {
            imageBytes = File.ReadAllBytes(filePath);
            return imageBytes;
        }

        private byte[] GetTrackBytes(string filePath) {
            return File.ReadAllBytes(filePath);
        }

        private void button_SelectFile_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            var resultado = ofd.ShowDialog();
            string path = "";
            if (resultado == true) {
                path = ofd.FileName;
                image_AlbumCover.Source = LoadImage(GetImageBytes(path));
                image_AlbumCover.Stretch = Stretch.Uniform;
            }
        }

        private void button_SingleSelectTrackFile_Click(object sender, RoutedEventArgs e) {
            try {
                OpenFileDialog ofd = new OpenFileDialog();
                var resultado = ofd.ShowDialog();
                string path = "";
                if (resultado == true) {
                    path = ofd.FileName;
                    var tfile = TagLib.File.Create(path);
                    fileBytes = GetTrackBytes(path);
                    textBox_SingleTrackTitle.Text = tfile.Tag.Title;
                    duration = Convert.ToInt32(tfile.Properties.Duration.TotalSeconds);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex + "in AddAlbum bSingleSelectTrackFile");
            }
        }

        private void button_AddSingle_Click(object sender, RoutedEventArgs e) {
            if (!ValidateBlankSpacesSingle()) {
                AddAlbumSingle();
                Window.GetWindow(this).Close();
            } else {
                label_Message.Text = "*Complete all fields";
            }
        }

        private async void AddAlbumSingle() {
            int n = random.Next();
            DateTime today = DateTime.Today;
            Album newAlbum = new Album();
            Date date = new Date();
            newAlbum.Title = textBox_AlbumTitle.Text;
            newAlbum.CoverPath = String.Concat(newAlbum.Title.ToString(), n);
            date.Day = Convert.ToInt16(today.Day);
            date.Month = Convert.ToInt16(today.Month);
            date.Year = Convert.ToInt16(today.Year);
            newAlbum.ReleaseDate = date;
            newAlbum.Gender = (MusicGender)Enum.Parse(typeof(MusicGender), comboBox_Gender.Text);
            newAlbum.IsSingle = true;
            short idNewAlbum = await (Session.serverConnection.albumService.AddAlbumAsync(newAlbum, Session.contentCreator.IdContentCreator));
            await Session.serverConnection.albumService.AddImageToMediaAsync(newAlbum.CoverPath, imageBytes);
            List<ContentCreator> contentCreators = await Session.serverConnection.contentCreatorService.GetContentCreatorsAsync();
            if (comboBox_Featuring.SelectedIndex != -1) {
                foreach (ContentCreator contentCreatorAux in contentCreators) {
                    if (comboBox_Featuring.SelectedItem.ToString() == contentCreatorAux.StageName) {
                        await Session.serverConnection.albumService.AddFeaturingAlbumAsync(idNewAlbum, contentCreatorAux.IdContentCreator);
                    }
                }
            }
            AddSingleTrack(idNewAlbum, contentCreators);
        }

        private async void AddSingleTrack(short idNewAlbum, List<ContentCreator> contentCreators) {
            int n = random.Next();
            string fileName = String.Concat(textBox_SingleTrackTitle.Text, n);
            Track newTrack = new Track();
            newTrack.TrackNumber = 1;
            newTrack.DurationSeconds = duration;
            newTrack.StoragePath = fileName;
            Console.WriteLine(newTrack.StoragePath);
            newTrack.Title = textBox_SingleTrackTitle.Text;
            newTrack.Gender = (MusicGender)Enum.Parse(typeof(MusicGender), comboBox_SingleTrackGender.Text);
            await Session.serverConnection.trackService.AddTrackToAlbumAsync(idNewAlbum, newTrack, Session.contentCreator.IdContentCreator);
            if (comboBox_SingleFeaturing.SelectedIndex != -1) {
                foreach (ContentCreator contentCreatorAux in contentCreators) {
                    if (comboBox_SingleFeaturing.SelectedItem.ToString() == contentCreatorAux.StageName) {
                        await Session.serverConnection.trackService.AddFeaturingTrackAsync(idNewAlbum, contentCreatorAux.IdContentCreator);
                    }
                }
            }
            TrackAudio newTrackAudio = new TrackAudio();
            newTrackAudio.Filename = fileName;
            newTrackAudio.Song = fileBytes;
            await Session.streamingServerConnection.streamingService.UploadTrackAsync(newTrackAudio);
        }
    }
}
