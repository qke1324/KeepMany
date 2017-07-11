using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeepMany.FKM;
using System.Data.SqlClient;
using System.Collections;




namespace KeepMany
{
    public partial class FKeepMany : Form
    {
        //전역변수 선언
        Point mousePoint;
        Point pnt;
        static private bool flag = true;
        public const int 지출높이 = 166;
        public const int 수입높이 = 166;
        public const int 저축높이 = 136;
        public readonly int[] 지출위치 = new int[2] { 3, 3 };
        public readonly int[] 수입위치 = new int[2] { 3, 175 };
        public readonly int[] 저축위치 = new int[2] { 3, 344 };
        public int 지출행 = 0;
        public int 수입행 = 0;
        public int 저축행 = 0;
        static public int[] tot = new int[4];
        ArrayList 지출대분류배열 = new ArrayList();
        public int j = 0;
        DataSet myDs;

        DataTable mySaveDt;
        MethodClass myMehtod;

        //-----------USER-------------
        public static string userId;
        public static Label lblID;
        public static int groupNo = 0;
        //-----------USER-------------

        //-----------DATE-------------
        public static string startDate = "";
        public static string endDate = "";
        //-----------DATE-------------


        public FKeepMany()
        {
            InitializeComponent();
            

        }
        
        //데이터그리드뷰에 DB데이터 출력 사용하지않음 MethodClass에 구현해놓음
        public void DisplayList()
        {


            //myMehtod = new MethodClass();
            //DbClass dbc = new DbClass();
            //myDs = dbc.GetSpendData();
            //mySaveDt = myDs.Tables[0];
            //dbc.Close();
            //j = 0;
            //지출총액 = 0;

            //foreach (DataRow r in mySaveDt.Rows)
            //{

            //    dgrv지출.Rows.Add();

            //    if (j >= mySaveDt.Rows.Count)
            //        j = 0;
            //    for (int i = 0; i < mySaveDt.Columns.Count; i++)
            //    {

            //        if (i != 0 && i != 5 && i != 8)
            //        {
            //            dgrv지출[i, j].Value = r[i].ToString().TrimEnd();

            //        }
            //        else if(i == 0)
            //        {
            //            dgrv지출[i, j].Value = false;

            //        }

            //        else if(i == 5)
            //        {
            //            if (r[i] != null)
            //            {
            //                DataGridViewRow row = dgrv지출.Rows[j];
            //                DataGridViewComboBoxCell myCb = (DataGridViewComboBoxCell)(row.Cells[5]);
            //                myCb.Items.Add(r[i].ToString().TrimEnd());
            //                myCb.Value = myCb.Items[0];
            //            }
            //        }
            //        else
            //        {
            //            //
            //            if(dgrv지출[i,j].Value != r[i])
            //            {
            //                dgrv지출[i, j].Value = r[i];
            //            }

            //        }

            //    }
            //    지출총액 += int.Parse(dgrv지출[3, j].Value.ToString());
            //    lbl지출합계.Text = 지출총액.ToString();
            //    j++;

            //}

            ////지출날짜열 내림차순 표시
            //dgrv지출.Sort(지출날짜, ListSortDirection.Descending);

            ////myMehtod.removeRows(dgrv지출);
            //dgrv지출.ClearSelection();

        }

        private void logoKeepMany_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Application.ExitThread();
            Environment.Exit(0);
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

        private void 지출버튼_Click(object sender, EventArgs e)
        {
            dgrv지출.Visible = true;
            dgrv수입.Visible = false;
            dgrv저축.Visible = false;
            dgrv지출.Height = 지출높이 + 수입높이 + 저축높이;
        }

        private void 수입버튼_Click(object sender, EventArgs e)
        {
            dgrv지출.Visible = false;
            dgrv수입.Visible = true;
            dgrv저축.Visible = true;
            pnt = new Point(지출위치[0], 지출위치[1]);
            dgrv수입.Location = pnt;
            dgrv수입.Height = 지출높이 + 수입높이 + 5;
        }

