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
using RestSharp;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net;

namespace AsyncClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RestClient client;
        string URL,sText;
        int totEntries;
        int index = 0;
        public MainWindow()
        {
            InitializeComponent();
            URL = "https://localhost:44310/";
            client = new RestClient(URL);
            RestRequest request = new RestRequest("api/totalval");
            RestResponse numOfThings = client.Get(request);
            indexBox.Text = numOfThings.Content;
            urltext.Text = URL;
            totEntries = Int32.Parse(indexBox.Text);
        }

        private async void sButton_Click(object sender, RoutedEventArgs e)
        {
            int res = 0;
            index = 0;
            if (!int.TryParse(indexBox.Text, out res))
            {
                indexBox.Text = "incorrect";
            }
            else
            {
                index = Int32.Parse(indexBox.Text);

                //Console.WriteLine(int.TryParse(index.ToString(), out res));
                if ((index > 0) && (index <= totEntries))
                {
                    name_search.IsReadOnly = true;
                    indexBox.IsReadOnly = true;
                    sButton.IsEnabled = false;
                    nameSearch_button.IsEnabled = false;
                    progress_bar.IsIndeterminate = true;

                    Task<APIClasses.DataIntermed> task = new Task<APIClasses.DataIntermed>(SearchIndex);
                    task.Start();
                    APIClasses.DataIntermed dataIntermed = await task;

                    fNameBox.Text = dataIntermed.fname;
                    lNameBox.Text = dataIntermed.lname;
                    balanceBox.Text = dataIntermed.bal.ToString("C");
                    accNoBox.Text = dataIntermed.acct.ToString();
                    pinBox.Text = dataIntermed.pin.ToString("D4");
                    imageBox.Source = new BitmapImage(new Uri(dataIntermed.image));

                    progress_bar.IsIndeterminate = false;
                    name_search.IsReadOnly = false;
                    indexBox.IsReadOnly = false;
                    sButton.IsEnabled = true;
                    nameSearch_button.IsEnabled = true;

                }
                else
                {
                    indexBox.Text = "incorrect";
                }
            }
        }

        private async void nameSearch_button_Click(object sender, RoutedEventArgs e)
        {
            int res;
            Regex rgx = new Regex("[^A-Za-z0-9]");
            if (!String.IsNullOrEmpty(name_search.Text))
            {
                if ((!int.TryParse(name_search.Text, out res)) && !(rgx.IsMatch(name_search.Text)))
                {

                    name_search.IsReadOnly = true;
                    indexBox.IsReadOnly = true;
                    sButton.IsEnabled = false;
                    nameSearch_button.IsEnabled = false;
                    progress_bar.IsIndeterminate = true;

                    sText = name_search.Text;
                    Task<APIClasses.DataIntermed> task = new Task<APIClasses.DataIntermed>(SearchName);
                    task.Start();
                    APIClasses.DataIntermed dataIntermed = await task;

                    if(dataIntermed.fname == null)
                    {
                        name_search.Text = "not found";
                    }else
                    {
                        fNameBox.Text = dataIntermed.fname;
                        lNameBox.Text = dataIntermed.lname;
                        balanceBox.Text = dataIntermed.bal.ToString("C"); ;
                        accNoBox.Text = dataIntermed.acct.ToString();
                        pinBox.Text = dataIntermed.pin.ToString("D4");
                        imageBox.Source = new BitmapImage(new Uri(dataIntermed.image));
                    }


                    progress_bar.IsIndeterminate = false;
                    name_search.IsReadOnly = false;
                    indexBox.IsReadOnly = false;
                    sButton.IsEnabled = true;
                    nameSearch_button.IsEnabled = true;
                }
                else
                {
                    name_search.Text = "incorrect";
                }
            }
            else
            {
                name_search.Text = "incorrect";
            }
        }
        private void urlButton_Click(object sender, RoutedEventArgs e)
        {
            name_search.IsReadOnly = true;
            indexBox.IsReadOnly = true;
            sButton.IsEnabled = false;
            nameSearch_button.IsEnabled = false;

            SetUrl setUrl = new SetUrl(this);
            setUrl.ShowDialog();
            if (setUrl.valid)
            {
                urltext.Text = setUrl.GetURL();
                URL = setUrl.GetURL();
                if (CheckUrl())
                {
                    progress_bar.IsIndeterminate = true;


                    client = new RestClient(URL);
                    RestRequest request = new RestRequest("api/totalval");
                    RestResponse numOfThings = client.Get(request);

                    urltext.Text = URL;

                    progress_bar.IsIndeterminate = false;
                    name_search.IsReadOnly = false;
                    indexBox.IsReadOnly = false;
                    sButton.IsEnabled = true;
                    nameSearch_button.IsEnabled = true;
                }
                else
                {
                    urltext.Text = "unable to connect";
                }
            }
        }


        private APIClasses.DataIntermed SearchIndex()
        {
            RestRequest request = new RestRequest("api/getvalues/" + index.ToString());
            RestResponse resp = client.Get(request);
            APIClasses.DataIntermed dataIntermed = JsonConvert.DeserializeObject<APIClasses.DataIntermed>(resp.Content);

            return dataIntermed;
        }

        private APIClasses.DataIntermed SearchName()
        {
            //Make a search class
            APIClasses.SearchData mySearch = new APIClasses.SearchData();
            mySearch.searchStr = sText;
            Console.WriteLine(mySearch.searchStr);
            //Build a request with the json in the body
            RestRequest request = new RestRequest("api/search/");
            request.AddJsonBody(mySearch);
            //Do the request
            RestResponse resp = client.Post(request);
            //Deserialize the result
            APIClasses.DataIntermed dataIntermed = JsonConvert.DeserializeObject<APIClasses.DataIntermed>(resp.Content);
            return dataIntermed;
        }

        public string GetURL()
        {
            return URL;
        }

        public bool CheckUrl()
        {
            bool up;
            WebRequest request = WebRequest.Create(URL);
            WebResponse res;
            try
            {
                res = request.GetResponse();
                up = true;
                return up;
            }
            catch (WebException webEx)
            {
                MessageBox.Show(webEx.Message);
                up = false;
                return up;
            }
        }
    }
}
