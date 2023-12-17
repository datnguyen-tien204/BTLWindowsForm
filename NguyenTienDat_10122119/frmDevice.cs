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
using Bunifu.UI.WinForms.BunifuButton;
using static Bunifu.UI.WinForms.BunifuButton.BunifuButton;
using Newtonsoft.Json;

namespace NguyenTienDat_10122119
{
    public partial class frmDevice : Form
    {
        private bool moreButtonClicked = false;
        public frmDevice()
        {
            InitializeComponent();
            AddButtonsToPanel();
        }

        private void frmDevice_Load(object sender, EventArgs e)
        {
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                mainForm.EnableBtnSetting(false, "btnConnect");
                mainForm.EnableBtnSetting(false, "btnDashboard");
                mainForm.EnableBtnSetting(false, "btnSetting");
                mainForm.EnableBtnSetting(false, "btnProfile");
            }
            btnMore.Image = Properties.Resources.icons8_forward_32;
            moreButtonClicked = false;
            bunifuPanel3.Visible = false;
            Active_SettingMain(true);

            txtSearchingDevice.TextChanged += new EventHandler(txtSearchingDevice_TextChanged);
            CheckSearchEmpty();
        }
        private void CheckSearchEmpty()
        {
            if (txtSearchingDevice.Text.Trim().Length == 0)
            {
                isSearchEmpty = true;
            }
            else
            {
                isSearchEmpty = false;
            }
        }

