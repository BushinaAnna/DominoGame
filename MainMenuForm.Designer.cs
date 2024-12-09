namespace Domino
{
    partial class MainMenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.cbComputerFirst = new System.Windows.Forms.CheckBox();
            this.cbPlayerFirst = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartGame
            // 
            this.btnStartGame.AutoSize = true;
            this.btnStartGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(240)))), ((int)(((byte)(227)))));
            this.btnStartGame.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStartGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(81)))), ((int)(((byte)(89)))));
            this.btnStartGame.Location = new System.Drawing.Point(21, 377);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(407, 61);
            this.btnStartGame.TabIndex = 0;
            this.btnStartGame.Text = "Начать новую игру";
            this.btnStartGame.UseVisualStyleBackColor = false;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.AutoSize = true;
            this.btnLoadGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(240)))), ((int)(((byte)(227)))));
            this.btnLoadGame.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoadGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(81)))), ((int)(((byte)(89)))));
            this.btnLoadGame.Location = new System.Drawing.Point(452, 377);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(321, 61);
            this.btnLoadGame.TabIndex = 1;
            this.btnLoadGame.Text = "Загрузить игру";
            this.btnLoadGame.UseVisualStyleBackColor = false;
            this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);
            // 
            // cbComputerFirst
            // 
            this.cbComputerFirst.AutoSize = true;
            this.cbComputerFirst.Font = new System.Drawing.Font("Segoe UI Black", 12F);
            this.cbComputerFirst.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(81)))), ((int)(((byte)(89)))));
            this.cbComputerFirst.Location = new System.Drawing.Point(193, 200);
            this.cbComputerFirst.Name = "cbComputerFirst";
            this.cbComputerFirst.Size = new System.Drawing.Size(243, 49);
            this.cbComputerFirst.TabIndex = 2;
            this.cbComputerFirst.Text = "Компьютер";
            this.cbComputerFirst.UseVisualStyleBackColor = true;
            this.cbComputerFirst.CheckedChanged += new System.EventHandler(this.cbComputerFirst_CheckedChanged);
            // 
            // cbPlayerFirst
            // 
            this.cbPlayerFirst.AutoSize = true;
            this.cbPlayerFirst.Font = new System.Drawing.Font("Segoe UI Black", 12F);
            this.cbPlayerFirst.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(81)))), ((int)(((byte)(89)))));
            this.cbPlayerFirst.Location = new System.Drawing.Point(491, 200);
            this.cbPlayerFirst.Name = "cbPlayerFirst";
            this.cbPlayerFirst.Size = new System.Drawing.Size(151, 49);
            this.cbPlayerFirst.TabIndex = 3;
            this.cbPlayerFirst.Text = "Игрок";
            this.cbPlayerFirst.UseVisualStyleBackColor = true;
            this.cbPlayerFirst.CheckedChanged += new System.EventHandler(this.cbPlayerFirst_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 14F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(81)))), ((int)(((byte)(89)))));
            this.label1.Location = new System.Drawing.Point(184, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(458, 51);
            this.label1.TabIndex = 4;
            this.label1.Text = "Первым будет ходить";
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(206)))), ((int)(((byte)(188)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPlayerFirst);
            this.Controls.Add(this.cbComputerFirst);
            this.Controls.Add(this.btnLoadGame);
            this.Controls.Add(this.btnStartGame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenuForm";
            this.Text = "Домино";
            this.FormClosing += MainMenuForm_Closing;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnLoadGame;
        private System.Windows.Forms.CheckBox cbComputerFirst;
        private System.Windows.Forms.CheckBox cbPlayerFirst;
        private System.Windows.Forms.Label label1;
    }
}