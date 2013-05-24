using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySQLDriverCS;
using MySql.Data.MySqlClient;

namespace StudentMS
{
    public partial class LoginForm : Form
    {
        private MySqlConnection conn;
        string server = "localhost";
        string userid = "root";
        string password = "123456";
        string database = "studentms";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            string connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false",server, userid, password,database);
            try
            {
                conn = new MySqlConnection(connStr);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error connecting to the server: " + ex.Message);
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string userName = txtName.Text;
            string password = txtPwd.Text;
            string sql = String.Format("select count(*) from admin where aname='{0}'and pass='{1}'", userName, password);
            MySqlCommand msqlCommand = new MySqlCommand();
            msqlCommand.Connection = this.conn;
            msqlCommand.CommandText = sql;
            try
            {
                conn.Open();
                if (msqlCommand.ExecuteScalar() != null)
                {
                    //this.DialogResult = DialogResult.OK;
                    this.Tag = true;
                    MainForm mainfrm = new MainForm();
                    this.Hide();
                    mainfrm.Show();
                }
                else
                {
                    MessageBox.Show("您输入的用户名或密码错误！请重试", "登录失败",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Tag = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作数据库出错！",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Tag = false;
            }
            finally
            {
                conn.Close();	// 关闭数据库连接
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPwd.Text = "";
            txtName.Focus();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
