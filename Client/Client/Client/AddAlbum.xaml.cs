using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.ComboBox;
using Image = System.Windows.Controls.Image;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace Client {

    public partial class AddAlbum {

        ContentCreator thisContentCreator;
        short idNewAlbum;
        int thisVar;
        int duration;
        string path;
        List<Track> albumTracks;
        List<string> fileNames;
        List<byte[]> audioBytes;
        Random random;

        public AddAlbum(ContentCreator contentCreator, int var) {
            thisContentCreator = contentCreator;
            idNewAlbum = -1;
            thisVar = var;
            duration = -1;
            path = "";
            albumTracks = new List<Track> { null, null, null, null, null };
            fileNames = new List<string> { null, null, null, null, null };
            audioBytes = new List<byte[]> { null, null, null, null, null };
            random = new Random();
            InitializeComponent();
            if (contentCreator.ImageStoragePath == null) {
                image.Fill = LoadImage("C:\\Users\\Bruno\\Desktop\\IMAGES\\DefaultCover.jpg");
            } else {
                image.Fill = LoadImage(contentCreator.ImageStoragePath);
            }
            textBlock_StageName.Text = thisContentCreator.StageName;
            if (var == 0) {
                LoadNewAlbum();
            } else {
                LoadNewSingle();
            }
        }

        private void button_Albums_Click(object sender, RoutedEventArgs e) {
            MainWindowContentCreator mainWindowContentCreator = new MainWindowContentCreator(thisContentCreator, 0);
            mainWindowContentCreator.Show();
            this.Close();
        }

        private void button_Singles_Click(object sender, RoutedEventArgs e) {
            MainWindowContentCreator mainWindowContentCreator = new MainWindowContentCreator(thisContentCreator, 1);
            mainWindowContentCreator.Show();
            this.Close();
        }

        private void button_Settings_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = true;
        }

        private void button_Back_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = false;
        }

        private void button_Configuration_Click(object sender, RoutedEventArgs e) {

        }

        private void button_Logout_Click(object sender, RoutedEventArgs e) {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }

        private ImageBrush LoadImage(string path) {
            try {
                Image imageX = new Image();
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(path);
                src.EndInit();
                imageX.Source = src;

                ImageBrush ib = new ImageBrush();
                ib.ImageSource = src;
                return ib;
            } catch (Exception ex) {
                Console.WriteLine(ex + " in AddAlbum LoadImage");
                return null;
            }
        }

        private async void LoadNewSingle() {
            button_AddAlbum.Content = "ADD SINGLE";
            label_Title.Text = "NEW SINGLE";
            label_TrackMessage.Text = "TRACK";
            label_SingleGender.Visibility = Visibility.Visible;
            comboBox_SingleTrackGender.Visibility = Visibility.Visible;
            textBox_SingleTrackTitle.Visibility = Visibility.Visible;
            label_SingleTitle.Visibility = Visibility.Visible;
            button_SingleSelectTrackFile.Visibility = Visibility.Visible;
            label_SingleFeaturing.Visibility = Visibility.Visible;
            comboBox_SingleFeaturing.Visibility = Visibility.Visible;
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

        private async void LoadNewAlbum() {
            button_AddAlbum.Content = "ADD ALBUM";
            label_Title.Text = "NEW ALBUM";
            label_TrackMessage.Text = "TRACK LIST";
            label_1stTrack.Visibility = Visibility.Visible;
            label_2ndTrack.Visibility = Visibility.Visible;
            label_3rdTrack.Visibility = Visibility.Visible;
            label_4thTrack.Visibility = Visibility.Visible;
            label_5thTrack.Visibility = Visibility.Visible;
            textBox_1stTrack.Visibility = Visibility.Visible;
            textBox_2ndTrack.Visibility = Visibility.Visible;
            textBox_3rdTrack.Visibility = Visibility.Visible;
            textBox_4thTrack.Visibility = Visibility.Visible;
            textBox_5thTrack.Visibility = Visibility.Visible;
            button_1stSelectFile.Visibility = Visibility.Visible;
            button_2ndSelectFile.Visibility = Visibility.Visible;
            button_3rdSelectFile.Visibility = Visibility.Visible;
            button_4thSelectFile.Visibility = Visibility.Visible;
            button_5thSelectFile.Visibility = Visibility.Visible;
            comboBox_1stGender.Visibility = Visibility.Visible;
            comboBox_2ndGender.Visibility = Visibility.Visible;
            comboBox_3rdGender.Visibility = Visibility.Visible;
            comboBox_4thGender.Visibility = Visibility.Visible;
            comboBox_5thGender.Visibility = Visibility.Visible;
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
            path = "";
            if (resultado == true) {
                path = ofd.FileName;
                image_albumCover.Fill = LoadImage(path);
                image_albumCover.Stretch = Stretch.Uniform;
                textBox_SelectedFilePath.Text = path;
            }
        }

        private void button_AddAlbum_Click(object sender, RoutedEventArgs e) {
            if (thisVar == 0) {
                if (!ValidateBlankSpacesAlbum()) {
                    AddAlbumNotSingle();
                    MainWindowContentCreator mainWindowContentCreator = new MainWindowContentCreator(thisContentCreator, 0);
                    mainWindowContentCreator.Show();
                    this.Close();
                } else {
                    label_Message.Text = "*Complete all fields";
                }
            } else {
                if (!ValidateBlankSpacesSingle()) {
                    AddAlbumSingle();
                    MainWindowContentCreator mainWindowContentCreator = new MainWindowContentCreator(thisContentCreator, 0);
                    mainWindowContentCreator.Show();
                    this.Close();
                } else {
                    label_Message.Text = "*Complete all fields";
                }
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
                    textBox_SingleTrackTitle.Text = tfile.Tag.Title;
                    duration = Convert.ToInt32(tfile.Properties.Duration.TotalSeconds);
                    textBox_SingleSelectTrackFile.Text = path;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex + "in AddAlbum bSingleSelectTrackFile");
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

        private bool ValidateBlankSpacesAlbum() {
            if (textBox_AlbumTitle.Text == "" || comboBox_Gender.SelectedIndex == -1 ||  textBox_1stTrack.Text == "" || 
                textBox_2ndTrack.Text == "" || textBox_3rdTrack.Text == "" || textBox_4thTrack.Text == "" || comboBox_1stGender.SelectedIndex == -1 || 
                comboBox_2ndGender.SelectedIndex == -1 || comboBox_3rdGender.SelectedIndex == -1 || comboBox_4thGender.SelectedIndex == -1) {
                return true;
            } else {
                return false;
            }
        }

        private async void AddAlbumSingle() {
            DateTime today = DateTime.Today;
            Album newAlbum = new Album();
            Date date = new Date();
            newAlbum.Title = textBox_AlbumTitle.Text;
            newAlbum.CoverPath = textBox_SelectedFilePath.Text;
            date.Day = Convert.ToInt16(today.Day);
            date.Month = Convert.ToInt16(today.Month);
            date.Year = Convert.ToInt16(today.Year);
            newAlbum.ReleaseDate = date;
            newAlbum.Gender = (MusicGender)Enum.Parse(typeof(MusicGender), comboBox_Gender.Text);
            newAlbum.IsSingle = true;
            idNewAlbum = await (Session.serverConnection.albumService.AddAlbumAsync(newAlbum, thisContentCreator.IdContentCreator));
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

        private byte[] GetTrackBytes(string filePath) {
            return File.ReadAllBytes(filePath);
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
            await Session.serverConnection.trackService.AddTrackToAlbumAsync(idNewAlbum,newTrack, thisContentCreator.IdContentCreator);
            if (comboBox_SingleFeaturing.SelectedIndex != -1) {
                foreach (ContentCreator contentCreatorAux in contentCreators) {
                    if (comboBox_SingleFeaturing.SelectedItem.ToString() == contentCreatorAux.StageName) {
                        await Session.serverConnection.trackService.AddFeaturingTrackAsync(idNewAlbum, contentCreatorAux.IdContentCreator);
                    }
                }
            }
            await Session.serverConnection.trackService.AddTrackToMediaAsync(newTrack.StoragePath, GetTrackBytes(textBox_SingleSelectTrackFile.Text));
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

        private async void AddAlbumNotSingle() {
            DateTime today = DateTime.Today;
            Album newAlbum = new Album();
            Date date = new Date();
            newAlbum.Title = textBox_AlbumTitle.Text;
            newAlbum.CoverPath = textBox_SelectedFilePath.Text;
            date.Day = Convert.ToInt16(today.Day);
            date.Month = Convert.ToInt16(today.Month);
            date.Year = Convert.ToInt16(today.Year);
            newAlbum.ReleaseDate = date;
            newAlbum.Gender = (MusicGender)Enum.Parse(typeof(MusicGender), comboBox_Gender.Text);
            newAlbum.IsSingle = false;
            idNewAlbum = await (Session.serverConnection.albumService.AddAlbumAsync(newAlbum, thisContentCreator.IdContentCreator));
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
                await Session.serverConnection.trackService.AddTrackToAlbumAsync(idNewAlbum, albumTracks[n], thisContentCreator.IdContentCreator);
                await Session.serverConnection.trackService.AddTrackToMediaAsync(fileNames[n], audioBytes[n]);
            }
        }
    }
}
