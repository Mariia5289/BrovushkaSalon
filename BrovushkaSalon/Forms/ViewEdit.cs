using BrovushkaSalon.ModelDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrovushkaSalon.Forms
{
    public partial class ViewEdit : Form
    {
        private Product _product;
        private Model1 _model;
        public ViewEdit(Product product, Model1 model)
        {
            InitializeComponent();
            _product = product;
            _model = model;
            LoadData();
        }
        private void LoadData()
        {
            textBoxName.Text = _product.Title;
            textBoxCost.Text = _product.Cost.ToString();
            textBoxManufacture.Text = _product.Manufacturer.Name;
            textBoxDescription.Text = _product.Description;
            checkBoxActive.Checked = _product.IsActive;
            try
            {
                pictureBox1.Image = Image.FromFile(_product.MainImagePath);
            }
            catch
            {
                pictureBox1.Image = Properties.Resources.beauty_logo;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxName.Text) || String.IsNullOrEmpty(textBoxCost.Text) ||
               String.IsNullOrEmpty(textBoxManufacture.Text))
            {
                MessageBox.Show("Есть пустые поля в Title, Cost, Manufacturer");
                return;
            }
            else
            {
                _product.Title = textBoxName.Text;
                _product.Cost = Convert.ToDecimal(textBoxCost.Text);
                _product.Manufacturer.Name = textBoxManufacture.Text;
                _product.Description = textBoxDescription.Text;
                _product.IsActive = checkBoxActive.Checked;
                _product.MainImagePath = pictureBox1.Image.ToString();
                _model.SaveChanges();
                MessageBox.Show("Данные успешно сохранены");
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
