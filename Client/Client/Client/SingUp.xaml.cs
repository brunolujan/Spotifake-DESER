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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Lógica de interacción para SingUp.xaml
    /// </summary>
    public partial class SingUp : Window
    {
        public SingUp()
        {
            InitializeComponent();
            textBlock_StageName.Visibility = Visibility.Hidden;
            textBox_StageName.Visibility = Visibility.Hidden;
            button_SignUp.Visibility = Visibility.Hidden;
        }

        private void button_Logout_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }

        private void comboBox_UserType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button_SignUp.Visibility = Visibility.Visible;
            if (comboBox_UserType.SelectedIndex == 0)
            {
                textBlock_StageName.Visibility = Visibility.Hidden;
                textBox_StageName.Visibility = Visibility.Hidden;
            } else 
            {
                textBlock_StageName.Visibility = Visibility.Visible;
                textBox_StageName.Visibility = Visibility.Visible;
            }
        }

        private void button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            this.Close();
            loginWindow.Show();
        }
    }
}