        private void btnKeepMany_Click(object sender, EventArgs e)
        {
            dgrv지출.Visible = true;
            dgrv수입.Visible = true;
            dgrv저축.Visible = true;
            pnt = new Point(수입위치[0], 수입위치[1]);
            dgrv수입.Location = pnt;
            dgrv수입.Height = 수입높이;
            dgrv지출.Height = 지출높이;
        }


        private void FKeepMany_Load(object sender, EventArgs e)
        {

            //No값을 비교하기 위해 디폴트값 0 지정
            //dgrv지출[8, 0].Value = 0;

            lblID = lblUserId;
            lblID.Text = userId;
            //오늘 날짜를 표시
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd");
            lblToday.Text = nowTime;
            dateTimePicker1.Text = DateTime.Today.AddDays(-14).ToString();

            //콤보박스열에 데이터 추가

            AllSet(userId);
        }
        public void AllSet(string ID)
        {
            userId = ID;
            MethodClass myMc = new MethodClass();
            myMc.GetCategory("spendBig", dgrv지출, 지출대분류,ID);
            myMc.GetCategory("incomebig", dgrv수입, 수입대분류,ID);
            myMc.get카드명(dgrv수입, userId, 지출카드명, 지출사용처);
            myMc.GetSave(dgrv저축, userId, 저축명, groupNo);
            //this.DisplayList();
            myMc = new MethodClass();
            tot = myMc.DisplayList(dgrv지출, dgrv수입, dgrv저축, startDate, endDate, userId, groupNo);


            //날짜열 내림차순 표시
            //dgrv지출.Sort(지출날짜, ListSortDirection.Descending);
            //dgrv수입.Sort(수입날짜, ListSortDirection.Descending);

            dgrv지출.ClearSelection();
            dgrv수입.ClearSelection();
            dgrv저축.ClearSelection();

            lbl수입합계.Text = tot[0].ToString();
            lbl현금합계.Text = tot[1].ToString();
            lbl카드합계.Text = tot[2].ToString();
            lbl지출합계.Text = tot[3].ToString();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FReport fReport = new FReport();
            fReport.ShowDialog();
            
        }

        private void DisplayDataGrid(string sQuery, string tablename, DataGridView myDg)
        {
            DbClass myDb = new DbClass();
            myDs = myDb.AdapterFill(sQuery, tablename);
            myDg.DataSource = myDs;
            
            myDb.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            MethodClass myMc = new MethodClass();
            tot = myMc.DisplayList(dgrv지출, dgrv수입, dgrv저축, startDate, endDate, userId, groupNo);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FCategory fCatg = new FCategory(userId,groupNo);
            fCatg.ShowDialog();
        }

        //DataGridView셀이 편집컨트롤로 변했을 때 
        //커서가 닿은 셀이 ComboBox라면 ComboBox객체에 담기게 되고,
        //ComboBox를 담고 있는 객체가 비어있지 않다면
        //커서가 닿거나 나갈때 이벤트핸들러 지정
        private void dgr_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
                ComboBox comboCell = e.Control as ComboBox;

                if (comboCell != null)
                {
                    //comboCell.SelectedIndexChanged -= new EventHandler(comboCell_SelectedIndexChanged);
                    comboCell.SelectedIndexChanged += new EventHandler(comboCell_SelectedIndexChanged);
                }

            
        }


        //콤보셀에 이벤트를 지정 선택된인덱스가 바뀔떄 코드실행
        private void comboCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (dgrv지출.CurrentRow.Cells[4].Value != null)
                MethodClass myMc = new MethodClass();
            if (dgrv지출.CurrentCell.ColumnIndex == 4)
                {
                    ComboBox cmb = (ComboBox)sender;
               if (dgrv지출.CurrentRow.Cells[4].Value != cmb.SelectedItem)
                   {
                        
                  if (cmb.SelectedItem != null)
                    {
                            DataGridViewComboBoxCell cmb3 = new DataGridViewComboBoxCell();
                          
                            cmb3 = (DataGridViewComboBoxCell)dgrv지출.CurrentRow.Cells[5];
                          
                            cmb3.Items.Clear();
         
                            string cmbText = cmb.SelectedItem.ToString();
                            myMc.GetSmallCategory(cmbText, cmb3, 1);
                            
                            //기존에 선택된 값을 다시 설정 하려는 경우 오류
                            
                     }
                    
                    }
            }
           
