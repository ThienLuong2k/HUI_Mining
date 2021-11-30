
namespace HUIMining
{
    partial class frmMain
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
            this.lbl_title = new System.Windows.Forms.Label();
            this.txt_filename = new System.Windows.Forms.TextBox();
            this.group_data = new System.Windows.Forms.GroupBox();
            this.btn_importdata = new System.Windows.Forms.Button();
            this.btn_run = new System.Windows.Forms.Button();
            this.lbl_transactioncount = new System.Windows.Forms.Label();
            this.lbl_transaction = new System.Windows.Forms.Label();
            this.lbl_itemcount = new System.Windows.Forms.Label();
            this.lbl_item = new System.Windows.Forms.Label();
            this.lbl_pathfile = new System.Windows.Forms.Label();
            this.lbl_minutil = new System.Windows.Forms.Label();
            this.txt_minutil = new System.Windows.Forms.TextBox();
            this.btn_choosefile = new System.Windows.Forms.Button();
            this.group_algo = new System.Windows.Forms.GroupBox();
            this.rdo_fhm = new System.Windows.Forms.RadioButton();
            this.rdo_huiminer = new System.Windows.Forms.RadioButton();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.group_data.SuspendLayout();
            this.group_algo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(6, 9);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(305, 31);
            this.lbl_title.TabIndex = 0;
            this.lbl_title.Text = "Dùng thuật toán tìm HUI";
            // 
            // txt_filename
            // 
            this.txt_filename.Location = new System.Drawing.Point(64, 40);
            this.txt_filename.Name = "txt_filename";
            this.txt_filename.ReadOnly = true;
            this.txt_filename.Size = new System.Drawing.Size(344, 26);
            this.txt_filename.TabIndex = 1;
            // 
            // group_data
            // 
            this.group_data.Controls.Add(this.btn_importdata);
            this.group_data.Controls.Add(this.btn_run);
            this.group_data.Controls.Add(this.lbl_transactioncount);
            this.group_data.Controls.Add(this.lbl_transaction);
            this.group_data.Controls.Add(this.lbl_itemcount);
            this.group_data.Controls.Add(this.lbl_item);
            this.group_data.Controls.Add(this.lbl_pathfile);
            this.group_data.Controls.Add(this.lbl_minutil);
            this.group_data.Controls.Add(this.txt_minutil);
            this.group_data.Controls.Add(this.btn_choosefile);
            this.group_data.Controls.Add(this.txt_filename);
            this.group_data.Location = new System.Drawing.Point(12, 144);
            this.group_data.Name = "group_data";
            this.group_data.Size = new System.Drawing.Size(442, 305);
            this.group_data.TabIndex = 2;
            this.group_data.TabStop = false;
            this.group_data.Text = "Dữ liệu";
            // 
            // btn_importdata
            // 
            this.btn_importdata.Location = new System.Drawing.Point(298, 81);
            this.btn_importdata.Name = "btn_importdata";
            this.btn_importdata.Size = new System.Drawing.Size(110, 37);
            this.btn_importdata.TabIndex = 13;
            this.btn_importdata.Text = "Đọc dữ liệu";
            this.btn_importdata.UseVisualStyleBackColor = true;
            this.btn_importdata.Click += new System.EventHandler(this.btn_importdata_Click);
            // 
            // btn_run
            // 
            this.btn_run.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_run.ForeColor = System.Drawing.Color.Green;
            this.btn_run.Location = new System.Drawing.Point(226, 247);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(210, 37);
            this.btn_run.TabIndex = 12;
            this.btn_run.Text = "Thực thi và xuất ra file";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // lbl_transactioncount
            // 
            this.lbl_transactioncount.AutoSize = true;
            this.lbl_transactioncount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_transactioncount.Location = new System.Drawing.Point(177, 202);
            this.lbl_transactioncount.Name = "lbl_transactioncount";
            this.lbl_transactioncount.Size = new System.Drawing.Size(19, 20);
            this.lbl_transactioncount.TabIndex = 11;
            this.lbl_transactioncount.Text = "0";
            // 
            // lbl_transaction
            // 
            this.lbl_transaction.AutoSize = true;
            this.lbl_transaction.Location = new System.Drawing.Point(10, 202);
            this.lbl_transaction.Name = "lbl_transaction";
            this.lbl_transaction.Size = new System.Drawing.Size(151, 20);
            this.lbl_transaction.TabIndex = 10;
            this.lbl_transaction.Text = "Số lượng giao dịch:";
            // 
            // lbl_itemcount
            // 
            this.lbl_itemcount.AutoSize = true;
            this.lbl_itemcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_itemcount.Location = new System.Drawing.Point(132, 152);
            this.lbl_itemcount.Name = "lbl_itemcount";
            this.lbl_itemcount.Size = new System.Drawing.Size(19, 20);
            this.lbl_itemcount.TabIndex = 9;
            this.lbl_itemcount.Text = "0";
            // 
            // lbl_item
            // 
            this.lbl_item.AutoSize = true;
            this.lbl_item.Location = new System.Drawing.Point(10, 152);
            this.lbl_item.Name = "lbl_item";
            this.lbl_item.Size = new System.Drawing.Size(116, 20);
            this.lbl_item.TabIndex = 8;
            this.lbl_item.Text = "Số lượng item:";
            // 
            // lbl_pathfile
            // 
            this.lbl_pathfile.AutoSize = true;
            this.lbl_pathfile.Location = new System.Drawing.Point(10, 43);
            this.lbl_pathfile.Name = "lbl_pathfile";
            this.lbl_pathfile.Size = new System.Drawing.Size(48, 20);
            this.lbl_pathfile.TabIndex = 5;
            this.lbl_pathfile.Text = "Path:";
            // 
            // lbl_minutil
            // 
            this.lbl_minutil.AutoSize = true;
            this.lbl_minutil.Location = new System.Drawing.Point(10, 255);
            this.lbl_minutil.Name = "lbl_minutil";
            this.lbl_minutil.Size = new System.Drawing.Size(80, 20);
            this.lbl_minutil.TabIndex = 4;
            this.lbl_minutil.Text = "Min utility";
            // 
            // txt_minutil
            // 
            this.txt_minutil.Location = new System.Drawing.Point(96, 252);
            this.txt_minutil.Name = "txt_minutil";
            this.txt_minutil.Size = new System.Drawing.Size(100, 26);
            this.txt_minutil.TabIndex = 3;
            this.txt_minutil.TextChanged += new System.EventHandler(this.txt_minutil_TextChanged);
            this.txt_minutil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_minutil_KeyPress);
            // 
            // btn_choosefile
            // 
            this.btn_choosefile.Location = new System.Drawing.Point(64, 81);
            this.btn_choosefile.Name = "btn_choosefile";
            this.btn_choosefile.Size = new System.Drawing.Size(110, 37);
            this.btn_choosefile.TabIndex = 2;
            this.btn_choosefile.Text = "Chọn file txt";
            this.btn_choosefile.UseVisualStyleBackColor = true;
            this.btn_choosefile.Click += new System.EventHandler(this.btn_choosefile_Click);
            // 
            // group_algo
            // 
            this.group_algo.Controls.Add(this.rdo_fhm);
            this.group_algo.Controls.Add(this.rdo_huiminer);
            this.group_algo.Location = new System.Drawing.Point(12, 61);
            this.group_algo.Name = "group_algo";
            this.group_algo.Size = new System.Drawing.Size(408, 59);
            this.group_algo.TabIndex = 3;
            this.group_algo.TabStop = false;
            this.group_algo.Text = "thuật toán";
            // 
            // rdo_fhm
            // 
            this.rdo_fhm.AutoSize = true;
            this.rdo_fhm.Location = new System.Drawing.Point(249, 25);
            this.rdo_fhm.Name = "rdo_fhm";
            this.rdo_fhm.Size = new System.Drawing.Size(67, 24);
            this.rdo_fhm.TabIndex = 1;
            this.rdo_fhm.TabStop = true;
            this.rdo_fhm.Text = "FHM";
            this.rdo_fhm.UseVisualStyleBackColor = true;
            // 
            // rdo_huiminer
            // 
            this.rdo_huiminer.AutoSize = true;
            this.rdo_huiminer.Location = new System.Drawing.Point(112, 25);
            this.rdo_huiminer.Name = "rdo_huiminer";
            this.rdo_huiminer.Size = new System.Drawing.Size(107, 24);
            this.rdo_huiminer.TabIndex = 0;
            this.rdo_huiminer.TabStop = true;
            this.rdo_huiminer.Text = "HUI-Miner";
            this.rdo_huiminer.UseVisualStyleBackColor = true;
            // 
            // btn_refresh
            // 
            this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_refresh.ForeColor = System.Drawing.Color.Green;
            this.btn_refresh.Location = new System.Drawing.Point(12, 455);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(174, 37);
            this.btn_refresh.TabIndex = 13;
            this.btn_refresh.Text = "Bắt đầu lại";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 509);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.group_algo);
            this.Controls.Add(this.group_data);
            this.Controls.Add(this.lbl_title);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm HUI";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.group_data.ResumeLayout(false);
            this.group_data.PerformLayout();
            this.group_algo.ResumeLayout(false);
            this.group_algo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.TextBox txt_filename;
        private System.Windows.Forms.GroupBox group_data;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Label lbl_transactioncount;
        private System.Windows.Forms.Label lbl_transaction;
        private System.Windows.Forms.Label lbl_itemcount;
        private System.Windows.Forms.Label lbl_item;
        private System.Windows.Forms.Label lbl_pathfile;
        private System.Windows.Forms.Label lbl_minutil;
        private System.Windows.Forms.TextBox txt_minutil;
        private System.Windows.Forms.Button btn_choosefile;
        private System.Windows.Forms.GroupBox group_algo;
        private System.Windows.Forms.RadioButton rdo_fhm;
        private System.Windows.Forms.RadioButton rdo_huiminer;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Button btn_importdata;
    }
}

