using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.IO.Packaging;
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
        
    public partial class SingUp
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

        private void button_Back_Click(object sender, RoutedEventArgs e)
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
                            ValidateConsumerEmail(textBox_Email.Text);
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
                            ValidateContentCreatorEmail(textBox_Email.Text);
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

        private async void SendConsumer()
        {
            try
            {
                newConsumer.GivenName = textBox_Name.Text;
                newConsumer.LastName = textBox_LastName.Text;
                newConsumer.Email = textBox_Email.Text;
                newConsumer.Password = passwordBox_Password.Password;
                newConsumer.ImageStoragePath = "DefaultCover";
                short idNewConsumer = await Session.serverConnection.consumerService.AddConsumerAsync(newConsumer);
                await Session.serverConnection.libraryService.AddLibraryAsync(idNewConsumer);
            } catch (Exception ex)
            {
                Console.WriteLine(ex + " in SingUp Send Consumer");
            }

        }

        private void SendContentCreator()
        {
            try
            {
                newContentCreator.GivenName = textBox_Name.Text;
                newContentCreator.LastName = textBox_LastName.Text;
                newContentCreator.StageName = textBox_StageName.Text;
                newContentCreator.Password = passwordBox_Password.Password;
                newContentCreator.Email = textBox_Email.Text;
                newContentCreator.Description = null;
                newContentCreator.ImageStoragePath = "DefaultCover";
                Session.serverConnection.contentCreatorService.AddContentCreatorAsync(newContentCreator);
            } catch (Exception ex)
            {
                Console.WriteLine(ex + " in SingUp SendContentCreator");
            }
        }

        private async void ValidateConsumerEmail(string email)
        {
            try
            {
                bool consumerAux = await Session.serverConnection.consumerService.GetConsumerByEmailAsync(textBox_Email.Text);
                if (consumerAux == true)
                {
                    textBlock_Message.Text = "*Email already exists in Spotifake";
                }
                else
                {
                    SendConsumer();
                    Login loginWindow = new Login();
                    this.Close();
                    loginWindow.Show();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex + " in SingUp ValidateConsumerEmail");
                textBlock_Message.Text = "*Service error, please close and try again";
            }
        }

        private async void ValidateContentCreatorEmail(string email)
        {
            try
            {
                bool consumerAux = await Session.serverConnection.contentCreatorService.GetContentCreatorByEmailAsync(textBox_Email.Text);
                if (consumerAux == true)
                {
                    textBlock_Message.Text = "*Email already exists in Spotifake";
                }
                else
                {
                    ValidateContentCreatorStageName(textBox_StageName.Text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " in SingUp ValidateContentCreatorEmail");
                textBlock_Message.Text = "*Service error, please close and try again";
            }
        }

        private async void ValidateContentCreatorStageName(string stageName)
        {
            try
            {
                bool contentCreatorAux = await Session.serverConnection.contentCreatorService.GetContentCreatorByStageNameAsync(textBox_StageName.Text);
                if (contentCreatorAux == true)
                {
                    textBlock_Message.Text = "*Stage name already exists in Spotifake";
                }
                else
                {
                    SendContentCreator();
                    Login loginWindow = new Login();
                    this.Close();
                    loginWindow.Show();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex + "in SingUn ValidateContentCreatorStageName");
                textBlock_Message.Text = "*Server error, please close and try again";
            }
        }
    }
}
