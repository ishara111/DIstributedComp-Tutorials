using BusinessTierWebAPI.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RestClient restClient;
        public MainWindow()
        {
            InitializeComponent();
            restClient = new RestClient("https://localhost:44366/");

            indexBox.Text = DBSize().ToString();
        }

        private void gennerate_btn_Click(object sender, RoutedEventArgs e)
        {
            
            RestRequest restRequest = new RestRequest("api/generate", Method.Post);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            MessageBox.Show(restResponse.Content);
            indexBox.Text = DBSize().ToString();

        }

        private void Insert_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select Account Image";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Accinfo data = new Accinfo();
                //imgPhoto.Source = new BitmapImage(new Uri(op.FileName));

                data.fname = fNameBox.Text;
                data.lname = lNameBox.Text;
                data.balance = Decimal.Parse(balanceBox.Text);
                data.accno = int.Parse(accNoBox.Text);
                data.pin = int.Parse(pinBox.Text);
                data.imageurl = op.FileName;

                RestRequest restRequest = new RestRequest("api/insert", Method.Post);
                restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
                RestResponse restResponse = restClient.Execute(restRequest);
                MessageBox.Show(restResponse.Content);

                indexBox.Text = DBSize().ToString();


            }
            else
            {
                MessageBox.Show("Insert cancelled");
            }

        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(indexBox.Text);
            Accinfo data = new Accinfo();
            //imgPhoto.Source = new BitmapImage(new Uri(op.FileName));

            data.fname = fNameBox.Text;
            data.lname = lNameBox.Text;
            data.balance = Decimal.Parse(balanceBox.Text);
            data.accno = int.Parse(accNoBox.Text);
            data.pin = int.Parse(pinBox.Text);
            data.imageurl = imageBox.Source.ToString();

            RestRequest restRequest = new RestRequest("api/update/{id}", Method.Post);
            restRequest.AddUrlSegment("id", index);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            MessageBox.Show(restResponse.Content);

            indexBox.Text = DBSize().ToString();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(indexBox.Text);
            RestRequest restRequest = new RestRequest("api/delete/{id}", Method.Delete);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            restRequest.AddUrlSegment("id", index);
            RestResponse restResponse = restClient.Execute(restRequest);

            indexBox.Text = DBSize().ToString();

        }

        private void sButton_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(indexBox.Text);
            RestRequest restRequest = new RestRequest("api/search/{id}", Method.Get);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            restRequest.AddUrlSegment("id", index);
            RestResponse restResponse = restClient.Execute(restRequest);
            Accinfo data = JsonConvert.DeserializeObject<Accinfo>(restResponse.Content);

            fNameBox.Text = data.fname;
            lNameBox.Text = data.lname;
            balanceBox.Text = data.balance.ToString("C");
            accNoBox.Text = data.accno.ToString();
            pinBox.Text = data.pin.ToString("D4");
            imageBox.Source = new BitmapImage(new Uri(data.imageurl));
            MessageBox.Show(restResponse.Content);
        }

        private void nameSearch_button_Click(object sender, RoutedEventArgs e)
        {

        }


        private int DBSize()
        {
            RestRequest restRequest = new RestRequest("api/getacc", Method.Get);
            // restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            List<Accinfo> accinfoList = JsonConvert.DeserializeObject<List<Accinfo>>(restResponse.Content);

            return accinfoList.Count;
        }
    }
}
