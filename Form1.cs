using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiDiem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static String conStr = ConfigurationManager.ConnectionStrings["QLDConnectionString"].ConnectionString.ToString();


        //FormLoad để chạy code load các bảng vào app
        private void Form1_Load(object sender, EventArgs e)
        {
            load_sinhvien();
            load_giangvien();
            load_monhoc();
            load_svbangdiem();
            load_lophanhchinh();
            load_loptinchi();

        }



        //---CHỬ NGỌC ÁNH
        //Sinh viên
        void load_sinhvien()
        {
            lsvSinhVien.Items.Clear();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblSinhVien", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaSV"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenSV"] + ""));
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", dataReader["NgaySinh"]));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["GioiTinh"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["sdt"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["Email"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["QueQuan"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["KhoaHoc"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["MaLopHC"] + ""));




                            lsvSinhVien.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }

        private void btnSV_them_Click(object sender, EventArgs e)
        {
            var a = DateTime.Today;
            if (Convert.ToDateTime(dateSV.Text) > a)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtMaSV.Text == "")
            {
                MessageBox.Show("Mã sinh viên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtTenSV.Text == "")
            {
                MessageBox.Show("Tên sinh viên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(cbSV_GT.Text == "")
            {
                MessageBox.Show("Giới tính không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtSDT_SV.Text == "")
            {
                MessageBox.Show("Số điện thoại không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtEmail_SV.Text == "")
            {
                MessageBox.Show("Email không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtQueQuan_SV.Text == "")
            {
                MessageBox.Show("Quê quán không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtKhoaHoc_SV.Text == "")
            {
                MessageBox.Show("Khóa học không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtMaLopHC.Text == "") {
                MessageBox.Show("Lớp hành chính không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else

                try
                {


                    using (SqlConnection cnn = new SqlConnection(conStr))
                    {

                        using (SqlCommand cmd = cnn.CreateCommand())

                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "insert_sv";
                            cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text);
                            cmd.Parameters.AddWithValue("@TenSV ", txtTenSV.Text);
                            cmd.Parameters.AddWithValue("@NgaySinh ", Convert.ToDateTime(dateSV.Text));
                            cmd.Parameters.AddWithValue("@GioiTinh ", cbSV_GT.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@sdt ", txtSDT_SV.Text);
                            cmd.Parameters.AddWithValue("@Email ", txtEmail_SV.Text);
                            cmd.Parameters.AddWithValue("@QueQuan ", txtQueQuan_SV.Text);
                            cmd.Parameters.AddWithValue("@KhoaHoc ", txtKhoaHoc_SV.Text);
                            cmd.Parameters.AddWithValue("@MaLopHC ", txtMaLopHC.Text);


                            cnn.Open();

                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                MessageBox.Show("Thêm sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cnn.Close();
                                load_sinhvien();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void cbSV_LHC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnSV_sua_Click(object sender, EventArgs e)
        {
            var a = DateTime.Today;
            if (Convert.ToDateTime(dateSV.Text) > a)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }else if (txtMaSV.Text == "")
            {
                MessageBox.Show("Mã sinh viên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtTenSV.Text == "")
            {
                MessageBox.Show("Tên sinh viên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cbSV_GT.Text == "")
            {
                MessageBox.Show("Giới tính không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtSDT_SV.Text == "")
            {
                MessageBox.Show("Số điện thoại không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtEmail_SV.Text == "")
            {
                MessageBox.Show("Email không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtQueQuan_SV.Text == "")
            {
                MessageBox.Show("Quê quán không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtKhoaHoc_SV.Text == "")
            {
                MessageBox.Show("Khóa học không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtMaLopHC.Text == "")
            {
                MessageBox.Show("Lớp hành chính không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                using (SqlConnection cnn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "edit_sv";
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text);
                    cmd.Parameters.AddWithValue("@TenSV ", txtTenSV.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh ", Convert.ToDateTime(dateSV.Text));
                    cmd.Parameters.AddWithValue("@GioiTinh ", cbSV_GT.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sdt ", txtSDT_SV.Text);
                    cmd.Parameters.AddWithValue("@Email ", txtEmail_SV.Text);
                    cmd.Parameters.AddWithValue("@QueQuan ", txtQueQuan_SV.Text);
                    cmd.Parameters.AddWithValue("@KhoaHoc ", txtKhoaHoc_SV.Text);
                    cmd.Parameters.AddWithValue("@MaLopHC ", txtMaLopHC.Text);

                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Sửa sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cnn.Close();
                        load_sinhvien();
                    }
                }
            }
        }

        private void btnSV_xoa_Click(object sender, EventArgs e)
        {

            DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa sinh viên có mã " + txtMaSV.Text + " không?", "xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                try
                {

                    string procName = "del_sv";

                    using (SqlConnection Cnn = new SqlConnection(conStr))
                    {
                        using (SqlCommand cmd = new SqlCommand(procName, Cnn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text);

                            Cnn.Open();
                            int rows = cmd.ExecuteNonQuery();

                            if (rows > 0)
                                MessageBox.Show("Đã xóa thành công sinh viên " + txtMaSV.Text);
                            Cnn.Close();
                            load_sinhvien();
                        }

                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Không thể xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    lsvSinhVien.Items.Clear();
                    load_sinhvien();
                }
            }
        }

        private void btnSV_Boqua_Click(object sender, EventArgs e)
        {
            txtMaSV.Text = txtTenSV.Text
                          = cbSV_GT.Text
                          = dateSV.Text = txtSDT_SV.Text = txtEmail_SV.Text = txtQueQuan_SV.Text = txtKhoaHoc_SV.Text = txtMaLopHC.Text = string.Empty;
            txtMaSV.Focus();
            // btnSV_them.Text = "thêm mới";
            // btnSV_them.Tag = string.Empty;

        }

        private void lsvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lsvSinhVien.SelectedItems)
            {
                txtMaSV.Text = item.SubItems[0].Text;
                txtTenSV.Text = item.SubItems[1].Text;
                dateSV.Text = item.SubItems[2].Text;
                cbSV_GT.Text = item.SubItems[3].Text;
                txtSDT_SV.Text = item.SubItems[4].Text;
                txtEmail_SV.Text = item.SubItems[5].Text;
                txtQueQuan_SV.Text = item.SubItems[6].Text;
                txtKhoaHoc_SV.Text = item.SubItems[7].Text;
                txtMaLopHC.Text = item.SubItems[8].Text;
                
            }
        } //Khi chọn 1 hàng ở ListView sẽ đưa các thông tin trong trường vào các textbox




        /// <summary>
        /// ////////////GIẢNG VIÊN
        /// </summary>
        void load_giangvien()
        {
            lsvGiangVien.Items.Clear();

            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblGiangVien", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaGV"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenGV"] + ""));
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", dataReader["NgaySinh"]));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["GioiTinh"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["sdt"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["Email"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["QueQuan"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TrinhDo"] + ""));
                            lsvGiangVien.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }

        //Khi chọn 1 hàng ở ListView sẽ đưa các thông tin trong trường vào các textbox
        private void lsvGiangVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lsvGiangVien.SelectedItems)
            {
                txtMaGV.Text = item.SubItems[0].Text;
                txtTenGV.Text = item.SubItems[1].Text;
                dateGV.Text = item.SubItems[2].Text;
                cbGT_GV.Text = item.SubItems[3].Text;
                txtSDT_GV.Text = item.SubItems[4].Text;
                txtEmail_GV.Text = item.SubItems[5].Text;
                txtQueQuan_GV.Text = item.SubItems[6].Text;
                txtTrinhDo_GV.Text = item.SubItems[7].Text;
            }
        }

        private void btnBoqua_GV_Click(object sender, EventArgs e)
        {

            txtMaGV.Text = txtTenGV.Text = txtSDT_GV.Text = txtEmail_GV.Text = cbGT_GV.Text
                         = dateGV.Text = txtQueQuan_GV.Text = txtTrinhDo_GV.Text = string.Empty;
            txtMaGV.Focus();
            // btnThemMoi_GV.Text = "Thêm mới";
            //btnThemMoi_GV.Tag = string.Empty;
        }

        private void btnThemMoi_GV_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_gv";
                    cmd.Parameters.AddWithValue("@MaGV", txtMaGV.Text);
                    cmd.Parameters.AddWithValue("@TenGV", txtTenGV.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtSDT_GV.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail_GV.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", cbGT_GV.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@NgaySinh", Convert.ToDateTime(dateGV.Text));
                    cmd.Parameters.AddWithValue("@QueQuan", txtQueQuan_GV.Text);
                    cmd.Parameters.AddWithValue("@TrinhDo", txtTrinhDo_GV.Text);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Thêm giảng viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cnn.Close();
                        load_giangvien();
                    }
                }
            }
        }

        private void btnSua_GV_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "edit_gv";
                    cmd.Parameters.AddWithValue("@MaGV", txtMaGV.Text);
                    cmd.Parameters.AddWithValue("@TenGV", txtTenGV.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtSDT_GV.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail_GV.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", cbGT_GV.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@NgaySinh", Convert.ToDateTime(dateGV.Text));
                    cmd.Parameters.AddWithValue("@QueQuan", txtQueQuan_GV.Text);
                    cmd.Parameters.AddWithValue("@TrinhDo", txtTrinhDo_GV.Text);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        // btnThemMoi_GV.Text = "ghi nhận";

                        MessageBox.Show("Sửa giảng viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cnn.Close();
                        load_giangvien();

                    }
                }
            }
        }

        private void btnXoa_GV_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa giảng viên có mã " + txtMaGV.Text + " không?", "xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                try
                {

                    string procName = "del_gv";

                    using (SqlConnection Cnn = new SqlConnection(conStr))
                    {
                        using (SqlCommand cmd = new SqlCommand(procName, Cnn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MaGV", txtMaGV.Text);

                            Cnn.Open();
                            int rows = cmd.ExecuteNonQuery();

                            if (rows > 0)
                                MessageBox.Show("Đã xóa thành công giảng viên " + txtMaGV.Text);
                            Cnn.Close();
                            load_giangvien();
                        }

                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Không thể xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    lsvGiangVien.Items.Clear();
                    load_giangvien();
                }
            }

        }


        //---TRẦN NGỌC BẢO
        //Lớp hành chính
        void load_lophanhchinh()
        {
            lv_lhc.Items.Clear();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select MaLopHC, TenLopHC, tblGiangVien.TenGV,SoSV, tblLopHanhChinh.MaGV from tblLopHanhChinh, tblGiangVien where tblLopHanhChinh.MaGV = tblGiangVien.MaGV ", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaLopHC"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenLopHC"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenGV"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["SoSV"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["MaGV"] + ""));

                            lv_lhc.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }
        private void lv_lhcsv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void lv_lhc_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lv_lhc.SelectedItems)
            {
                txt_malhc.Text = item.SubItems[0].Text;
                txt_tenlhc.Text = item.SubItems[1].Text;
                txt_giaovienlhc.Text = item.SubItems[2].Text;
                txt_sisolhc.Text = item.SubItems[3].Text;
            }
            lv_lhcsv.Items.Clear();
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from tblSinhVien where MaLopHC= '" + txt_malhc.Text + "'", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ListViewItem item = new ListViewItem();
                                item.Text = dataReader["MaSV"].ToString();
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenSV"] + ""));
                                item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", dataReader["NgaySinh"]));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["GioiTinh"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["sdt"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["Email"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["QueQuan"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["KhoaHoc"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["MaLopHC"] + ""));
                                lv_lhcsv.Items.Add(item);
                            }
                        }
                        conn.Close();
                    }
                }
            }
        }
        private void btn_themlhc_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insert_lophc";
                        cmd.Parameters.AddWithValue("@MaLopHC", txt_malhc.Text);
                        cmd.Parameters.AddWithValue("@TenLopHC ", txt_tenlhc.Text);
                        cmd.Parameters.AddWithValue("@MaGV ", txt_magvlhc.Text);
                        cmd.Parameters.AddWithValue("@SoSV ", txt_sisolhc.Text);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Thêm lớp học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            load_lophanhchinh();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ, hãy thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_sualhc_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "edit_lophc";
                        cmd.Parameters.AddWithValue("@MaLopHC", txt_malhc.Text);
                        cmd.Parameters.AddWithValue("@TenLopHC ", txt_tenlhc.Text);
                        cmd.Parameters.AddWithValue("@MaGV ", txt_magvlhc.Text);
                        cmd.Parameters.AddWithValue("@SoSV ", txt_sisolhc.Text);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Sửa lớp học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            load_lophanhchinh();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ, hãy thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_xoalhc_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "del_lophc";
                        cmd.Parameters.AddWithValue("@MaLopHC", txt_malhc.Text);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Xóa lớp học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            load_lophanhchinh();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btn_huylhc_Click(object sender, EventArgs e)
        {
            foreach (Control c in grb_lhc.Controls)
            {
                if (c is TextBox)
                {
                    c.ResetText();
                }
            }
        }




        //Lớp tín chỉ
        void load_loptinchi()
        {
            lv_ltc.Items.Clear();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(" select MaLopTC, tblMonHoc.TenMH,tblMonHoc.MaMH, tblGiangVien.TenGV, tblGiangVien.MaGV, SoSV from tblLopTinChi, tblMonHoc,tblGiangVien where tblLopTinChi.MaMH = tblMonHoc.MaMH and tblGiangVien.MaGV = tblLopTinChi.MaGV ", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaLopTC"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenMH"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["MaMH"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenGV"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["MaGV"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["SoSV"] + ""));
                            lv_ltc.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }

        private void lv_ltc_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lv_ltc.SelectedItems)
            {
                txt_maltc.Text = item.SubItems[0].Text;
                txt_tenmonltc.Text = item.SubItems[1].Text;
                txt_mamhltc.Text = item.SubItems[2].Text;
                txt_giaovienltc.Text = item.SubItems[3].Text;
                txt_magvltc.Text = item.SubItems[4].Text;
                txt_sosinhvien.Text = item.SubItems[5].Text;
            }

            lv_ltcsv.Items.Clear();
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = new SqlCommand("select  tblSV_LopTC.MaSV, TenSV, NgaySinh, GioiTinh, sdt, Email, QueQuan, KhoaHoc, MaLopHC, tblSV_LopTC.MaLopTC from tblSinhVien,tblSV_LopTC, tblLopTinChi where tblSV_LopTC.MaSV = tblSinhVien.MaSV and tblSV_LopTC.MaLopTC = tblLopTinChi.MaLopTC and tblLopTinChi.MaLopTC ='" + txt_maltc.Text + "'", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ListViewItem item = new ListViewItem();
                                item.Text = dataReader["MaSV"].ToString();
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenSV"] + ""));
                                item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", dataReader["NgaySinh"]));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["GioiTinh"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["sdt"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["Email"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["QueQuan"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["KhoaHoc"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["MaLopHC"] + ""));
                                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["MaLopTC"] + ""));
                                lv_ltcsv.Items.Add(item);
                            }
                        }
                        conn.Close();
                    }
                }
            }


        }
        private void btn_themltc_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insert_loptc";
                        cmd.Parameters.AddWithValue("@MaLopTC", txt_maltc.Text);
                        cmd.Parameters.AddWithValue("@MaMH ", txt_mamhltc.Text);
                        cmd.Parameters.AddWithValue("@MaGV ", txt_magvltc.Text);
                        cmd.Parameters.AddWithValue("@SoSV ", txt_sosinhvien.Text);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Thêm lớp học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            load_loptinchi();
                        }
                    }
                }
            }


            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ, hãy thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_sualtc_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "edit_loptc";
                        cmd.Parameters.AddWithValue("@MaLopTC", txt_maltc.Text);
                        cmd.Parameters.AddWithValue("@MaMH ", txt_mamhltc.Text);
                        cmd.Parameters.AddWithValue("@MaGV ", txt_magvltc.Text);
                        cmd.Parameters.AddWithValue("@SoSV ", txt_sosinhvien.Text);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("sửa lớp học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            load_loptinchi();
                        }
                    }
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ, hãy thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btn_xoaltc_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "del_loptc";
                        cmd.Parameters.AddWithValue("@MaLopTC", txt_maltc.Text);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Xóa lớp học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            load_loptinchi();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            foreach (Control c in grb_ltc.Controls)
            {
                if (c is TextBox)
                {
                    c.ResetText();
                }
            }
        }
        //---VŨ ĐỨC ANH
        //Môn học
        void load_monhoc()
        {
            lsvMonHoc.Items.Clear();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblMonHoc", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaMH"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenMH"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["SoTC"] + ""));
                            lsvMonHoc.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }

        private void lsvMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lsvMonHoc.SelectedItems)
            {
                txtMH_ma.Text = item.SubItems[0].Text;
                txtMH_ten.Text = item.SubItems[1].Text;
                txtMH_sotc.Text = item.SubItems[2].Text;
            }
            lsvMonHoc_loptc.Items.Clear();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select MaLopTC, tblMonHoc.TenMH, tblGiangVien.TenGV, SoSV from tblLopTinChi, tblMonHoc, tblGiangVien where tblLopTinChi.MaMH = tblMonHoc.MaMH and tblLopTinChi.MaMH = '" + txtMH_ma.Text + "' and tblGiangVien.MaGV = tblLopTinChi.MaGV", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaLopTC"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenMH"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenGV"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["SoSV"] + ""));
                            lsvMonHoc_loptc.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }

        private void btnMH_them_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insert_mh";
                        cmd.Parameters.AddWithValue("@MaMH", txtMH_ma.Text);
                        cmd.Parameters.AddWithValue("@TenMH ", txtMH_ten.Text);
                        cmd.Parameters.AddWithValue("@SoTC ", txtMH_sotc.Text);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Thêm môn học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            load_monhoc();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ, hãy thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMH_sua_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "edit_mh";
                        cmd.Parameters.AddWithValue("@MaMH", txtMH_ma.Text);
                        cmd.Parameters.AddWithValue("@TenMH ", txtMH_ten.Text);
                        cmd.Parameters.AddWithValue("@SoTC ", txtMH_sotc.Text);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Sửa môn học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            load_monhoc();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ, hãy thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMH_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "del_mh";
                        cmd.Parameters.AddWithValue("@MaMH", txtMH_ma.Text);
                        cnn.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Xóa môn học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cnn.Close();
                            load_monhoc();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa. Hãy xóa lớp tín chỉ của môn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMH_huy_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox2.Controls)
            {
                if (c is TextBox)
                {
                    c.ResetText();
                }
            }
        }

        //Bảng điểm
        void load_svbangdiem()
        {
            lsvBangDiem_sv.Items.Clear();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select MaSV, TenSV, NgaySinh, MaLopHC from tblSinhVien", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaSV"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenSV"] + ""));
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", dataReader["NgaySinh"]));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["MaLopHC"] + ""));
                            lsvBangDiem_sv.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }

        void load_bangdiem()
        {
            lsvBangDiem.Items.Clear();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select tblBangDiemCN.MaLopTC, tblMonHoc.TenMH, tblGiangVien.TenGV, DiemCC, DiemGK, DiemCK, Diem, Case when DiemCK is null then N'Đang học' when Diem < 4 then N'Trượt' else N'Qua môn' end as TrangThai from tblBangDiemCN, tblMonHoc, tblLopTinChi, tblGiangVien where tblBangDiemCN.MaSV = '" + txtBD_masv.Text + "' and tblBangDiemCN.MaLopTC = tblLopTinChi.MaLopTC and tblLopTinChi.MaMH = tblMonHoc.MaMH and tblLopTinChi.MaGV = tblGiangVien.MaGV", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaLopTC"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenMH"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenGV"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["DiemCC"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["DiemGK"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["DiemCK"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["Diem"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TrangThai"] + ""));
                            lsvBangDiem.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }

        private void lsvBangDiem_sv_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lsvBangDiem_sv.SelectedItems)
            {
                txtBD_masv.Text = item.SubItems[0].Text;
                txtBD_tensv.Text = item.SubItems[1].Text;
                txtBD_ns.Text = item.SubItems[2].Text;
                txtBD_lophc.Text = item.SubItems[3].Text;

            }
            lsvBangDiem.Items.Clear();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select tblBangDiemCN.MaLopTC, tblMonHoc.TenMH, tblGiangVien.TenGV, DiemCC, DiemGK, DiemCK, Diem, Case when DiemCK is null then N'Đang học' when Diem < 4 then N'Trượt' else N'Qua môn' end as TrangThai from tblBangDiemCN, tblMonHoc, tblLopTinChi, tblGiangVien where tblBangDiemCN.MaSV = '" + txtBD_masv.Text + "' and tblBangDiemCN.MaLopTC = tblLopTinChi.MaLopTC and tblLopTinChi.MaMH = tblMonHoc.MaMH and tblLopTinChi.MaGV = tblGiangVien.MaGV", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaLopTC"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenMH"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenGV"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["DiemCC"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["DiemGK"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["DiemCK"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["Diem"] + ""));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TrangThai"] + ""));
                            lsvBangDiem.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }

        private void lsvBangDiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lsvBangDiem.SelectedItems)
            {
                txtBD_loptc.Text = item.SubItems[0].Text;
                txtBD_tenmh.Text = item.SubItems[1].Text;
                txtBD_tengv.Text = item.SubItems[2].Text;
                txtBD_cc.Text = item.SubItems[3].Text;
                txtBD_gk.Text = item.SubItems[4].Text;
                txtBD_ck.Text = item.SubItems[5].Text;
            }
            foreach (ListViewItem item in lsvBangDiem_sv.SelectedItems)
            {
                txtBD_masv.Text = item.SubItems[0].Text;
                txtBD_tensv.Text = item.SubItems[1].Text;
                txtBD_ns.Text = item.SubItems[2].Text;
                txtBD_lophc.Text = item.SubItems[3].Text;

            }
        }

        private void btnBD_update_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(conStr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cnn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "edit_bangdiem";
                        cmd.Parameters.AddWithValue("@DiemCC", txtBD_cc.Text);
                        cmd.Parameters.AddWithValue("@DiemGK ", txtBD_gk.Text);
                        if (txtBD_ck.Text == "")
                        {
                            cmd.Parameters.AddWithValue("@DiemCK", DBNull.Value);
                        }
                        else
                            cmd.Parameters.AddWithValue("@DiemCK", txtBD_ck.Text);
                        cmd.Parameters.AddWithValue("@Diem", 0);
                        cmd.Parameters.AddWithValue("@MaSV ", txtBD_masv.Text);
                        cmd.Parameters.AddWithValue("@MaLopTC ", txtBD_loptc.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cnn.Close();
                        load_bangdiem();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không hợp lệ, hãy thử lại: Error: '" + txtBD_ck.Text + "'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnBD_in_Click(object sender, EventArgs e)
        {
            CRBangDiem frm = new CRBangDiem();
            BangDiem cr = new BangDiem();
            cr.RecordSelectionFormula = "{tblBangDiemCN.MaSV} = '" + txtBD_masv.Text + "'";
            frm.crystalReportViewer1.ReportSource = cr;
            cr.SetDatabaseLogon("sa", "1");
            frm.crystalReportViewer1.RefreshReport();
            frm.ShowDialog();
        }

        private void btnBD_search_Click(object sender, EventArgs e)
        {
            lsvBangDiem_sv.Items.Clear();
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("select MaSV, TenSV, NgaySinh, MaLopHC from tblSinhVien where TenSV = N'" + txtBD_search.Text + "'", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = dataReader["MaSV"].ToString();
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["TenSV"] + ""));
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", dataReader["NgaySinh"]));
                            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dataReader["MaLopHC"] + ""));
                            lsvBangDiem_sv.Items.Add(item);
                        }
                    }
                    conn.Close();

                }
            }
        }

        private void txtBD_search_TextChanged(object sender, EventArgs e)
        {
            if (txtBD_search.Text == "")
            {
                load_svbangdiem();
            }
        }

        private void dateSV_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

