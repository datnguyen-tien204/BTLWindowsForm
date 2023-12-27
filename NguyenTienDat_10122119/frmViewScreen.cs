using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;

namespace NguyenTienDat_10122119
{
    public partial class frmViewScreen : Form
    {
        /// <Form Radius>
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        /// </End>
        private void CheckAndChangeBackground()
        {
            
            string jsonContent = File.ReadAllText("AllFormsState.json");
            JObject jsonObject = JObject.Parse(jsonContent);
            bool lockInterfaceValue = (bool)jsonObject["frmController"]["LockInterface"];
            if (lockInterfaceValue)
            {
                this.BackColor = Color.Black;
            }
            else
            {
                this.BackColor = Color.White;
            }
        }
        NetworkStream ns;
        int pbWidth, pbHeight;
        Thread showScreen;
        int resolutionRemoteX, resolutionRemoteY;

        public frmViewScreen(ref NetworkStream net, String resolution)
        {
            InitializeComponent();
            this.ns = net;
            string[] arr = resolution.Split(':');
            resolutionRemoteX = Int32.Parse(arr[1]);
            resolutionRemoteY = Int32.Parse(arr[2]);

            //this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }


        private void frmViewScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void frmViewScreen_Resize(object sender, EventArgs e)
        {
            pbWidth = pbViewScreen.Width;
            pbHeight = pbViewScreen.Height;
        }


        private void frmViewScreen_Load(object sender, EventArgs e)
        {
            pbWidth = pbViewScreen.Width;
            pbHeight = pbViewScreen.Height;
            showScreen = new Thread(ShowScreen);
            showScreen.IsBackground = true;
            showScreen.Start();
        }
        private string GetMinutesText()
        {
            string jsonContent = File.ReadAllText("AllFormsState.json");
            JObject jsonObject = JObject.Parse(jsonContent);
            string minutesText = (string)jsonObject["frmRecipient"]["MinutesText"]; 
            return minutesText;
        }
        private Bitmap ResizeImage(Bitmap originalImage, int desiredWidth, int desiredHeight)
        {
            Bitmap resizedImage = new Bitmap(originalImage, new Size(desiredWidth, desiredHeight));
            return resizedImage;
        }


        private void ShowScreen()
        {
            try
            {
                while (true)
                {
                    BinaryFormatter bFormat = new BinaryFormatter();
                    Bitmap bmp = bFormat.Deserialize(ns) as Bitmap;
                    Bitmap resized = new Bitmap(bmp);
                    string minutesText = GetMinutesText();
                    {
                        int desiredWidth = 800; 
                        int desiredHeight = 600;
                        if (minutesText == "0")
                        {
                            pbViewScreen.BackgroundImage = (Image)bmp;
                        }
                        else if(minutesText=="1")
                        {
                            Bitmap highResImage = ResizeImage(bmp, desiredWidth, desiredHeight); 
                            pbViewScreen.BackgroundImage = (Image)highResImage;
                        }    
                        else if(minutesText=="2")
                        {
                            Bitmap lowQualityImage = ResizeImage(bmp, desiredWidth, desiredHeight); 
                            pbViewScreen.BackgroundImage = (Image)lowQualityImage;
                        }    
                    }
                    pbViewScreen.BackgroundImage = (Image)resized;
                }
            }
            catch
            {
            }
            this.Close();
        }


        ////////////////////////////////////// HANDLE_Mouse_Event//////////////////////////////////////////////////
        private void pbViewScreen_MouseClick(object sender, MouseEventArgs e)
        {
            sendMessage($":mouse_click");
        }

        private void pbViewScreen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            sendMessage($":mouse_dbclick");
        }

        private void pbViewScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                sendMessage($":mouse_leftdown");
            }
            else if (e.Button == MouseButtons.Right)
            {
                sendMessage($":mouse_rightdown");
            }
            if (e.Button == MouseButtons.Middle)
            {
                sendMessage($":mouse_middledown");
            }
        }

        private void pbViewScreen_MouseMove(object sender, MouseEventArgs e)
        {
            int x = (int)((float)e.X / pbWidth * resolutionRemoteX);
            int y = (int)((float)e.Y / pbHeight * resolutionRemoteY);
            sendMessage($":mouse_move:{x}:{y}");
        }

        private void pbViewScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                sendMessage($":mouse_leftup");
            }
            else if (e.Button == MouseButtons.Right)
            {
                sendMessage($":mouse_rightup");
            }
            if (e.Button == MouseButtons.Middle)
            {
                sendMessage($":mouse_middleup");
            }
        }

        ///////////////////////////// Key Event
        private void frmViewScreen_KeyUp(object sender, KeyEventArgs e)
        {
            sendMessage($":key_up:" + ((byte)e.KeyCode).ToString());
        }

        private void pbViewScreen_Click(object sender, EventArgs e)
        {

        }

        private void frmViewScreen_KeyDown(object sender, KeyEventArgs e)
        {
            sendMessage($":key_down:" + ((byte)e.KeyCode).ToString());
        }
        //////////////////////////////
        private void sendMessage(string message)
        {
            try
            {
                ns.Write(Encoding.ASCII.GetBytes(message), 0, message.Length);
                ns.Flush();
            }
            catch { }
        }
    }
}
