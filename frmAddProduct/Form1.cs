using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmAddProduct
{
    public partial class Form1 : Form
    {
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;

        private BindingSource showProductList;
        public Form1()
        {
            InitializeComponent();
            Form1_load();
            showProductList = new BindingSource();
        }

        private void Form1_load()
        {
            string[] ListOfProductCatgory = { "Beverages", "Bread/Bakery", "Canned/Jarred Goods", "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other" };

            foreach (string category in ListOfProductCatgory)
            {
                cbCategory.Items.Add(category);
            }
        }

        public class NumberFormatException : Exception
        {
            public NumberFormatException(string message) : base(message) { }
        }
        public class StringFormatException : Exception
        {
            public StringFormatException(string message) : base(message) { }
        }
        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string message) : base(message) { }
        }
        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatException("Use letter only for product name");
                }
                return name;
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show("$\"Error: {ex.Message}\", \"String Format Error\", MessageBoxButtons.OK, MessageBoxIcon.Error");
                return string.Empty;
            }
            finally
            {
                MessageBox.Show("Product name is valid");
            }

        }
        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]"))
                {
                    throw new NumberFormatException("Input valid number only");
                }
                return Convert.ToInt32(qty);
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Number Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                MessageBox.Show("Quantity is valid");
            }
        }
        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                {
                    throw new CurrencyFormatException("Use a valid decimal numbers");
                }
                return Convert.ToDouble(price);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Currency Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0.0;
            }
            finally
            {
                MessageBox.Show("Selling Price is valid");
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);

            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
            _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;
        }
    }
}
    
