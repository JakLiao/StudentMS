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
    public partial class SubjectMsgFrm : Form
    {
        
        string server = "localhost";
        string userid = "root";
        string password = "123456";
        string database = "studentms";
        private int current = 1;
        string connStr;
        public SubjectMsgFrm()
        {
            InitializeComponent();
        }
        private void SubjectMsgFrm_Load(object sender, EventArgs e)
        {
            connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", server, userid, password, database);
            current = 1;
            ShowCurrentCourse();
        }

        private void bingtxtSubComboBox()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = String.Format("select SubName from subject order by SubId");
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
                txtSub.Items.Clear();
                txtSub.Items.Add("");
                while (reader.Read())
                {
                    txtSub.Items.Add(reader.GetValue(0));
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

        private void ShowCurrentCourse()
        {
            string sql = String.Format("SELECT * FROM subject WHERE SubId='{0}'",current);
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    txtSub.Text = reader.GetString(1);
                    txtType.Text = reader.GetString(2);
                    txtPoint.Text = reader.GetString(3);
                }
                else
                {
                    MessageBox.Show("前面或后面已无数据了", "没有数据",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                reader.Close();
            }
            this.bingtxtSubComboBox();
        }


        private void txtSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.txtSub.Text != "")
            {
                string sql = String.Format("SELECT * FROM subject WHERE SubName='{0}'", this.txtSub.Text);
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        current = reader.GetInt32(0);
                        txtType.Text = reader.GetString(2);
                        txtPoint.Text = reader.GetString(3);
                    }
                    reader.Close();
                }
            }

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            current--;
            ShowCurrentCourse();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            current++;
            ShowCurrentCourse();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = String.Format("Update subject SET SubName='{0}',Type='{1}',Point='{2}' WHERE SubId='{3}'", txtSub.Text,txtType.Text, txtPoint.Text,current);
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
            string sql = String.Format("DELETE FROM subject WHERE SubId='{0}'", txtType.Text);
            string sql2 = String.Format("DELETE FROM score WHERE SubId='{0}'", txtType.Text);
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sql, conn);
                int n = comm.ExecuteNonQuery();
                MySqlCommand comm2 = new MySqlCommand(sql2, conn);//若score表中有对应的课程成绩，则删除
                int n2 = comm2.ExecuteNonQuery();
                if (n <= 0)
                {
                    MessageBox.Show("删除失败，请与管理员联系！", "操作数据库出错！",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    current--;
                    ShowCurrentCourse();
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



    }
}
