﻿using Client.Pages;
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

namespace Client {
    /// <summary>
    /// Lógica de interacción para Main.xaml
    /// </summary>
    public partial class Main {
        public Main() {
            InitializeComponent();
            centralFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
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
    }
}
