using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace POSAndInventorySystem
{
    public class DBConnection
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        private double dailysales;
        private int productline;
        private int stockonhand;
        private int criticalitems;
      
        public string MyConnection() 
        {
              string con = @"Data Source=DESKTOP-5DN3RJN;Initial Catalog=POSAndInventorySystem;Integrated Security=True";
              //string con = @"Data Source=DESKTOP-5DN3RJN;Initial Catalog=POSAndInventorySystem;User ID=sa;Password=1234"; // For Client
              //string con = @"Data Source=192.168.1.1,1433;Initial Catalog=POSAndInventorySystem;User ID=sa;Password=1234"; // For Client
              return con;
        }

        public double DailySales()
        {
            string sdate = DateTime.Now.ToShortDateString();
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand("SELECT isnull(SUM(total),0) as total FROM tblCart WHERE sdate between '"+ sdate +"' AND '"+ sdate +"' AND status LIKE 'SOLD'", cn);
            dailysales = double.Parse(cm.ExecuteScalar().ToString());
            cn.Close();
            return dailysales;
        }

        public double ProductLine()
        {
            string sdate = DateTime.Now.ToShortDateString();
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand("SELECT count(*) FROM tblProduct", cn);
            productline = int.Parse(cm.ExecuteScalar().ToString());
            cn.Close();
            return productline;
        }
        public double StockOnHand()
        {
            string sdate = DateTime.Now.ToShortDateString();
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand("SELECT isnull(SUM(qty),0) as qty FROM tblProduct", cn);
            stockonhand = int.Parse(cm.ExecuteScalar().ToString());
            cn.Close();
            return stockonhand;
        }

        public double CriticalItems()
        {
            string sdate = DateTime.Now.ToShortDateString();
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand("SELECT count(*) FROM vwCriticalItems", cn);
            criticalitems = int.Parse(cm.ExecuteScalar().ToString());
            cn.Close();
            return criticalitems;
        }

        public double GetVal() 
        {
            double vat = 0;
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand("SELECT  * FROM tblVat", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                vat = Double.Parse(dr["vat"].ToString());
            }
            dr.Close();
            cn.Close();
            return vat;
        }

        public string GetPassword(string user)
        {
            string password ="";

            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand("SELECT  * FROM tblUser WHERE username = @username", cn);
            cm.Parameters.AddWithValue("@username", user);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                password = dr["password"].ToString();
            }           
            dr.Close();
            cn.Close();
            return password;
        }
    }
}
