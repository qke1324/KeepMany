using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;
using System.Security.Cryptography;
namespace KeepMany
{
    class MethodClass
    {
        int j = 0;
        DbClass myDb;

        string date, contents, bigC, smallC, cardname, useplace, tablename;
        string note, savename; 
        int amount, no;
        
        public static string Id;
        static DataSet spendMyDs;
        static DataSet spendAllDs;
        static DataSet spendSmallDs;
        static DataSet incomeMyDs;
        static DataSet incomeAllDs;
        static DataSet incomeSmallDs;
        
        static DataSet saveMyDs;
        

        //DatSource Binding으로 데이터그리드뷰 초기화 해보기
        public bool newInsert(DataGridView myDg)
        {
            bool status = false;


            return status;
        }

        //데이터그리드뷰 콤보박스에 분류 항목 가져와 설정
        public void GetCategory(string tableName, DataGridView myDg, DataGridViewComboBoxColumn myCbcm,string userid)
        {
            Id = userid;
            myCbcm.Items.Clear();
            myDb = new DbClass();
            switch (myDg.Name)
            {
                case "dgrv지출":
                    spendMyDs = new DataSet();
                    string mySpendQuery = "SELECT userSpendBigCtgy FROM userSpendCtgy WHERE userid = 'admin' OR userid = '" + Id + "' GROUP BY userSpendBigCtgy";
                    spendMyDs = myDb.AdapterFill(mySpendQuery, tableName);
                    myDb.Close();
                    myCbcm.Items.Add("");
                    foreach(DataRow r in spendMyDs.Tables[0].Rows)
                    {
                        for (int i = 0; i < spendMyDs.Tables[0].Columns.Count; i++)
                        {
                            myCbcm.Items.Add(r[i].ToString().TrimEnd());
                        }
                                
                    }
                    break;

                case "dgrv수입":
                    incomeMyDs = new DataSet();
                    string myIncomeQuery = "SELECT userIncomeBigCtgy FROM userIncomeCtgy GROUP BY userIncomeBigCtgy";
                    incomeMyDs = myDb.AdapterFill(myIncomeQuery, tableName);
                    myDb.Close();
                    myCbcm.Items.Add("");
                    foreach (DataRow r in incomeMyDs.Tables[0].Rows)
                    {
                        for (int i = 0; i < incomeMyDs.Tables[0].Columns.Count; i++)
                        {
                            myCbcm.Items.Add(r[i].ToString().TrimEnd());
                        }

                    }
                    break;

            }
               
                
            


        }
        //대분류 클릭했을 시 해당 소분류 아이템 가져와 설정
        public void GetSmallCategory(string selectedItem, DataGridViewComboBoxCell cmb, int where)
        {
            switch (where)
            {
                case 1: //1 --> 지출
                    string spendSmallQuery = "SELECT userSpendSmallCtgy FROM userSpendCtgy WHERE userSpendBigCtgy = '" + selectedItem + "'";
                    DbClass myDb = new DbClass();
                    spendSmallDs = myDb.AdapterFill(spendSmallQuery, "spendSmall");
                    myDb.Close();
                    cmb.Items.Add("");
                    foreach (DataRow r in spendSmallDs.Tables[0].Rows)
                    {
                        for(int i = 0; i < spendSmallDs.Tables[0].Columns.Count;i++)
                        {
                            cmb.Items.Add(r[i].ToString().TrimEnd());
                        }
                    }
                    break;
                case 2: //2 --> 수입
                    string incomeSmallQuery = "SELECT userIncomeSmallCtgy FROM userIncomeCtgy WHERE userIncomeBigCtgy = '" + selectedItem + "'";
                    myDb = new DbClass();
                    incomeSmallDs = myDb.AdapterFill(incomeSmallQuery, "incomeSmall");
                    myDb.Close();
                    cmb.Items.Add("");
                    foreach (DataRow r in incomeSmallDs.Tables[0].Rows)
                    {
                        for (int i = 0; i < incomeSmallDs.Tables[0].Columns.Count;i++)
                        {
                            cmb.Items.Add(r[i].ToString().TrimEnd());
                        }
                    }
                    break;
            }
        }

        

