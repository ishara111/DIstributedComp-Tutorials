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

namespace AsyncClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RestClient client;
        string URL;
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
                    Task<APIClasses.DataIntermed> task = new Task<APIClasses.DataIntermed>(SearchIndex);
                    task.Start();
                    APIClasses.DataIntermed dataIntermed = await task;

                    fNameBox.Text = dataIntermed.fname;
                    lNameBox.Text = dataIntermed.lname;
                    balanceBox.Text = dataIntermed.bal.ToString("C");
                    accNoBox.Text = dataIntermed.acct.ToString();
                    pinBox.Text = dataIntermed.pin.ToString("D4");
                    imageBox.Source = new BitmapImage(new Uri(dataIntermed.image));

                }
                else
                {
                    indexBox.Text = "incorrect";
                }
            }
        }

        private void nameSearch_button_Click(object sender, RoutedEventArgs e)
        {
            //Make a search class
            APIClasses.SearchData mySearch = new APIClasses.SearchData();
            mySearch.searchStr = name_search.Text;
            Console.WriteLine(mySearch.searchStr);
            //Build a request with the json in the body
            RestRequest request = new RestRequest("api/search/");
            request.AddJsonBody(mySearch);
            //Do the request
            RestResponse resp = client.Post(request);
            //Deserialize the result
            APIClasses.DataIntermed dataIntermed = JsonConvert.DeserializeObject<APIClasses.DataIntermed>(resp.Content);
            fNameBox.Text = dataIntermed.fname;
            lNameBox.Text = dataIntermed.lname;
            balanceBox.Text = dataIntermed.bal.ToString("C"); ;
            accNoBox.Text = dataIntermed.acct.ToString();
            pinBox.Text = dataIntermed.pin.ToString("D4");
            imageBox.Source = new BitmapImage(new Uri(dataIntermed.image));
            //notFound = false;
            //int res;
            //Regex rgx = new Regex("[^A-Za-z0-9]");
            //if (!String.IsNullOrEmpty(name_search.Text))
            //{
            //    if ((!int.TryParse(name_search.Text, out res)) && !(rgx.IsMatch(name_search.Text)))
            //    {
            //        Task<Data> task = new Task<Data>(Search);
            //        task.Start();

            //        name_search.IsReadOnly = true;
            //        indexBox.IsReadOnly = true;
            //        sButton.IsEnabled = false;
            //        nameSearch_button.IsEnabled = false;
            //        progress_bar.IsIndeterminate = true;


            //        Data data = await task;

            //        if (notFound == true)
            //        {
            //            name_search.Text = "not found";
            //        }
            //        else
            //        {
            //            Update();
            //        }

            //        progress_bar.IsIndeterminate = false;
            //        name_search.IsReadOnly = false;
            //        indexBox.IsReadOnly = false;
            //        sButton.IsEnabled = true;
            //        nameSearch_button.IsEnabled = true;
            //    }
            //    else
            //    {
            //        name_search.Text = "incorrect";
            //    }
            //}
            //else
            //{
            //    name_search.Text = "incorrect";
            //}
        }

    //    private Data Search()
    //    {
    //        data = new Data();

    //        foob.GetValuesForSearch(searchval, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image);
    //        if (!fName.Equals(""))
    //        {
    //            data.fname = fName;
    //            data.lname = lName;
    //            data.bal = bal.ToString("C");
    //            data.acc = acctNo.ToString();
    //            data.pin = pin.ToString("D4");
    //            data.img = image;
    //            return data;
    //        }
    //        else
    //        {
    //            notFound = true;
    //            return null;
    //        }
    //    }

    //    private void Update()
    //    {
    //        fNameBox.Text = data.fname;
    //        lNameBox.Text = data.lname;
    //        balanceBox.Text = data.bal;
    //        accNoBox.Text = data.acc;
    //        pinBox.Text = data.pin;
    //        imageBox.Source = new BitmapImage(new Uri(data.img));
    //    }
    //}

        private APIClasses.DataIntermed SearchIndex()
        {
            RestRequest request = new RestRequest("api/getvalues/" + index.ToString());
            RestResponse resp = client.Get(request);
            APIClasses.DataIntermed dataIntermed = JsonConvert.DeserializeObject<APIClasses.DataIntermed>(resp.Content);

            return dataIntermed;
        }
}
}
