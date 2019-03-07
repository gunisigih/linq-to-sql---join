using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQTOSQL_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NorthwindDataContext ctx = new NorthwindDataContext();
        private void Form1_Load(object sender, EventArgs e)
        {
            var query = from p in ctx.Products
                        join od in ctx.Order_Details on p.ProductID equals od.ProductID
                        join o in ctx.Orders on od.OrderID equals o.OrderID
                        join c in ctx.Customers on o.CustomerID equals c.CustomerID
                        select new
                        {
                            p.ProductID,
                            o.OrderID,
                            c.CustomerID,
                            p.ProductName,
                            p.UnitsInStock,
                            od.UnitPrice,
                            o.OrderDate,
                            c.CompanyName
                        };
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = query;
            dataGridView1.Columns["OrderID"].Visible = false;
            dataGridView1.Columns["CustomerID"].Visible = false;
            dataGridView1.Columns["ProductID"].Visible = false;
        }

        private void btnGroupby_Click(object sender, EventArgs e)
        {
            Order_Detail odd = new Order_Detail();

            var query = from p in ctx.Products
                        join od in ctx.Order_Details on p.ProductID equals od.ProductID
                        join o in ctx.Orders on od.OrderID equals o.OrderID
                        join c in ctx.Customers on o.CustomerID equals c.CustomerID
                        group od by p.ProductName into table
                        orderby table.Key
                        select new
                        {
                            table.Key,
                            SatisAdedi = table.Count(),
                            MinSatis=table.Min(x=>x.Quantity),
                            MaxSatis=table.Max(x=>x.Quantity),
                            ToplamSatis=table.Sum(x=>x.Quantity),
                            ToplamMiktar = table.Sum(x => x.Quantity * x.UnitPrice)
                        };
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = query;
            //dataGridView1.Columns["OrderID"].Visible = false;
            //dataGridView1.Columns["CustomerID"].Visible = false;
            //dataGridView1.Columns["ProductID"].Visible = false;
        }
    }
}
