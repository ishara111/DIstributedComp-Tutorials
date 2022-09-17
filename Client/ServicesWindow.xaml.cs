using Newtonsoft.Json;
using RegistryClasses;
using RestSharp;
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
    /// Interaction logic for ServicesWindow.xaml
    /// </summary>
    public partial class ServicesWindow : Window
    {
        private int token;
        private string URL,search;
        private RestClient client;
        private bool canShow,expired;
        public ServicesWindow(int token)
        {
            InitializeComponent();
            this.token = token;

            URL = "https://localhost:44327/";
            client = new RestClient(URL);

        }

        private async void showAll_btn_Click(object sender, RoutedEventArgs e)
        {
            expired = false;
            canShow = false;
            progress.IsIndeterminate = true;
            showAll_btn.IsEnabled = false;
            search_btn.IsEnabled = false;
            test_btn.IsEnabled = false;

            Task<List<Service>> task = new Task<List<Service>>(AsyncShowList);
            task.Start();
            List<Service> showList = await task;

            if (expired)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }

            if (canShow)
            {
                listView.ItemsSource = showList;
            }

            progress.IsIndeterminate = false;
            showAll_btn.IsEnabled = true;
            search_btn.IsEnabled = true;
            test_btn.IsEnabled = true;
        }

        private List<Service> AsyncShowList()
        {
            List<Service> list = new List<Service>();
            RestRequest request = new RestRequest("api/allservices?token=" + token);
            RestResponse response = client.Get(request);

            if (response.Content.Equals("\"No services published\""))
            {
                MessageBox.Show("No services published");
            }
            else if (response.Content.Equals("{\"status\":\"Denied\",\"reason\":\"Authentication Error\"}"))
            {
                MessageBox.Show("Session Expired Login Again");
                expired = true;
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<Service>>(response.Content);
                canShow = true;
            }
            return list;
        }

        private async void search_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(searchbox.Text))
            {
                search = searchbox.Text;
                expired = false;
                canShow = false;

                progress.IsIndeterminate = true;
                showAll_btn.IsEnabled = false;
                search_btn.IsEnabled = false;
                test_btn.IsEnabled = false;

                Task<List<Service>> task = new Task<List<Service>>(AsyncSearch);
                task.Start();
                List<Service> showList = await task;

                if (expired)
                {
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }

                if (canShow)
                {
                    listView.ItemsSource = showList;
                }

                progress.IsIndeterminate = false;
                showAll_btn.IsEnabled = true;
                search_btn.IsEnabled = true;
                test_btn.IsEnabled = true;

            }
            else
            {
                MessageBox.Show("Search Cannot Be Empty");
            }
        }

        private List<Service> AsyncSearch()
        {
            List<Service> list = new List<Service>();
            RestRequest request = new RestRequest("api/search?token=" + token+"&search="+search);
            RestResponse response = client.Get(request);

            if (response.Content.Equals("\"No services published\""))
            {
                MessageBox.Show("No services published");
            }
            else if (response.Content.Equals("\"not found\""))
            {
                MessageBox.Show("No Search Result Found");
            }
            else if (response.Content.Equals("{\"status\":\"Denied\",\"reason\":\"Authentication Error\"}"))
            {
                MessageBox.Show("Session Expired Login Again");
                expired = true;
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<Service>>(response.Content);
                canShow = true;
            }
            return list;
        }

        private void test_btn_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                Service s = (Service)listView.SelectedItem;
                MessageBox.Show(s.APIEndpoint);
            }
            else
            {
                MessageBox.Show("Select Service From List First");
            }

        }
    }
}