        //대분류 가져오기 --> 대분류 카테고리 데이터그리드뷰에 항목채워넣기
        public DataTable SetCtgyList(string tableName, string userId, int groupNo, DataGridView myDg)
        {
            string bigSpendQuery = "SELECT userSpendBigCtgy FROM userSpendCtgy WHERE userid = 'admin' OR userid = 'pille' GROUP BY userSpendBigCtgy";
            string smallSpendQuery = "SELECT DISTINCT userincomebigctgy FROM userincomectgy ORDER BY userincomebigctgy DESC";
            string bankQuery = "SELECT DISTINCT bank FROM dbo.bank ORDER BY bank ASC";

            string myQuery = "";

            switch(tableName)
            {
                case "spendBig":
                    myQuery = "SELECT userSpendBigCtgy FROM userSpendCtgy WHERE userid = 'admin' OR userid = '" + userId + "' GROUP BY userSpendBigCtgy";
                    break;
                case "spendSmall":
                    //myQuery = "SELECT DISTINCT userincomebigctgy FROM userincomectgy WHERE userid = 'admin' OR userid = '" + userId + "' ORDER BY userincomebigctgy DESC";
                    break;
                case "incomeBig":
                    myQuery = "SELECT DISTINCT userincomebigctgy FROM userincomectgy WHERE userid = 'admin' OR userid = '" + userId + "' ORDER BY userincomebigctgy DESC";
                    break;
                case "incomeSmall":
                    //myQuery = "SELECT DISTINCT userincomesmallctgy FROM userincomectgy WHERE userid = 'admin' OR userid = '" + userId + "' ORDER BY userincomesmallctgy DESC";
                    break;

                case "bank":
                    myQuery = "SELECT DISTINCT bank FROM dbo.bank ORDER BY bank ASC";
                    break;

                case "save":
                    if(groupNo == 0)
                    myQuery = "SELECT savetype, savename FROM saveInfo WHERE userid = '"+ userId +"' AND groupNo = 0";
                    else
                        myQuery = "SELECT savetype, savename FROM saveInfo as si, grouptable as gt WHERE si.groupNo = " + groupNo;
                    break;
            }

            myDb = new DbClass();
            DataSet myDs = new DataSet(tableName);
            myDs = myDb.AdapterFill(myQuery, tableName);
            myDb.Close();
            DataTable myDt = myDs.Tables[tableName];



            foreach (DataRow r in myDt.Rows)
            {
                myDg.Rows.Add();

                for (int i = 0; i < myDt.Columns.Count; i++)
                {
                    //MessageBox.Show(r[i].ToString().TrimEnd());

                    myDg[i+1, j].Value = r[i].ToString().TrimEnd();
                } j++;
            }
            j = 0;
            return myDt;
        }

        public DataTable get카드명(string myQuery, string tableName, DataGridView myDg)
        {
            myDb = new DbClass();
            DataSet myDs = new DataSet(tableName);
            myDs = myDb.AdapterFill(myQuery, tableName);
            myDb.Close();
            DataTable myDt = myDs.Tables[tableName];



            foreach (DataRow r in myDt.Rows)
            {
                myDg.Rows.Add();

                for (int i = 0; i < myDt.Columns.Count + 1; i++)
                {
                    //MessageBox.Show(r[i].ToString().TrimEnd());

                    myDg[1, j].Value = r[0].ToString().TrimEnd();
                }
                j++;
            }
            j = 0;
            return myDt;
        }
        public void get카드명(DataGridView myDg, string userid, DataGridViewComboBoxColumn bank, DataGridViewComboBoxColumn place)
        {
            myDb = new DbClass();
            DataSet myDs = new DataSet();
            string myQuery = "SELECT bank.bank From bank WHERE userid = 'admin' OR userid = '" + userid + "'";
            string myQuery2 = "SELECT useplace FROM place WHERE userid = 'admin' or userid = '" + userid + "'";
            myDs = myDb.AdapterFill(myQuery, "bank");
            
            
            
            //은행사
            foreach (DataRow r in myDs.Tables[0].Rows)
            {
                for (int i = 0; i < myDs.Tables[0].Columns.Count;i++)
                {
                    bank.Items.Add(r[i].ToString().TrimEnd());
                }
            }
            myDs = myDb.AdapterFill(myQuery2, "place");
            myDb.Close();
            //사용처
            foreach (DataRow r in myDs.Tables["place"].Rows)
            {
                for (int i = 0; i < myDs.Tables["place"].Columns.Count; i++)
                {
                    place.Items.Add(r[i].ToString().TrimEnd());
                }
            }

        }

