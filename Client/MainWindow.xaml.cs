using BusinessTierWebAPI.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
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
        Accinfo data;
        int index;
        string name;
        public MainWindow()
        {
            InitializeComponent();
            restClient = new RestClient("https://localhost:44366/");

            count_label.Content = "Acc Count: " + DBSize().ToString();
        }

        private async void gennerate_btn_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task(AsyncGen);
            task.Start();
            await task;


            count_label.Content = "Acc Count: " + DBSize().ToString();

        }

        private void AsyncGen()
        {
            RestRequest restRequest = new RestRequest("api/generate", Method.Post);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            MessageBox.Show(restResponse.Content.ToString());
        }

        private async void Insert_btn_Click(object sender, RoutedEventArgs e)
        {
            int res;
            decimal resd;
            if (!String.IsNullOrEmpty(fNameBox.Text) && !String.IsNullOrEmpty(lNameBox.Text)
                && !String.IsNullOrEmpty(balanceBox.Text) && !String.IsNullOrEmpty(accNoBox.Text) &&
                !String.IsNullOrEmpty(pinBox.Text))
            {
                if (int.TryParse(accNoBox.Text, out res) && int.TryParse(pinBox.Text, out res)
                    && decimal.TryParse(balanceBox.Text, out resd))
                {
                    OpenFileDialog op = new OpenFileDialog();
                    op.Title = "Select Account Image";
                    op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                      "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                      "Portable Network Graphic (*.png)|*.png";
                    if (op.ShowDialog() == true)
                    {
                        data = new Accinfo();
                        //imgPhoto.Source = new BitmapImage(new Uri(op.FileName));

                        data.fname = fNameBox.Text;
                        data.lname = lNameBox.Text;
                        data.balance = Decimal.Parse(balanceBox.Text);
                        data.accno = int.Parse(accNoBox.Text);
                        data.pin = int.Parse(pinBox.Text);
                        data.imageurl = op.FileName;

                        Task task = new Task(AsyncInsert);
                        task.Start();
                        await task;

                        count_label.Content = "Acc Count: " + DBSize().ToString();
                    }
                }
                else
                {
                    MessageBox.Show("input not in correct format");
                }
            }
            else
            {
                MessageBox.Show("input cannot be empty");
            }

        }

        private void AsyncInsert()
        {
            RestRequest restRequest = new RestRequest("api/insert", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);

            MessageBox.Show("Inserted");
        }

        private async void update_btn_Click(object sender, RoutedEventArgs e)
        {
            int res;
            decimal resd;
            if (!String.IsNullOrEmpty(fNameBox.Text) && !String.IsNullOrEmpty(lNameBox.Text)
                && !String.IsNullOrEmpty(balanceBox.Text) && !String.IsNullOrEmpty(accNoBox.Text) &&
                !String.IsNullOrEmpty(pinBox.Text))
            {
                if (int.TryParse(accNoBox.Text, out res) && int.TryParse(pinBox.Text, out res)
                    && decimal.TryParse(balanceBox.Text, out resd))
                {

                    MessageBoxResult result = MessageBox.Show("Do you want to update image", "Update", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        OpenFileDialog op = new OpenFileDialog();
                        op.Title = "Select Account Image";
                        op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                          "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                          "Portable Network Graphic (*.png)|*.png";
                        if (op.ShowDialog() == true)
                        {
                            index = int.Parse(indexBox.Text);
                            data = new Accinfo();
                            //imgPhoto.Source = new BitmapImage(new Uri(op.FileName));

                            data.Id = index;
                            data.fname = fNameBox.Text;
                            data.lname = lNameBox.Text;
                            data.balance = Decimal.Parse(balanceBox.Text);
                            data.accno = int.Parse(accNoBox.Text);
                            data.pin = int.Parse(pinBox.Text);
                            data.imageurl = op.FileName.ToString();

                            Task task = new Task(AsyncUpdate);
                            task.Start();
                            await task;

                            count_label.Content = "Acc Count: " + DBSize().ToString();
                        }
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        index = int.Parse(indexBox.Text);
                        data = new Accinfo();
                        //imgPhoto.Source = new BitmapImage(new Uri(op.FileName));

                        data.Id = index;
                        data.fname = fNameBox.Text;
                        data.lname = lNameBox.Text;
                        data.balance = Decimal.Parse(balanceBox.Text);
                        data.accno = int.Parse(accNoBox.Text);
                        data.pin = int.Parse(pinBox.Text);
                        data.imageurl = imageBox.Source.ToString();

                        Task task = new Task(AsyncUpdate);
                        task.Start();
                        await task;

                        count_label.Content = "Acc Count: " + DBSize().ToString();
                    }

                }
                else
                {
                    MessageBox.Show("input not in correct format");
                }
            }
            else
            {
                MessageBox.Show("input cannot be empty");
            }
        }

        private void AsyncUpdate()
        {
            //RestRequest restRequest = new RestRequest("api/update/{id}", Method.Post);
            //restRequest.AddUrlSegment("id", data.Id);
            RestRequest request = new RestRequest("api/update/" + index.ToString());
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse resp = restClient.Post(request);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            //RestResponse restResponse = restClient.Execute(restRequest);
            MessageBox.Show("Updated Account");
        }

        private async void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            index = int.Parse(indexBox.Text);

            Task task = new Task(AsyncDelete);
            task.Start();
            await task;

            count_label.Content = "Acc Count: " + DBSize().ToString();

        }

        private void AsyncDelete()
        {
            RestRequest restRequest = new RestRequest("api/delete/{id}", Method.Delete);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            restRequest.AddUrlSegment("id", index);
            RestResponse restResponse = restClient.Execute(restRequest);
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
                if ((index > 0) && (index <= DBSize()))
                {
                    index = int.Parse(indexBox.Text);

                    Task<Accinfo> task = new Task<Accinfo>(AsyncSearchID);
                    task.Start();
                    data = await task;

                    if (data != null)
                    {
                        fNameBox.Text = data.fname;
                        lNameBox.Text = data.lname;
                        balanceBox.Text = data.balance.ToString("C");
                        accNoBox.Text = data.accno.ToString();
                        pinBox.Text = data.pin.ToString("D4");
                        imageBox.Source = new BitmapImage(new Uri(data.imageurl));
                    }
                    else
                    {
                        indexBox.Text = "not found";
                        MessageBox.Show("Not found");
                    }
                    //MessageBox.Show(restResponse.Content);
                }
                else
                {
                    indexBox.Text = "incorrect";
                }
            }
        }

        private Accinfo AsyncSearchID()
        {
            RestRequest restRequest = new RestRequest("api/search/{id}", Method.Get);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            restRequest.AddUrlSegment("id", index);
            RestResponse restResponse = restClient.Execute(restRequest);
            data = JsonConvert.DeserializeObject<Accinfo>(restResponse.Content);
            return data;
        }

        private async void nameSearch_button_Click(object sender, RoutedEventArgs e)
        {
            int res;
            Regex rgx = new Regex("[^A-Za-z0-9]");
            if (!String.IsNullOrEmpty(name_search.Text))
            {
                if ((!int.TryParse(name_search.Text, out res)) && !(rgx.IsMatch(name_search.Text)))
                {
                    name = name_search.Text;

                    Task<Accinfo> task = new Task<Accinfo>(AsyncSearchName);
                    task.Start();
                    data = await task;


                    if (data != null)
                    {
                        fNameBox.Text = data.fname;
                        lNameBox.Text = data.lname;
                        balanceBox.Text = data.balance.ToString("C");
                        accNoBox.Text = data.accno.ToString();
                        pinBox.Text = data.pin.ToString("D4");
                        imageBox.Source = new BitmapImage(new Uri(data.imageurl));
                    }
                    else
                    {
                        name_search.Text = "not found";
                        MessageBox.Show("Not found");
                    }

                    //MessageBox.Show(restResponse.Content);
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

        private Accinfo AsyncSearchName()
        {
            RestRequest request = new RestRequest("api/search?name=" + name);
            RestResponse resp = restClient.Get(request);
            RestResponse restResponse = restClient.Execute(request);
            data = JsonConvert.DeserializeObject<Accinfo>(restResponse.Content);
            return data;
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
