using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public partial class AddAlbumPage : Page {

        List<Track> albumTracks;
        List<string> fileNames;
        List<byte[]> audioBytes;
        byte[] imageBytes;
        Random random;

        public AddAlbumPage() {
            InitializeComponent();
            LoadContent();
            albumTracks = new List<Track> { null, null, null, null, null };
            fileNames = new List<string> { null, null, null, null, null };
            audioBytes = new List<byte[]> { null, null, null, null, null };
            imageBytes = null;
            random = new Random();
        }

        private async void LoadContent() {
            image_AlbumCover.Source = LoadImage(await Session.serverConnection.albumService.GetImageToMediaAsync("DefaultAlbumCover"));
            image_AlbumCover.Stretch = Stretch.Uniform;

            foreach (int r in Enum.GetValues(typeof(MusicGender))) {
                var gender = new ListItem(Enum.GetName(typeof(MusicGender), r), r.ToString());
                comboBox_Gender.Items.Add(gender);
                comboBox_1stGender.Items.Add(gender);
                comboBox_2ndGender.Items.Add(gender);
                comboBox_3rdGender.Items.Add(gender);
                comboBox_4thGender.Items.Add(gender);
                comboBox_5thGender.Items.Add(gender);
            }

            List<ContentCreator> contentCreators = await Session.serverConnection.contentCreatorService.GetContentCreatorsAsync();
            foreach (ContentCreator contentCreatorAux in contentCreators) {
                comboBox_Featuring.Items.Add(contentCreatorAux.StageName);
            }
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

        private void button_AddAlbum_Click(object sender, RoutedEventArgs e) {
            if (!ValidateBlankSpacesAlbum()) {
                AddAlbum();
                Window.GetWindow(this).Close();
            } else {
                label_Message.Text = "*Complete all fields";
            }
        }

        private byte[] GetImageBytes(string filePath) {
            imageBytes = File.ReadAllBytes(filePath);
            return imageBytes;
        }

        private byte[] GetTrackBytes(string filePath) {
            return File.ReadAllBytes(filePath);
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

        private bool ValidateBlankSpacesAlbum() {
            if (textBox_AlbumTitle.Text == "" || comboBox_Gender.SelectedIndex == -1 || textBox_1stTrack.Text == "" ||
                textBox_2ndTrack.Text == "" || textBox_3rdTrack.Text == "" || textBox_4thTrack.Text == "" || comboBox_1stGender.SelectedIndex == -1 ||
                comboBox_2ndGender.SelectedIndex == -1 || comboBox_3rdGender.SelectedIndex == -1 || comboBox_4thGender.SelectedIndex == -1) {
                return true;
            } else {
                return false;
            }
        }

        private async void AddAlbum() {
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
            newAlbum.IsSingle = false;
            short idNewAlbum = await (Session.serverConnection.albumService.AddAlbumAsync(newAlbum, Session.contentCreator.IdContentCreator));
            await Session.serverConnection.albumService.AddImageToMediaAsync(newAlbum.CoverPath, imageBytes);
            if (comboBox_Featuring.SelectedIndex != -1) {
                List<ContentCreator> contentCreators = await Session.serverConnection.contentCreatorService.GetContentCreatorsAsync();
                foreach (ContentCreator contentCreatorAux in contentCreators) {
                    if (comboBox_Featuring.SelectedItem.ToString() == contentCreatorAux.StageName) {
                        await Session.serverConnection.albumService.AddFeaturingAlbumAsync(idNewAlbum, contentCreatorAux.IdContentCreator);
                    }
                }
            }
            AddAlbumTrack(idNewAlbum, albumTracks);
        }

        private async void AddAlbumTrack(short idNewAlbum, List<Track> albumTracks) {
            for (int n = 0; n <= 4; n++) {
                TrackAudio newTrackAudio = new TrackAudio();
                await Session.serverConnection.trackService.AddTrackToAlbumAsync(idNewAlbum, albumTracks[n], Session.contentCreator.IdContentCreator);
                newTrackAudio.Filename = albumTracks[n].StoragePath;
                newTrackAudio.Song = audioBytes[n];
                await Session.streamingServerConnection.streamingService.UploadTrackAsync(newTrackAudio);

            }
        }

        private void button_1stSelectFile_Click(object sender, RoutedEventArgs e) {
            if (comboBox_1stGender.SelectedIndex != -1) {
                try {
                    int n = random.Next();
                    Track newTrack = new Track();
                    OpenFileDialog ofd = new OpenFileDialog();
                    var resultado = ofd.ShowDialog();
                    string path = "";
                    if (resultado == true) {
                        path = ofd.FileName;
                        var tfile = TagLib.File.Create(path);
                        textBox_1stTrack.Text = tfile.Tag.Title;
                        string fileName = String.Concat(textBox_1stTrack.Text, n);
                        newTrack.IdTrack = 1;
                        newTrack.TrackNumber = 1;
                        newTrack.DurationSeconds = Convert.ToInt32(tfile.Properties.Duration.TotalSeconds); ;
                        newTrack.StoragePath = fileName;
                        fileNames[0] = fileName;
                        newTrack.Title = textBox_1stTrack.Text;
                        newTrack.Gender = (MusicGender)Enum.Parse(typeof(MusicGender), comboBox_1stGender.Text);
                        albumTracks[0] = newTrack;
                        audioBytes[0] = GetTrackBytes(path);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex + "in AddAlbum SingleSelectTrackFile");
                }
            } else {
                label_Message.Text = "*First Select gender";
            }
        }

        private void button_2ndSelectFile_Click(object sender, RoutedEventArgs e) {
            if (comboBox_2ndGender.SelectedIndex != -1) {
                try {
                    int n = random.Next();
                    Track newTrack = new Track();
                    OpenFileDialog ofd = new OpenFileDialog();
                    var resultado = ofd.ShowDialog();
                    string path = "";
                    if (resultado == true) {
                        path = ofd.FileName;
                        var tfile = TagLib.File.Create(path);
                        textBox_2ndTrack.Text = tfile.Tag.Title;
                        string fileName = String.Concat(textBox_2ndTrack.Text, n);
                        newTrack.IdTrack = 1;
                        newTrack.TrackNumber = 2;
                        newTrack.DurationSeconds = Convert.ToInt32(tfile.Properties.Duration.TotalSeconds); ;
                        newTrack.StoragePath = fileName;
                        fileNames[1] = fileName;
                        newTrack.Title = textBox_2ndTrack.Text;
                        newTrack.Gender = (MusicGender)Enum.Parse(typeof(MusicGender), comboBox_2ndGender.Text);
                        albumTracks[1] = newTrack;
                        audioBytes[1] = GetTrackBytes(path);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex + "in AddAlbum SingleSelectTrackFile");
                }
            } else {
                label_Message.Text = "*First Select gender";
            }
        }

        private void button_3rdSelectFile_Click(object sender, RoutedEventArgs e) {
            if (comboBox_3rdGender.SelectedIndex != -1) {
                try {
                    int n = random.Next();
                    Track newTrack = new Track();
                    OpenFileDialog ofd = new OpenFileDialog();
                    var resultado = ofd.ShowDialog();
                    string path = "";
                    if (resultado == true) {
                        path = ofd.FileName;
                        var tfile = TagLib.File.Create(path);
                        textBox_3rdTrack.Text = tfile.Tag.Title;
                        string fileName = String.Concat(textBox_3rdTrack.Text, n);
                        newTrack.IdTrack = 1;
                        newTrack.TrackNumber = 3;
                        newTrack.DurationSeconds = Convert.ToInt32(tfile.Properties.Duration.TotalSeconds); ;
                        newTrack.StoragePath = fileName;
                        fileNames[2] = fileName;
                        newTrack.Title = textBox_3rdTrack.Text;
                        newTrack.Gender = (MusicGender)Enum.Parse(typeof(MusicGender), comboBox_3rdGender.Text);
                        albumTracks[2] = newTrack;
                        audioBytes[2] = GetTrackBytes(path);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex + "in AddAlbum SingleSelectTrackFile");
                }
            } else {
                label_Message.Text = "*First Select gender";
            }
        }

        private void button_4thSelectFile_Click(object sender, RoutedEventArgs e) {
            if (comboBox_4thGender.SelectedIndex != -1) {
                try {
                    int n = random.Next();
                    Track newTrack = new Track();
                    OpenFileDialog ofd = new OpenFileDialog();
                    var resultado = ofd.ShowDialog();
                    string path = "";
                    if (resultado == true) {
                        path = ofd.FileName;
                        var tfile = TagLib.File.Create(path);
                        textBox_4thTrack.Text = tfile.Tag.Title;
                        string fileName = String.Concat(textBox_4thTrack.Text, n);
                        newTrack.IdTrack = 1;
                        newTrack.TrackNumber = 4;
                        newTrack.DurationSeconds = Convert.ToInt32(tfile.Properties.Duration.TotalSeconds); ;
                        newTrack.StoragePath = fileName;
                        fileNames[3] = fileName;
                        newTrack.Title = textBox_4thTrack.Text;
                        newTrack.Gender = (MusicGender)Enum.Parse(typeof(MusicGender), comboBox_4thGender.Text);
                        albumTracks[3] = newTrack;
                        audioBytes[3] = GetTrackBytes(path);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex + "in AddAlbum SingleSelectTrackFile");
                }
            } else {
                label_Message.Text = "*First Select gender";
            }
        }

        private void button_5thSelectFile_Click(object sender, RoutedEventArgs e) {
            if (comboBox_5thGender.SelectedIndex != -1) {
                try {
                    int n = random.Next();
                    Track newTrack = new Track();
                    OpenFileDialog ofd = new OpenFileDialog();
                    var resultado = ofd.ShowDialog();
                    string path = "";
                    if (resultado == true) {
                        path = ofd.FileName;
                        var tfile = TagLib.File.Create(path);
                        textBox_5thTrack.Text = tfile.Tag.Title;
                        string fileName = String.Concat(textBox_5thTrack.Text, n);
                        newTrack.IdTrack = 1;
                        newTrack.TrackNumber = 5;
                        newTrack.DurationSeconds = Convert.ToInt32(tfile.Properties.Duration.TotalSeconds); ;
                        newTrack.StoragePath = fileName;
                        fileNames[4] = fileName;
                        newTrack.Title = textBox_5thTrack.Text;
                        newTrack.Gender = (MusicGender)Enum.Parse(typeof(MusicGender), comboBox_5thGender.Text);
                        albumTracks[4] = newTrack;
                        audioBytes[4] = GetTrackBytes(path);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex + "in AddAlbum SingleSelectTrackFile");
                }
            } else {
                label_Message.Text = "*First Select gender";
            }
        }
    }
}
