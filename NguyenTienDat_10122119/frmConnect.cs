using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace NguyenTienDat_10122119
{
    public partial class frmConnect : Form
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_KEYDOWN = 0;

        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800,
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }

        public enum MouseEventDataXButtons : uint
        {
            XBUTTON1 = 0x00000001,
            XBUTTON2 = 0x00000002
        }

        const MouseButtons MOUSE_LEFT = MouseButtons.Left;
        const MouseButtons MOUSE_RIGHT = MouseButtons.Right;

        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x01000;
        ///////////////////////////////////////////////////////////////////////////


        frmViewScreen viewScreenForm;
        TcpListener server;
        TcpClient remoteServer;
        NetworkStream ns, nsRemoteServer;
        Thread listenForConnectThread, castScreenThread, listenForControlThread;
        Thread connectToRemoteThread, remoteScreenThread, cancelRemoteScreenThread;

        int connectStatus = 2;

        /// <Form chính>
        
        public frmConnect()
        {
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            Init_CustomLabel_Font();

            //lblMyID.Text = GetLocalIPAddress();
            string text = GetLocalIPAddress();
            string textWithoutDots = text.Replace('.', ' ');
            lblMyID.Text= textWithoutDots;
            lblSecurityCode.Text = getUsername();

            lblDeviceID.Font = new Font(pfc.Families[0], lblDeviceID.Font.Size);
            lblDeviceID.Text = "The device ID";
            lblPortReceive.Font = new Font(pfc.Families[0], lblPortReceive.Font.Size);
            lblPortReceive.Text = "Temporary security code:";
            bunifuLabel8.Font = new Font(pfc.Families[0], bunifuLabel8.Font.Size);
            bunifuLabel8.Text = "Accept Control";

            bunifuLabel4.Font = new Font(pfc.Families[0], bunifuLabel8.Font.Size);
            bunifuLabel4.Text = "Remote Control";

            bunifuLabel5.Font = new Font(pfc.Families[0], bunifuLabel8.Font.Size);
            bunifuLabel5.Text = "The Partner ID";

            bunifuLabel6.Font = new Font(pfc.Families[0], bunifuLabel8.Font.Size);
            bunifuLabel6.Text = "Password";
            
        }
        ///</Xử lí Button từ form Main>

        private void Active_Menu(bool isActive)
        {
            if (isActive)
            {
                frmMain mainForm = this.ParentForm as frmMain;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(true, "btnConnect");
                }
            }
            else
            {
                frmSettings mainForm = this.ParentForm as frmSettings;
                if (mainForm != null)
                {
                    mainForm.EnableBtnSetting(false, "btnConnect");
                }
            }
        }

        /// </Kết thúc form chính>
        /// </Các hiển thị liên quan đến form chính>
        public static string getUsername()
        {
            string userName = Environment.UserName;
            Console.WriteLine(userName);
            return userName;
            
        }

        /// <summary>
        /// <Setting các nút nếu nút đậm thì true, nhạt thì false>
        public void Active_SettingMain(bool aka)
        {
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                if (aka)
                {
                    mainForm.EnableBtnSetting(true, "btnConnect");
                }
                else
                {
                    mainForm.EnableBtnSetting(false, "btnConnect");
                }
            }
        }
        private void frmConnect_Load(object sender, EventArgs e)
        {
            Active_Menu(true);

            btnDisconnectRemote.Hide();

            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {            
                    mainForm.EnableBtnSetting(false, "btnSetting");
                    mainForm.EnableBtnSetting(false, "btnDashboard");
                    mainForm.EnableBtnSetting(false, "btnDevice");
                    mainForm.EnableBtnSetting(false, "btnProfile");
            }

            string hostname = Dns.GetHostName();
            IPHostEntry iphe = Dns.GetHostEntry(hostname);
            IPAddress ipaddress = null;
            int i = 0;
            foreach (IPAddress item in iphe.AddressList)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipaddress = item;
                    break;
                }
            }

            //lblMyID.Text = ipaddress.ToString();
            string text = ipaddress.ToString();
            string textWithoutDots = text.Replace('.', ' ');
            lblMyID.Text = textWithoutDots;


            //lblSecurityCode.Text = RandomPassword();
            string filePath = "code.txt"; 
            if (File.Exists(filePath) && new FileInfo(filePath).Length == 0)
            {
                lblSecurityCode.Text = RandomPassword();
            }
            else
            {
                string fileContent = File.ReadAllText(filePath);
                lblSecurityCode.Text = fileContent;
            }
            txtIPRemote.Focus();

            listenForConnectThread = new Thread(ListenForConnect);
            listenForConnectThread.IsBackground = true;
            listenForConnectThread.Start();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void btnCopyIP_Click(object sender, EventArgs e)
        {
            Active_Menu(true);
            try
            {
                string myString = GetLocalIPAddress();
                Clipboard.SetText(myString);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        } 

        
        private PrivateFontCollection pfc;
        private void Init_CustomLabel_Font()
        {
            pfc = new PrivateFontCollection();

            int fontLength = Properties.Resources.Hanken_Design_Co___Neue_Einstellung_Bold.Length;
            byte[] fontdata = Properties.Resources.Hanken_Design_Co___Neue_Einstellung_Bold;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);
        }
        public string RandomPassword()
        {
            Random r = new Random();
            return r.Next(1010, 99999).ToString();
        }
        /////////////////////////////////////// SERVER/////////////////////////////////////////////////////////
        ///<Setting SERVER>
        private void ListenForConnect()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, 3005);
                server.Start();
                byte[] bytes = new byte[1024];


                TcpClient client = server.AcceptTcpClient();
                ns = client.GetStream();
                int length = ns.Read(bytes, 0, bytes.Length);
                string message = Encoding.ASCII.GetString(bytes, 0, length);
                if (message != "password:" + lblSecurityCode.Text)
                {
                    sendMessage("Incorrect Password");
                    Thread.Sleep(50);
                    if (ns != null) ns.Close();
                    if (client != null) client.Close();
                    ReListenConnect();
                    return;
                }

                IPEndPoint clientInfo = (IPEndPoint)client.Client.RemoteEndPoint;
                DialogResult result = MessageBox.Show($"Do you accept for another PC remote your PC", "Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    String permisstinDenied = "Permission Denied\nCan't connect to this PC";
                    sendMessage(permisstinDenied);
                    Thread.Sleep(50);
                    if (ns != null) ns.Close();
                    if (client != null) client.Close();
                    ReListenConnect();
                    return;
                }

                connectStatus = 1;
                btnDisconnectRemote.Enabled = true;
                //txtStatus.Text = $"Remote Desktop Info: IPAddress: {clientInfo.Address} and port: {clientInfo.Port}";

                int resolutionWidth = Screen.PrimaryScreen.Bounds.Width;
                int resolutionHeight = Screen.PrimaryScreen.Bounds.Height;
                sendMessage("Resolution:" + resolutionWidth + ":" + resolutionHeight);

                txtIPRemote.Enabled = false;
                txtPasswordRemote.Enabled = false;
                btnConnect_Remote.Enabled = false;


                castScreenThread = new Thread(CastScreen);
                castScreenThread.IsBackground = true;
                castScreenThread.Start(client);

                listenForControlThread = new Thread(ListenForControl);
                listenForControlThread.IsBackground = true;
                listenForControlThread.Start();
            }
            catch { }//this.Close(); }
        }

        private void ReListenConnect()
        {
            txtIPRemote.Focus();
            if (server != null) server.Stop();
            listenForConnectThread = new Thread(ListenForConnect);
            listenForConnectThread.IsBackground = true;
            listenForConnectThread.Start();
        }

        private void CastScreen(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            while (connectStatus == 1)
            {
                try
                {
                    Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    Graphics gp = Graphics.FromImage(bmp);
                    gp.CopyFromScreen(0, 0, 0, 0, new Size(bmp.Width, bmp.Height));
                    BinaryFormatter bFormat = new BinaryFormatter();
                    bFormat.Serialize(ns, bmp);
                    Thread.Sleep(50);
                    gp.Dispose();
                    bmp.Dispose();
                }
                catch { break; }
            }

            connectStatus = 2;
            txtIPRemote.Enabled = true;
            txtPasswordRemote.Enabled = true;

            btnConnect_Remote.Enabled = true;
            btnDisconnectRemote.Enabled = false;
            //txtStatus.Text = "";


            ns.Close();
            if (client != null) client.Close();
            if (server != null) server.Stop();
            StopThread(ref listenForControlThread);

            ReListenConnect();
        }
        private void StopThread(ref Thread thread)
        {
            if (thread != null && thread.IsAlive) thread.Abort();
        }

        private void ListenForControl()
        {
            byte[] bytes;
            int length;
            string[] arr;
            int curPosX, curPosY;
            string key = "";
            string message = "";
            while (true)
            {
                try
                {
                    bytes = new byte[1024];
                    length = ns.Read(bytes, 0, bytes.Length);
                    message = Encoding.ASCII.GetString(bytes, 0, length);

                    arr = message.Split(':');


                    switch (arr[1])
                    {
                        case "mouse_move":
                            curPosX = Int32.Parse(arr[2]);
                            curPosY = Int32.Parse(arr[3]);
                            MoveCursor(curPosX, curPosY);
                            break;
                        case "mouse_click":
                            EventMouseClick();
                            break;
                        case "mouse_dbclick":
                            MouseDBClick();
                            break;
                        case "mouse_leftdown":
                            MouseUpDown(MOUSEEVENTF_LEFTDOWN);
                            break;
                        case "mouse_rightdown":
                            MouseUpDown(MOUSEEVENTF_RIGHTDOWN);
                            break;
                        case "mouse_middledown":
                            MouseUpDown(MOUSEEVENTF_MIDDLEDOWN);
                            break;
                        case "mouse_leftup":
                            MouseUpDown(MOUSEEVENTF_LEFTUP);
                            break;
                        case "mouse_rightup":
                            MouseUpDown(MOUSEEVENTF_RIGHTUP);
                            break;
                        case "mouse_middleup":
                            MouseUpDown(MOUSEEVENTF_MIDDLEUP);
                            break;
                        case "key_down":
                            key = arr[2];
                            EventKeyDown(key);
                            break;
                        case "key_up":
                            key = arr[2];
                            EventKeyUp(key);
                            break;
                        default: break;
                    }
                }
                catch { }
            }
        }

        private void EventKeyUp(string key)
        {
            keybd_event(Byte.Parse(key), 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }

        private void EventKeyDown(string key)
        {

            keybd_event(Byte.Parse(key), 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN, 0);
        }

        private void MouseUpDown(uint action)
        {
            mouse_event(action, 0, 0, 0, 0);
        }

        private void MouseDBClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void EventMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        private void ViewScreen(Object obj)
        {
            String resolution = (String)obj;
            viewScreenForm = new frmViewScreen(ref nsRemoteServer, resolution);
            viewScreenForm.ShowDialog();
        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {
            Active_Menu(true);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Active_Menu(true);
        }

        private void MoveCursor(int curPosX, int curPosY)
        {
            Cursor.Position = new Point(curPosX, curPosY);
        }

        private void sendMessage(string message)
        {
            ns.Write(Encoding.ASCII.GetBytes(message), 0, message.Length);
            ns.Flush();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Active_SettingMain(true);
        }

        private void txtIPRemote_TextChanged(object sender, EventArgs e)
        {
            Active_SettingMain(true);
        }

        private void txtPasswordRemote_TextChanged(object sender, EventArgs e)
        {
            Active_SettingMain(true);
        }

        private void txtIPRemote_Load(object sender, EventArgs e)
        {
            Active_Menu(true);
        }

        private void txtPasswordRemote_Load(object sender, EventArgs e)
        {
            Active_Menu(true);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            string filePath = "code.txt";
            if (File.Exists(filePath) && new FileInfo(filePath).Length == 0)
            {
                lblSecurityCode.Text = RandomPassword();
            }
            else
            {
                string fileContent = File.ReadAllText(filePath);
                lblSecurityCode.Text = fileContent;
            }
            txtIPRemote.Focus();
        }

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDisconnectRemote_Click(object sender, EventArgs e)
        {
            connectStatus = 2;
        }
        ///<End Setting SERVER>

        ////////////////////////////////////////////////// CLIENT //////////////////////////////////////////////////////////////

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Active_Menu(true);

            Active_SettingMain(true);

            if (connectStatus == 1)
            {
                viewScreenForm.Close();
                txtIPRemote.Focus();
                return;
            }

            connectToRemoteThread = new Thread(ConnectToRemote);
            connectToRemoteThread.IsBackground = true;
            connectToRemoteThread.Start();
        }
        private void LogConnectionInfo(string ipAddress, DateTime connectTime)
        {
            string filePath = "connection_log.txt"; 

            try
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine($"{ipAddress},{connectTime}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        private void ConnectToRemote()
        {
            try
            {
                IPAddress remoteIP;
                if (!IPAddress.TryParse(txtIPRemote.Text.Replace(' ', '.').Trim(), out remoteIP))
                {
                    AlertDanger("Invalid IP Address");
                    return;
                }
                if (txtIPRemote.Text.Trim() == lblMyID.Text)
                {
                    AlertDanger("You can not connect to your computer");
                    return;
                }

                remoteServer = new TcpClient();
                remoteServer.Connect(remoteIP, 3005);
                nsRemoteServer = remoteServer.GetStream();

                string remotePassword = "password:" + txtPasswordRemote.Text.Trim();

                nsRemoteServer.Write(Encoding.ASCII.GetBytes(remotePassword), 0, remotePassword.Length);
                nsRemoteServer.Flush();

                byte[] bytes = new byte[1024];
                int length = nsRemoteServer.Read(bytes, 0, bytes.Length);
                string message = Encoding.ASCII.GetString(bytes, 0, length);

                if (message.Contains("Incorrect Password") || message.Contains("Permission Denied"))
                {
                    connectStatus = 2;
                    if (nsRemoteServer != null) nsRemoteServer.Close();
                    if (remoteServer != null) remoteServer.Close();
                    txtIPRemote.Enabled = true;
                    txtPasswordRemote.Enabled = true;
                    btnConnect_Remote.Text = "Connect";
                    AlertDanger(message);
                    return;
                }

                connectStatus = 1;
                LogConnectionInfo(txtIPRemote.Text.Trim(), DateTime.Now);
                btnConnect_Remote.Text = "Disconnect";
                txtIPRemote.Enabled = false;
                txtPasswordRemote.Enabled = false;

                remoteScreenThread = new Thread(ViewScreen);
                remoteScreenThread.IsBackground = true;
                remoteScreenThread.Start(message);


                cancelRemoteScreenThread = new Thread(CancelRemoteSceen);
                cancelRemoteScreenThread.IsBackground = true;
                cancelRemoteScreenThread.Start();
            }
            catch
            {
                connectStatus = 2;
                AlertDanger("Connect to remote PC failed");
                txtIPRemote.Enabled = true;
                txtPasswordRemote.Enabled = true;
                btnConnect_Remote.Text = "Connect";
                txtIPRemote.Focus();
            }
        }
        void AlertDanger(String message)
        {
            MessageBox.Show(message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void CancelRemoteSceen()
        {
            while (true)
            {
                if (connectStatus == 1 && remoteScreenThread != null && !remoteScreenThread.IsAlive)
                {
                    connectStatus = 2;

                    if (nsRemoteServer != null) nsRemoteServer.Close();
                    if (remoteServer != null) remoteServer.Close();
                    txtIPRemote.Enabled = true;
                    txtPasswordRemote.Enabled = true;
                    btnConnect_Remote.Text = "Connect";
                    txtIPRemote.Focus();
                    StopThread(ref cancelRemoteScreenThread);
                }
                Thread.Sleep(1000);
            }
        }
    }
}
