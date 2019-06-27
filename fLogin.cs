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
            var client = new RestClient("http://localhost:8000/api");

            //client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", bearerToken));


            var request = new RestRequest("auth/login ", Method.POST);
            Login obj = new Login(txbUser.Text, txbPassWord.Text, true);
            request.AddJsonBody(obj);
            var respone = client.Execute<Token>(request);
           
           
            if (respone.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                var token = respone.Data.access_token;
                Program.access_token = token;
                var getUserRequest = new RestRequest("auth/user ", Method.GET);
                client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", Program.access_token));
                var respone1 = client.Execute<User>(getUserRequest);
                if (respone1.Data.isAdmin)
                {
                    this.Hide();
                   fTableManager f = new fTableManager(respone1.Data.name);
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
