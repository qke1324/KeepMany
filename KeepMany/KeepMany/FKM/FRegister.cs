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
    public partial class FRegister : Form
    {
        MethodClass myMc;
        bool IsDuplChecked = false;
        string Mgender = "";
        bool IsFirst = true;


        public FRegister()
        {
            InitializeComponent();
            
        }


        private void btnRDupl_Click(object sender, EventArgs e)
        {
            
            if(textBoxID.Text.Length <= 0)
            {
                lblRError.Text = "ID를 입력해 주세요.";
                textBoxID.Focus();
                return;
            }

            myMc = new MethodClass();
            if(IsDuplChecked = myMc.IsDuplicated(textBoxID.Text))
            {
                lblRError.Text = "사용가능한 ID 입니다.";
                textBoxID.Focus();
            }
            else
            {
                lblRError.Text = "이미 사용중인 ID 입니다.";
                textBoxID.Text = "";
                textBoxID.Focus();
            }
            

        }

        private void btnRMale_Click(object sender, EventArgs e)
        {
            btnRFemale.ForeColor = Color.DeepSkyBlue;
            btnRMale.ForeColor = Color.Firebrick;
            Mgender = "남자";
        }

        private void btnRFemale_Click(object sender, EventArgs e)
        {
            btnRFemale.ForeColor = Color.Firebrick;
            btnRMale.ForeColor = Color.SkyBlue;
            Mgender = "여자";
        }

        private void btnRRegister_Click(object sender, EventArgs e)
        {
            if(!IsDuplChecked)
            {
                textBoxID.Focus();
                lblRError.Text = "중복검사를 해주세요.";
                return;
            }
            int result = myMc.RegisterUser(textBoxID.Text, textBoxPWD.Text, textBoxName.Text, Mgender, textBoxEmail.Text, textBoxPhone.Text, comboBoxYear.SelectedItem.ToString() + "-" + comboBoxMonth.SelectedItem.ToString() + "-" + comboBoxDay.SelectedItem.ToString());
            if (result == 1)
            {
                MessageBox.Show("회원가입 성공");
                this.Close();
            }
            else
            {
                MessageBox.Show("회원가입 실패");
                ClearAll();
            }
        }
        public void ClearAll()
        {
            textBoxID.Text = "ID";
            textBoxID.ForeColor = Color.Silver;
            textBoxPWD.Text = "PW";
            textBoxPWD.ForeColor = Color.Silver;
            textBoxPWD2.Text = "PW 재확인";
            textBoxPWD2.ForeColor = Color.Silver;
            textBoxName.Text = "이름";
            textBoxName.ForeColor = Color.Silver;
            btnRFemale.ForeColor = Color.DeepSkyBlue;
            btnRMale.ForeColor = Color.DeepSkyBlue;
            textBoxEmail.Text = "이메일";
            textBoxEmail.ForeColor = Color.Silver;
            textBoxPhone.Text = "전화번호";
            textBoxPhone.ForeColor = Color.Silver;

        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox myTB = (TextBox)sender;
            if (myTB.Text == "ID" || myTB.Text == "PW" || myTB.Text == "PW  재확인" || myTB.Text == "이름" || myTB.Text == "이메일" || myTB.Text == "전화번호")
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
                    case 3:
                        myTB.Text = "PW";
                        break;
                    case 4:
                        myTB.Text = "PW  재확인";
                        break;
                    case 5:
                        myTB.Text = "이름";
                        break;
                    case 6:
                        myTB.Text = "이메일";
                        break;
                    case 7:
                        myTB.Text = "전화번호";
                        break;


                }
                myTB.ForeColor = Color.Silver;
            }
            else
            {
                
            }

        }

        private void FRegister_Load(object sender, EventArgs e)
        {
            
            SetDate(sender, e);
            
            textBoxID.Select();
        }

        public void SetDate(object sender, EventArgs e)
        {
            int i;
            int currentYear = int.Parse(DateTime.Now.Year.ToString());
            for (i = 1901; i <= currentYear; i++)
            {
                comboBoxYear.Items.Add(i);
                
            }
            comboBoxYear.Update();
            
            comboBoxYear.SelectedIndex = i - 1901 - 1;
            //comboBoxYear.SelectedIndex = 0;
            for (i = 1; i <= 12; i++)
            {
                comboBoxMonth.Items.Add(i);

            }
            comboBoxMonth.Update();
            IsFirst = false;
            comboBoxMonth.SelectedIndex = 0;

            //comboBoxMonth_SelectedIndexChanged(sender, e);


            //-------------------------------------
        //    int dayMax = 0;
        //    int selectedDay;
        //    int selectedYear = int.Parse(comboBoxYear.SelectedItem.ToString());
        //    if (comboBoxDay.SelectedIndex != -1)
        //    {
        //        selectedDay = comboBoxDay.SelectedIndex;
        //    }
        //    else
        //    {
        //        selectedDay = 0;
        //    }
        //    if ((selectedYear % 400) == 0 || (selectedYear % 4) == 0 && (selectedYear % 100) != 0)
        //    {
        //        switch (comboBoxMonth.SelectedItem.ToString())
        //        {
        //            case "2": dayMax = 29; break;
        //            case "4":
        //            case "6":
        //            case "7":
        //            case "9":
        //            case "11": dayMax = 30; break;
        //            default: dayMax = 31; break;
        //        }

        //    }
        //    else
        //    {

        //        switch (comboBoxMonth.SelectedItem.ToString())
        //        {
        //            case "2": dayMax = 28; break;
        //            case "4":
        //            case "6":
        //            case "7":
        //            case "9":
        //            case "11": dayMax = 30; break;
        //            default: dayMax = 31; break;
        //        }
        //    }

        //    comboBoxDay.Items.Clear();
        //    for (int i = 1; i <= dayMax; i++) comboBoxDay.Items.Add(i);
        //    comboBoxDay.SelectedIndex = selectedDay;
        
    }

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dayMax = 0;
            int selectedDay;
            int selectedYear = int.Parse(comboBoxYear.SelectedItem.ToString());
            if (!IsFirst)
            {
                if (comboBoxDay.SelectedIndex != -1)
                {
                    selectedDay = comboBoxDay.SelectedIndex;
                }
                else
                {
                    selectedDay = 0;
                }
                if ((selectedYear % 400) == 0 || (selectedYear % 4) == 0 && (selectedYear % 100) != 0)
                {
                    switch ((comboBoxMonth.SelectedIndex + 1).ToString())
                    {
                        case "2": dayMax = 29; break;
                        case "4":
                        case "6":
                        case "7":
                        case "9":
                        case "11": dayMax = 30; break;
                        default: dayMax = 31; break;
                    }

                }
                else
                {

                    switch ((comboBoxMonth.SelectedIndex + 1).ToString())
                    {
                        case "2": dayMax = 28; break;
                        case "4":
                        case "6":
                        case "7":
                        case "9":
                        case "11": dayMax = 30; break;
                        default: dayMax = 31; break;
                    }
                }

                comboBoxDay.Items.Clear();
                for (int i = 1; i <= dayMax; i++) comboBoxDay.Items.Add(i);
                comboBoxDay.SelectedIndex = selectedDay;
            }
            IsFirst = false;
        }
    }
}
