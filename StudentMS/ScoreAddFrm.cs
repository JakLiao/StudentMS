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
    public partial class ScoreAddFrm : Form
    {
        string server = "localhost";
        string userid = "root";
        string password = "123456";
        string database = "studentms";
        string connStr;
        public ScoreAddFrm()
        {
            InitializeComponent();
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
                        txtNo.Text = reader.GetString(0);
                    }
                    reader.Close();
                }
            }
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
                        txtSubId.Text = reader.GetString(0);
                    }
                    reader.Close();
                }
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            string sql = String.Format("INSERT INTO score(StdId,StdName,SubId,SubName,class,Score) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", txtNo.Text, txtName.Text,txtSubId.Text,txtSub.Text,txtClass.Text,txtScore.Text);
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.Connection = conn;
                comm.CommandText = sql;
                int n = comm.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("添加成绩信息成功", "添加成功",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加成绩信息失败", "添加失败",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ScoreAddFrm_Load(object sender, EventArgs e)
        {
            connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", server, userid, password, database);
            this.bingtxtNameComboBox();
            this.bingtxtSubComboBox();
        }
    }
}
