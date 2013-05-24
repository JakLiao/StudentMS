using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentMS
{
    public partial class StudentMsgFrm : Form
    {
        string server = "localhost";
        string userid = "root";
        string password = "123456";
        string database = "studentms";
        private int current = 1;
        string connStr;
        public StudentMsgFrm()
        {
            InitializeComponent();
        }

        private void StudentFrm_Load(object sender, EventArgs e)
        {
            connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", server, userid, password, database);
            current = 1;
            ShowCurrentStudent();
        }

        private void bingtxtNameComboBox()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = String.Format("select StdName from student order by StdId");
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
                txtName.Items.Clear();
                txtName.Items.Add("");
                while (reader.Read())
                {
                    txtName.Items.Add(reader.GetValue(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作数据库出错！",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();//关闭数据库连接
            }
        }

        private void ShowCurrentStudent()
        {
            string sql = String.Format("SELECT * FROM Student WHERE StdId='{0}'",current);
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    txtNo.Text = reader.GetString(0);
                    txtName.Text = reader.GetString(1);
                }
                else
                {
                    MessageBox.Show("前面或后面已无数据了", "没有数据",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                reader.Close();
            }
            this.bingtxtNameComboBox();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            current--;
            ShowCurrentStudent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            current++;
            ShowCurrentStudent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = String.Format("Update Student SET StdName='{1}' WHERE StdId='{0}'", txtNo.Text, txtName.Text);
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sql, conn);
                int n = comm.ExecuteNonQuery();
                if (n <= 0)
                {
                    MessageBox.Show("数据更新操作失败，请检查数据格式！", "操作数据库出错！",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作数据库出错！",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();//关闭数据库连接
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = String.Format("DELETE FROM student WHERE StdId='{0}'", txtNo.Text);
            string sql2 = String.Format("DELETE FROM score WHERE StdId='{0}'", txtNo.Text);
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sql, conn);
                int n = comm.ExecuteNonQuery();
                MySqlCommand comm2 = new MySqlCommand(sql2, conn);//若score表中有对应的学生成绩，则删除
                int n2 = comm2.ExecuteNonQuery();
                if (n <= 0 )
                {
                    MessageBox.Show("删除失败，请与管理员联系！", "操作数据库出错！",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    current--;
                    ShowCurrentStudent();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作数据库出错！",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();//关闭数据库连接
            }
        }

        private void txtName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.txtName.Text != "")
            {
                string sql = String.Format("SELECT * FROM student WHERE StdName='{0}'", this.txtName.Text);
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        current = reader.GetInt32(0);
                        txtNo.Text = reader.GetString(0);
                    }
                    reader.Close();
                }
            }
        }

    }
}
