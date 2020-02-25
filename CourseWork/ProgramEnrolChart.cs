using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class programChart : Form
    {
        public programChart()
        {
            InitializeComponent();
            BindGrid();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        public void BindGrid()
        {
            Student obj = new Student();
            List<Student> listStudents = obj.List();
            DataTable dt = Utility.ConvertToDataTable(listStudents);
           // dataGridStudents.DataSource = dt;
            BindChart(listStudents);
        }
        private void BindChart(List<Student> lst)
        {
            if (lst != null)
            {
                var result = lst
                    .GroupBy(l => l.Program)
                    .Select(cl => new
                    {
                        Program = cl.First().Program,
                        Count = cl.Count().ToString()
                    }).ToList();
                DataTable dt = Utility.ConvertToDataTable(result);
                chart1.DataSource = dt;
                chart1.Name = "Enrolled Programs";
                chart1.Series["Series1"].XValueMember = "Program";
                chart1.Series["Series1"].YValueMembers = "Count";
                this.chart1.Titles.Remove(this.chart1.Titles.FirstOrDefault());
                this.chart1.Titles.Add("Enrollment Chart");
                chart1.Series["Series1"].Enabled = true;
                chart1.Series["Series1"].IsValueShownAsLabel = true;

            }
        }

        private void programChart_Load(object sender, EventArgs e)
        {

        }
    }
}
