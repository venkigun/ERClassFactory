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
    public partial class TestMain : Form
    {
        DataTable dtDBs = null;
        DataTable dtTables = null;
        DataTable dtColumns = null;
        public TestMain()
        {
            InitializeComponent();
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            dtTables = new DataTable();
            ERClassFactory.Args.WheredDB = listBox1.SelectedItem.ToString();
            ERClassFactory.Adapters.LstTablesDA.Fill(dtTables);
            listBox2.BeginUpdate();
            foreach (DataRow row in dtTables.Rows)
            {
                listBox2.Items.Add(row["TABLE_NAME"]);
            }
            listBox2.EndUpdate();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtxtClass.Clear();
            dtColumns = new DataTable();
            string tableName;
            try
            {
                tableName = listBox2.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                return;
            }
            string dbName = listBox1.SelectedItem.ToString();
            ERClassFactory.Args.InitialCatalog = dbName;
            ERClassFactory.Args.WheredTable = tableName;
            ERClassFactory.Adapters.LstColumnsDA.Fill(dtColumns);
            //Now columns are in hand
            //Time to generate rawclasstext:
            rtxtClass.ForeColor =
                    Color.FromKnownColor(KnownColor.Blue);
            try
            {
                rtxtClass.LoadFile
                    (Application.StartupPath + @"..\..\..\..\Class.txt", 
                    RichTextBoxStreamType.PlainText);
            }
            catch (Exception)
            {
                rtxtClass.ForeColor =
                    Color.FromKnownColor(KnownColor.Red);
                rtxtClass.Text = "Resources Not Found.";
            }
            ERClassFactory.Generator.BeginGeneration(rtxtClass.Text);
            ERClassFactory.Generator.AddTableDBName(tableName, dbName);
            foreach (DataRow row in dtColumns.Rows)
            {
                ERClassFactory.Generator.AddField
                    (row["DATA_TYPE"].ToString(), row["COLUMN_NAME"].ToString());
                
                //textBox1.Text += ""+ row["COLUMN_NAME"] +"    "+ row["DATA_TYPE"] + Environment.NewLine;
            }
            ERClassFactory.Generator.AddParametersToSaveMethod(dtColumns);
            ERClassFactory.Generator.AddGetEntityFields(dtColumns);
            ERClassFactory.Generator.EndGeneration();
            rtxtClass.Text = ERClassFactory.Generator.RawClassText;
        }

        private void TestMain_Load(object sender, EventArgs e)
        {
            this.Hide();
            Input inputForm = new Input();
            inputForm.ShowDialog();
            //Input recieved.
            ArrayList tagContent = null;
            bool isTrusted = true;
            bool isValid = true;
            try
            {
                tagContent = inputForm.Tag as ArrayList;
                isTrusted = Convert.ToBoolean(tagContent[0]);
            }
            catch
            {
                MessageBox.Show("Input Cancelled.");
                isValid = false;
                goto Validity;
            }

            if (isTrusted)
            {
                //TrustedConnection.
                string dataSource = tagContent[1] as string;
                dtDBs = new DataTable();
                ERClassFactory.Args.Reset();
                ERClassFactory.Args.Type = ERClassFactory.enmConnStrType.Trusted;
                ERClassFactory.Args.DataSource = dataSource;
                ERClassFactory.Args.InitialCatalog = "master";
                try
                {
                    dtDBs.Clear();
                    ERClassFactory.Adapters.ServerDA.Fill(dtDBs);
                }
                catch
                {
                    MessageBox.Show("Invalid Input");
                    Application.Exit();
                }
            }
            else
            {
                //Authorized Connection.
                string server = tagContent[1] as string;
                string dataBase = tagContent[2] as string;
                string userID = tagContent[3] as string;
                string password = tagContent[4] as string;
                dtDBs = new DataTable();
                ERClassFactory.Args.Reset();
                ERClassFactory.Args.Type = ERClassFactory.enmConnStrType.Authorized;
                ERClassFactory.Args.Server = server;
                ERClassFactory.Args.Database = dataBase;
                ERClassFactory.Args.UserID = userID;
                ERClassFactory.Args.Password = password;
                try
                {
                    ERClassFactory.Adapters.ServerDA.Fill(dtDBs);
                }
                catch
                {
                    MessageBox.Show("Invalid Input");
                    Application.Exit();
                }

                //Now table is filled.
            }
            
            

            foreach (DataRow row in dtDBs.Rows)
            {
                listBox1.Items.Add(row["name"]);
            }

            Validity:
            if (isValid)
                this.Show();
            else
                this.Dispose(true);
        }

        private void btnSaveClass_Click(object sender, EventArgs e)
        {
            if (rtxtClass.Text != String.Empty)
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    rtxtClass.ReadOnly = true;
                    rtxtClass.SaveFile(saveDialog.FileName, RichTextBoxStreamType.PlainText);
                } 
            }
            else
            {
                MessageBox.Show("Please Select A Table.");
            }
            
        }
    }
}
