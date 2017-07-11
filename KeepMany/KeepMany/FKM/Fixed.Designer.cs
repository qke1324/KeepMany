namespace KeepMany.FKM
{
    partial class Fixed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgrv지출 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnFixedClose = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.고정수입체크 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.고정수입날짜 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.고정수입내역 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.고정수입금액 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.고정수입대분류 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.고정수입소분류 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.고정지출체크 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.고정지출날짜 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.고정지출내역 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.고정지출금액 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.고정지출대분류 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.고정지출소분류 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgrv지출)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrv지출
            // 
            this.dgrv지출.AllowUserToResizeColumns = false;
            this.dgrv지출.AllowUserToResizeRows = false;
            this.dgrv지출.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgrv지출.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrv지출.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.고정지출체크,
            this.고정지출날짜,
            this.고정지출내역,
            this.고정지출금액,
            this.고정지출대분류,
            this.고정지출소분류});
            this.dgrv지출.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgrv지출.Location = new System.Drawing.Point(12, 62);
            this.dgrv지출.MultiSelect = false;
            this.dgrv지출.Name = "dgrv지출";
            this.dgrv지출.RowHeadersVisible = false;
            this.dgrv지출.RowTemplate.Height = 23;
            this.dgrv지출.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgrv지출.Size = new System.Drawing.Size(628, 166);
            this.dgrv지출.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.고정수입체크,
            this.고정수입날짜,
            this.고정수입내역,
            this.고정수입금액,
            this.고정수입대분류,
            this.고정수입소분류});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataGridView1.Location = new System.Drawing.Point(12, 270);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(628, 166);
            this.dataGridView1.TabIndex = 14;
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.textBox1.Location = new System.Drawing.Point(12, 246);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 21);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "고정수입목록";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Tomato;
            this.textBox2.Location = new System.Drawing.Point(12, 38);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(112, 21);
            this.textBox2.TabIndex = 16;
            this.textBox2.Text = "고정지출목록";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnFixedClose
            // 
            this.btnFixedClose.Location = new System.Drawing.Point(344, 457);
            this.btnFixedClose.Name = "btnFixedClose";
            this.btnFixedClose.Size = new System.Drawing.Size(61, 21);
            this.btnFixedClose.TabIndex = 18;
            this.btnFixedClose.Text = "닫기";
            this.btnFixedClose.UseVisualStyleBackColor = true;
            this.btnFixedClose.Click += new System.EventHandler(this.btnFixedClose_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(277, 457);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(61, 21);
            this.button5.TabIndex = 17;
            this.button5.Text = "저장";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // 고정수입체크
            // 
            this.고정수입체크.Frozen = true;
            this.고정수입체크.HeaderText = "";
            this.고정수입체크.Name = "고정수입체크";
            this.고정수입체크.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.고정수입체크.Width = 30;
            // 
            // 고정수입날짜
            // 
            this.고정수입날짜.Frozen = true;
            this.고정수입날짜.HeaderText = "날짜";
            this.고정수입날짜.Name = "고정수입날짜";
            this.고정수입날짜.Width = 150;
            // 
            // 고정수입내역
            // 
            this.고정수입내역.Frozen = true;
            this.고정수입내역.HeaderText = "내역";
            this.고정수입내역.Name = "고정수입내역";
            this.고정수입내역.Width = 145;
            // 
            // 고정수입금액
            // 
            this.고정수입금액.Frozen = true;
            this.고정수입금액.HeaderText = "금액";
            this.고정수입금액.Name = "고정수입금액";
            // 
            // 고정수입대분류
            // 
            this.고정수입대분류.Frozen = true;
            this.고정수입대분류.HeaderText = "대분류";
            this.고정수입대분류.Items.AddRange(new object[] {
            "식비"});
            this.고정수입대분류.Name = "고정수입대분류";
            this.고정수입대분류.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.고정수입대분류.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.고정수입대분류.ToolTipText = "대분류";
            // 
            // 고정수입소분류
            // 
            this.고정수입소분류.Frozen = true;
            this.고정수입소분류.HeaderText = "소분류";
            this.고정수입소분류.Name = "고정수입소분류";
            // 
            // 고정지출체크
            // 
            this.고정지출체크.Frozen = true;
            this.고정지출체크.HeaderText = "";
            this.고정지출체크.Name = "고정지출체크";
            this.고정지출체크.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.고정지출체크.Width = 30;
            // 
            // 고정지출날짜
            // 
            this.고정지출날짜.Frozen = true;
            this.고정지출날짜.HeaderText = "날짜";
            this.고정지출날짜.Name = "고정지출날짜";
            this.고정지출날짜.Width = 150;
            // 
            // 고정지출내역
            // 
            this.고정지출내역.Frozen = true;
            this.고정지출내역.HeaderText = "내역";
            this.고정지출내역.Name = "고정지출내역";
            this.고정지출내역.Width = 145;
            // 
            // 고정지출금액
            // 
            this.고정지출금액.Frozen = true;
            this.고정지출금액.HeaderText = "금액";
            this.고정지출금액.Name = "고정지출금액";
            // 
            // 고정지출대분류
            // 
            this.고정지출대분류.Frozen = true;
            this.고정지출대분류.HeaderText = "대분류";
            this.고정지출대분류.Items.AddRange(new object[] {
            "식비"});
            this.고정지출대분류.Name = "고정지출대분류";
            this.고정지출대분류.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.고정지출대분류.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.고정지출대분류.ToolTipText = "대분류";
            // 
            // 고정지출소분류
            // 
            this.고정지출소분류.Frozen = true;
            this.고정지출소분류.HeaderText = "소분류";
            this.고정지출소분류.Name = "고정지출소분류";
            // 
            // Fixed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(654, 490);
            this.Controls.Add(this.btnFixedClose);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dgrv지출);
            this.Name = "Fixed";
            this.Text = "Fixed";
            this.Load += new System.EventHandler(this.Fixed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrv지출)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrv지출;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 고정지출체크;
        private System.Windows.Forms.DataGridViewTextBoxColumn 고정지출날짜;
        private System.Windows.Forms.DataGridViewTextBoxColumn 고정지출내역;
        private System.Windows.Forms.DataGridViewTextBoxColumn 고정지출금액;
        private System.Windows.Forms.DataGridViewComboBoxColumn 고정지출대분류;
        private System.Windows.Forms.DataGridViewComboBoxColumn 고정지출소분류;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 고정수입체크;
        private System.Windows.Forms.DataGridViewTextBoxColumn 고정수입날짜;
        private System.Windows.Forms.DataGridViewTextBoxColumn 고정수입내역;
        private System.Windows.Forms.DataGridViewTextBoxColumn 고정수입금액;
        private System.Windows.Forms.DataGridViewComboBoxColumn 고정수입대분류;
        private System.Windows.Forms.DataGridViewComboBoxColumn 고정수입소분류;
        private System.Windows.Forms.Button btnFixedClose;
        private System.Windows.Forms.Button button5;
    }
}