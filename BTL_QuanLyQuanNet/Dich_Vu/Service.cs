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
            foreach (Control control in this.Controls)
            {
                if (control is TabControl tabcontrol)
                {
                    foreach (TabPage tabpage in tabcontrol.TabPages)
                    {
                        foreach (Control ctrl in tabpage.Controls)
                        {
                            if (ctrl is FlowLayoutPanel flp)
                            {
                                foreach (Control tmp in flp.Controls)
                                {
                                    if (tmp is Panel panel)
                                    {
                                        foreach (Control res in panel.Controls)
                                        {
                                            if (res is PictureBox pb)
                                            {
                                                pb.Click += PictureBox_Click;
                                            }
                                        }
                                    }
                                }
                            }
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
                Panel panel = pic.Parent as Panel; // Lấy Panel chứa PictureBox
                if (panel != null)
                {
                    // Lấy Label tên món
                    Label lblTenMon = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Name.Contains("lblTenMon"));
                    // Lấy Label giá món
                    Label lblGiaMon = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Name.Contains("lblGiaMon"));

                    if (lblTenMon != null && lblGiaMon != null)
                    {
                        string tenMon = lblTenMon.Text;
                        int giaMon = int.Parse(lblGiaMon.Text.Replace(".", "").Replace(" đ", "")); // Chuyển giá từ string sang số

                        // Kiểm tra nếu món đã có trong DataGridView thì tăng số lượng
                        bool daCoMon = false;
                        foreach (DataGridViewRow row in dgvOrder.Rows)
                        {
                            if (row.Cells["colTenmon"].Value != null && row.Cells["colTenmon"].Value.ToString() == tenMon)
                            {
                                int soLuong = Convert.ToInt32(row.Cells["colSoluong"].Value);
                                row.Cells["colSoluong"].Value = soLuong + 1;
                                row.Cells["colThanhtien"].Value = (soLuong + 1) * giaMon; // Cập nhật thành tiền
                                daCoMon = true;
                                break;
                            }
                        }

                        // Nếu món chưa có thì thêm mới với số lượng là 1
                        if (!daCoMon)
                        {
                            // Tính STT dựa trên số lượng hàng hiện có
                            int stt = dgvOrder.Rows.Count + 1;

                            dgvOrder.Rows.Add(stt, tenMon, 1, giaMon, giaMon);
                        }

                        // Cập nhật tổng tiền
                        TinhTongTien();

                        // Cập nhật lại STT cho tất cả các hàng để đảm bảo thứ tự đúng
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
            lblTongTien.Text = tongTien.ToString("N0") + " đ"; // Hiển thị số tiền có dấu chấm
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            dgvOrder.Rows.Clear(); // Xóa toàn bộ dữ liệu trong DataGridView
            TinhTongTien(); // Cập nhật lại tổng tiền về 0
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
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tcLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
