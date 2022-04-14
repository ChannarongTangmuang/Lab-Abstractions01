using System.Text;

namespace Lab_Abstractions01
{
    public partial class Form1 : Form
    {
        private double sum1 = 0;
        private double sum2 = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void  openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV (*.csv) | *.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] readAllLine = File.ReadAllLines(openFileDialog.FileName);

                string readAllText = File.ReadAllText(openFileDialog.FileName);
                for (int i = 0; i < readAllLine.Length; i++)
                {
                    string allDATARAW = readAllLine[i];
                    string[] allDATASplited = allDATARAW.Split(',');
                    this.dataGridView2.Rows.Add(allDATASplited[0], allDATASplited[1], allDATASplited[2], allDATASplited[3]);
                }
            }
        }

        private void buttonCheckOut_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows.RemoveAt(rowIndex);

        }

        private void buttonCheckIn_Click(object sender, EventArgs e)
        {
            this.sum1 += sum2;

            string User_or_Id = textBoxUserorId.Text;
            string Password = textBoxPassword.Text;

            int n = dataGridView1.Rows.Add();
            dataGridView1.Rows[n].Cells[0].Value = "Staff";
            dataGridView1.Rows[n].Cells[1].Value = sum1;
            dataGridView1.Rows[n].Cells[2].Value = User_or_Id;
            dataGridView1.Rows[n].Cells[3].Value = Password;

            textBoxUserorId.Text = "";
            textBoxPassword.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Major = comboBoxMajor.Text;
            string SSID = textBoxSSID.Text;
            string Name = textBoxName.Text;
            string Password = textBoxPassword.Text;
            string Staff = " NULL ";

            int n = dataGridView1.Rows.Add();

            if (Major == "Staff")
            {
                Staff staff = new Staff();

                staff.Name = Name;
                staff.SSID = SSID;
                staff.Password = Password;

                dataGridView2.Rows[n].Cells[2].Value = staff.Name;
                dataGridView2.Rows[n].Cells[1].Value = staff.SSID;
                dataGridView2.Rows[n].Cells[3].Value = staff.Password;
                dataGridView2.Rows[n].Cells[0].Value = Staff;
            }

            if (Major == "Student")
            {
                Student student = new Student();

                student.Major = Major;
                student.Name = Name;
                student.SSID = SSID;

                dataGridView2.Rows[n].Cells[0].Value = student.Major;
                dataGridView2.Rows[n].Cells[2].Value = student.Name;
                dataGridView2.Rows[n].Cells[1].Value = student.Name;
            }

            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV(*.csv)*.csv";
                bool filrError = false;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if(!filrError)
                    {
                        try
                        {
                            int columnCount = dataGridView2.Columns.Count;
                            string column = "";
                            string[] outputCSV = new string[dataGridView2.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                column += dataGridView2.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCSV[0] += column;
                            for (int i = 1; (i - 1) < dataGridView2.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCSV[i] += dataGridView2.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }
                            File.WriteAllLines(saveFileDialog.FileName, outputCSV, Encoding.UTF8);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Eroro :" + ex.Message);
                        }
                    }
                }
            }    
        }
    }
}