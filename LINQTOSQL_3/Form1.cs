using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQTOSQL_3
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
            var query = ctx.Products.Join(ctx.Categories,
                                    p => p.CategoryID,
                                    c => c.CategoryID,
                                    (p, c) => new
                                    {
                                        p.SupplierID,
                                        p.ProductName,
                                        p.UnitPrice,
                                        p.UnitsInStock,
                                        c.CategoryName
                                    }).Join(ctx.Suppliers,
                                     a => a.SupplierID,
                                     b => b.SupplierID,

                                    (a, b) => new
                                    {
                                        a.ProductName,
                                        a.UnitPrice,
                                        a.UnitsInStock,
                                        a.CategoryName,
                                        b.CompanyName
                                    });

            dataGridView1.DataSource = query;










        }
    }
}
