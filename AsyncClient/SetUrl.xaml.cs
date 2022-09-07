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

namespace AsyncClient
{
    /// <summary>
    /// Interaction logic for SetUrl.xaml
    /// </summary>
    public partial class SetUrl : Window
    {
        MainWindow mw;
        public bool valid;
        public SetUrl(MainWindow nmw)
        {
            InitializeComponent();
            mw = nmw;
            urlTextbox.Text = mw.GetURL();
        }

        private void set_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateUrl(urlTextbox.Text))
            {
                valid = true;
                MessageBox.Show("URL changed to "+urlTextbox.Text);
                this.Close();
            }
            else
            {
                valid = false;
                MessageBox.Show(urlTextbox.Text + " is an invalid URL");
            }
        }

        public bool ValidateUrl(string source)
        {
            Uri urlRes;
            bool valid = Uri.TryCreate(source, UriKind.Absolute, out urlRes)
                && (urlRes.Scheme == Uri.UriSchemeHttp || urlRes.Scheme == Uri.UriSchemeHttps);
            return valid;
        }

        public string GetURL()
        {
            return urlTextbox.Text;
        }
    }
}
