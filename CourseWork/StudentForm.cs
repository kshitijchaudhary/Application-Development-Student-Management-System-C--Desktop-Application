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

namespace CourseWork
{
    public partial class StudentForm : Form

    {
        String data = "True";
        public StudentForm()
        {
            InitializeComponent();
            BindGrid();
            btnUpdate.Visible = false;
            dataGridReport.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //for making error message in one line
            /* if (txtFirstName.Text == "" || txtLastName.Text == "" || txtAddress.Text == "" || txtEmail.Text == "" || txtContactNo.Text == "" || cbGender.SelectedItem == null || cbProgramEnrol.SelectedItem == null)
             //{
               //  MessageBox.Show("Please enter details in all fields!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
             }*/
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Please enter your first name!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtLastName.Text == "" || txtLastName.Text == null)
            {
                MessageBox.Show("Please enter your last name!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtAddress.Text == "" || txtAddress.Text == null)
            {
                MessageBox.Show("Please enter your address!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtEmail.Text == "" || txtEmail.Text == null)
            {
                MessageBox.Show("Please enter your valid email!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (txtContactNo.Text == "" || txtEmail.Text == null)
            {
                MessageBox.Show("Please enter your valid contact number!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbGender.SelectedItem == null)
            {
                MessageBox.Show("Please select your Gender!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cbProgramEnrol.SelectedItem == null)
            {
                MessageBox.Show("Please select your enrolled program!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Student obj = new Student();
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                obj.Name = firstName + " " + lastName;
                obj.Address = txtAddress.Text;
                if (txtEmail.Text.ToString().LastIndexOf("@") > -1)
                {
                    obj.Email = txtEmail.Text;
                    obj.BirthDate = dpBirthDate.Value;
                    if (Regex.IsMatch(txtContactNo.Text, @"[0-9]"))
                    {
                        obj.ContactNo = txtContactNo.Text;
                        obj.RegdDate = dtRegDate.Value;
                        if (cbGender.SelectedItem != null)
                        {
                            obj.Gender = cbGender.SelectedItem.ToString();
                        }
                        if (cbProgramEnrol.SelectedItem != null)
                        {
                            obj.Program = cbProgramEnrol.SelectedItem.ToString();
                        }
                        obj.Add(obj);
                        MessageBox.Show("Data is Added Successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindGrid();
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Enter your Valid Phone Number", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                    }
                }
                else
                {
                    MessageBox.Show("Enter Valid Email only", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
    }
        public void BindGrid()
        {
            Student obj = new Student();
            List<Student> listStudents = obj.List();
            DataTable dt = Utility.ConvertToDataTable(listStudents);
            dataGridStudents.DataSource = dt;
        }
        private void Clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            dpBirthDate.Value = DateTime.Today;
            dtRegDate.Value = DateTime.Today;
            txtContactNo.Text = "";
            cbProgramEnrol.SelectedItem = null;
            cbGender.SelectedItem = null;   
        }
        private void dataGridStudents_DoubleClick(object sender, EventArgs e)
        {
            int id = 0; //get the clicked id
                        //read text file
            Student obj = new Student();
            List<Student> listStudents = obj.List();
            Student s = listStudents.Where(x => x.Id == id).FirstOrDefault();
            txtFirstName.Text = s.Name.Split(' ')[0];
            txtLastName.Text = s.Name.Split(' ')[1];
        }
        private void dataGridStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Student obj = new Student();
            if (e.ColumnIndex == 0)
            {
                //get the value of clicked rows id column
                string value = dataGridStudents[2, e.RowIndex].Value.ToString();
                int id = 0;
                if (String.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Nothing available to update", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    id = int.Parse(value);
                    Student s = obj.List().Where(X => X.Id == id).FirstOrDefault();
                    txtId.Text = s.Id.ToString();
                    txtFirstName.Text = s.Name.Split(' ')[0];
                    txtLastName.Text = s.Name.Split(' ')[1];
                    txtAddress.Text = s.Address;
                    txtEmail.Text = s.Email;
                    dpBirthDate.Value = s.BirthDate;
                    dtRegDate.Value = s.RegdDate;
                    txtContactNo.Text = s.ContactNo;
                    cbGender.SelectedItem = s.Gender;
                    cbProgramEnrol.SelectedItem = s.Program;
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            else if (e.ColumnIndex == 1)
            {
                try {
                    string value = dataGridStudents[2, e.RowIndex].Value.ToString();
                    obj.Delete(int.Parse(value));
                    BindGrid();
                }
                catch (Exception)
                {
                    MessageBox.Show("Nothing available to Delete", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                }
            }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Student obj = new Student();
                obj.Id = int.Parse(txtId.Text);
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                obj.Name = firstName + " " + lastName;
                obj.Address = txtAddress.Text;
                obj.Email = txtEmail.Text;
                obj.BirthDate = dpBirthDate.Value;
                obj.RegdDate = dtRegDate.Value;
                obj.ContactNo = txtContactNo.Text;
                obj.Gender = cbGender.SelectedItem.ToString();
                obj.Program = cbProgramEnrol.SelectedItem.ToString();
                obj.Edit(obj);
                BindGrid();
                Clear();
                btnSubmit.Visible = true;
                MessageBox.Show("Data is successfully updated", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception)
            {
                MessageBox.Show("No data to Update", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void cbProgramEnrol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLoadChart_Click(object sender, EventArgs e)
        {
            programChart pc1 = new programChart();
            pc1.ShowDialog();
        }

        private void btnStudentStatus_Click(object sender, EventArgs e)
        {
           // StudentStatus form2 = new StudentStatus();
            //form2.ShowDialog();

            Student obj = new Student();

            //get input value of registered date
            DateTime registeredDate = reportDate.Value;

            //invoke the List method of Student class which return a List of Student
            List<Student> listStudents = obj.List();

            // invoke the FindWeek method which returns array containing week start and end date
            DateTime[] dateArray = obj.FindWeek(registeredDate);

            // invoke WeekStudent method which returns a list of student of a week
            List<Student> weeklyStudents = obj.WeeklyStudent(dateArray, listStudents);


            // group the list by course and create two columns i.e. course and count
            var result = weeklyStudents
                    .GroupBy(l => l.Program)
                    .Select(cl => new
                    {
                        Course = cl.First().Program,
                        Count = cl.Count().ToString()
                    }).ToList();
            if (data=="True")
            { 
                dataGridReport.Show();
                data = "False";
            }
            else
            {
                dataGridReport.Hide();
                data = "True";
            }            
            dataGridReport.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //convert the result to datatable and show in a datagrid view
            DataTable dt = Utility.ConvertToDataTable(result);
            dataGridReport.DataSource = dt;
            dataGridReport.AutoResizeColumns();
            dataGridReport.CurrentCell = null;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {            
            Clear();
            btnSubmit.Visible = true;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (cbSort.SelectedItem != null)
            {
                if (cbSort.SelectedItem.ToString() == "First Name")
                {
                    Student obj = new Student();

                    //original list of student
                    List<Student> listStudents = obj.List();

                    //list after sorting
                    List<Student> lst = obj.Sort(listStudents, "First Name");

                    //adding sorted list to datatable
                    DataTable dt = Utility.ConvertToDataTable(lst);
                    dataGridStudents.DataSource = dt;
                }
                else
                {
                    Student obj = new Student();

                    //original list of student
                    List<Student> listStudents = obj.List();

                    //list after sorting
                    List<Student> lst = obj.Sort(listStudents, "Registration Date");

                    //adding sorted list to datatable
                    DataTable dt = Utility.ConvertToDataTable(lst);
                    dataGridStudents.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("Invalid Input! Please, select any value.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void reportDate_ValueChanged(object sender, EventArgs e)
        {
            Student obj = new Student();

            //get input value of registered date
            DateTime registeredDate = reportDate.Value;

            //invoke the List method of Student class which return a List of Student
            List<Student> listStudents = obj.List();

            // invoke the FindWeek method which returns array containing week start and end date
            DateTime[] dateArray = obj.FindWeek(registeredDate);

            // invoke WeekStudent method which returns a list of student of a week
            List<Student> weeklyStudents = obj.WeeklyStudent(dateArray, listStudents);


            // group the list by course and create two columns i.e. course and count
            var result = weeklyStudents
                    .GroupBy(l => l.Program)
                    .Select(cl => new
                    {
                        Course = cl.First().Program,
                        Count = cl.Count().ToString()
                    }).ToList();
            dataGridReport.Show();
            data="False";



            dataGridReport.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //convert the result to datatable and show in a datagrid view
            DataTable dt = Utility.ConvertToDataTable(result);
            dataGridReport.DataSource = dt;
            dataGridReport.AutoResizeColumns();
            dataGridReport.CurrentCell = null;
        }
    }
}
