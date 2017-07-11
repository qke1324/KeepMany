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
    public partial class FCategory : Form
    {
        static string ID;
        static int GroupNo;
        

        
        public FCategory()
        {
            InitializeComponent();
        }
        public FCategory(string userId, int groupNo)
        {
            InitializeComponent();
            ID = userId;
            GroupNo = groupNo;
        }

        private void FCategory_Load(object sender, EventArgs e)
        {
            //객체생성이후 데이터그리드뷰에 데이터 채워넣는 메소드 실행
            MethodClass myMc = new MethodClass();
            myMc.SetCtgyList("spendBig",ID, GroupNo, dgrv지출대분류);
            myMc.SetCtgyList("incomeBig",ID, GroupNo, dgrv수입대분류);
            myMc.SetCtgyList("spendBig",ID, GroupNo, dgrv지출대분류서브);
            myMc.SetCtgyList("incomeBig",ID, GroupNo, dgrv수입대분류서브);
            myMc.SetCtgyList("bank",ID, GroupNo, dgrv카드명추가);
            myMc.SetCtgyList("save", ID, GroupNo, dgrv저축명추가);


        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
