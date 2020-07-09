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

namespace Client
{

    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            TSocket transport = new TSocket("localhost", 5000);
            transport.Open();
            
            TBinaryProtocol protocol = new TBinaryProtocol(transport);
            
            TMultiplexedProtocol multiplexedProtocolConsumer = new TMultiplexedProtocol(protocol, "Consumer");
            ConsumerService .Client service = new Calculator.Client(mp);
            
            TMultiplexedProtocol multiplexedProtocolContentCreator = new TMultiplexedProtocol(protocol, "ContentCreator");
            WeatherReport.Client service2 = new WeatherReport.Client(mp2);
            
            System.out.println(service.add(2, 2));
            System.out.println(service2.getTemperature());
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
