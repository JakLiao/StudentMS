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
    public partial class SubjectAddFrm : Form
    {
        string server = "localhost";
        string userid = "root";
        string password = "123456";
        string database = "studentms";
        public SubjectAddFrm()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            string connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", server, userid, password, database);
            string sql = String.Format("INSERT INTO subject(SubName,Type,Point) VALUES('{0}','{1}','{2}')", txtSub.Text,txtType.Text,txtPoint.Text);
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                comm.Connection = conn;
                comm.CommandText = sql;
                int n = comm.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("添加课程信息成功", "添加成功",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加课程信息失败", "添加失败",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
