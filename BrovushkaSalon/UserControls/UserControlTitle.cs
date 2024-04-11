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
using BrovushkaSalon.Forms;


namespace BrovushkaSalon.UserControls
{
    public partial class UserControlTitle : UserControl
    {
        private Product _product;
        private Model1 _model;
        public UserControlTitle(Product product, Model1 model)
        {
            InitializeComponent();
            Fill(product);
            _model = model;

        }

        public void Fill(Product product)//
        {
            _product = product;
            labelTitle.Text = _product.Title;
            labelCost.Text = $"Стоимость: {_product.Cost} руб.";
            try
            {
                pictureBox1.Image = Image.FromFile(_product.MainImagePath);
            }
            catch
            {
                pictureBox1.Image = Properties.Resources.beauty_logo;
            }
            BackColor = _product.IsActive ? Color.White : Color.LightGray;
        }

        //Метод попытки удаления данного элемента
        private void Delete()
        {
            DialogResult result = MessageBox.Show(
            $"Вы действительно хотите удалить товар с ID {_product.ID}",
            "Сообщение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    _model.Product.Remove(
                    _model.Product.First(x => x.ID == _product.ID));
                    _model.SaveChanges();
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Метод проверки нажатия правой или левой кнопки мыши 
        public void Clicking(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ViewEdit f = new ViewEdit(_product, _model);
                f.ShowDialog();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Delete();
            }
        }
        private void UserControlTitle_Load(object sender, EventArgs e)
        {

        }

        private void UserControlTitle_Click(object sender, EventArgs e)
        {

        }

        private void UserControlTitle_MouseClick(object sender, MouseEventArgs e)
        {
            Clicking(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
