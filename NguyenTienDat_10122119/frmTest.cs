using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NguyenTienDat_10122119
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            string jsonFilePath = "AllFormsState.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            dynamic jsonData = JsonConvert.DeserializeObject(jsonString);
            string minutesTextOfRecipient = jsonData.frmRecipient.MinutesText;
            Console.WriteLine(minutesTextOfRecipient);
        }
    }
}
