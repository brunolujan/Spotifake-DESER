﻿using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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
        int thisVar;

        public AddAlbum(ContentCreator contentCreator, int var) {
            thisContentCreator = contentCreator;
            thisVar = var;
            InitializeComponent();           
            image.Fill = LoadImage("C:\\Users\\Bruno\\Documents\\BRUNO\\oregon-coast-3840x2400-sunset-beach-purple-sky-4k-17946.jpg");
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

        private void button_Search_Click(object sender, RoutedEventArgs e) {

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

        private void LoadNewSingle() {
            button_AddAlbum.Content = "ADD SINGLE";
            label_Title.Text = "NEW SINGLE";
            label_TrackMessage.Text = "TRACK";
            label_SingleGender.Visibility = Visibility.Visible;
            textBox_SingleTrackTitle.Visibility = Visibility.Visible;
            label_SingleTitle.Visibility = Visibility.Visible;
            comboBox_SingleTrackGender.Visibility = Visibility.Visible;
            button_SingleSelectTrackFile.Visibility = Visibility.Visible;
            foreach (int r in Enum.GetValues(typeof(MusicGender))) {
                var items = new ListItem(Enum.GetName(typeof(MusicGender), r), r.ToString());
                comboBox_Gender.Items.Add(items);
                comboBox_SingleTrackGender.Items.Add(items);
            }
        }

        private void LoadNewAlbum() {
            button_AddAlbum.Content = "ADD ALBUM";
            label_Title.Text = "NEW ALBUM";
            label_TrackMessage.Text = "TRACK LIST";
        }

        private void button_SelectFile_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            var resultado = ofd.ShowDialog ();
            string path = "";
            if (resultado == true) {
                path = ofd.FileName;
                image_albumCover.Fill = LoadImage(path);
                image_albumCover.Stretch = Stretch.Uniform;
            }
        }

        private void button_AddAlbum_Click(object sender, RoutedEventArgs e) {
            if (thisVar == 0) {

            } else {
                if (ValidateBlankSpacesSingle() == true) {
                    label_Message.Text = "*Complete all fields";
                } else {

                }
            }
        }

        private void button_SelectTrackFile_Click(object sender, RoutedEventArgs e) {

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
                    int duration = Convert.ToInt32(tfile.Properties.Duration.TotalSeconds);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex + "in AddAlbum bSingleSelectTrackFile");
            }
        }

        private bool ValidateBlankSpacesSingle() {
            if (textBox_AlbumTitle.Text == "" || comboBox_Featuring.SelectedIndex == -1 || comboBox_Gender.SelectedIndex == -1 || 
                textBox_SingleTrackTitle.Text == "" || comboBox_SingleFeaturing.SelectedIndex == -1 || comboBox_SingleTrackGender.SelectedIndex == -1) {
                return true;
            } else {
                return false;
            }
        }
    }
}
