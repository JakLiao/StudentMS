using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentMS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;//设定为父窗体
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 重新登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginFrm = new LoginForm();
            loginFrm.Show();
        }

        private void 添加学生信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentAddFrm sForm = new StudentAddFrm();	// 创建子窗体对象
            sForm.MdiParent = this;		//指定当前窗体为MDI父窗体
            sForm.Show();				//打开子窗体
            tssMsg.Text = sForm.Text;		//在状态栏中显示操作内容
        }

        private void 学生信息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentMsgFrm sForm = new StudentMsgFrm();	// 创建子窗体对象
            sForm.MdiParent = this;		//指定当前窗体为MDI父窗体
            sForm.Show();				//打开子窗体
            tssMsg.Text = sForm.Text;		//在状态栏中显示操作内容
        }

        private void 添加课程信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubjectAddFrm sForm = new SubjectAddFrm();	// 创建子窗体对象
            sForm.MdiParent = this;		//指定当前窗体为MDI父窗体
            sForm.Show();				//打开子窗体
            tssMsg.Text = sForm.Text;		//在状态栏中显示操作内容
        }

        private void 课程信息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubjectMsgFrm sForm = new SubjectMsgFrm();	// 创建子窗体对象
            sForm.MdiParent = this;		//指定当前窗体为MDI父窗体
            sForm.Show();				//打开子窗体
            tssMsg.Text = sForm.Text;		//在状态栏中显示操作内容
        }

        private void 添加成绩信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScoreAddFrm sForm = new ScoreAddFrm();	// 创建子窗体对象
            sForm.MdiParent = this;		//指定当前窗体为MDI父窗体
            sForm.Show();				//打开子窗体
            tssMsg.Text = sForm.Text;		//在状态栏中显示操作内容
        }

        private void 成绩信息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScoreMsgFrm sForm = new ScoreMsgFrm();	// 创建子窗体对象
            sForm.MdiParent = this;		//指定当前窗体为MDI父窗体
            sForm.Show();				//打开子窗体
            tssMsg.Text = sForm.Text;		//在状态栏中显示操作内容
        }


    }
}
