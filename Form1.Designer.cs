namespace Serial2Socket
{
    partial class Form1
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRead = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbWrite = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btSendWrite = new System.Windows.Forms.Button();
            this.btReadFlash = new System.Windows.Forms.Button();
            this.btWriteClear = new System.Windows.Forms.Button();
            this.tbSend = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btSendClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(46, 60);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(133, 43);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Com Port";
            // 
            // tbRead
            // 
            this.tbRead.Location = new System.Drawing.Point(204, 60);
            this.tbRead.Multiline = true;
            this.tbRead.Name = "tbRead";
            this.tbRead.ReadOnly = true;
            this.tbRead.Size = new System.Drawing.Size(180, 136);
            this.tbRead.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(237, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "ComPort Read";
            // 
            // tbWrite
            // 
            this.tbWrite.Location = new System.Drawing.Point(393, 60);
            this.tbWrite.Multiline = true;
            this.tbWrite.Name = "tbWrite";
            this.tbWrite.Size = new System.Drawing.Size(180, 136);
            this.tbWrite.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(425, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "ComPort Write";
            // 
            // btSendWrite
            // 
            this.btSendWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSendWrite.Location = new System.Drawing.Point(483, 202);
            this.btSendWrite.Name = "btSendWrite";
            this.btSendWrite.Size = new System.Drawing.Size(80, 37);
            this.btSendWrite.TabIndex = 7;
            this.btSendWrite.Text = "Write";
            this.btSendWrite.UseVisualStyleBackColor = true;
            this.btSendWrite.Click += new System.EventHandler(this.btSendWrite_Click);
            // 
            // btReadFlash
            // 
            this.btReadFlash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btReadFlash.Location = new System.Drawing.Point(266, 202);
            this.btReadFlash.Name = "btReadFlash";
            this.btReadFlash.Size = new System.Drawing.Size(97, 37);
            this.btReadFlash.TabIndex = 8;
            this.btReadFlash.Text = "Clear";
            this.btReadFlash.UseVisualStyleBackColor = true;
            this.btReadFlash.Click += new System.EventHandler(this.btReadFlash_Click);
            // 
            // btWriteClear
            // 
            this.btWriteClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btWriteClear.Location = new System.Drawing.Point(397, 202);
            this.btWriteClear.Name = "btWriteClear";
            this.btWriteClear.Size = new System.Drawing.Size(82, 37);
            this.btWriteClear.TabIndex = 9;
            this.btWriteClear.Text = "Clear";
            this.btWriteClear.UseVisualStyleBackColor = true;
            this.btWriteClear.Click += new System.EventHandler(this.btWriteClear_Click);
            // 
            // tbSend
            // 
            this.tbSend.Location = new System.Drawing.Point(588, 59);
            this.tbSend.Multiline = true;
            this.tbSend.Name = "tbSend";
            this.tbSend.ReadOnly = true;
            this.tbSend.Size = new System.Drawing.Size(180, 136);
            this.tbSend.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(634, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "ComPort Send";
            // 
            // btSendClear
            // 
            this.btSendClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSendClear.Location = new System.Drawing.Point(639, 201);
            this.btSendClear.Name = "btSendClear";
            this.btSendClear.Size = new System.Drawing.Size(82, 37);
            this.btSendClear.TabIndex = 12;
            this.btSendClear.Text = "Clear";
            this.btSendClear.UseVisualStyleBackColor = true;
            this.btSendClear.Click += new System.EventHandler(this.btSendClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 251);
            this.Controls.Add(this.btSendClear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbSend);
            this.Controls.Add(this.btWriteClear);
            this.Controls.Add(this.btReadFlash);
            this.Controls.Add(this.btSendWrite);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbWrite);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbRead);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbWrite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btSendWrite;
        private System.Windows.Forms.Button btReadFlash;
        private System.Windows.Forms.Button btWriteClear;
        private System.Windows.Forms.TextBox tbSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btSendClear;
    }
}