        //저축명추가
        public void GetSave(DataGridView myDg, string userid, DataGridViewComboBoxColumn myDgCbc, int groupNo)
        {
            myDb = new DbClass();
            string myQuery;
            if(groupNo > 0)
            {
                myQuery = "SELECT savename FROM savetable WHERE userid = 'admin' or groupNo = " + groupNo + " GROUP BY savename";
            }
            else
            {
                myQuery = "SELECT savename FROM savetable WHERE userid = 'admin' or userid = '" + userid + "' AND groupNo = " + groupNo + " GROUP BY savename";
            }

            
            saveMyDs = myDb.AdapterFill(myQuery, "saveTable");
            foreach(DataRow r in saveMyDs.Tables[0].Rows)
            {
                for (int i = 0; i < 1; i++)
                {
                    myDgCbc.Items.Add(r["savename"].ToString().TrimEnd());
                }
            }
        }

        // 저장 버튼 클릭시 실행될 메소드
        //그리드뷰 행 데이터를 읽어 Insert OR UPDATE 
        //그리드뷰 No열의 셀값을 읽어 DB에 각 테이블마다의 No값을 비교 없으면 INSERT 있으면 UPDATE
        public bool insertRows(DataGridView myDgrv, string tablename,string userId)
        {
            Id = userId;
            bool status = false;
            int x = 0;
            int y = myDgrv.RowCount;
            foreach (DataGridViewRow r in myDgrv.Rows)
            {
                x++; //마지막행은 입력용이므로 예외
                if (x < y)
                {

                    //index 0 : check / index 8 : No
                    //0 과 8은 입력 시 자동 입력 값임
                    //글에 있는 No값을 받아와서 Null값일 때만 실행

                    
                        for (int i = 1; i < myDgrv.ColumnCount + 1; i++)
                        {

                        if (myDgrv.Name == "dgrv지출")
                        {

                            //만약 삭제버튼을 눌렀던 행이라면 => 비지블이 false라면
                            if (r.Visible == false)
                            {
                                DeleteRow("spendtable", (int)r.Cells[8].Value);
                            }
                            else
                            {
                                switch (i)
                                {
                                    case 1:
                                        if (r.Cells[i].Value != null)
                                            date = r.Cells[i].Value.ToString();
                                        else
                                        {
                                            this.showMessage();
                                            goto EXIT;
                                        }
                                        break;
                                    case 2:
                                        if (r.Cells[i].Value != null)
                                            contents = r.Cells[i].Value.ToString();
                                        else contents = DBNull.Value.ToString();
                                        break;
                                    case 3:
                                        if (r.Cells[i].Value != null)
                                            amount = int.Parse(r.Cells[i].Value.ToString());
                                        else
                                        {
                                            this.showMessage();
                                            goto EXIT;
                                        }
                                        break;
                                    case 4:
                                        if (r.Cells[i].Value != null)
                                            bigC = r.Cells[i].Value.ToString();
                                        else bigC = DBNull.Value.ToString();
                                        break;
                                    case 5:
                                        if (r.Cells[i].Value != null)
                                            smallC = r.Cells[i].Value.ToString();
                                        else smallC = DBNull.Value.ToString();
                                        break;
                                    case 6:
                                        if (r.Cells[i].Value != null)
                                            cardname = r.Cells[i].Value.ToString();
                                        else cardname = DBNull.Value.ToString();
                                        break;
                                    case 7:
                                        if (r.Cells[i].Value != null)
                                            useplace = r.Cells[i].Value.ToString();
                                        else useplace = DBNull.Value.ToString();
                                        break;
                                    case 8:
                                        if (r.Cells[i].Value != null)
                                            no = int.Parse(r.Cells[i].Value.ToString());
                                        else no = 0;
                                        break;
                                    default:
                                        break;


                                }
                            }
                        }

                        else if (myDgrv.Name == "dgrv수입")
                        {
                            if (r.Visible == false)
                            {
                                DeleteRow("incometable", (int)r.Cells[7].Value);
                            }
                            else
                            {
                                switch (i)
                                {
                                    case 1://수입날짜 NOT NULL
                                        if (r.Cells[i].Value != null)
                                            date = r.Cells[i].Value.ToString();
                                        else
                                        {
                                            this.showMessage();
                                            goto EXIT;
                                        }
                                        break;
                                    case 2: //수입내역
                                        if (r.Cells[i].Value != null)
                                            contents = r.Cells[i].Value.ToString();
                                        else contents = DBNull.Value.ToString();
                                        break;
                                    case 3://금액 NOT NULL
                                        if (r.Cells[i].Value != null)
                                            amount = int.Parse(r.Cells[i].Value.ToString());
                                        else
                                        {
                                            this.showMessage();
                                            goto EXIT;
                                        }
                                        break;
                                    case 4://대분류
                                        if (r.Cells[i].Value != null)
                                            bigC = r.Cells[i].Value.ToString();
                                        else bigC = DBNull.Value.ToString();
                                        break;
                                    case 5://소분류
                                        if (r.Cells[i].Value != null)
                                            smallC = r.Cells[i].Value.ToString();
                                        else smallC = DBNull.Value.ToString();
                                        break;
                                    case 6://비고
                                        if (r.Cells[i].Value != null)
                                            note = r.Cells[i].Value.ToString();
                                        else note = DBNull.Value.ToString();
                                        break;

                                    case 7://신규행이면 No값 없음
                                        if (r.Cells[i].Value != null)
                                            no = int.Parse(r.Cells[i].Value.ToString());
                                        else no = 0;
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }
                        else //저축
                        {
                            if (r.Visible == false)
                            {
                                DeleteRow("savetable", (int)r.Cells[6].Value);
                            }
                            else
                            {
                                switch (i)
                                {

                                    case 1: //날짜 NOT NULL
                                        if (r.Cells[i].Value != null)
                                            date = r.Cells[i].Value.ToString();
                                        else
                                        {
                                            this.showMessage();
                                            goto EXIT;
                                        }
                                        break;
                                    case 2: //저축명 NOT NULL
                                        if (r.Cells[i].Value != null)
                                            savename = r.Cells[i].Value.ToString();
                                        else
                                        {
                                            this.showMessage();
                                            goto EXIT;
                                        }
                                        break;
                                    case 3: //불입금액 NOT NULL
                                        if (r.Cells[i].Value != null)
                                            amount = int.Parse(r.Cells[i].Value.ToString());
                                        else
                                        {
                                            this.showMessage();
                                            goto EXIT;
                                        }
                                        break;
                                    case 4://누적금액 할일없음

                                    case 5://비고 NULL
                                        if (r.Cells[i].Value != null)
                                            note = r.Cells[i].Value.ToString();
                                        else note = DBNull.Value.ToString();
                                        break;

                                    case 6://No 신규행이면 No 없음
                                        if (r.Cells[i].Value != null)
                                            no = int.Parse(r.Cells[i].Value.ToString());
                                        else no = 0;
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }
                    }

                    //현재 내 데이터그리드뷰의 한줄을 읽어올때
                    //no열의 필드값이 Null일때
                    //새로운 줄에 입력했던 데이터를 입력한다.
                    try
                    {

                        if (myDgrv.Name == "dgrv지출")
                        {
                            if (no == 0)
                            {
                                DbClass myDb = new DbClass();
                                myDb.InsertNewRow(tablename, date, contents, amount, bigC, smallC, cardname, useplace, null, userId, null);
                                myDb.Close();
                            }

                            else if (no == (int)r.Cells[8].Value)
                            {
                                
                                UpdateRow("spendtable", date, contents, amount, bigC, smallC, cardname, useplace, userId, no, null, null);
                                
                            }
                        }

                        else if (myDgrv.Name == "dgrv수입")
                        {
                            if (no == 0)
                            {
                                DbClass myDb = new DbClass();
                                myDb.InsertNewRow(tablename, date, contents, amount, bigC, smallC, null, null, note, userId, null);
                                myDb.Close();
                            }

                            else if (no == (int)r.Cells[7].Value)
                            {
                                
                                UpdateRow("incometable", date, contents, amount, bigC, smallC, null, null, userId, no, note,null);
                                
                            }
                        }

                        else
                        {
                            if (no == 0)
                            {
                                DbClass myDb = new DbClass();
                                myDb.InsertNewRow(tablename, date, null, amount, bigC, smallC, null, null, note, userId, null);
                                myDb.Close();
                            }

                            else if (no == (int)r.Cells[6].Value)
                            {
                                
                                UpdateRow("savetable", date, null, amount, bigC, smallC, null, null, userId, no, note, savename);
                                
                            }
                        }
                        status = true;
                    }
                    catch
                    {
                        status = false;
                        MessageBox.Show(String.Format("{0}의 {1}가 오류발생",tablename,no.ToString()));
                    }


                }
                

            }

            //status = true;
        EXIT:;
            return status;
        }

        public void showMessage()
        {
            MessageBox.Show("입력값을 확인하세요.");
        }

        //값이 없는 빈행들 일괄 삭제
        public void removeRows(DataGridView myDgrv)
        {
            int cntRows = 0;
            int myDgrvCnt = myDgrv.RowCount-1;
            
            foreach (DataGridViewRow r in myDgrv.Rows)
            {
                

                if(r.Cells[1].Value == null || r.Cells[3].Value == null || r.Cells[8].Value == null)
                {

                    //myDgrv.Rows.Remove(myDgrv.CurrentRow);
                    if (cntRows < myDgrvCnt)
                        
                            myDgrv.Rows.Remove(myDgrv.Rows[cntRows]);
                    
                    
                    
                    

                }
                
                
                cntRows++;
            }
            //myFKM.DisplayList();
            //MessageBox.Show("빈 행 삭제 완료");
            
        }

        public int[] DisplayList(DataGridView spend, DataGridView income, DataGridView save, string startDate, string endDate, string userid,int groupNo)
        {
            spend.Rows.Clear();
            income.Rows.Clear();
            save.Rows.Clear();
            //
            int[] tot = new int[4];

            int j = 0;
            myDb = new DbClass();
            //지출 설정
            string sQuery;
            if (groupNo > 0)
            {
                sQuery = "SELECT spendcheck, Convert(varchar,datespend,126) as datespend, spendcontent, spendmoney, sortlarge, sortsmall, cardname, useplace, no FROM spendtable as st, groupMemberTable as gmt WHERE datespend >= '" + startDate + "' AND datespend <= '" + endDate + "' AND st.groupNo = " + groupNo + " AND gmt.groupNo = " + groupNo + " ORDER BY datespend DESC";
            }
            else
            {
                sQuery = "SELECT spendcheck, Convert(varchar,datespend,126) as datespend, spendcontent, spendmoney, sortlarge, sortsmall, cardname, useplace, no FROM spendtable WHERE datespend >= '" + startDate + "' AND datespend <= '" + endDate + "' AND userid = '" + userid + "' AND groupNo = " + groupNo + " ORDER BY datespend DESC";
            }
            spendAllDs = myDb.AdapterFill(sQuery, "spendtable");
            foreach (DataRow r in spendAllDs.Tables[0].Rows)
            {
                spend.Rows.Add();
                //자동으로 생성된 마지막 행에는 접근 x
                if (j >= spendAllDs.Tables[0].Rows.Count)
                    j = 0;
                for (int i = 0; i < spendAllDs.Tables[0].Columns.Count; i++)
                {
                    if (i != 0 && i != 5 && i != 8 && i != 3)
                    {
                        spend[i, j].Value = r[i].ToString().TrimEnd();

                    }
                    else if (i == 0)
                    {
                        spend[i, j].Value = false;

                    }
                    

                    else if (i == 5)
                    {
                        if (r[i] != null)
                        {
                            DataGridViewRow row = spend.Rows[j];
                            DataGridViewComboBoxCell myCb = (DataGridViewComboBoxCell)(row.Cells[5]);
                            myCb.Items.Add(r[i].ToString().TrimEnd());
                            myCb.Value = myCb.Items[0];
                        }
                    }

                    //합계 구하기
                    else if (i == 3)
                    {
                        if (r["cardname"].ToString().TrimEnd() == "") //카드명이 있다면 카드합계에
                            tot[1] += int.Parse(r[i].ToString().TrimEnd());
                        else
                            tot[2] += int.Parse(r[i].ToString().TrimEnd());

                        spend[i, j].Value = r[i].ToString().TrimEnd();
                    }

                    else
                    {
                        //
                        if (spend[i, j].Value != r[i])
                        {
                            spend[i, j].Value = r[i];
                        }

                    }

                }
                j++;

            }

            //수입
            string sQuery2;
            if(groupNo > 0)
            {
                sQuery2 = "SELECT incomecheck, Convert(varchar,dateincome,126) as dateincome, incomecontent, incomemoney, incomelarge, incomesmall,note, no FROM incometable as it, groupMemberTable as gmt WHERE dateincome >= '" + startDate + "' AND dateincome <= '" + endDate + "' AND it.groupNo = " + groupNo + " AND gmt.groupNo = " + groupNo + " ORDER BY dateincome DESC";

            }
            else
            {
                sQuery2 = "SELECT incomecheck, Convert(varchar,dateincome,126) as dateincome, incomecontent, incomemoney, incomelarge, incomesmall,note, no FROM incometable WHERE dateincome >= '" + startDate + "' AND dateincome <= '" + endDate + "' AND userid = '" + userid + "' AND groupNo = "+groupNo+" ORDER BY dateincome DESC";
            }
            incomeAllDs = myDb.AdapterFill(sQuery2, "incometable");
            j = 0;
            foreach (DataRow r in incomeAllDs.Tables[0].Rows)
            {
                income.Rows.Add();
                
                //자동으로 생성된 마지막 행에는 접근 x
                if (j >= incomeAllDs.Tables[0].Rows.Count)
                    j = 0;
                for (int i = 0; i < incomeAllDs.Tables[0].Columns.Count; i++)
                {
                    if (i != 0 && i != 5 && i != 7 && i != 3)
                    {
                        income[i, j].Value = r[i].ToString().TrimEnd();

                    }
                    else if (i == 0)
                    {
                        income[i, j].Value = false;

                    }
                    else if (i == 3) //합계구하기
                    {

                        tot[0] += int.Parse(r[i].ToString().TrimEnd());
                        income[i, j].Value = r[i].ToString().TrimEnd();
                    }

                    else if (i == 5)
                    {
                        if (r[i] != null)
                        {
                            DataGridViewRow row = income.Rows[j];
                            DataGridViewComboBoxCell myCb = (DataGridViewComboBoxCell)(row.Cells[5]);
                            myCb.Items.Add(r[i].ToString().TrimEnd());
                            myCb.Value = myCb.Items[0];
                        }
                    }
                    else
                    {
                        //
                        if (income[i, j].Value != r[i])
                        {
                            income[i, j].Value = r[i];
                        }

                    }

                }
                j++;

            }

            //저축
            string sQuery3;
            if(groupNo > 0)
            {
                sQuery3 = "SELECT savecheck, Convert(varchar,savedate,126), savename, savemoney,savenote,no FROM savetable as st, groupMemberTable as gmt WHERE savedate >= '" + startDate + "' AND savedate <= '" + endDate + "' AND userid = '" + userid + "' AND st.groupNo = '"+groupNo +"' AND gmt.groupNo = " + groupNo + "";

            }
            else
            {
                sQuery3 = "SELECT savecheck, Convert(varchar,savedate,126), savename, savemoney,savenote,no FROM savetable WHERE savedate >= '" + startDate + "' AND savedate <= '" + endDate + "' AND groupNo = '" + groupNo + "' AND userid = '" + userid +"'";

            }
            saveMyDs = myDb.AdapterFill(sQuery3, "savetable");
            j = 0;
            foreach (DataRow r in saveMyDs.Tables[0].Rows)
            {
                save.Rows.Add();
                //자동으로 생성된 마지막 행에는 접근 x
                if (j >= saveMyDs.Tables[0].Rows.Count)
                    j = 0;
                for (int i = 0; i < saveMyDs.Tables[0].Columns.Count; i++)
                {
                    if (i != 0 && i != 2 && i != 5)
                    {
                        if(i < 4)
                        save[i, j].Value = r[i].ToString().TrimEnd();
                        else
                        save[i+1, j].Value = r[i].ToString().TrimEnd();


                    }
                    else if (i == 0)//체크박스
                    {
                        save[i, j].Value = false;

                    }
                   

                    else if (i == 2)//저축명 콤보박스
                    {
                        if (r[i] != null)
                        {
                            //DataGridViewRow row = save.Rows[j];
                            //DataGridViewComboBoxCell myCb = (DataGridViewComboBoxCell)(row.Cells[2]);
                            //myCb.Items.Add(r[i].ToString().TrimEnd());
                            //myCb.Value = myCb.Items[0];
                            save[i, j].Value = r[i].ToString().TrimEnd();
                        }
                    }
                    else //no확인
                    {
                        
                        if (save[i, j].Value != r[i])//현재 행no와 같지 않다면
                        {
                            save[i, j].Value = r[i];
                        }

                    }

                }
                j++;

            }

            spend.ClearSelection();
            income.ClearSelection();
            save.ClearSelection();
            tot[3] = tot[1] + tot[2];
            return tot;
        }

        public int UpdateRow(string tablename, string date, string content, int money, string big, string small, string card, string useplace, string userid, int no, string note, string savename)
        {
            int result = 0 ;

            switch (tablename)
            {
                case "spendtable":
                    string qryUpdate = "UPDATE " + tablename + " SET datespend = '" + date + "', spendcontent = '" + content + "', spendmoney ="
                + money + ", sortlarge='" + big + "', sortsmall='" + small + "', cardname='"
                + card + "', useplace='" + useplace + "' WHERE no =" + no + " AND userid = '"+userid+"'";
                    SqlCommand myCmd = new SqlCommand();
                    myDb = new DbClass();
                    myCmd = myDb.getCmd(qryUpdate);
                    myCmd.ExecuteNonQuery();
                    myDb.Close();
                    break;

                case "incometable":
                    qryUpdate = "UPDATE " + tablename + " SET dateincome = '" + date + "', incomecontent = '" + content + "', incomemoney ="
                        + money + ", incomelarge = '" + big + "', incomesmall = '" + small + "', note ='" + note + "' WHERE no = " + no + " AND userid = '" + userid + "'";
                    myCmd = new SqlCommand();
                    myDb = new DbClass();
                    myCmd = myDb.getCmd(qryUpdate);
                    myCmd.ExecuteNonQuery();
                    myDb.Close();
                    break;

                case "savetable":
                    qryUpdate = "UPDATE " + tablename + " SET savedate = '" + date + "', savename = '" + savename + "', savemoney = " + money + ", savenote ='" 
                        + note + "' WHERE no = " + no + " AND userid = '" + userid + "'";
                    myCmd = new SqlCommand();
                    myDb = new DbClass();
                    myCmd = myDb.getCmd(qryUpdate);
                    myCmd.ExecuteNonQuery();
                    myDb.Close();
                    break;


            }

            return result;
        }

        //로그인 인증 확인
        public bool Authenticate(string id, string pwd)
        {
            bool isAuthen = false;
            string sQuery = "SELECT userpwd, [status] FROM Members WHERE userid = '" + id + "'";

            myDb = new DbClass();
            SqlDataReader myReader = myDb.ExecuteReader(sQuery);
            if(myReader.Read())
            {
                if (bool.Parse(myReader["status"].ToString()) == false)
                    return false;
                MD5 md5 = new MD5CryptoServiceProvider();

                byte[] hashArray = md5.ComputeHash(Encoding.ASCII.GetBytes(pwd));

                string hashText = Convert.ToBase64String(hashArray);
                //MessageBox.Show(hashText);
                if (hashText == myReader["userpwd"].ToString().TrimEnd())
                    isAuthen = true;

            }
            myReader.Close();
            myDb.Close();


            return isAuthen;
        }

        //현재 로그인된 유저 정보를 가져와 가입한 그룹을 넘겨줌
        public DataSet SetGroupList(ListBox myLB, string userId)
        {
            myLB.Items.Clear();
            DataSet myDs = new DataSet();
            string sQuery = "SELECT groupName, gt.groupNo FROM groupTable as gt, groupMemberTable as gmt WHERE gmt.[status] = 1 AND gt.groupNo = gmt.groupNo AND gmt.userid = '" + userId + "' ORDER BY gmt.GroupJoinDate DESC";
            myDb = new DbClass();
            myDs = myDb.AdapterFill(sQuery, "GroupList");
            
            myDb.Close();
            
            
            foreach (DataRow r in myDs.Tables["GroupList"].Rows)
            {
                for (int i = 0; i < myDs.Tables["GroupList"].Columns.Count-1; i++)
                {
                    myLB.Items.Add(r[i].ToString().TrimEnd());
                }
            }

            return myDs;
        }

        //그룹선택시 해당 그룹멤버를 표시해줌
        public void SetGroupMemberList(ListBox myLb, string userId, string groupName, DataSet myDs)
        {
            myLb.Items.Clear();
            DataSet myMemberDs = new DataSet();
            //int groupNo = int.Parse(myDs.Tables["GroupList"].Select("groupName = '" + groupName + "'").GetValue(1).ToString());
            DataRow[] r2 = myDs.Tables["GroupList"].Select("GroupName = '" + groupName + "'");
            string sQuery = "SELECT gmt.userid FROM groupTable as gt, groupMemberTable as gmt WHERE gmt.[status] = 1 AND gt.groupName = '" + groupName + "' AND gmt.groupNo = " + r2[0]["groupNo"] + " GROUP BY userid";

            myDb = new DbClass();
            myMemberDs = myDb.AdapterFill(sQuery, "GroupMemberList");
            myDb.Close();
            foreach(DataRow r in myMemberDs.Tables["GroupMemberList"].Rows)
            {
                for(int i = 0; i < myMemberDs.Tables["GroupMemberList"].Columns.Count;i++)
                {
                    myLb.Items.Add(r[i].ToString().TrimEnd());
                }
            }
        }


        //그룹 고유 정보를 받아와 lbl 바꿔주고 추가정보를 넘겨줌
        public DataSet SetGroupInfo(string GroupName, string GroupCode, Label lblID, Label lblDate)
        {
            DataSet myGroupSet = new DataSet();
            string sQuery = "SELECT groupName, groupCode, groupMaster, Convert(varchar,groupDate,126) as groupDate FROM groupTable WHERE groupName = '" + GroupName + "' AND groupCode = '" + GroupCode + "'";

            myDb = new DbClass();
            myGroupSet = myDb.AdapterFill(sQuery, "GroupInfo");
            myDb.Close();
            foreach(DataRow r in myGroupSet.Tables["GroupInfo"].Rows)
            {
                for(int i = 0; i<myGroupSet.Tables["GroupInfo"].Columns.Count;i++)
                {
                    lblID.Text = r[2].ToString().TrimEnd();
                    lblDate.Text = r[3].ToString().TrimEnd();
                }
            }

            return myGroupSet;
        }

        public bool IsDuplicated(string userId)
        {
            //디폴트값 참 => 중복이다.
            bool IsDupl = true;
            string sQuery = "SELECT * FROM Members WHERE userid = '" + userId + "'";
            myDb = new DbClass();
            SqlDataReader myReader = myDb.ExecuteReader(sQuery);
            if (myReader.Read())
                IsDupl = false; //값이 있으면 중복아님
            myReader.Close();
            return IsDupl;

        }

        public int RegisterUser(string userId, string pwd, string username, string gender, string email, string phone, string birthday)
        {
            int result = 0;
            myDb = new DbClass();
            SqlCommand myCmd = myDb.getCmd("procInsertMember");
            myCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter myParam;
            //id
            myParam = new SqlParameter("@userid", SqlDbType.Char, 20);
            myParam.Value = userId;
            myCmd.Parameters.Add(myParam);
            //pwd
            myParam = new SqlParameter("@userpwd", SqlDbType.Char, 32);
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] hashArray = md5.ComputeHash(Encoding.ASCII.GetBytes(pwd));

            string hashText = Convert.ToBase64String(hashArray);
            myParam.Value = hashText;
            myCmd.Parameters.Add(myParam);
            //username
            myParam = new SqlParameter("@username", SqlDbType.NVarChar, 20);
            myParam.Value = username;
            myCmd.Parameters.Add(myParam);
            //gender
            myParam = new SqlParameter("@usergender", SqlDbType.NVarChar, 4);
            myParam.Value = gender;
            myCmd.Parameters.Add(myParam);
            //email
            myParam = new SqlParameter("@email", SqlDbType.Char, 50);
            myParam.Value = email;
            myCmd.Parameters.Add(myParam);
            //phone
            myParam = new SqlParameter("@phone", SqlDbType.NChar, 13);
            myParam.Value = phone;
            myCmd.Parameters.Add(myParam);
            //birthday
            myParam = new SqlParameter("@birthday", SqlDbType.SmallDateTime);
            myParam.Value = birthday;
            myCmd.Parameters.Add(myParam);
            //result
            SqlParameter resultParam = new SqlParameter("@result", SqlDbType.Int);
            resultParam.Direction = ParameterDirection.Output;
            myCmd.Parameters.Add(resultParam);

            myCmd.ExecuteNonQuery();
            result = (int)resultParam.Value;
            //0 실패 , 1 성공
            
            return result;
        }

        public void DeleteRow(string tablename, int no)
        {
            myDb = new DbClass();
            string sQuery = "DELETE FROM " + tablename + " WHERE no = " + no;
            myDb.ExecuteNonQuery(sQuery);
            myDb.Close();
        }
    }

}
