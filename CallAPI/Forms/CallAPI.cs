using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CallAPI.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CallAPI
{
    public partial class CallAPI : Form
    {
        public CallAPI()
        {
            InitializeComponent();
        }

        private async void btnGetAll_Click(object sender, EventArgs e)
        {
            var responce = await RestHelper.GetALL();

            var products = JsonConvert.DeserializeObject<List<Product>>(responce);
            
            productView.Rows.Clear();
            
            foreach (var product in products)
            {
                DataGridViewRow row = (DataGridViewRow)productView.Rows[0].Clone();

                row.Cells[0].Value = product.ProductID;
                row.Cells[1].Value = product.Name;
                row.Cells[2].Value = product.Price;
                row.Cells[3].Value = product.Stock;
                row.Cells[4].Value = product.Code;

                productView.Rows.Add(row);
            }
            
            productView.ReadOnly = true;
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            var responce = await RestHelper.Post(txtName.Text, txtPrice.Text, txtCode.Text, txtStock.Text);
            btnGetAll_Click(null, null);
        }

        private async void btnPut_Click(object sender, EventArgs e)
        {
            if (!productView.ReadOnly)
            {
                var id = productView.Rows[0].Cells[0].Value?.ToString();
                var name = productView.Rows[0].Cells[1].Value?.ToString();
                var price = productView.Rows[0].Cells[2].Value?.ToString();
                var stock = productView.Rows[0].Cells[3].Value?.ToString();
                var code = productView.Rows[0].Cells[4].Value?.ToString();

                var responce = await RestHelper.Put(id, name, price, stock, code);
                btnGetAll_Click(null, null);
            }
        }

        private async void btnGetById_Click(object sender, EventArgs e)
        {
            var responce = await RestHelper.Get(txtId.Text);

            var product = JsonConvert.DeserializeObject<Product>(responce);

            productView.Rows.Clear();

            DataGridViewRow row = (DataGridViewRow)productView.Rows[0].Clone();

            row.Cells[0].Value = product.ProductID;
            row.Cells[1].Value = product.Name;
            row.Cells[2].Value = product.Price;
            row.Cells[3].Value = product.Stock;
            row.Cells[4].Value = product.Code;

            productView.Rows.Add(row);

            productView.ReadOnly = false;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (productView.SelectedCells.Count > 0)
            {
                int rowIndex = productView.SelectedCells[0].RowIndex;

                if (rowIndex + 1 == productView.Rows.Count)
                {
                    return;
                }

                string productId = productView.Rows[rowIndex].Cells[0].Value.ToString();

                await RestHelper.Delete(productId);

                btnGetAll_Click(null, null);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
