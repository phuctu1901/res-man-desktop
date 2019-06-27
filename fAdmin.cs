using RestaurantManager.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantManager.DTO;
using RestSharp;
using RestaurantManager.Caller;
using RestaurantManager.Models;
namespace RestaurantManager
{
    public partial class fAdmin : MetroFramework.Forms.MetroForm
    {
        BindingSource listFood = new BindingSource();
        BindingSource listCategory = new BindingSource();
        BindingSource listTable = new BindingSource();
        BindingSource listTableStatus = new BindingSource();
        BindingSource listAcc = new BindingSource();
        BindingSource listUnit = new BindingSource();
        
        int Type;
        public fAdmin(int type)
        {
            Type = type;
            InitializeComponent();
        
            load();
        }

      

        public void load()
        {
            loadDate();
            getListBill(metroDateTimeFromDate.Value, metroDateTimeToDate.Value.AddDays(1));
            //FoodList
            loadFoodList();

            dataGridViewListFood.DataSource = listFood;
            loadCategory();
            addFoodBinding();
            //CategoryList
            loadListCategory();
            dataGridViewListFoodCategory.DataSource = listCategory;
            addCategoryBinding();
            //TableFoodList
            loadListTable();
            
            dataGridViewListTable.DataSource = listTable;
            addTableBinding();
            //AccList
            loadListAcc();
            dataGridViewListAccount.DataSource = listAcc;
            loadTypeAcc();
            addAccBinding();

            //UnitList
            loadUnitList();
            loadTableStatus();

        }
        public void loadDate()
        {
            DateTime now = DateTime.Now;
            metroDateTimeFromDate.Value = new DateTime(now.Year, now.Month, 1);
            metroDateTimeToDate.Value = metroDateTimeFromDate.Value.AddDays(-1).AddMonths(1);
        }
        public void getListBill(DateTime fromDate, DateTime toDate)
        {
            dataGridViewListBill.DataSource = BillDAO.Instance.getListBillByDate(fromDate, toDate);

        }


        public void loadFoodList()
        {
            var foodcaller = new RestSharpCaller<RestaurantManager.Models.Food>("http://localhost:8000/api");
            var foods = foodcaller.Get("food");
            listFood.DataSource = foods;
            //listFood.DataSource= FoodDAO.Instance.loadFoodList();// chú ý gán BindingSource
        }


        public void loadUnitList()
        {
            var unitcaller = new RestSharpCaller<RestaurantManager.Models.Unit>("http://localhost:8000/api");
            var units = unitcaller.Get("unit");
            listUnit.DataSource = units;
            cbUnit.DataSource = units;
            cbUnit.ValueMember = "id";
            cbUnit.DisplayMember = "title";
        }

       
        public void addFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text", dataGridViewListFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dataGridViewListFood.DataSource, "title", true, DataSourceUpdateMode.Never));
            numericUpDownPrice.DataBindings.Add(new Binding("Value", dataGridViewListFood.DataSource, "price", true, DataSourceUpdateMode.Never));
            cbFoodCategory.DataBindings.Add(new Binding("SelectedValue", dataGridViewListFood.DataSource, "foodcategory_id", true, DataSourceUpdateMode.Never));
            cbUnit.DataBindings.Add(new Binding("SelectedValue", dataGridViewListFood.DataSource, "unit_id", true, DataSourceUpdateMode.Never));
        }
        
