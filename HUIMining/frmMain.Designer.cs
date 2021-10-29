
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
            this.group_results = new System.Windows.Forms.GroupBox();
            this.lbl_showmemory = new System.Windows.Forms.Label();
            this.lbl_showtimer = new System.Windows.Forms.Label();
            this.lbl_memory = new System.Windows.Forms.Label();
            this.lbl_time = new System.Windows.Forms.Label();
            this.list_hui = new System.Windows.Forms.ListView();
            this.col_items = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_utility = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_huicount = new System.Windows.Forms.Label();
            this.lbl_hui = new System.Windows.Forms.Label();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.group_data.SuspendLayout();
            this.group_algo.SuspendLayout();
            this.group_results.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(13, 13);
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
            this.txt_filename.Size = new System.Drawing.Size(330, 26);
            this.txt_filename.TabIndex = 1;
            // 
            // group_data
            // 
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
            this.group_data.Location = new System.Drawing.Point(19, 77);
            this.group_data.Name = "group_data";
            this.group_data.Size = new System.Drawing.Size(408, 311);
            this.group_data.TabIndex = 2;
            this.group_data.TabStop = false;
            this.group_data.Text = "Dữ liệu";
            // 
            // btn_run
            // 
            this.btn_run.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_run.ForeColor = System.Drawing.Color.Green;
            this.btn_run.Location = new System.Drawing.Point(248, 247);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(131, 37);
            this.btn_run.TabIndex = 12;
            this.btn_run.Text = "Thực thi";
            this.btn_run.UseVisualStyleBackColor = true;
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
            // 
            // group_algo
            // 
            this.group_algo.Controls.Add(this.rdo_fhm);
            this.group_algo.Controls.Add(this.rdo_huiminer);
            this.group_algo.Location = new System.Drawing.Point(573, 12);
            this.group_algo.Name = "group_algo";
            this.group_algo.Size = new System.Drawing.Size(336, 59);
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
            // group_results
            // 
            this.group_results.Controls.Add(this.lbl_showmemory);
            this.group_results.Controls.Add(this.lbl_showtimer);
            this.group_results.Controls.Add(this.lbl_memory);
            this.group_results.Controls.Add(this.lbl_time);
            this.group_results.Controls.Add(this.list_hui);
            this.group_results.Controls.Add(this.lbl_huicount);
            this.group_results.Controls.Add(this.lbl_hui);
            this.group_results.Location = new System.Drawing.Point(433, 77);
            this.group_results.Name = "group_results";
            this.group_results.Size = new System.Drawing.Size(476, 394);
            this.group_results.TabIndex = 4;
            this.group_results.TabStop = false;
            this.group_results.Text = "Kết quả";
            // 
            // lbl_showmemory
            // 
            this.lbl_showmemory.AutoSize = true;
            this.lbl_showmemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_showmemory.Location = new System.Drawing.Point(80, 360);
            this.lbl_showmemory.Name = "lbl_showmemory";
            this.lbl_showmemory.Size = new System.Drawing.Size(19, 20);
            this.lbl_showmemory.TabIndex = 17;
            this.lbl_showmemory.Text = "0";
            // 
            // lbl_showtimer
            // 
            this.lbl_showtimer.AutoSize = true;
            this.lbl_showtimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_showtimer.Location = new System.Drawing.Point(155, 326);
            this.lbl_showtimer.Name = "lbl_showtimer";
            this.lbl_showtimer.Size = new System.Drawing.Size(19, 20);
            this.lbl_showtimer.TabIndex = 16;
            this.lbl_showtimer.Text = "0";
            // 
            // lbl_memory
            // 
            this.lbl_memory.AutoSize = true;
            this.lbl_memory.Location = new System.Drawing.Point(7, 360);
            this.lbl_memory.Name = "lbl_memory";
            this.lbl_memory.Size = new System.Drawing.Size(67, 20);
            this.lbl_memory.TabIndex = 15;
            this.lbl_memory.Text = "Bộ nhớ:";
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(7, 326);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(142, 20);
            this.lbl_time.TabIndex = 14;
            this.lbl_time.Text = "Thời gian thực thi:";
            // 
            // list_hui
            // 
            this.list_hui.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_items,
            this.col_utility});
            this.list_hui.FullRowSelect = true;
            this.list_hui.HideSelection = false;
            this.list_hui.Location = new System.Drawing.Point(11, 63);
            this.list_hui.Name = "list_hui";
            this.list_hui.Size = new System.Drawing.Size(459, 248);
            this.list_hui.TabIndex = 13;
            this.list_hui.UseCompatibleStateImageBehavior = false;
            this.list_hui.View = System.Windows.Forms.View.Details;
            // 
            // col_items
            // 
            this.col_items.Text = "item / itemset";
            this.col_items.Width = 330;
            // 
            // col_utility
            // 
            this.col_utility.Text = "utility";
            this.col_utility.Width = 98;
            // 
            // lbl_huicount
            // 
            this.lbl_huicount.AutoSize = true;
            this.lbl_huicount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_huicount.Location = new System.Drawing.Point(206, 40);
            this.lbl_huicount.Name = "lbl_huicount";
            this.lbl_huicount.Size = new System.Drawing.Size(19, 20);
            this.lbl_huicount.TabIndex = 12;
            this.lbl_huicount.Text = "0";
            // 
            // lbl_hui
            // 
            this.lbl_hui.AutoSize = true;
            this.lbl_hui.Location = new System.Drawing.Point(7, 40);
            this.lbl_hui.Name = "lbl_hui";
            this.lbl_hui.Size = new System.Drawing.Size(193, 20);
            this.lbl_hui.TabIndex = 0;
            this.lbl_hui.Text = "Tổng số tập hữu ích cao:";
            // 
            // btn_refresh
            // 
            this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_refresh.ForeColor = System.Drawing.Color.Green;
            this.btn_refresh.Location = new System.Drawing.Point(19, 420);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(174, 37);
            this.btn_refresh.TabIndex = 13;
            this.btn_refresh.Text = "Bắt đầu lại";
            this.btn_refresh.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 477);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.group_results);
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
            this.group_results.ResumeLayout(false);
            this.group_results.PerformLayout();
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
        private System.Windows.Forms.GroupBox group_results;
        private System.Windows.Forms.Label lbl_showmemory;
        private System.Windows.Forms.Label lbl_showtimer;
        private System.Windows.Forms.Label lbl_memory;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.ListView list_hui;
        private System.Windows.Forms.ColumnHeader col_items;
        private System.Windows.Forms.ColumnHeader col_utility;
        private System.Windows.Forms.Label lbl_huicount;
        private System.Windows.Forms.Label lbl_hui;
        private System.Windows.Forms.Button btn_refresh;
    }
}

