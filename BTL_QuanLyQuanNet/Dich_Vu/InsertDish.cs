using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyQuanNet.Dich_Vu
{
    public partial class InsertDish: Form
    {
        private Service mainForm;
        public InsertDish(Service parentForm)
        {
            InitializeComponent();
            this.mainForm = parentForm;
        }
        public void SetTabPages(TabControl tabControl)
        {
            cmbLoaiMon.Items.Clear();
            foreach (TabPage tab in tabControl.TabPages)
            {
                cmbLoaiMon.Items.Add(tab.Text); // Lấy tên TabPage
            }
        }
        private string ImagePath;

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImagePath = openFileDialog.FileName;
                picThemMon.Image = Image.FromFile(ImagePath);
                picThemMon.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mainForm == null) return;  // Đảm bảo mainForm hợp lệ

            string tenMon = txtTenmonthem.Text;
            string giaMon = txtGiamonthem.Text;
            Image hinhAnh = picThemMon.Image;
            string loaiMon = cmbLoaiMon.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(tenMon) || string.IsNullOrEmpty(giaMon) || hinhAnh == null || string.IsNullOrEmpty(loaiMon))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (hinhAnh == null || string.IsNullOrEmpty(loaiMon))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            Panel panelMonAn = new Panel
            {
                Size = new Size(200, 250),
                BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox pic = new PictureBox
            {
                Size = new Size(180, 150),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = hinhAnh
            };

            Label lblTen = new Label
            {
                Text = tenMon,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, 170),
                AutoSize = true
            };

            Label lblGia = new Label
            {
                Text = giaMon + " VND",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, 200),
                ForeColor = Color.Red,
                AutoSize = true
            };

            panelMonAn.Controls.Add(pic);
            panelMonAn.Controls.Add(lblTen);
            panelMonAn.Controls.Add(lblGia);

            // Lấy TabControl từ Form2
            TabControl tabControl = mainForm.GetTabControl();

            // Xác định TabPage để thêm món ăn vào đúng danh mục
            bool found = false;
            foreach (TabPage tab in tabControl.TabPages)
            {
                if (tab.Text == loaiMon)
                {
                    foreach (Control ctrl in tab.Controls)
                    {
                        if (ctrl is FlowLayoutPanel flp)
                        {
                            flp.Controls.Add(panelMonAn);
                            found = true;
                            break;
                        }
                    }
                }
                //{
                //    //tab.Controls.Add(panelMonAn);
                //    //panelMonAn.BringToFront();
                //    //found = true;
                //    //break;
                //}
            }

            if (!found)
            {
                MessageBox.Show("Không tìm thấy danh mục phù hợp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Món ăn đã được thêm vào danh mục " + loaiMon, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }
    }
}
