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
    public partial class ScoreMsgFrm : Form
    {
        string server = "localhost";
        string userid = "root";
        string password = "123456";
        string database = "studentms";

        private DataTable data;
        private MySqlDataAdapter adapter;
        private MySqlCommandBuilder command;
        string connStr;

        public ScoreMsgFrm()
        {
            InitializeComponent();
        }

        private void ScoreAddFrm_Load(object sender, EventArgs e)
        {
            connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", server, userid, password, database);
            ShowScoreTable();
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

        private void ShowScoreTable()
        {
            data = new DataTable();
            adapter = new MySqlDataAdapter("SELECT * FROM score", connStr);
            command = new MySqlCommandBuilder(adapter);
            adapter.Fill(data);
            dataGridView.DataSource = data;
            bingtxtNameComboBox();
            bingtxtSubComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable changes = data.GetChanges();
            adapter.Update(changes);
            data.AcceptChanges();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM score WHERE 1=1";
            if (this.txtName.Text != "")
            {
                sql = String.Format("{0} AND StdName='{1}'", sql, txtName.Text);
            }
            if (this.txtSub.Text != "")
            {
                sql = String.Format("{0} AND SubName='{1}'", sql, txtSub.Text);
            }
            if (this.txtScore.Text == "不及格")
            {
                sql = String.Format("{0} AND Score<60", sql);
            }
            if (this.txtScore.Text == "及格")
            {
                sql = String.Format("{0} AND Score>=60", sql);
            }
            data = new DataTable();
            adapter = new MySqlDataAdapter(sql, connStr);
            command = new MySqlCommandBuilder(adapter);
            adapter.Fill(data);
            dataGridView.DataSource = data;
        }
    }
}