        public void loadCategory()
        {
            //cbFoodCategory.DataSource = CategoryDAO.Instance.loadCategory();
            var foodcategorycaller = new RestSharpCaller<RestaurantManager.Models.FoodCategory>("http://localhost:8000/api");
            var foodcategories = foodcategorycaller.Get("foodcategory");
            cbFoodCategory.DataSource = foodcategories;
            cbFoodCategory.ValueMember = "id";
            cbFoodCategory.DisplayMember = "title";

        }
        public void loadListCategory()
        {
            var foodcategorycaller = new RestSharpCaller<RestaurantManager.Models.FoodCategory>("http://localhost:8000/api");
            var foodcategories = foodcategorycaller.Get("foodcategory");
            listCategory.DataSource = foodcategories;
            //listCategory.DataSource = CategoryDAO.Instance.loadCategory2();
        }
        public void addCategoryBinding()
        {
            txbCateID.DataBindings.Add(new Binding("Text", dataGridViewListFoodCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbCateName.DataBindings.Add(new Binding("Text", dataGridViewListFoodCategory.DataSource, "title", true, DataSourceUpdateMode.Never));
        }
        public void loadListTable()
        {
            var tablecaller = new RestSharpCaller<RestaurantManager.Models.Table>("http://localhost:8000/api");
            var tables = tablecaller.Get("table");
            listTable.DataSource = tables;
        }

        public void loadTableStatus()
        {
            var tablestatuscaller = new RestSharpCaller<RestaurantManager.Models.TableStatus>("http://localhost:8000/api");
            var tablestatus = tablestatuscaller.Get("tablestatus");
            listTableStatus.DataSource = tablestatus;
            cbTableStatus.DataSource = tablestatus;
            cbTableStatus.ValueMember = "id";
            cbTableStatus.DisplayMember = "title";
        }
    

        public void addTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dataGridViewListTable.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dataGridViewListTable.DataSource, "title", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("SelectedValue", dataGridViewListTable.DataSource, "tablestatus_id", true, DataSourceUpdateMode.Never));
        }
        public void loadListAcc()
        {
            if (Type == 1) listAcc.DataSource = AccountDAO.Instance.loadListAccForAdmin();
            else listAcc.DataSource = AccountDAO.Instance.loadListAccForRoot();
        }
        public void loadTypeAcc()
        {
            if (Type == 1) cbAccType.Items.Add(0);
            else
            {
                cbAccType.Items.Add(0);
                cbAccType.Items.Add(1);
            }
        }
        public void addAccBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dataGridViewListAccount.DataSource, "Tên đăng nhập", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("Text", dataGridViewListAccount.DataSource, "Tên hiển thị", true, DataSourceUpdateMode.Never));
            cbAccType.DataBindings.Add(new Binding("Text", dataGridViewListAccount.DataSource, "Loại tài khoản", true, DataSourceUpdateMode.Never));
        }
        private void btnPay_Click(object sender, EventArgs e)
        {
            DateTime toDate = metroDateTimeToDate.Value.AddDays(1);
            getListBill(metroDateTimeFromDate.Value, toDate);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            loadFoodList();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            int idCate = (cbFoodCategory.SelectedValue as Category).Id;
            if (FoodDAO.Instance.insertFood(txbFoodName.Text, idCate, (float)numericUpDownPrice.Value))
            {
                MessageBox.Show("Thêm món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Thêm món ăn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadFoodList();
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int idCate = (cbFoodCategory.SelectedValue as Category).Id;
            if (FoodDAO.Instance.updateFood(txbFoodName.Text, idCate, (float)numericUpDownPrice.Value, Convert.ToInt32(txbFoodID.Text)))
            {
                MessageBox.Show("Sửa món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Sửa món ăn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadFoodList();
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            BillInfoDAO.Instance.deleteBillInfoByIdFood(Convert.ToInt32(txbFoodID.Text));
            if (FoodDAO.Instance.deleteFood(Convert.ToInt32(txbFoodID.Text)))
            {
                MessageBox.Show("Xóa món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Xóa món ăn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadFoodList();
        }





        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            listFood.DataSource = FoodDAO.Instance.getListFoodByName(txbSerarchFood.Text);

        }


        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);
            if (TableDAO.Instance.deleteTable(id))
            {
                MessageBox.Show("Xóa bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Xóa bàn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListTable();
        }

        private void btnEditCate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCateID.Text);
            if (CategoryDAO.Instance.updateCate(txbCateName.Text, id))
            {
                MessageBox.Show("Sửa danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Sửa danh mục thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListCategory();
            loadFoodList();
            loadCategory();
        }

        private void btnDeleteCate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCateID.Text);
            if (CategoryDAO.Instance.deleteCate(id))
            {
                MessageBox.Show("Xóa danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Xóa danh mục thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListCategory();
            loadFoodList();
            loadCategory();
        }

        private void btnAddCate_Click(object sender, EventArgs e)
        {
            if (CategoryDAO.Instance.insertCate(txbCateName.Text))
            {
                MessageBox.Show("Thêm danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Thêm danh mục thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListCategory();
            loadFoodList();
            loadCategory();

        }

        private void tabPageTable_Click(object sender, EventArgs e)
        {

        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            if (TableDAO.Instance.updateTable(Convert.ToInt32(txbTableID.Text), txbTableName.Text, cbTableStatus.Text))
            {
                MessageBox.Show("Sửa bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Sửa bàn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if (cbTableStatus.Text == "") MessageBox.Show("Vui lòng chọn trạng thái của bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (TableDAO.Instance.insertTable(txbTableName.Text, cbTableStatus.Text))
                {
                    MessageBox.Show("Thêm bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Thêm bàn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadListTable();
            }
        }



        private void btnEditAcc_Click(object sender, EventArgs e)
        {

            if (AccountDAO.Instance.updateAcc(txbUserName.Text, txtDisplayName.Text, Convert.ToInt32(cbAccType.Text)))
            {
                MessageBox.Show("Sửa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Sửa tài khoản thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListAcc();
        }

        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            if (AccountDAO.Instance.deleteAcc(txbUserName.Text))
            {
                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Xóa tài khoản thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListAcc();
        }

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            if (txbPass.Text == "") MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (AccountDAO.Instance.checkUserName(txbUserName.Text) == 1)
                    MessageBox.Show("Tên đăng nhập bị trùng, vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (AccountDAO.Instance.insertAcc(txbUserName.Text, txtDisplayName.Text, txbPass.Text, Convert.ToInt32(cbAccType.Text)))
                    {
                        MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Thêm tài khoản thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadListAcc();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fReport f = new fReport(metroDateTimeFromDate.Value, metroDateTimeToDate.Value);
            f.ShowDialog();
        }

      
    }
}
