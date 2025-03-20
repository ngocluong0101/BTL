using BTL_QuanLyQuanNet.Dich_Vu;
using BTL_QuanLyQuanNet.THONG_KE;
using BTL_QuanLyQuanNet.Quan_ly_may_tram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyQuanNet
{
    public partial class Main: Form
    {
        ThongKe TK = new ThongKe();
        KhachHang KH = new KhachHang();
        QuanLyThoiGian QLTG = new QuanLyThoiGian();
        private Form formTinhTrangMay;
        private Form formKhachHang;
        private Form formService;
        private Form formThongKe;
        private Form formQuanLyThoiGian;
        private Form CurrentChildForm;
        public Main()
        {
            InitializeComponent();
            this.Size = this.ClientSize;
        }

        private void OpenChildForm(ref Form ChildForm,Form newForm)
        {
            if (CurrentChildForm != null && CurrentChildForm == ChildForm)
                return;
            if (CurrentChildForm != null)
            {
                CurrentChildForm.Hide();
            }
            if (ChildForm == null)
            {
                ChildForm = newForm;
                ChildForm.TopLevel = false;
                ChildForm.FormBorderStyle = FormBorderStyle.None;
                ChildForm.Dock = DockStyle.Fill;
                panelChildForm.Controls.Add(ChildForm);
            }
            CurrentChildForm = ChildForm;
            CurrentChildForm.BringToFront();
            CurrentChildForm.Show();
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTinhtrangmay_Click(object sender, EventArgs e)
        {
            OpenChildForm(ref formTinhTrangMay, new TinhTrangMay());
            txtXinChao.Text = btnTinhtrangmay.Text;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            OpenChildForm(ref formTinhTrangMay, new TinhTrangMay());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnKhachhang_Click(object sender, EventArgs e)
        {
            KH.Load_data();
            OpenChildForm(ref formKhachHang, new KhachHang());
            txtXinChao.Text = btnKhachhang.Text;
        }

        private void btnDichvu_Click(object sender, EventArgs e)
        {
            OpenChildForm(ref formService, new Service());
            txtXinChao.Text = btnDichvu.Text;
        }

        private void btnDangxuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap formDangnhap = new DangNhap();
            formDangnhap.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            TK.Load_Data();
            OpenChildForm(ref formThongKe, new ThongKe());
            txtXinChao.Text = btnThongKe.Text;
        }

        private void btnQuanlythoigian_Click(object sender, EventArgs e)
        {
            QLTG.Load_Data();
            OpenChildForm(ref formQuanLyThoiGian, new QuanLyThoiGian());
            txtXinChao.Text = btnQuanlythoigian.Text;
        }
    }
}
