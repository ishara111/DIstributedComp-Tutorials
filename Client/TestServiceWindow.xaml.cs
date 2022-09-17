using RegistryClasses;
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
        private Dictionary<String, TextBox> textBoxes = new Dictionary<String, TextBox>();
        private List<string> displayList = new List<string>();
        public TestServiceWindow(Service service)
        {
            InitializeComponent();

            CreateTextBlocks(service);
            GenerateTextBoxes(service.NoOfOperands);
            CreatePrgAndBtns();
            //textBoxes["txtbox2"].Text = "working";

        }
        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Run_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateTextBlocks(Service service)
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

            TextBox txtbox = new TextBox();
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
            ProgressBar progress = new ProgressBar();
            progress.Height = 10;
            progress.Width = 100;
            progress.Margin = new Thickness(0, 35, 0, 0);
            this.stackpanel.Children.Add(progress);

            Button close_btn = new Button();
            close_btn.Content = "Close";
            close_btn.Click += Close_btn_Click;
            close_btn.Height = 24;
            close_btn.Width = 60;
            close_btn.Margin = new Thickness(0, 35, 100, 0);
            this.stackpanel.Children.Add(close_btn);

            Button run_btn = new Button();
            run_btn.Content = "Run";
            run_btn.Click += Run_btn_Click;
            run_btn.Height = 24;
            run_btn.Width = 60;
            run_btn.Margin = new Thickness(100, -24, 0, 0);
            this.stackpanel.Children.Add(run_btn);
        }
    }
}
