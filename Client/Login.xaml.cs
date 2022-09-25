/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: the login window
 */
using Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private AuthenticateInterface authenticator;
        private string user, pass;
        private int token;
        public Login()
        {
            InitializeComponent();

            ChannelFactory<AuthenticateInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string authURL = "net.tcp://localhost:8100/AuthenticationService";
            foobFactory = new ChannelFactory<AuthenticateInterface>(tcp, authURL);
            authenticator = foobFactory.CreateChannel();

            token = 0;
        }

        private async void register_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(username.Text) && !String.IsNullOrEmpty(password.Password))
            {
                progress.IsIndeterminate = true;
                register_btn.IsEnabled = false;
                login_btn.IsEnabled = false;
                username.IsEnabled = false;
                password.IsEnabled = false;


                user = username.Text;
                pass = password.Password;
                Task task = new Task(AsyncRegister); //calls register asynchronously
                task.Start();
                await task;

                progress.IsIndeterminate = false;
                register_btn.IsEnabled = true;
                login_btn.IsEnabled = true;
                username.IsEnabled = true;
                password.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Username Or Password Cannot Be Empty");
            }
        }

        private void AsyncRegister()
        {
            string msg = authenticator.Register(user, pass);
            if (msg.Equals("Successfully Registered"))
            {
                MessageBox.Show(msg);

            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private async void login_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(username.Text) && !String.IsNullOrEmpty(password.Password))
            {
                progress.IsIndeterminate = true;
                register_btn.IsEnabled = false;
                login_btn.IsEnabled = false;
                username.IsEnabled = false;
                password.IsEnabled = false;


                user = username.Text;
                pass = password.Password;
                Task<int> task = new Task<int>(AsyncLogin);//calls login async
                task.Start();
                token = await task;

                if (token!=0)
                {
                    ServicesWindow sw = new ServicesWindow(token);
                    this.Close();
                    sw.Show();
                }


                progress.IsIndeterminate = false;
                register_btn.IsEnabled = true;
                login_btn.IsEnabled = true;
                username.IsEnabled = true;
                password.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Username Or Password Cannot Be Empty");
            }

        }

        private int AsyncLogin()
        {
            token = authenticator.Login(user, pass);
            if (token.Equals(0))
            {
                MessageBox.Show("Username Or Password Incorrect");

            }
            else
            {
                MessageBox.Show("Successfully Logged In");
            }
            return token;
        }
    }
}
