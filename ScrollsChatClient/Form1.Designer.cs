namespace ScrollsChatClient
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
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.txtRoomName = new System.Windows.Forms.TextBox();
            this.lblLoggedIn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(50, 6);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(131, 20);
            this.txtEmail.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(383, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(246, 6);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(131, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(464, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(15, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(524, 446);
            this.tabControl1.TabIndex = 4;
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.Enabled = false;
            this.btnJoinRoom.Location = new System.Drawing.Point(463, 480);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(75, 23);
            this.btnJoinRoom.TabIndex = 6;
            this.btnJoinRoom.Text = "Join Room";
            this.btnJoinRoom.UseVisualStyleBackColor = true;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // txtRoomName
            // 
            this.txtRoomName.Location = new System.Drawing.Point(93, 482);
            this.txtRoomName.Name = "txtRoomName";
            this.txtRoomName.Size = new System.Drawing.Size(364, 20);
            this.txtRoomName.TabIndex = 5;
            this.txtRoomName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRoomName_KeyUp);
            // 
            // lblLoggedIn
            // 
            this.lblLoggedIn.AutoSize = true;
            this.lblLoggedIn.Location = new System.Drawing.Point(12, 485);
            this.lblLoggedIn.Name = "lblLoggedIn";
            this.lblLoggedIn.Size = new System.Drawing.Size(75, 13);
            this.lblLoggedIn.TabIndex = 7;
            this.lblLoggedIn.Text = "Not Logged In";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 512);
            this.Controls.Add(this.lblLoggedIn);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.txtRoomName);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmail);
            this.Name = "Form1";
            this.Text = "Scrolls Chat Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.TextBox txtRoomName;
        private System.Windows.Forms.Label lblLoggedIn;
    }
}

