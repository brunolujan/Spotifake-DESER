using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
        
    public partial class SingUp : Window
    {

        Consumer newConsumer = null;
        ContentCreator newContentCreator = null;

        public SingUp()
        {
            InitializeComponent();
            newConsumer = new Consumer();
            newContentCreator = new ContentCreator();
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
                textBox_StageName.Text = "";
            }
        }

        private void button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEmail(textBox_Email.Text))
            {

                if (comboBox_UserType.SelectedIndex == 0)
                {
                    if (ValidateBlankSpacesConsumer(textBox_Name.Text,textBox_LastName.Text, textBox_Email.Text, textBox_ConfirmEmail.Text, passwordBox_Password.Password))
                    { 
                        if (textBox_Email.Text == textBox_ConfirmEmail.Text)
                        {
                            bool aux = SendConsumer();
                            if (aux)
                            {
                                Login loginWindow = new Login();
                                this.Close();
                                loginWindow.Show();
                            }
                            else
                            {
                                textBlock_Message.Text = "*Service error, please close and try again";
                            }
                        } else
                        {
                            textBlock_Message.Text = "*Email have to be the same";
                        }
                    } else
                    {
                        textBlock_Message.Text = "*Complete all fields";
                    }
                } else
                {
                    if (ValidateBlankSpacesContentCreator(textBox_Name.Text, textBox_LastName.Text, textBox_Email.Text, textBox_ConfirmEmail.Text, passwordBox_Password.Password, textBox_StageName.Text))
                    {
                        if (textBox_Email.Text == textBox_ConfirmEmail.Text)
                        {
                            bool aux = SendContentCreator();
                            if (aux)
                            {
                                Login loginWindow = new Login();
                                this.Close();
                                loginWindow.Show();
                            }
                            else
                            {
                                textBlock_Message.Text = "*Service error, please close and try again";
                            }
                        } else
                        {
                            textBlock_Message.Text = "*Email have to be the same";
                        }
                    }
                    else
                    {
                        textBlock_Message.Text = "*Complete all fields";
                    }
                }
            } else
            {
                textBlock_Message.Text = "*Use a correct email";
            }
        }

        private bool ValidateEmail(String email)
        {
            String expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool ValidateBlankSpacesConsumer(String name, String lastName, String email, String emailConfirmed, String password)
        {
            if (name == "" || lastName == "" || email == "" || emailConfirmed == "" ||
                password == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateBlankSpacesContentCreator(String name, String lastName, String email, String emailConfirmed, String password, String stageName)
        {
            if (name == "" || lastName == "" || email == "" || emailConfirmed == "" || password == "" || stageName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool SendConsumer()
        {
            try
            {
                newConsumer.GivenName = textBox_Name.Text;
                newConsumer.LastName = textBox_LastName.Text;
                newConsumer.Email = textBox_Email.Text;
                newConsumer.Password = passwordBox_Password.Password;
                newConsumer.ImageStoragePath = null;
                Session.serverConnection.consumerService.AddConsumerAsync(newConsumer);
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

        private bool SendContentCreator()
        {
            try
            {
                newContentCreator.GivenName = textBox_Name.Text;
                newContentCreator.LastName = textBox_LastName.Text;
                newContentCreator.StageName = textBlock_StageName.Text;
                newContentCreator.Password = passwordBox_Password.Password;
                newContentCreator.Email = textBox_Email.Text;
                newContentCreator.Description = null;
                newContentCreator.ImageStoragePath = null;
                Session.serverConnection.contentCreatorService.AddContentCreatorAsync(newContentCreator);
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
