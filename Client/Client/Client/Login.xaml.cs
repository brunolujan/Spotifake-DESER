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
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace Client
{

    public partial class Login : Window
    {
        ConsumerService.Client consumerService;
        ContentCreatorService.Client contentCreatorService;

        public Login()
        {
            
            TTransport transport = new TSocketTransport("localhost", 5000);

            TBinaryProtocol protocol = new TBinaryProtocol(transport);

            TMultiplexedProtocol multiplexedProtocolConsumer = new TMultiplexedProtocol(protocol, "ConsumerService");
            consumerService = new ConsumerService.Client(multiplexedProtocolConsumer);

            TMultiplexedProtocol multiplexedProtocolContentCreator = new TMultiplexedProtocol(protocol, "ContentCreatorService");
            contentCreatorService = new ContentCreatorService.Client(multiplexedProtocolContentCreator);

            InitializeComponent();
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            LoginConsumer();
        }

        private void button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            SingUp singUpWindow = new SingUp();
            this.Hide();
            singUpWindow.Show();
        }

        private async void LoginConsumer()
        {
            try
            {
                if (textBox_Email.Text != "" && passwordBox_Password.Password != "")
                {
                    Consumer ConsumerLog = await consumerService.LoginConsumerAsync(textBox_Email.Text, passwordBox_Password.Password);
                    if (ConsumerLog != null)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                }
                else
                {
                    textBlock_Message.Text = "Complete all fields";
                }
            }
            catch (Exception ex)
            {
                textBlock_Message.Text = "Email or password is wrong";
            }
        }
    }
}
