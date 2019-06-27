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
using RestaurantManager.Caller;
using RestaurantManager.Models;
using RestSharp;

namespace RestaurantManager
{
    public partial class fHistoryPay : MetroFramework.Forms.MetroForm
    {
        public fHistoryPay()
        {
            
            InitializeComponent();
            loadListPay(metroDateTimeSelectDate.Value);
        }
       public void loadListPay(DateTime date)
        {
            listViewListPay.Items.Clear();
        
            CultureInfo culture = new CultureInfo("vi-VN");

            var client = new RestClient("http://localhost:8000/api");
            var loadPayHistoryRequest = new RestRequest("loadpayhistory", Method.POST);
            loadPayHistoryRequest.AddParameter("date", date.Year +"-"+date.Month+"-"+date.Day);
            var response = client.Execute<List<HistoryPay>>(loadPayHistoryRequest);
            
            var pay = response.Data;

            foreach (HistoryPay item in pay)
            {
                ListViewItem lsvItem = new ListViewItem(item.title);
                lsvItem.SubItems.Add(item.checkin.ToString());
                lsvItem.SubItems.Add(item.checkout.ToString());
                lsvItem.SubItems.Add(item.discount.ToString()+"%");
                lsvItem.SubItems.Add(item.total.ToString("c",culture));
                lsvItem.SubItems.Add(item.afterdiscount.ToString("c", culture));
                listViewListPay.Items.Add(lsvItem);
            }
        }

        private void metroDateTimeSelectDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = (sender as MetroFramework.Controls.MetroDateTime).Value;
            loadListPay(date);
        }
    }
}
