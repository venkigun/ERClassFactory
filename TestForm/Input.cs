using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace TestForm
{
    public partial class Input : Form
    {
        public Input()
        {
            InitializeComponent();
        }
        ArrayList tagContent = new ArrayList();
        private void btnConnect_Click(object sender, EventArgs e)
        {
            tagContent.Clear();
            tagContent.Add(true);//is it TrustedConnection or not.
            tagContent.Add(txtDataSource.Text);
            this.Tag = tagContent as object;
            this.Close();
            
        }

        private void btnConnect2_Click(object sender, EventArgs e)
        {
            tagContent.Clear();
            tagContent.Add(false);//is it TrustedConnection or not.
            tagContent.Add(txtServer.Text);
            tagContent.Add(txtDatabase.Text);
            tagContent.Add(txtUserID.Text);
            tagContent.Add(txtPassword.Text);
            this.Tag = tagContent as object;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Input_Load(object sender, EventArgs e)
        {
            
        }
    }
}
