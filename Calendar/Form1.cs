using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            TamDB db = new TamDB();
            dataGridView1.DataSource = db.Liches.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var start = dtpStart.Value;
            var end = dtpEnd.Value;

            Lich appoint = new Lich();
            appoint.Name = txtName.Text;
            appoint.StartTime = start;
            appoint.EndTime = end;

           
            TamDB db = new TamDB();
            var list = db.Liches.ToList();
            bool check = true;
            Lich dateTrung = new Lich();
            foreach(var d in list)
            {
                //int tmp = (start.Hour*60 + start.Minute) -( d.EndTime.Hour*60+d.EndTime.Minute);
                if (DateTime.Compare(start, d.EndTime)<0)
                {
                    dateTrung = d;
                    check = false;
                    break;
                }
            }
            if (check)
            {
                
                db.Liches.Add(appoint);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                DialogResult res = MessageBox.Show("Lịch trùng, bạn có muốn thay thế?", "Xác nhận", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)
                {
                    db.Liches.Remove(dateTrung);
                    db.Liches.Add(appoint);
                    db.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                }
                else
                {
                    MessageBox.Show("Chỉnh lại thời gian");
                }
            }
            LoadData();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                TamDB db = new TamDB();
                var appoint = db.Liches.Find(id);   
                db.Liches.Remove(appoint);

                db.SaveChanges();

                LoadData();
            }
        }

       
    }
}
