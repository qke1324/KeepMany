using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace KeepMany.FKM
{
    public partial class FGroup : Form
    {

        Point mousePoint;
        DbClass myDb;
        MethodClass myMc;
        public static DataSet myDs;
        public static DataSet myGroupInfo;
        static string userId = FKeepMany.userId; 
        public FGroup()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;

            this.Width = 800;
            //panelRight1.Location = new Point(810, 0);
            //panelRight2.Location = new Point(399, 0);
            this.Location = new Point(300, 150);
        }



        private void formTopBanner_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void formTopBanner_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X), this.Top - (mousePoint.Y - e.Y));
            }
        }

        private void FGroup_Load(object sender, EventArgs e)
        {
            myMc = new MethodClass();
            myDs = myMc.SetGroupList(lbxGroup, userId);
            //myMc.SetGroupList(lbxGroup, userId);
        }

        private void btnGClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            myMc = new MethodClass();
            myMc.SetGroupMemberList(lbxMember, userId, lbxGroup.SelectedItem.ToString(), myDs);
        }

        private void btnGSearch_Click(object sender, EventArgs e)
        {
            myMc = new MethodClass();
            myGroupInfo = myMc.SetGroupInfo(txtGName.Text, txtGCode.Text, lblGUserId, lblGMDate);
            

        }

        private void btnJoinGroup_Click(object sender, EventArgs e)
        {

        }

        private void btnGDup_Click(object sender, EventArgs e)
        {

        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {

        }

        private void btnGMOut_Click(object sender, EventArgs e)
        {

        }

        private void btnGroupDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnGroupInfo_Click(object sender, EventArgs e)
        {

        }

        private void lbxMember_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
