namespace AstraAccess.Footing
{
    partial class frm_ColumnSelection
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_column = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_coordinates = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_grid_lengths = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_L = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_B = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Load = new System.Windows.Forms.TextBox();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_finish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Column";
            this.label1.Visible = false;
            // 
            // txt_column
            // 
            this.txt_column.Location = new System.Drawing.Point(12, 88);
            this.txt_column.Name = "txt_column";
            this.txt_column.Size = new System.Drawing.Size(48, 21);
            this.txt_column.TabIndex = 1;
            this.txt_column.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_column.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Coordinates (X,Y)";
            // 
            // txt_coordinates
            // 
            this.txt_coordinates.Location = new System.Drawing.Point(45, 38);
            this.txt_coordinates.Name = "txt_coordinates";
            this.txt_coordinates.Size = new System.Drawing.Size(110, 21);
            this.txt_coordinates.TabIndex = 1;
            this.txt_coordinates.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Grid lengths  (X,Y)";
            // 
            // txt_grid_lengths
            // 
            this.txt_grid_lengths.Location = new System.Drawing.Point(164, 38);
            this.txt_grid_lengths.Name = "txt_grid_lengths";
            this.txt_grid_lengths.Size = new System.Drawing.Size(110, 21);
            this.txt_grid_lengths.TabIndex = 1;
            this.txt_grid_lengths.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(282, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Length  (M)";
            // 
            // txt_L
            // 
            this.txt_L.Location = new System.Drawing.Point(285, 38);
            this.txt_L.Name = "txt_L";
            this.txt_L.Size = new System.Drawing.Size(69, 21);
            this.txt_L.TabIndex = 1;
            this.txt_L.Text = "0.3";
            this.txt_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Width (M)";
            // 
            // txt_B
            // 
            this.txt_B.Location = new System.Drawing.Point(369, 38);
            this.txt_B.Name = "txt_B";
            this.txt_B.Size = new System.Drawing.Size(62, 21);
            this.txt_B.TabIndex = 1;
            this.txt_B.Text = "0.3";
            this.txt_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(445, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 26);
            this.label6.TabIndex = 0;
            this.label6.Text = "Applied \r\nLoad (kN)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Load
            // 
            this.txt_Load.Location = new System.Drawing.Point(448, 38);
            this.txt_Load.Name = "txt_Load";
            this.txt_Load.Size = new System.Drawing.Size(60, 21);
            this.txt_Load.TabIndex = 1;
            this.txt_Load.Text = "500";
            this.txt_Load.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Next
            // 
            this.btn_Next.Location = new System.Drawing.Point(121, 76);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(132, 43);
            this.btn_Next.TabIndex = 2;
            this.btn_Next.Text = "Select Next Column";
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // btn_finish
            // 
            this.btn_finish.Location = new System.Drawing.Point(287, 76);
            this.btn_finish.Name = "btn_finish";
            this.btn_finish.Size = new System.Drawing.Size(132, 43);
            this.btn_finish.TabIndex = 2;
            this.btn_finish.Text = "Finish";
            this.btn_finish.UseVisualStyleBackColor = true;
            this.btn_finish.Click += new System.EventHandler(this.btn_finish_Click);
            // 
            // frm_ColumnSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 131);
            this.Controls.Add(this.btn_finish);
            this.Controls.Add(this.btn_Next);
            this.Controls.Add(this.txt_Load);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_B);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_L);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_grid_lengths);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_coordinates);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_column);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_ColumnSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Column Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_column;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_coordinates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_grid_lengths;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_L;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_B;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Load;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Button btn_finish;
    }
}