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
        public Login()
        {
            InitializeComponent();

            TTransport transport = new TSocketTransport("localhost", 5000);

            TBinaryProtocol protocol = new TBinaryProtocol(transport);

            TMultiplexedProtocol multiplexedProtocolConsumer = new TMultiplexedProtocol(protocol, "Consumer");
            ConsumerService.Client consumerService = new ConsumerService.Client(multiplexedProtocolConsumer);

            TMultiplexedProtocol multiplexedProtocolContentCreator = new TMultiplexedProtocol(protocol, "ContentCreator");
            ContentCreatorService.Client contentCreatorService = new ContentCreatorService.Client(multiplexedProtocolContentCreator);
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            SingUp singUpWindow = new SingUp();
            this.Hide();
            singUpWindow.Show();
        }

        private void textBox_Email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