        public void Active_SettingMain(bool aka)
        {
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                if (aka)
                {
                    mainForm.EnableBtnSetting(true, "btnDevice");
                }
                else
                {
                    mainForm.EnableBtnSetting(false, "btnDevice");
                }
            }
        }
        private string GetLatestTimeForID(List<string> connectionLogLines, string id)
        {
            // Lọc các dòng chứa ID cần tìm và sắp xếp theo thời gian
            List<DateTime> times = connectionLogLines
                .Where(line => line.StartsWith(id))
                .Select(line =>
                {
                    DateTime time;
                    DateTime.TryParseExact(line.Split(',')[1].Trim(), "MM/dd/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out time);
                    return time;
                })
                .OrderByDescending(time => time)
                .ToList();

            // Lấy thời gian gần nhất nếu có
            if (times.Any())
            {
                return times.First().ToString("MM/dd/yyyy h:mm:ss tt");
            }

            return string.Empty;
        }
        private void AddButtonsToPanel()
        {
            string filePath = "connection_log.txt"; // Đường dẫn tới file văn bản chứa các ID

            // Kiểm tra xem file có tồn tại không
            if (File.Exists(filePath))
            {
                bunifuPanel3.SuspendLayout();
                string[] lines = File.ReadAllLines(filePath);

                Dictionary<string, DateTime> latestConnections = new Dictionary<string, DateTime>();

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 2)
                    {
                        string id = parts[0];
                        DateTime time;
                        if (DateTime.TryParseExact(parts[1], "MM/dd/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out time))
                        {
                            if (!latestConnections.ContainsKey(id) || time > latestConnections[id])
                            {
                                latestConnections[id] = time;
                            }
                        }
                    }
                }
                var selectedRecords = latestConnections.OrderByDescending(x => x.Value).Take(5).ToList();

                int buttonIndex = 0;
                string jsonFilePath = "button_info.json";
                string connectionLogFilePath = "connection_log.txt";
                List<string> connectionLogLines = File.ReadAllLines(connectionLogFilePath).ToList();
                foreach (var record in selectedRecords)
                {
                    Active_SettingMain(true);
                    Bunifu.UI.WinForms.BunifuButton.BunifuButton newButton = ID_ButtonCreate(record.Key,buttonIndex);
                    newButton.Click += (sender, e) =>
                    {
                        string id = newButton.Name;
                        string latestTime = GetLatestTimeForID(connectionLogLines, id);
                        ButtonInfo buttonInfo = new ButtonInfo
                        {
                            ID = id,
                            Time = latestTime
                        };
                        string json = JsonConvert.SerializeObject(buttonInfo);
                        File.WriteAllText(jsonFilePath, json);
                    };

                    newButton.Click += (sender, e) => 
                    {
                        Bunifu.UI.WinForms.BunifuImageButton propertiesButton = ButtonFactory.Properties();
                        propertiesButton.Click += PropertiesClick;
                        bunifuPanel2.Controls.Add(propertiesButton);
                    };
                    bunifuPanel3.Controls.Add(newButton);
                    buttonIndex++;
                }
            }
        }
        private void PropertiesClick(object sender, EventArgs e)
        {

            frmDeviceProperties frmNew=new frmDeviceProperties();
            frmNew.ShowDialog();
            frmNew.BringToFront();

        }


        private void btnMore_Click(object sender, EventArgs e)
        {
            Active_SettingMain(true);
            if (!moreButtonClicked)
            {
                btnMore.Image = Properties.Resources.icons8_forward_32__1_;
                moreButtonClicked = true;
                bunifuPanel3.Visible = true;
            }
            else
            {
                btnMore.Image = Properties.Resources.icons8_forward_32;
                moreButtonClicked = false;
                bunifuPanel3.Visible = false;
            }
        }

        private bool isSearchEmpty = true;

        private void txtSearchingDevice_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchingDevice.Text.Trim().ToLower();
            if (searchText.Length > 0)
            {
                isSearchEmpty = false;
                foreach (Control control in bunifuPanel3.Controls)
                {
                    if (control is Bunifu.UI.WinForms.BunifuButton.BunifuButton button)
                    {
                        if (button.ButtonText.ToLower().Contains(searchText))
                        {
                            button.Visible = true;
                        }
                        else
                        {
                            button.Visible = false;
                        }
                    }
                }
            }
            else
            {
                isSearchEmpty = true;
                foreach (Control control in bunifuPanel3.Controls)
                {
                    if (control is Bunifu.UI.WinForms.BunifuButton.BunifuButton button)
                    {
                        button.Visible = true;
                    }
                }
            }
        }


        private void txtSearchingDevice_Click(object sender, EventArgs e)
        {
            Active_SettingMain(true);
            CheckSearchEmpty();
            if (isSearchEmpty)
            {
                foreach (Control control in bunifuPanel3.Controls)
                {
                    if (control is Bunifu.UI.WinForms.BunifuButton.BunifuButton button)
                    {
                        button.Visible = true;
                    }
                }
            }
        }

        private void btnArrange_Click(object sender, EventArgs e)
        {
            Active_SettingMain(true);
            List<Bunifu.UI.WinForms.BunifuButton.BunifuButton> buttons = bunifuPanel3.Controls
        .OfType<Bunifu.UI.WinForms.BunifuButton.BunifuButton>()
        .ToList();

            var sortedButtons = buttons.OrderBy(button =>
            {
                // Giả sử button text có định dạng là "ID - Tên"
                string[] parts = button.ButtonText.Split('-');
                if (parts.Length > 0 && int.TryParse(parts[0].Trim(), out int id))
                {
                    return id;
                }
                return int.MaxValue; // Nếu không phân tích được ID, sử dụng giá trị lớn nhất
            }).ToList();

            // Xóa các button hiện tại từ bunifuPanel3
            bunifuPanel3.Controls.Clear();

            // Thêm lại các button đã được sắp xếp vào bunifuPanel3
            int buttonIndex = 0;
            foreach (var button in sortedButtons)
            {
                button.Location = new Point(10, 55 * buttonIndex); // Đặt lại vị trí cho từng button
                bunifuPanel3.Controls.Add(button); // Thêm button vào panel
                buttonIndex++;
            }
        }
        public Bunifu.UI.WinForms.BunifuButton.BunifuButton ID_ButtonCreate(string record_key,int buttonIndex)
        {
            Bunifu.UI.WinForms.BunifuButton.BunifuButton newButton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            newButton.AllowAnimations = true;
            newButton.AllowMouseEffects = true;
            newButton.AllowToggling = false;
            newButton.AnimationSpeed = 200;
            newButton.AutoGenerateColors = false;
            newButton.AutoRoundBorders = false;
            newButton.AutoSizeLeftIcon = true;
            newButton.AutoSizeRightIcon = true;
            newButton.BackColor = System.Drawing.Color.Transparent;
            newButton.BackColor1 = System.Drawing.Color.White;
            newButton.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            newButton.ButtonText = $"{record_key}";
            newButton.ButtonTextMarginLeft = 0;
            newButton.ColorContrastOnClick = 45;
            newButton.ColorContrastOnHover = 45;
            newButton.Cursor = System.Windows.Forms.Cursors.Default;
            newButton.DialogResult = System.Windows.Forms.DialogResult.None;
            newButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            newButton.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            newButton.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            newButton.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            newButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newButton.ForeColor = System.Drawing.Color.Black;
            newButton.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            newButton.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            newButton.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            newButton.IconMarginLeft = 11;
            newButton.IconPadding = 10;
            newButton.IdleIconLeftImage = Properties.Resources.icons8_windows_10_30;
            newButton.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            newButton.IconRightCursor = System.Windows.Forms.Cursors.Default;
            newButton.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);

            newButton.IconSize = 25;
            newButton.IdleBorderColor = System.Drawing.Color.White;
            newButton.IdleBorderRadius = 15;
            newButton.IdleBorderThickness = 1;
            newButton.IdleFillColor = System.Drawing.Color.White;
            newButton.IdleIconLeftImage = null;
            newButton.IdleIconRightImage = null;
            newButton.IndicateFocus = false;
            //newButton.Location = new System.Drawing.Point(8, 99);
            newButton.Location = new Point(10, 55 * buttonIndex);
            newButton.Name = $"{record_key}";
            newButton.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            newButton.OnDisabledState.BorderRadius = 15;
            newButton.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            newButton.OnDisabledState.BorderThickness = 1;
            newButton.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            newButton.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            newButton.OnDisabledState.IconLeftImage = null;
            newButton.OnDisabledState.IconRightImage = null;
            newButton.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            newButton.onHoverState.BorderRadius = 15;
            newButton.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            newButton.onHoverState.BorderThickness = 1;
            newButton.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            newButton.onHoverState.ForeColor = System.Drawing.Color.Black;
            newButton.onHoverState.IconLeftImage = Properties.Resources.icons8_windows_10_30;
            newButton.onHoverState.IconRightImage = null;

            newButton.OnIdleState.BorderColor = System.Drawing.Color.White;
            newButton.OnIdleState.BorderRadius = 15;
            newButton.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            newButton.OnIdleState.BorderThickness = 1;
            newButton.OnIdleState.FillColor = System.Drawing.Color.White;
            newButton.OnIdleState.ForeColor = System.Drawing.Color.Black;
            newButton.OnIdleState.IconLeftImage = Properties.Resources.icons8_windows_10_30;
            newButton.OnIdleState.IconRightImage = null;
            newButton.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            newButton.OnPressedState.BorderRadius = 15;
            newButton.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            newButton.OnPressedState.BorderThickness = 1;
            newButton.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            newButton.OnPressedState.ForeColor = System.Drawing.Color.Black;
            newButton.OnPressedState.IconLeftImage = Properties.Resources.icons8_windows_10_30;
            newButton.OnPressedState.IconRightImage = null;
            newButton.Size = new System.Drawing.Size(150, 50);
            newButton.TabIndex = 0;
            newButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            newButton.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            newButton.TextMarginLeft = 0;
            newButton.TextPadding = new System.Windows.Forms.Padding(0);
            newButton.UseDefaultRadiusAndThickness = true;
            return newButton;
        }
    }
    public class LogEntry
    {
        public string ID { get; set; }
        public DateTime LastConnectionTime { get; set; }
    }
    public class ButtonInfo
    {
        public string ID { get; set; }
        public string Time { get; set; }
    }

    public static class ButtonFactory
    {
        public static Bunifu.UI.WinForms.BunifuImageButton Properties()
        {
            Bunifu.UI.WinForms.BunifuImageButton btnProperties = new Bunifu.UI.WinForms.BunifuImageButton();
            btnProperties.ActiveImage = null;
            btnProperties.AllowAnimations = true;
            btnProperties.AllowBuffering = false;
            btnProperties.AllowToggling = false;
            btnProperties.AllowZooming = true;
            btnProperties.AllowZoomingOnFocus = false;
            btnProperties.BackColor = System.Drawing.Color.Transparent;
            btnProperties.DialogResult = System.Windows.Forms.DialogResult.None;
            btnProperties.FadeWhenInactive = false;
            btnProperties.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            btnProperties.Image = global::NguyenTienDat_10122119.Properties.Resources.icons8_information_64;
            btnProperties.ImageActive = null;
            btnProperties.ImageLocation = null;
            btnProperties.ImageMargin = 20;
            btnProperties.ImageSize = new System.Drawing.Size(54, 53);
            btnProperties.ImageZoomSize = new System.Drawing.Size(74, 73);
            btnProperties.Location = new System.Drawing.Point(49, 46);
            btnProperties.Name = "btnProperties";
            btnProperties.Rotation = 0;
            btnProperties.ShowActiveImage = true;
            btnProperties.ShowCursorChanges = true;
            btnProperties.ShowImageBorders = true;
            btnProperties.ShowSizeMarkers = false;
            btnProperties.Size = new System.Drawing.Size(74, 73);
            btnProperties.TabIndex = 0;
            btnProperties.ToolTipText = "";
            btnProperties.WaitOnLoad = false;
            btnProperties.Zoom = 20;
            btnProperties.ZoomSpeed = 10;
            return btnProperties;
        }
        
    }
}
