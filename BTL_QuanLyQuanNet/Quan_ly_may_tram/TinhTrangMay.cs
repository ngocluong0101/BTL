using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyQuanNet.Quan_ly_may_tram
{
    public partial class TinhTrangMay: Form
    {
        private Button selectedButton = null;
        private Dictionary<Button, bool> machineStatus = new Dictionary<Button, bool>(); // Trạng thái máy
        private Dictionary<Button, DateTime?> startTime = new Dictionary<Button, DateTime?>(); // Thời gian bắt đầu
        private const double pricePerHour = 8000;
        private Timer timer;
        public TinhTrangMay()
        {
            InitializeComponent();
            InitializeMachines();
            InitializeTimer();
        }

        private List<Button> GetMachineButtons()
        {
            List<Button> machineButtons = new List<Button>();

            foreach (Control ctrl in splitContainer2.Panel2.Controls)
            {
                if (ctrl is Button btn && btn.Name.StartsWith("btnMay"))
                {
                    machineButtons.Add(btn);
                }
            }

            return machineButtons;
        }
        private void InitializeMachines()
        {
            List<Button> machineButtons = GetMachineButtons();
            foreach (Button btn in machineButtons)
            {
                btn.BackColor = Color.Gray;
                btn.Click += Machine_Click;
                machineStatus[btn] = false;
                startTime[btn] = null;
            }

            if (btnPower != null)
                btnPower.Click += Power_Click;
        }
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateMachineInfo();
        }
        private void Machine_Click(object sender, EventArgs e)
        {
            if (selectedButton != null)
            {
                selectedButton.BackColor = machineStatus[selectedButton] ? Color.FromArgb(0, 192, 192) : Color.Gray;
            }

            selectedButton = sender as Button;
            selectedButton.BackColor = Color.Green;
            lblMay.Text = selectedButton.Text;
            UpdateMachineInfo();
        }
        private void Power_Click(object sender, EventArgs e)
        {
            if (selectedButton != null)
            {
                if (machineStatus[selectedButton])
                {
                    selectedButton.BackColor = Color.Gray;
                    startTime[selectedButton] = null;
                }
                else
                {
                    selectedButton.BackColor = Color.FromArgb(0, 192, 192);
                    startTime[selectedButton] = DateTime.Now;
                }

                machineStatus[selectedButton] = !machineStatus[selectedButton];
            }
        }
        private void UpdateMachineInfo()
        {
            if (selectedButton != null)
            {
                string status = machineStatus[selectedButton] ? "Online" : "Offline";
                string timeUsed = "00:00";
                double cost = 0;

                if (machineStatus[selectedButton] && startTime[selectedButton] != null)
                {
                    TimeSpan duration = DateTime.Now - startTime[selectedButton].Value;
                    int hours = duration.Hours;
                    int minutes = duration.Minutes;
                    timeUsed = $"{hours:D2}:{minutes:D2}";


                    double totalHours = duration.TotalMinutes / 60;
                    cost = Math.Round(totalHours * pricePerHour, 0);
                }

                lblStatus.Text = $"Trạng thái: {status}";
                lblTimeUsed.Text = $"Thời gian sử dụng: {timeUsed}";
                lblCost.Text = $"Tạm tính: {cost:N0}đ";
            }
        }
        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {
            splitContainer2.BorderStyle = BorderStyle.FixedSingle;
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
