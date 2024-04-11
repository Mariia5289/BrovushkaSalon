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
using BrovushkaSalon.ModelDB;
using BrovushkaSalon.UserControls;

namespace BrovushkaSalon
{
    public partial class Main : Form
    {
        private enum SvipeType
        {
            Left,
            Right
        }
        private Model1 model = new Model1();
        private List<Product> products = new List<Product>();
        private int SvipeID; //переменная для номера записи текущего элемента
        public Main()
        {
            InitializeComponent();
            Loadproducts();
            CreateTile();
        }
        private void Loadproducts()//Метод для загрузки первоначальных данных
        {
            products.Clear();
            SvipeID = 0;
            products = model.Product.ToList();
            comboBox1.SelectedIndex = 0;
        }
        private void Sort()
        {
            if (checkBox1.Checked == false)
            {
                if (comboBox1.SelectedIndex == 0)
                    products = products.OrderBy(x => x.ID).ToList();
                else if (comboBox1.SelectedIndex == 1)
                    products = products.OrderBy(x => x.Title).ToList();
                else if (comboBox1.SelectedIndex == 2)
                    products = products.OrderBy(x => x.Cost).ToList();
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)
                    products = products.OrderByDescending(x => x.ID).ToList();
                else if (comboBox1.SelectedIndex == 1)
                    products = products.OrderByDescending(x => x.Title).ToList();
                else if (comboBox1.SelectedIndex == 2)
                    products = products.OrderByDescending(x => x.Cost).ToList();
            }
            CreateTile();
        }
        //Метод для загрузки первоначальных данных
        private void SetTextlabel()
        {
            if (products.Count != 0)
            {
                labelCount.Text = products.Count >= 6 ?
                $"с {SvipeID + 1} по {SvipeID + 6}  из {products.Count} Товаров" :
                $"с 1 по {products.Count} Товаров";
            }
            else
                labelCount.Text = $"с 0 из {products.Count} Товаров";
        }

        //Метод для добавления элементов
        private void CreateTile()
        {
            flowLayoutPanel.Controls.Clear();
            SetTextlabel();
            for (int i = 0; i < 6; i++)
            {
                if (products.Count > i)
                {
                    int count = i + SvipeID;
                    UserControlTitle tile = new UserControlTitle(products[count], model);
                    flowLayoutPanel.Controls.Add(tile);
                }
            }
        }

        private void Search()
        {
            products.Clear();
            SvipeID = 0;
            products = model.Product.Where(x => x.Title.Contains(textBoxSearch.Text)).ToList();
            labelNothing.Visible = products.Count == 0 ? true : false;
            CreateTile();
        }

        private void Svipe(SvipeType svipeType)//Перемотка
        {
            if (svipeType == SvipeType.Left &&
            SvipeID != 0)
            {
                SvipeID--;
                CreateTile();
            }
            if (svipeType == SvipeType.Right &&
            SvipeID + 5 < products.Count - 1)
            {
                SvipeID++;
                CreateTile();
            }
        }
        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            Svipe(SvipeType.Left);
        }

        private void buttonLeftx2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
                Svipe(SvipeType.Left);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            Svipe(SvipeType.Right);
        }

        private void buttonRightx2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
                Svipe(SvipeType.Right);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sort();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Sort();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Add().ShowDialog();
            Hide();
        }
    }
}
