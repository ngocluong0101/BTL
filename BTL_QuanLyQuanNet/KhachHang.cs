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

        private void Load_data()
        {
            db.moKN();
            string query = "select * from KHACHHANG";
            SqlDataAdapter adt = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dgvKhachHang.DataSource = dt;
            db.dongKN();
        }
        private void Load_LichSu()
        {
            db.moKN();
            string query = "select * from LICHSU";
            SqlDataAdapter adt = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dgvLichSu.DataSource = dt;
            db.dongKN();
        }


        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            dgvLichSu.Visible = false;
            ThemTaiKhoan formThem = new ThemTaiKhoan();
            formThem.ShowDialog();
            Load_data();
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            dgvLichSu.Visible = false;
            if (string.IsNullOrWhiteSpace(txtTimkiem.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản muốn xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                db.moKN();
                string query = "delete KHACHHANG where Taikhoan = '" + txtTimkiem.Text + "'";
                string Lichsu = "insert into LICHSU (Taikhoan, Mota) values ('" + txtTimkiem.Text + "', N'Đã được xóa.')";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                SqlCommand cmdLichsu = new SqlCommand(Lichsu, db.GetConnection());
                cmd.ExecuteNonQuery();
                cmdLichsu.ExecuteNonQuery();
                db.dongKN();
                MessageBox.Show("Đã xóa tài khoản '" + txtTimkiem.Text + "' thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Load_data();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dgvLichSu.Visible = false;
            Sua formSua = new Sua();
            formSua.ShowDialog();
            Load_data();
        }

        private void btnNapTien_Click(object sender, EventArgs e)
        {
            dgvLichSu.Visible = false;
            NapTien formNap = new NapTien();
            formNap.ShowDialog();
            Load_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            dgvLichSu.Visible = true;
            Load_LichSu();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            db.moKN();
            string query = "select * from KHACHHANG where Taikhoan = '" + txtTimkiem.Text + "'";
            SqlDataAdapter adt = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dgvKhachHang.DataSource = dt;
            db.dongKN();
        }
    }
}
