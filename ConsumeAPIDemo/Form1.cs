using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumeAPIDemo
{
    public partial class Form1 : Form
    {
        HttpClient client;
        public Form1()
        {
            InitializeComponent();            
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "TWFsZVVzZXI6MTIzNDU2");
            HttpResponseMessage response = client.GetAsync("http://localhost:55390/api/employee").Result;
            var emp = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
        }
    }
}
