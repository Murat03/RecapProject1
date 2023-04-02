using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecapProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListProducts();
            ListCategories();
        }

        private void ListProducts()
        {
            using (BikeStoresContext context = new BikeStoresContext())
            {
                dgwProduct.DataSource = context.Products.ToList();
            }
        }
        private void ListProductsByCategory(int categoryId)
        {
            using (BikeStoresContext context = new BikeStoresContext())
            {
                dgwProduct.DataSource = context.Products.Where(p=>p.category_id==categoryId).ToList();
            }
        }
        private void ListProductsByContains(string containString)
        {
            using (BikeStoresContext context = new BikeStoresContext())
            {
                dgwProduct.DataSource = context.Products.Where(p => p.product_name.ToLower().Contains(containString.ToLower())).ToList();
            }
        }
        private void ListCategories()
        {
            using (BikeStoresContext context = new BikeStoresContext())
            {
                cbxCategory.DataSource = context.Categories.ToList();
                cbxCategory.DisplayMember = "category_name";
                cbxCategory.ValueMember = "category_id";
            }
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch(Exception exception)
            {
                
            }

        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            string key = tbxSearch.Text;
            if(string.IsNullOrEmpty(key))
            {
                ListProducts();
            }
            else
            {
                ListProductsByContains(tbxSearch.Text.ToString());
            }
        }
    }
}
