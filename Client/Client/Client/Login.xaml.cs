using MahApps.Metro.Controls.Dialogs;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace Client
{
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_UserType.SelectedIndex == -1)
            {
                textBlock_Message.Text = "*Select user type";
            } else
            {
                if (comboBox_UserType.SelectedIndex == 0)
                {
                    LoginConsumer();
                }
                else
                {
                    LoginContentCreator();
                }
            }
        }

        private void button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            SingUp singUpWindow = new SingUp();
            this.Close();
            singUpWindow.Show();
        }

        public async void LoginConsumer()
        {
            try
            {
                if (textBox_Email.Text != "" && passwordBox_Password.Password != "")
                {
                    try
                    {
                        Consumer ConsumerLog = await Session.serverConnection.consumerService.LoginConsumerAsync(textBox_Email.Text, passwordBox_Password.Password);
                        if (ConsumerLog != null)
                        {
                            short idLibrary = await Session.serverConnection.libraryService.getLibraryByIdConsumerAsync(ConsumerLog.IdConsumer);
                            MainWindow mainWindow = new MainWindow(ConsumerLog, idLibrary);
                            mainWindow.Show();
                            this.Close();
                        }
                    } catch (NullReferenceException ex)
                    {
                        textBlock_Message.Text = "*Service error, please close and try again";
                        Console.WriteLine(ex + " in Login loginConsumer");
                    }
                }
                else
                {
                    textBlock_Message.Text = "*Complete all fields";
                }
            }
            catch (Exception ex)
            {
                textBlock_Message.Text = "*Email or password are wrong";
                Console.WriteLine(ex + " in Login LoginConsumer");
            }
        }

        public async void LoginContentCreator()
        {
            try
            {
                if (textBox_Email.Text != "" && passwordBox_Password.Password != "")
                {
                    try
                    {
                        ContentCreator ContentCreatorLog = await Session.serverConnection.contentCreatorService.LoginContentCreatorAsync(textBox_Email.Text, passwordBox_Password.Password);
                        if (ContentCreatorLog != null)
                        {
                            MainWindowContentCreator mainWindowCC = new MainWindowContentCreator(ContentCreatorLog, 0);
                            mainWindowCC.Show();
                            this.Close();
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        textBlock_Message.Text = "*Service error, please close and try again";
                        Console.WriteLine(ex + " in Login LoginContentCreator");
                    }
                }
                else
                {
                    textBlock_Message.Text = "*Complete all fields";
                }
            }
            catch (Exception ex)
            {
                textBlock_Message.Text = "*Email or password are wrong";
                Console.WriteLine(ex + " in Login LoginContentCreator");
            }
        }
    }
}
