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
    public partial class InsertDish : Form
    {
        private Service mainForm;
        public InsertDish(Service parentForm)
        {
            InitializeComponent();
            this.mainForm = parentForm;
        }
        public void SetTabPages(TabControl tabControl)
        {
            foreach (TabPage tab in tabControl.TabPages)
            {
                cmbLoaiMon.Items.Add(tab.Text);
            }
        }
        private string ImagePath;

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImagePath = openFileDialog.FileName;
                picThemMon.Image = Image.FromFile(ImagePath);
                picThemMon.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mainForm == null) return;

            string tenMon = txtTenmonthem.Text;
            string giaMon = txtGiamonthem.Text;
            Image hinhAnh = picThemMon.Image;
            string loaiMon = cmbLoaiMon.SelectedItem.ToString();

            if (string.IsNullOrEmpty(tenMon) || string.IsNullOrEmpty(giaMon) || hinhAnh == null || string.IsNullOrEmpty(loaiMon))
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
            pic.Click += mainForm.PictureBox_Click;
            Label lblTen = new Label
            {
                Name = "lblTenMon",
                Text = tenMon,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, 170),
                AutoSize = true
            };

            Label lblGia = new Label
            {
                Name = "lblGiaMon",
                Text = giaMon + "",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, 200),
                ForeColor = Color.Red,
                AutoSize = true
            };

            panelMonAn.Controls.Add(pic);
            panelMonAn.Controls.Add(lblTen);
            panelMonAn.Controls.Add(lblGia);

            TabControl tabControl = mainForm.GetTabControl();
            foreach (TabPage tab in tabControl.TabPages)
            {
                if (tab.Text == loaiMon)
                {
                    foreach (Control ctrl in tab.Controls)
                    {
                        if (ctrl is FlowLayoutPanel flp)
                        {
                            flp.Controls.Add(panelMonAn);
                            MessageBox.Show("Món ăn đã được thêm vào danh mục " + loaiMon, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            return; 
                        }
                    }
                }
            }
        }
    }
}

