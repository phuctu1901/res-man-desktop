﻿using RestaurantManager.Caller;
using RestaurantManager.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RestaurantManager
{
    public partial class fTableManager : MetroFramework.Forms.MetroForm
    {
        string UserName;
        string DisplayName;
        string PassWord;
        int Type;
        public fTableManager(string displayName)
        {
            DisplayName = displayName;
            Type = 1;
            InitializeComponent();
            loadListTable();
            loadCategory();
            loadFoodListbyCategory((int)cbCategory.SelectedValue);
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin cá nhân ( " + DisplayName + " )";
        }
        #region method
        public string getTableStatusByID(int id)
        {
            var tablestatuscaller = new RestSharpCaller<RestaurantManager.Models.TableStatus>("http://localhost:8000/api");
            List<RestaurantManager.Models.TableStatus> tablestatus = tablestatuscaller.Get("tablestatus");
            string result = tablestatus.Find(item => item.id == id).title;
            return result;
        }
        public void loadListTable()
        {
            flowLayoutPanel1.Controls.Clear();
            var tablecaller = new RestSharpCaller<RestaurantManager.Models.Table>("http://localhost:8000/api");
            var tables = tablecaller.Get("table");
            List<RestaurantManager.Models.Table> listTable = tables;
            foreach (RestaurantManager.Models.Table item in listTable)
            {
                Button button = new Button();
                int id = item.tablestatus_id;
                button.Text = item.title + "\n" + getTableStatusByID(id);
                button.Width = 80;
                button.Height = 80;
                button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                button.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
                button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
                button.BackColor = Color.FromArgb(17, 17, 17);
                button.Tag = item;
                if (item.tablestatus_id == 0)
                {
                    button.ForeColor = Color.FromArgb(243, 119, 53);
                    button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(243, 119, 53);
                }
                if (item.tablestatus_id != 0)
                {
                    button.ForeColor = Color.SeaGreen;
                    button.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
                }
                button.Click += Button_Click;
                flowLayoutPanel1.Controls.Add(button);

            }
            cbListTable.DataSource = listTable;
            cbListTable.DisplayMember = "title";
        }
        public void loadTable(int idTable)
        {
            var tablecaller = new RestSharpCaller<RestaurantManager.Models.Table>("http://localhost:8000/api");
            RestaurantManager.Models.Table table = tablecaller.GetSingle("table/" + idTable);

            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if ((item.Tag as RestaurantManager.Models.Table).id == table.id)
                {

                    item.Text = table.title + "\n" + getTableStatusByID(table.tablestatus_id);
                    if (table.tablestatus_id == 0)
                    {

                        item.ForeColor = Color.FromArgb(243, 119, 53);
                        (item as Button).FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(243, 119, 53);
                    }
                    else
                    {
                        item.ForeColor = Color.SeaGreen;
                        (item as Button).FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
                    }
                }
            }

        }

        public void loadMenuByIdTable(int idTable)
        {
            CultureInfo culture = new CultureInfo("vi-VN");

            var menucaller = new RestSharpCaller<RestaurantManager.Models.Menu>("http://localhost:8000/api");
            List<RestaurantManager.Models.Menu> menu = menucaller.Get("loadMenuByTableId/" + idTable);

            listViewMenu.Items.Clear();
            foreach (RestaurantManager.Models.Menu item in menu)
            {
                ListViewItem lsvItem = new ListViewItem(item.title);
                lsvItem.SubItems.Add(item.count.ToString());
                lsvItem.SubItems.Add(item.food_price.ToString("c", culture));
                lsvItem.SubItems.Add(item.food_price.ToString("c", culture));
                listViewMenu.Items.Add(lsvItem);
            }

        }
        public void loadFinalTotalPrice(int idTable)
        {
            float finalTotalPrice = 0;
            var menucaller = new RestSharpCaller<RestaurantManager.Models.Menu>("http://localhost:8000/api");
            List<RestaurantManager.Models.Menu> menu = menucaller.Get("loadMenuByTableId/" + idTable);
            foreach (RestaurantManager.Models.Menu item in menu)
            {
                finalTotalPrice += item.total_price;
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalPrice.Text = finalTotalPrice.ToString("c", culture);

        }
        public void loadCategory()
        {
            var foodcategorycaller = new RestSharpCaller<RestaurantManager.Models.FoodCategory>("http://localhost:8000/api");
            var foodcategories = foodcategorycaller.Get("foodcategory");
            cbCategory.DataSource = foodcategories;
            cbCategory.ValueMember = "id";
            cbCategory.DisplayMember = "title";
        }

        public void loadFoodListbyCategory(int id)
        {
            var foodcaller = new RestSharpCaller<RestaurantManager.Models.Food>("http://localhost:8000/api");
            var foods = foodcaller.Get("food/" + id);
            cbFood.DataSource = foods;
            cbFood.ValueMember = "id";
            cbFood.DisplayMember = "title";
        }

        #endregion
        #region event
        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(217, 235, 249);
            btn.BackColor = Color.FromArgb(217, 235, 249);
            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if (item != btn)
                {
                    (item as Button).BackColor = System.Drawing.Color.FromArgb(17, 17, 17);

                }
            }
            RestaurantManager.Models.Table table = btn.Tag as RestaurantManager.Models.Table;
            listViewMenu.Tag = btn.Tag;
            loadMenuByIdTable(table.id);
            loadFinalTotalPrice(table.id);
        }


        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Type != 0)
            {
                this.Hide();
                fAdmin f = new fAdmin(Type);
                f.ShowDialog();
                this.Show();
                loadCategory();

                if (listViewMenu.Tag as Table != null)
                {
                    int idTable = (listViewMenu.Tag as Table).id;
                    loadListTable();
                    loadMenuByIdTable(idTable);
                }
            }
        }

        //private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    fAccountProfile f = new fAccountProfile(UserName, DisplayName, PassWord, Type);
        //    f.ShowDialog();
        //    this.Show();
        //    string Name = AccountDAO.Instance.getDisplayNameByUserName(UserName).DisplayName;
        //    thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin cá nhân ( " + Name + " )";
        //    DisplayName = Name;
        //}



        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            
            loadFoodListbyCategory((int) cb.SelectedValue);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {


            if (listViewMenu.Tag as RestaurantManager.Models.Table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbFood.SelectedValue == null) return;
            int idTable = (listViewMenu.Tag as RestaurantManager.Models.Table).id;


            var client = new RestClient("http://localhost:8000/api");

            var request = new RestRequest("getBillUnPaid/" + idTable, Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var queryResult = client.Execute(request);


            string idCurrentBill = queryResult.Content;
            int idFood = (int)cbFood.SelectedValue;
            int count = (int)numericUpDownCountFood.Value;
            if (idCurrentBill == "0")
            {
                var addBillRequest = new RestRequest("addBill", Method.POST);
                addBillRequest.AddParameter("table_id", idTable);
                var queryAddBillResult = client.Execute(addBillRequest);

                queryResult = client.Execute(request);
                idCurrentBill = queryResult.Content;

                var addBillInfoRequest = new RestRequest("addBillInfo", Method.POST);
                addBillInfoRequest.AddParameter("bill_id", idCurrentBill);
                addBillInfoRequest.AddParameter("food_id", idFood);
                addBillInfoRequest.AddParameter("food_count", count);
                addBillInfoRequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var queryAddBillInfoResult = client.Execute(addBillInfoRequest);
            }
            else
            {
                var addBillInfoRequest = new RestRequest("addBillInfo", Method.POST);
                addBillInfoRequest.AddParameter("bill_id", idCurrentBill);
                addBillInfoRequest.AddParameter("food_id", idFood);
                addBillInfoRequest.AddParameter("food_count", count);
                addBillInfoRequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var queryResult1 = client.Execute(addBillInfoRequest);
            }
            numericUpDownCountFood.Value = 1;
            loadFinalTotalPrice(idTable);
            loadMenuByIdTable(idTable);
            //loadListTable();
            loadTable(idTable);
        }


        private void btnPay_Click(object sender, EventArgs e)
        {
            RestaurantManager.Models.Table table = listViewMenu.Tag as RestaurantManager.Models.Table;
            var client = new RestClient("http://localhost:8000/api");
            var request = new RestRequest("getBillUnPaid/" + table.id, Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);


            string idCurrentBill = queryResult.Content;

           
            if (idCurrentBill != "0")
            {
                
                if (MessageBox.Show("Bạn có chắc thanh toán cho " + table.title + " không?", "Thanh toán", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    var addDiscountRequest = new RestRequest("discount", Method.POST);
                    addDiscountRequest.AddParameter("bill_id", idCurrentBill);
                    addDiscountRequest.AddParameter("discount", (float)numericUpDownDiscount.Value);
                    addDiscountRequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                    var queryResult1 = client.Execute(addDiscountRequest);


                    var checkOutRequest = new RestRequest("checkout", Method.POST);
                    checkOutRequest.AddParameter("bill_id", idCurrentBill);
                    addDiscountRequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                    var checkOutQueryResult = client.Execute(checkOutRequest);
                    if (numericUpDownDiscount.Value != 0)
                    {
                        double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]) * 1000;
                        double final_price = totalPrice - (totalPrice / 100) * (float)numericUpDownDiscount.Value;
                        string text = "Số tiền bạn cần thanh toán sau khi giảm giá " + numericUpDownDiscount.Value + "% là:\n" + totalPrice.ToString() + " - " + totalPrice.ToString() + " * " + numericUpDownDiscount.Value + "% =" + final_price.ToString() + " VND";
                        MessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //SẼ PHÁT TRIỂN THÊM IN HÓA ĐƠN FILE EXCEL
                    }
                    loadMenuByIdTable(table.id);
                    CultureInfo culture = new CultureInfo("vi-VN");
                    int finalPrice = 0;
                    txbTotalPrice.Text = finalPrice.ToString("c", culture);
                }

            }
            loadFinalTotalPrice(table.id);
            loadTable(table.id);
            //loadListTable();
            numericUpDownDiscount.Value = 0;
        }


        //private void btnSwichTable_Click(object sender, EventArgs e)
        //{
        //    if ((listViewMenu.Tag as Table).title != cbListTable.Text)
        //    {
        //        if (MessageBox.Show("Bạn có chắc chuyển " + (listViewMenu.Tag as Table).title + " cho " + cbListTable.Text + " không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
        //        {
        //            int id = (cbListTable.SelectedValue as Table).id;
        //            TableDAO.Instance.switchTable((listViewMenu.Tag as Table).id, id);
        //            //loadListTable();
        //            loadTable((listViewMenu.Tag as Table).Id);
        //            loadTable(id);
        //            loadMenuByIdTable((listViewMenu.Tag as Table).Id);
        //        }
        //    }
        //}

        private void lịchSửToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fHistoryPay f = new fHistoryPay();
            f.ShowDialog();
        }

        #endregion

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }

        //private void chuyểnBànToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    btnSwichTable_Click(this, new EventArgs());
        //}

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPay_Click(this, new EventArgs());
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/ndc07.it");
        }

        private void listViewMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}