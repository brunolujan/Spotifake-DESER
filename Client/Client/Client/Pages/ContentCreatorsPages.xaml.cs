﻿using System;
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

    public partial class ContentCreatorsPages : Page {

        public ContentCreatorsPages() {
            InitializeComponent();
            LoadContentCreators();
        }

        public async void LoadContentCreators() {
            List<ContentCreator> contentCreators = await Session.serverConnection.contentCreatorService.GetContentCreatorByLibraryIdAsync(Session.library.IdLibrary);
            datagrid_ContentCreator.ItemsSource = contentCreators;
        }

        private void datagrid_ContentCreator_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var contentCreatorAux = (ContentCreator)datagrid_ContentCreator.SelectedItem;
            if (contentCreatorAux != null)
            {
                NavigationService.Navigate(new AlbumsContentCreatorPage(contentCreatorAux));
            }
        }
    }
}
