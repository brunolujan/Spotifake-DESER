using Client.Pages;
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
using System.Windows.Threading;

namespace Client {
    
    public partial class Main {

        DispatcherTimer loadProgressTrackTimer;

        public Main() {
            InitializeComponent();
            loadProgressTrackTimer = new DispatcherTimer();
            loadProgressTrackTimer.Tick += new EventHandler(PrintProgress);
            loadProgressTrackTimer.Interval = new TimeSpan(0, 0, 0, 1);
            centralFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            StreamingPlayer.Initialize();
            LoadImageBytes();
            textBlock_NameUser.Text = "Hi, " + Session.consumer.GivenName;
        }

        private async void LoadImageBytes() {
            image_Consumer.Source = LoadImage(await Session.serverConnection.consumerService.GetImageToMediaAsync(Session.consumer.ImageStoragePath));
            image_Consumer.Stretch = Stretch.Uniform;
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

        private void button_Settings_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = true;
        }

        private void button_Logout_Click(object sender, RoutedEventArgs e) {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }

        private void button_Configuration_Click(object sender, RoutedEventArgs e) {
            PopUpWindow popUpWindow = new PopUpWindow(new ConfigurationConsumerPage());
            flyout.IsOpen = false;
            popUpWindow.ShowDialog();
        }

        private void button_Back_Click(object sender, RoutedEventArgs e) {
            flyout.IsOpen = false;
        }

        private void button_Tracks_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new TracksPages());
        }

        private void button_ContentCreators_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new ContentCreatorsPages());
        }

        private void button_Albums_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new AlbumsPages());
        }

        private void button_Playlists_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new PlaylistsPages());
        }
        private void button_Search_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new SearchPage());
        }


        private void button_NewPlaylist_Click(object sender, RoutedEventArgs e) {
            PopUpWindow popUpWindow = new PopUpWindow(new AddPlaylistPage());
            popUpWindow.ShowDialog();
        }

        private void button_MyOwnTracks_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            centralFrame.Navigate(new MyOwnTracksPage());
        }

        private void PrintProgress(object sender, EventArgs e) {
            TimeSpan timeSpan = TimeSpan.FromSeconds(StreamingPlayer.GetTotalSecondsTrack());
            TextBlock_final_duration.Text = string.Format("{0}:{1}", timeSpan.Duration().Minutes, timeSpan.Duration().Seconds);
            var time = StreamingPlayer.GetCurretTimeSeconds();
            TimeSpan timeInitial = TimeSpan.FromSeconds(time);
            TextBlock_initial_duration.Text = timeInitial.ToString(@"mm\:ss");
            Slider_track_duration.Value = StreamingPlayer.GetCurretTimeForSlider();
            if (StreamingPlayer.IsTrackOver()) {
                StopTrack();
                GoToNextTrack();
            }
        }

        private void StartTrack() {
            if (StreamingPlayer.StartPlayer()) {
                icon_playPause_button.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "Pause");
                loadProgressTrackTimer.Start();
            }
        }

        private void StopTrack() {
            if (StreamingPlayer.StopPlayer()) {
                icon_playPause_button.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "Play");
                loadProgressTrackTimer.Stop();
            }
        }

        public async void GoToNextTrack() {
            Track track = await StreamingPlayer.UploadNextTrack();
            if (track != null) {
                UpdateInfoPlayer(track);
                StartTrack();
            }
        }

        public async void UpdateInfoPlayer(Track track) {
            textBlock_TrackName.Text = track.Title;
            textBlock_ContentCreatorName.Text = ""; //MODIFICAR
            StartTrack();
            image_TrackStreaming.Source = null; //MODIFICAR
        }

        public void Button_track_previous_Click(object sender, RoutedEventArgs e) {
            StreamingPlayer.RestartTrack();
        }

        public void Button_track_playPause_Click(object sender, RoutedEventArgs e) {
            try {
                if (StreamingPlayer.IsTrackPlaying()) {
                    StopTrack();
                } else {
                    StartTrack();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        public void Button_track_next_Click(object sender, RoutedEventArgs e) {
            GoToNextTrack();
        }

        private void Slider_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            StreamingPlayer.UpdateVolume(Slider_volume.Value);
        }

        private void Slider_track_duration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            StreamingPlayer.UpdatePositionTrack(Slider_track_duration.Value);
        }

        private void Buttom_view_queue_Click(object sender, RoutedEventArgs e) {
            centralFrame.Navigate(new ViewQueue());
        }
    }
}
