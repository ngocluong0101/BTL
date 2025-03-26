using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyQuanNet
{
    public partial class KhachHang: Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public KhachHang()
        {
            InitializeComponent();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            dgvLichSu.Visible = false;
            Load_data();
            Load_LichSu();
        }

        public void Load_data()
        {
            db.moKN();
            string query = "select Taikhoan, Matkhau, Sodu from KHACHHANG";
            SqlDataAdapter adt = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dgvKhachHang.DataSource = dt;
            db.dongKN();
        }
        private void Load_LichSu()
        {
            db.moKN();
            string query = "select Thoigian, Taikhoan, Mota from LICHSU";
            SqlDataAdapter adt = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dgvLichSu.DataSource = dt;
            db.dongKN();
        }


        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            lblTimKiem.Visible = true;
            txtTimKiem.Visible = true;
            btnTimKiem.Visible = true;
            dgvLichSu.Visible = false;
            ThemTaiKhoan formThem = new ThemTaiKhoan();
            formThem.ShowDialog();
            Load_data();
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            dgvLichSu.Visible = false;
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                MessageBox.Show("Vui lòng nhập ( chọn ) tài khoản muốn xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                db.moKN();
                string query = "delete KHACHHANG where Taikhoan = '" + txtTimKiem.Text + "'";
                string Lichsu = "insert into LICHSU (Taikhoan, Mota) values ('" + txtTimKiem.Text + "', N'Đã được xóa.')";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                SqlCommand cmdLichsu = new SqlCommand(Lichsu, db.GetConnection());
                cmd.ExecuteNonQuery();
                cmdLichsu.ExecuteNonQuery();
                db.dongKN();
                MessageBox.Show("Đã xóa tài khoản '" + txtTimKiem.Text + "' thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTimKiem.Text = "";
            }
            Load_data();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            lblTimKiem.Visible = true;
            txtTimKiem.Visible = true;
            btnTimKiem.Visible = true;
            dgvLichSu.Visible = false;
            Sua formSua = new Sua();
            formSua.ShowDialog();
            Load_data();
        }

        private void btnNapTien_Click(object sender, EventArgs e)
        {
            lblTimKiem.Visible = true;
            txtTimKiem.Visible = true;
            btnTimKiem.Visible = true;
            dgvLichSu.Visible = false;
            NapTien formNap = new NapTien();
            formNap.ShowDialog();
            Load_data();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            db.moKN();
            string query = "select Taikhoan, Matkhau, Sodu from KHACHHANG where Taikhoan = '" + txtTimKiem.Text + "'";
            SqlDataAdapter adt = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dgvKhachHang.DataSource = dt;
            db.dongKN();
        }
        private void btnLichSu_Click(object sender, EventArgs e)
        {
            dgvLichSu.Visible = true;
            lblTimKiem.Visible = false;
            txtTimKiem.Visible = false;
            btnTimKiem.Visible = false;
            Load_LichSu();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtTimKiem.Text = row.Cells[0].Value.ToString().Trim();   
            }
        }
    }
}
