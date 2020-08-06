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

namespace Client.Pages {
    /// <summary>
    /// Lógica de interacción para UploadLocalTrackPage.xaml
    /// </summary>
    public partial class UploadLocalTrackPage : Page {

        private string filePath;
        private Random random;
        public UploadLocalTrackPage() {
            random = new Random();
            InitializeComponent();
        }

        public async Task<bool> AddLocalTrack() {
            
            LocalTrack localTrack = new LocalTrack()
            {
                IdConsumer = Session.consumer.IdConsumer,
                ArtistName = TextBox_ArtistNameLocalTrack.Text,
                Title = TextBox_TitleLocalTrack.Text,
                FileName = GenerateFileName()
            };

            await Session.serverConnection.trackService.AddLocalTrackAsync(localTrack);
            TrackAudio trackAudio = new TrackAudio()
            {
                Filename = localTrack.FileName,
                Song = GetTrackBytes()
            };

            return await Session.streamingServerConnection.streamingService.UploadTrackAsync(trackAudio);
        }

        private byte[] GetTrackBytes() {
            return File.ReadAllBytes(filePath);
        }

        private string GenerateFileName()
        {
            return String.Concat(TextBox_TitleLocalTrack.Text + random.Next());
        }

        private async void Button_AddLocalTrack_Click(object sender, RoutedEventArgs e) {
            if (AreEmptyFields())
            {
                MessageBox.Show("Are empty fields");
            }
            else if (!IsFileSelected())
            {
                MessageBox.Show("Please select a track");
            }
            else if( await AddLocalTrack())
            {
                MessageBox.Show("Local track added successfully");
                Window.GetWindow(this).Close();
            }
        }

        private bool AreEmptyFields() {
            bool areEmpty = false;

            if (String.IsNullOrEmpty(TextBox_TitleLocalTrack.Text))
            {
                areEmpty = true;
            }
            else if(String.IsNullOrEmpty(TextBox_ArtistNameLocalTrack.Text))
            {
                areEmpty = true;
            }
            return areEmpty;
        }

        private bool IsFileSelected() {
            bool isSelected = true;
            if (String.IsNullOrEmpty(filePath))
            {
                isSelected = false;
            }

            return isSelected;
        }

        private void Button_upload_file_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Mp3 Files | *.mp3";
            if (openFileDialog.ShowDialog() == true)
            {
                TextBlock_LocalTrackFileName.Text = openFileDialog.SafeFileName;
                filePath = openFileDialog.FileName;
            }
        }
    }
}
