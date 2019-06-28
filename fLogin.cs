using RestaurantManager.DAO;
using RestaurantManager.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManager
{
    public partial class fLogin:MetroFramework.Forms.MetroForm
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không?","Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
        private void btnLogin_Click(object sender, EventArgs e)
        {
            AccountDAO accountDAO = new AccountDAO();
            var respone = accountDAO.Login(txbUser.Text, txbPassWord.Text);
            if (respone.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                var token = respone.Data.access_token;
                Program.access_token = token;
                var getdetail_respone = accountDAO.GetDetail();
                if (getdetail_respone.Data.isAdmin)
                {
                    this.Hide();
                   fTableManager f = new fTableManager(getdetail_respone.Data.name);
                    f.ShowDialog();
                    txbPassWord.Clear();
                    txbUser.Clear();
                    txbUser.Focus();
                }
                else
                {
                    MessageBox.Show("Tài khoản của bạn không đủ quyền hạn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("Tên đăng nhận không tồn tại hoặc mật khẩu sai", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
