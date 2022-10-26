using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _25_10_22
{
    public partial class Main : Form
    {
        int Row;
        int ID;
        string NameMenu;
        int Quantity;
        double Cost;
        double TotalCost;

        DataTable Datatable = new DataTable();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.list_ordTableAdapter.Fill(this.cafeDataSet.list_ord);
            this.ordersTableAdapter.Fill(this.cafeDataSet.orders);
            this.positionsTableAdapter.Fill(this.cafeDataSet.positions);
            this.workersTableAdapter.Fill(this.cafeDataSet.workers);
            this.typesTableAdapter.Fill(this.cafeDataSet.types);
            this.menuTableAdapter.Fill(this.cafeDataSet.menu);

            Datatable.Columns.Add("ID", typeof(int));
            Datatable.Columns.Add("Name Menu", typeof(string));
            Datatable.Columns.Add("Quantity", typeof(int));
            Datatable.Columns.Add("Cost", typeof(double));
            Datatable.Columns.Add("Total Cost", typeof(double));
            dataGridView4.DataSource = Datatable;
        }

        private void btn_Insert_Menu_Click(object sender, EventArgs e)
        {
            this.menuTableAdapter.Insert(txtBoxNameMenu.Text, Convert.ToDouble(txtBoxCostMenu.Text), Convert.ToInt32(cmbBoxTypeMenu.SelectedValue));
            this.menuTableAdapter.Fill(this.cafeDataSet.menu);
            MessageBox.Show("ADD Menu");
        }

        private void btn_Insert_Workers_Click(object sender, EventArgs e)
        {
            this.workersTableAdapter.Insert(txtBoxWorker.Text, Convert.ToInt32(cmbBoxPosWorker.SelectedValue));
            this.workersTableAdapter.Fill(this.cafeDataSet.workers);
            MessageBox.Show("ADD Worker");
        }

        private void btn_Update_Menu_Click(object sender, EventArgs e)
        {
            this.menuTableAdapter.Update(this.cafeDataSet.menu);
        }

        private void btn_Update_Workers_Click(object sender, EventArgs e)
        {
            this.workersTableAdapter.Update(this.cafeDataSet.workers);
        }

        private void btn_Delete_Workers_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete worker?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.workersTableAdapter.DeleteWorker(Row);
                this.workersTableAdapter.Fill(this.cafeDataSet.workers);
                MessageBox.Show("Data deleted!");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void btn_Delete_Menu_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete menu?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.menuTableAdapter.DeleteMenu(Row);
                this.menuTableAdapter.Fill(this.cafeDataSet.menu);
                MessageBox.Show("Data deleted!");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            Quantity = Convert.ToInt32(txtBoxQuantity.Text);
            TotalCost = Quantity * Cost;
            Datatable.Rows.Add(ID, NameMenu, Quantity, Cost, TotalCost);

            textBoxSum.Text = (from DataGridViewRow row in dataGridView4.Rows
                               where row.Cells[4].FormattedValue.ToString() != string.Empty
                               select Convert.ToDouble(row.Cells[4].FormattedValue)).Sum().ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter.Insert(Convert.ToDouble(textBoxSum.Text), Convert.ToInt32(cmbBoxWorkers.SelectedValue));

            for (int i=0; i < dataGridView4.Rows.Count; i++)
                {
                list_ordTableAdapter.Insert(Convert.ToInt32(ordersTableAdapter.MaxOrder()), Convert.ToInt32(dataGridView4.Rows[i].Cells["ID"].Value), Convert.ToInt32(dataGridView4.Rows[i].Cells["Quantity"].Value));
                }

            this.ordersTableAdapter.Fill(this.cafeDataSet.orders);
            this.list_ordTableAdapter.Fill(this.cafeDataSet.list_ord);
            MessageBox.Show("ADD Order");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                Row = Convert.ToInt32(selectedRow.Cells[0].Value);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
                Row = Convert.ToInt32(selectedRow.Cells[0].Value);
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView3.Rows[e.RowIndex];
                ID = Convert.ToInt32(selectedRow.Cells[0].Value);
                NameMenu = selectedRow.Cells[1].Value.ToString();
                Cost = Convert.ToDouble(selectedRow.Cells[2].Value);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
