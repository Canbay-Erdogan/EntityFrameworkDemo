using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDemo
{
    public partial class Form1 : Form
    {

        //ETradeContext _context = new ETradeContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (ETradeContext _context = new ETradeContext())
            {
               var products = _context.Products.ToList();
               dgwProducts.DataSource = products;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductDal productDal = new ProductDal();
            Product product = new Product
            {
                Name = txtName.Text,
                StockAmount = Convert.ToInt32(txtStock.Text),
                UnitPrice = Convert.ToDecimal(txtPrice.Text)
            };
            productDal.Add(product);
            dgwProducts.DataSource = productDal.GetAll();
            MessageBox.Show("Added");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ProductDal productDal = new ProductDal();
            int id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value);
            productDal.Delete(id);
            dgwProducts.DataSource= productDal.GetAll();

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            ProductDal _productDal = new ProductDal();
            Product product = new Product
            {
                Id = Convert.ToInt32(tbxId.Text),
                Name = txtNameUp.Text,
                UnitPrice = Convert.ToDecimal(txtPriceUp.Text),
                StockAmount = Convert.ToInt32(txtStockUp.Text)
            };
            _productDal.Update(product);
            dgwProducts.DataSource = _productDal.GetAll();
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxId.Text = dgwProducts.CurrentRow.Cells[0].Value.ToString();
            txtNameUp.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            txtPriceUp.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            txtStockUp.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        ProductDal __productDal = new ProductDal();
        private void Search(string key)
        {
            List<Product> products = __productDal.GetAll().Where(p=>p.Name.ToLower().Contains(key.ToLower())).ToList();
            dgwProducts.DataSource = products;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
          // Search(txtSearch.Text);
          dgwProducts.DataSource = __productDal.GetByName(txtSearch.Text);
        }
    }
}