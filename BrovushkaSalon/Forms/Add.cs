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
    public partial class Add : Form
    {
        private Model1 Model = new Model1();
        private string _imageP;
        public Add()
        {
            InitializeComponent();
            List<Manufacturer> man = Model.Manufacturer.ToList();
            manufacturerBindingSource.DataSource = man;
            comboBoxMan.SelectedIndex = 0;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nameLabel_Click(object sender, EventArgs e)
        {

        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//выход
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)//Сохранение
        {
            if (String.IsNullOrEmpty(titleTextBox.Text) || String.IsNullOrEmpty(costTextBox.Text) ||
               String.IsNullOrEmpty(comboBoxMan.Text))
            {
                MessageBox.Show("Есть пустые поля в Title, Cost, Manufacturer");
                return;
            }
              Model1 model = new Model1();
              Product product = new Product();
                product.Title = titleTextBox.Text;
            product.ManufacturerID = (comboBoxMan.SelectedItem as Manufacturer).ID;
                product.Cost = Convert.ToDecimal(costTextBox.Text);
                product.Description = descriptionTextBox.Text;
                product.IsActive = isActiveCheckBox.Checked;
                product.MainImagePath = pictureBox1.Image.ToString();
            model.Product.Add(product);
            try
            {
            model.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Данные успешно сохранены");
            titleTextBox.Text = "";
            costTextBox.Text = "";
            comboBoxMan.Text = "";
            descriptionTextBox.Text = "";
            isActiveCheckBox.Checked = false;
            pictureBox1.Image = null;
        }
               
            

            private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберите фото";
            ofd.Filter = "Файлы JPEG, JPG, PNG|*.jpeg;*.jpg;*.png;|Все файлы (*.*)|*.*";
            ofd.ReadOnlyChecked = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            { 
            _imageP = ofd.FileName;
            pictureBox1.Image = Image.FromFile(_imageP);
            }
            ofd.Dispose();

        }

        private void Add_Load(object sender, EventArgs e)
        {

        }

        private void nameTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
