using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyQuanNet.Quan_ly_may_tram
{
    public partial class TinhTrangMay: Form
    {
        private Button selectedButton = null;
        private Dictionary<Button, bool> machineStatus = new Dictionary<Button, bool>();
        private Dictionary<Button, DateTime?> startTime = new Dictionary<Button, DateTime?>();
        private const double pricePerHour = 8000;
        private Timer timer;
        public TinhTrangMay()
        {
            InitializeComponent();
            InitializeMachines();
            InitializeTimer();
        }

        private List<Button> GetAllMachineButtons(TabControl tabControl)
        {
            return tabControl.TabPages
                .Cast<TabPage>()
                .SelectMany(tp => tp.Controls.OfType<Button>())
                .Where(btn => btn.Name.StartsWith("button"))
                .ToList();
        }
        private Dictionary<TabPage, double> zonePrices = new Dictionary<TabPage, double>();
        private void InitializeMachines()
        {
            zonePrices[tpTieuchuan] = 8000;
            zonePrices[tpGaming] = 13000;
            zonePrices[tpChuyennghiep] = 15000;
            zonePrices[tpThidau] = 19000;
            List<Button> machineButtons = GetAllMachineButtons(tcZone);
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
            timer = new Timer
            {
                Interval = 1000
            };
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
            TabPage selectedTab = selectedButton.Parent as TabPage;
            if (selectedTab != null && zonePrices.ContainsKey(selectedTab))
            {
                lblZone.Text = $"Khu vực: {selectedTab.Text}";
            }
            UpdateMachineInfo();
        }
        private void Power_Click(object sender, EventArgs e)
        {
            if (selectedButton == null) return;

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
        private void UpdateMachineInfo()
        {
            if (selectedButton == null) return;

            string status = machineStatus[selectedButton] ? "Online" : "Offline";
            string timeUsed = "00:00";
            double cost = 0;
            string zoneName = "Không xác định";

            if (machineStatus[selectedButton] && startTime[selectedButton] != null)
            {
                TimeSpan duration = DateTime.Now - startTime[selectedButton].Value;
                int hours = duration.Hours;
                int minutes = duration.Minutes;
                timeUsed = $"{hours:D2}:{minutes:D2}";

                double totalHours = duration.TotalMinutes / 60;


                TabPage selectedTab = selectedButton.Parent as TabPage;
                double zonePrice = 8000;

                if (selectedTab != null)
                {
                    if (zonePrices.ContainsKey(selectedTab))
                    {
                        zonePrice = zonePrices[selectedTab];
                    }
                    zoneName = selectedTab.Text;
                }

                cost = Math.Round(totalHours * zonePrice, 0);
            }

            lblStatus.Text = $"Trạng thái: {status}";
            lblTimeUsed.Text = $"Thời gian sử dụng: {timeUsed}";
            lblCost.Text = $"Tạm tính: {cost:N0}đ";
            lblZone.Text = $"Khu vực: {zoneName}";

        }
    }
}
