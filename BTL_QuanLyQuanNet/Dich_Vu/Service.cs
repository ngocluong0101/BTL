using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyQuanNet.Dich_Vu
{
    public partial class Service : Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public Service()
        {
            InitializeComponent();
        }
        public TabControl GetTabControl()
        {
            return tcLoaiMon;
        }

        private void btnAddDish_Click(object sender, EventArgs e)
        {
            InsertDish form3 = new InsertDish(this);
            form3.SetTabPages(tcLoaiMon);
            form3.ShowDialog();
            this.Refresh();

        }

        public void gan_su_kien_click()
        {
            foreach (TabPage tab in tcLoaiMon.TabPages)
            {
                foreach (FlowLayoutPanel flp in tab.Controls.OfType<FlowLayoutPanel>())
                {
                    foreach (Panel panel in flp.Controls.OfType<Panel>())
                    {
                        foreach (PictureBox pb in panel.Controls.OfType<PictureBox>())
                        {
                            pb.Click += PictureBox_Click;
                        }
                    }
                }
            }
        }

        private void Service_Load(object sender, EventArgs e)
        {
            gan_su_kien_click();
        }
        public void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (pic != null)
            {
                Panel panel = pic.Parent as Panel;
                if (panel != null)
                {
                    Label lblTenMon = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Name.Contains("lblTenMon"));
                    Label lblGiaMon = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Name.Contains("lblGiaMon"));

                    if (lblTenMon != null && lblGiaMon != null)
                    {
                        string tenMon = lblTenMon.Text;
                        int giaMon = int.Parse(lblGiaMon.Text.Replace(".", "").Replace(" đ", ""));

                        bool daCoMon = false;
                        foreach (DataGridViewRow row in dgvOrder.Rows)
                        {
                            if (row.Cells["colTenmon"].Value != null && row.Cells["colTenmon"].Value.ToString() == tenMon)
                            {
                                int soLuong = Convert.ToInt32(row.Cells["colSoluong"].Value);
                                row.Cells["colSoluong"].Value = soLuong + 1;
                                row.Cells["colThanhtien"].Value = (soLuong + 1) * giaMon;
                                daCoMon = true;
                                break;
                            }
                        }

                        if (!daCoMon)
                        {
                            int stt = dgvOrder.Rows.Count + 1;

                            dgvOrder.Rows.Add(stt, tenMon, 1, giaMon, giaMon);
                        }

                        TinhTongTien();

                        for (int i = 0; i < dgvOrder.Rows.Count; i++)
                        {
                            dgvOrder.Rows[i].Cells["colStt"].Value = i + 1;
                        }
                    }
                }
            }
        }
        int tongTien = 0;
        private void TinhTongTien()
        {
            tongTien = 0;
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (row.Cells["colThanhtien"].Value != null)
                {
                    tongTien += Convert.ToInt32(row.Cells["colThanhtien"].Value);
                }
            }
            lblTongTien.Text = "Tổng tiền : " + tongTien.ToString("N0") + " đ";
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvOrder.Rows.Clear();
            TinhTongTien();
        }

        private void btnXacnhan_Click(object sender, EventArgs e)
        {
            db.moKN();
            string query = "insert into TK_ThuNhap (Mota, Sotien) values (N'Dịch vụ ăn uống'," + tongTien + ")";
            SqlCommand cmd = new SqlCommand(query, db.GetConnection());
            cmd.ExecuteNonQuery();
            db.dongKN();
            MessageBox.Show("Dat Hang Thanh Cong!", "Thong bao");
            dgvOrder.Rows.Clear();
            tongTien = 0;
            lblTongTien.Text = "Tổng tiền : " + tongTien.ToString("N0") + " đ";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}


