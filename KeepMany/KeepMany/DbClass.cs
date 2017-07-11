using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace KeepMany
{
    class DbClass
    {
        string sDbSrc = "Server=PILLE\\SQLEXPRESS; uid=keepmany; pwd=2721; database=KeepMany;";

        SqlConnection myConn;
        public DbClass()
        {
            myConn = new SqlConnection(sDbSrc);
            myConn.Open();
        }

        public void Open()
        {
            myConn = new SqlConnection(sDbSrc);
            myConn.Open();
        }

        public void Close()
        {
            myConn.Close();
        }

        public SqlCommand getCmd(string sQuery)
        {
            SqlCommand myCmd = new SqlCommand(sQuery,myConn);
            return myCmd;
        }

        public void ExecuteNonQuery(string sQuery)
        {
            SqlCommand myCmd = new SqlCommand(sQuery, myConn);
            myCmd.ExecuteNonQuery();
        }
        public SqlDataReader ExecuteReader(string sQuery)
        {
            SqlCommand myCmd = new SqlCommand(sQuery, myConn);
            return myCmd.ExecuteReader();

        }

        public DataSet AdapterFill(string sQuery, string tablename)
        {
            SqlDataAdapter myAdpt = new SqlDataAdapter(sQuery, myConn);
            DataSet myDs = new DataSet(tablename);
            //DataTable myDt = new DataTable();
            myAdpt.Fill(myDs, tablename);
            myAdpt.Dispose();
            return myDs;
        }

        public DataTable AdapterFillTable(string sQuery)
        {
            SqlDataAdapter myAdpt = new SqlDataAdapter(sQuery, myConn);
            //DataSet myDs = new DataSet(tablename);
            DataTable myDt = new DataTable();
            myAdpt.Fill(myDt);
            return myDt;
        }

        public DataSet GetSpendData()
        {
            string sQuery = "SELECT spendcheck, Convert(varchar,datespend,126) as datespend, spendcontent, spendmoney, sortlarge, sortsmall, cardname, useplace, no FROM spendtable";

            return this.AdapterFill(sQuery, "spendtable");

        }

        //새로운 가계부행 추가
        public void InsertNewRow(string tablename, string date, string content, int money, string big, string small, string card, string useplace,string note, string userid, string savename)
        {
            //저장프로시져
            string myProc = "procInsert" + tablename;
            SqlCommand myCmd = new SqlCommand(myProc, myConn);
            myCmd.CommandType = CommandType.StoredProcedure;
            //파라미터 지정
            SqlParameter myParam;
            if (tablename == "Spend")
            {
                //date
                myParam = new SqlParameter("@date", SqlDbType.Date);
                myParam.Value = date;
                myCmd.Parameters.Add(myParam);
                //content
                myParam = new SqlParameter("@content", SqlDbType.NVarChar, 150);
                myParam.Value = content;
                myCmd.Parameters.Add(myParam);
                //money
                myParam = new SqlParameter("@money", SqlDbType.Int);
                myParam.Value = money;
                myCmd.Parameters.Add(myParam);
                //big
                myParam = new SqlParameter("@large", SqlDbType.NVarChar, 15);
                myParam.Value = big;
                myCmd.Parameters.Add(myParam);
                //small
                myParam = new SqlParameter("@small", SqlDbType.NVarChar, 15);
                myParam.Value = small;
                myCmd.Parameters.Add(myParam);
                //card
                myParam = new SqlParameter("@cardname", SqlDbType.NVarChar, 15);
                myParam.Value = card;
                myCmd.Parameters.Add(myParam);
                //useplace
                myParam = new SqlParameter("@useplace", SqlDbType.NVarChar, 20);
                myParam.Value = useplace;
                myCmd.Parameters.Add(myParam);
                //userid
                myParam = new SqlParameter("@id", SqlDbType.Char, 20);
                myParam.Value = userid;
                myCmd.Parameters.Add(myParam);
            }
            else if(tablename == "Income")
            {
                myParam = new SqlParameter("@date", SqlDbType.Date);
                myParam.Value = date;
                myCmd.Parameters.Add(myParam);
                //content
                myParam = new SqlParameter("@content", SqlDbType.NVarChar, 150);
                myParam.Value = content;
                myCmd.Parameters.Add(myParam);
                //money
                myParam = new SqlParameter("@money", SqlDbType.Int);
                myParam.Value = money;
                myCmd.Parameters.Add(myParam);
                //big
                myParam = new SqlParameter("@large", SqlDbType.NVarChar, 15);
                myParam.Value = big;
                myCmd.Parameters.Add(myParam);
                //small
                myParam = new SqlParameter("@small", SqlDbType.NVarChar, 15);
                myParam.Value = small;
                myCmd.Parameters.Add(myParam);
                //note
                myParam = new SqlParameter("@note", SqlDbType.NVarChar, 150);
                myParam.Value = note;
                myCmd.Parameters.Add(myParam);
                //id
                myParam = new SqlParameter("@id", SqlDbType.Char, 15);
                myParam.Value = userid;
                myCmd.Parameters.Add(myParam);
            }
            //@date date, @name nvarchar(50), @save int, 
            //@accure int, @note nvarchar(150), @userid char(20)
            else
            {
                //savedate
                myParam = new SqlParameter("@date", SqlDbType.Date);
                myParam.Value = date;
                myCmd.Parameters.Add(myParam);
                //savename
                myParam = new SqlParameter("@name", SqlDbType.NVarChar, 50);
                myParam.Value = savename;
                myCmd.Parameters.Add(myParam);
                //savemoney
                myParam = new SqlParameter("@money", SqlDbType.Int);
                myParam.Value = money;
                myCmd.Parameters.Add(myParam);
                ////accuremoney 계산해서 가져올것임
                //myParam = new SqlParameter("@accure", SqlDbType.Int);
                //myParam.Value = accure;
                //myCmd.Parameters.Add(myParam);
                //savenote
                myParam = new SqlParameter("@note", SqlDbType.NVarChar, 150);
                myParam.Value = note;
                myCmd.Parameters.Add(myParam);
                //id
                myParam = new SqlParameter("@userid", SqlDbType.Char,20);
                myParam.Value = userid;
                myCmd.Parameters.Add(myParam);
            }
            //실행
            myCmd.ExecuteNonQuery();

        }

        public void UpdateRow(string tablename, string date, string content, int money, string big, string small, string card, string useplace, string userid, int no)
        {
            string qryUpdate = "UPDATE " + tablename + " SET datespend = '" + date + "', spendcontent = '" + content + "', spendmoney =" 
                + money + ", sortlarge='" + big + "', sortsmall='" + small + "', cardname='" 
                + card + "', useplace='" + useplace + "' WHERE no =" + no;
            SqlCommand myCmd = new SqlCommand(qryUpdate, myConn);
            myCmd.ExecuteNonQuery();
        }


        
    
       
    
    }

    
}
