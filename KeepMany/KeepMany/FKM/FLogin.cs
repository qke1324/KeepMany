using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KeepMany.FKM
{
    public partial class FLogin : Form
    {
        FLogin myLogin;

        public FLogin()
        {
            InitializeComponent();
        }
       

        private void FLogin_Load(object sender, EventArgs e)
        {
            
        }
        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, e);
            }
            else
            {

            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox myTB = (TextBox)sender;
            if (myTB.Text == "ID" || myTB.Text == "PW")
            {
                myTB.Text = "";
                myTB.ForeColor = Color.Black;
            }



        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox myTB = (TextBox)sender;
            if (myTB.Text == "")
            {
                switch (myTB.TabIndex)
                {
                    case 1:
                        myTB.Text = "ID";
                        break;
                    case 2:
                        myTB.Text = "PW";
                        break;
                    


                }
                myTB.ForeColor = Color.Silver;
            }
            else
            {

            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MethodClass myMc = new MethodClass();
            bool IsAuthenticated = myMc.Authenticate(txtLID.Text, txtLPwd.Text);

            if(IsAuthenticated)
            {
                FKeepMany.userId = txtLID.Text;
                FKeepMany.lblID.Text = txtLID.Text;
                

                this.Close();
                
            }
            else
            {
                MessageBox.Show("ID와 비밀번호를 확인하세요.");
            }


            

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FRegister myReg = new FRegister();
            myReg.ShowDialog();
        }

        private void FLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            FKeepMany myKFM = new FKeepMany();
            
            myKFM.AllSet(txtLID.Text);
        }
    }
}