            else if(dgrv수입.CurrentCell.ColumnIndex == 4)
                {
                    ComboBox cmbIncome = (ComboBox)sender;
                    if(dgrv수입.CurrentCell.Value != cmbIncome.SelectedItem)
                    {
                        if(cmbIncome.SelectedItem != null)
                        {
                            DataGridViewComboBoxCell cmb3 = new DataGridViewComboBoxCell();


                            cmb3 = (DataGridViewComboBoxCell)dgrv수입.CurrentRow.Cells[5];

                            cmb3.Items.Clear();
                            string cmbText = cmbIncome.SelectedItem.ToString();
                            myMc.GetSmallCategory(cmbText, cmb3, 2);
                        }

                    }
                }

            }
            catch {}
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Fixed myFixed = new Fixed();
            myFixed.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //DbClass myDb = new DbClass();
            MethodClass myMc = new MethodClass();
            flag = myMc.insertRows(dgrv지출, "Spend",userId);
            flag = myMc.insertRows(dgrv수입, "Income",userId);
            if (flag) MessageBox.Show("데이터입력완료");

            //dgrv지출.Rows.Clear();

            //this.DisplayList();

            myMc = new MethodClass();
            tot = myMc.DisplayList(dgrv지출, dgrv수입, dgrv저축, startDate, endDate, userId,groupNo);
            lbl수입합계.Text = tot[0].ToString();
            lbl현금합계.Text = tot[1].ToString();
            lbl카드합계.Text = tot[2].ToString();
            lbl지출합계.Text = tot[3].ToString();

        }

        private void dgrv지출_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            {
                // If the data source raises an exception when a cell value is 
                // commited, display an error message.
                if (e.Exception != null &&
                    e.Context == DataGridViewDataErrorContexts.Commit)
                {
                    //MessageBox.Show("CustomerID value must be unique.");
                }
            }
        }

        private void dgrv지출_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //dgrv지출[8, e.RowIndex].Value = 0;
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            FGroup myGroup = new FGroup();
            myGroup.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FLogin myLogin = new FLogin();
            myLogin.ShowDialog();
        }


        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
                dateTimePicker1.Value = dateTimePicker2.Value;

            startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            MethodClass myMc = new MethodClass();
            tot = myMc.DisplayList(dgrv지출, dgrv수입, dgrv저축, startDate, endDate, userId,groupNo);
            lbl수입합계.Text = tot[0].ToString();
            lbl현금합계.Text = tot[1].ToString();
            lbl카드합계.Text = tot[2].ToString();
            lbl지출합계.Text = tot[3].ToString();
        }

        private void FKeepMany_Activated(object sender, EventArgs e)
        {
            AllSet(userId);
            lblID.Text = userId;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            
            dgrv지출.Rows.Insert(1, 1);
        }

        //데이터그리드뷰가 다중셀렉션이 되어 3개의 데이터그리드뷰중 하나만 선택가능 하도록
        private void dgrv_Enter(object sender, EventArgs e)
        {
            DataGridView dgrv = (DataGridView)sender;
            string dgrvName = dgrv.Name;
            switch(dgrvName)
            {
                case "dgrv지출":
                    dgrv수입.ClearSelection();
                    dgrv저축.ClearSelection();
                    break;
                case "dgrv수입":
                    dgrv지출.ClearSelection();
                    dgrv저축.ClearSelection();
                    break;
                case "dgrv저축":
                    dgrv지출.ClearSelection();
                    dgrv수입.ClearSelection();
                    break;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ArrayList chkNo = new ArrayList();
            int i = 0;
            foreach(DataGridViewRow r in dgrv지출.Rows)
            {
                DataGridViewCheckBoxCell chk = r.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    chkNo.Add(int.Parse(r.Cells[8].Value.ToString()));
                    i = (int)r.Cells[8].Value;
                    r.Visible = false;
                    r.Cells[8].Value = i;
                    
                }
            }
           
        }
    }
}
