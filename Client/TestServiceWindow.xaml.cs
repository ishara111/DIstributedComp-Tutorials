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
    /// Interaction logic for TestServiceWindow.xaml
    /// </summary>
    public partial class TestServiceWindow : Window
    {
        private RestClient serviceProvider;
        private Service service;
        private Dictionary<string, TextBox> textBoxes = new Dictionary<string, TextBox>();
        private Dictionary<string, int> nums = new Dictionary<string, int>();
        private List<string> displayList = new List<string>();
        private TextBox txtbox = new TextBox();
        private Button test_btn = new Button();
        private Button close_btn = new Button();
        private ProgressBar progress = new ProgressBar();
        private int amount,token;
        private string endpoint;
        public bool close,login;
        private ServicesWindow sw;
        public TestServiceWindow(Service service,int token,ServicesWindow sw)
        {
            InitializeComponent();
            this.service = service;
            this.token = token;
            this.sw = sw;
            amount = service.NoOfOperands;
            close = false;
            login = false;

            string[] url = service.APIEndpoint.ToLower().Split(new[] { "api" }, StringSplitOptions.None);
            try
            {
                serviceProvider = new RestClient(url[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                close=true;
            }

            CreateTextBlocks();
            GenerateTextBoxes(amount);
            CreatePrgAndBtns();

        }
        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void Test_btn_Click(object sender, RoutedEventArgs e)
        {
            endpoint = service.APIEndpoint.ToLower() + "?token=" + token;



            if (!CheckNull(amount)) //checks if all textboxs are empty
            {
                if (!GetInputNums())
                {
                    close_btn.IsEnabled = false;
                    test_btn.IsEnabled = false;
                    progress.IsIndeterminate = true;
                    ToggleTextBoxes(false);

                    Task task = new Task(AsyncTest);
                    task.Start();
                    await task;

                    if (close==true)
                    {
                        this.Close();
                    }
                    if (login==true)
                    {
                        Login login = new Login();
                        sw.Close();
                        login.Show();
                        this.Close();
                    }

                    close_btn.IsEnabled = true;
                    test_btn.IsEnabled = true;
                    progress.IsIndeterminate = false;
                    ToggleTextBoxes(true);
                }
            }
            else
            {
                MessageBox.Show("Input Field Cannot Be Empty");
            }
        }

        private void AsyncTest()
        {
            AddToEndpoint();
            try
            {
                RestRequest request = new RestRequest(endpoint);
                RestResponse resp = serviceProvider.Get(request);
                if (resp.Content.Equals("{\"status\":\"Denied\",\"reason\":\"Authentication Error\"}"))
                {
                    login = true;
                    MessageBox.Show("Session Expired Login Again");
                }
                else if (resp.Content.ToLower().Contains("no http resource was found"))
                {
                    close = true;
                    MessageBox.Show("Service Endpoint Or No of operands Incorrect");
                }
                else
                {
                    MessageBox.Show("Result: " + resp.Content);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message+"\nAPI Endpoint Incorrect");
                close = true;
            }
        }

        private void AddToEndpoint()
        {
            endpoint = endpoint + ("&num1=") + nums["num"];
            for (int i = 1; i < amount; i++)
            {
                endpoint = endpoint + ("&num" + (i+1) + "=") + nums["num"+i];
            }
        }

        private void ToggleTextBoxes(bool enable)
        {
            if (enable==false)
            {
                txtbox.IsEnabled = false;
                for (int i = 1; i < amount; i++)
                {
                    textBoxes["txtbox" + i].IsEnabled = false;
                }
            }
            else
            {
                txtbox.IsEnabled = true;
                for (int i = 1; i < amount; i++)
                {
                    textBoxes["txtbox" + i].IsEnabled = true;
                }
            }

        }

        private bool GetInputNums()
        {
            bool err=false;
            int val;
            if (int.TryParse(txtbox.Text, out val))
            {
                nums["num"] = val;
            }
            else
            {
                err = true;
                MessageBox.Show("input must be integer");
            }
            for (int i = 1; i < amount; i++)
            {
                if (int.TryParse(textBoxes["txtbox" + i].Text, out val))
                {
                    nums["num" + i] = val;
                }
                else
                {
                    err = true;
                    MessageBox.Show("input must be integer");
                }
            }
            return err;
        }

        private bool CheckNull(int amount)
        {
            if (String.IsNullOrEmpty(txtbox.Text))
            {
                return true;
            }
            for (int i = 1; i < amount; i++)
            {
                if (String.IsNullOrEmpty(textBoxes["txtbox"+i].Text))
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateTextBlocks()
        {
            displayList.Add("Service Name: "+service.name);
            displayList.Add("Description "+service.description);
            displayList.Add("Endpoint: " + service.APIEndpoint);
            displayList.Add("No Of Operands: "+service.NoOfOperands);
            displayList.Add("Operand Type: "+service.operandType);

            foreach (string d in displayList)
            {
                TextBlock text = new TextBlock();
                text.Text = d;
                text.Height = 18;
                text.Width = 300;
                text.Margin = new Thickness(0, 10, 0, 0);
                this.stackpanel.Children.Add(text);
            }
        }
        private void GenerateTextBoxes(int amount)
        {
            TextBlock block = new TextBlock();
            block.Text = "Operand 1";
            block.Height = 18;
            block.Width = 70;
            block.Margin = new Thickness(0, 30, 60, 0);
            this.stackpanel.Children.Add(block);

            txtbox.Height = 18;
            txtbox.Width = 120;
            txtbox.Margin = new Thickness(0, 5, 0, 0);
            this.stackpanel.Children.Add(txtbox);

            for (int i = 1; i < amount; i++)
            {
                textBoxes.Add("txtbox"+i, new TextBox());
            }

            for (int i = 1; i < amount; i++)
            {
                TextBlock blockn = new TextBlock();
                blockn.Text = "Operand "+(i+1);
                blockn.Height = 18;
                blockn.Width = 70;
                blockn.Margin = new Thickness(0, 25, 60, 0);
                this.stackpanel.Children.Add(blockn);


                textBoxes["txtbox" + i].Height = 18;
                textBoxes["txtbox" + i].Width = 120;
                textBoxes["txtbox" + i].Margin = new Thickness(0, 5, 0, 0);
                this.stackpanel.Children.Add(textBoxes["txtbox" + i]);
            }
        }

        private void CreatePrgAndBtns()
        {
            progress.Height = 10;
            progress.Width = 100;
            progress.Margin = new Thickness(0, 35, 0, 0);
            this.stackpanel.Children.Add(progress);


            close_btn.Content = "Close";
            close_btn.Click += Close_btn_Click;
            close_btn.Height = 24;
            close_btn.Width = 60;
            close_btn.Margin = new Thickness(0, 35, 100, 0);
            this.stackpanel.Children.Add(close_btn);


            test_btn.Content = "Test";
            test_btn.Click += Test_btn_Click;
            test_btn.Height = 24;
            test_btn.Width = 60;
            test_btn.Margin = new Thickness(100, -24, 0, 0);
            this.stackpanel.Children.Add(test_btn);
        }
    }
}
