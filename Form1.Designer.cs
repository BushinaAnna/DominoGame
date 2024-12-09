namespace Domino
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelBoard = new System.Windows.Forms.Panel();
            this.panelPlayerTiles = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnDrawFromBank = new System.Windows.Forms.Button();
            this.btnSaveGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelBoard
            // 
            this.panelBoard.AutoScroll = true;
            this.panelBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(206)))), ((int)(((byte)(188)))));
            this.panelBoard.Location = new System.Drawing.Point(12, 106);
            this.panelBoard.Margin = new System.Windows.Forms.Padding(4);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(2823, 1236);
            this.panelBoard.TabIndex = 0;
            this.panelBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PanelBoard_Click);
            // 
            // panelPlayerTiles
            // 
            this.panelPlayerTiles.AutoScroll = true;
            this.panelPlayerTiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(206)))), ((int)(((byte)(188)))));
            this.panelPlayerTiles.Location = new System.Drawing.Point(429, 1374);
            this.panelPlayerTiles.Margin = new System.Windows.Forms.Padding(4);
            this.panelPlayerTiles.Name = "panelPlayerTiles";
            this.panelPlayerTiles.Size = new System.Drawing.Size(2406, 96);
            this.panelPlayerTiles.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(81)))), ((int)(((byte)(89)))));
            this.lblStatus.Location = new System.Drawing.Point(615, 14);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1819, 51);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Состояние игры";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRestart
            // 
            this.btnRestart.AutoSize = true;
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(240)))), ((int)(((byte)(227)))));
            this.btnRestart.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRestart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(81)))), ((int)(((byte)(89)))));
            this.btnRestart.Location = new System.Drawing.Point(13, 9);
            this.btnRestart.Margin = new System.Windows.Forms.Padding(4);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(257, 89);
            this.btnRestart.TabIndex = 4;
            this.btnRestart.Text = "Перезапуск";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnDrawFromBank
            // 
            this.btnDrawFromBank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(240)))), ((int)(((byte)(227)))));
            this.btnDrawFromBank.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDrawFromBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(81)))), ((int)(((byte)(89)))));
            this.btnDrawFromBank.Location = new System.Drawing.Point(12, 1374);
            this.btnDrawFromBank.Margin = new System.Windows.Forms.Padding(4);
            this.btnDrawFromBank.Name = "btnDrawFromBank";
            this.btnDrawFromBank.Size = new System.Drawing.Size(370, 96);
            this.btnDrawFromBank.TabIndex = 5;
            this.btnDrawFromBank.Text = "Взять фишку";
            this.btnDrawFromBank.UseVisualStyleBackColor = false;
            this.btnDrawFromBank.Click += new System.EventHandler(this.btnDrawFromBank_Click);
            // 
            // btnSaveGame
            // 
            this.btnSaveGame.AutoSize = true;
            this.btnSaveGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(240)))), ((int)(((byte)(227)))));
            this.btnSaveGame.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(81)))), ((int)(((byte)(89)))));
            this.btnSaveGame.Location = new System.Drawing.Point(278, 9);
            this.btnSaveGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveGame.Name = "btnSaveGame";
            this.btnSaveGame.Size = new System.Drawing.Size(337, 89);
            this.btnSaveGame.TabIndex = 6;
            this.btnSaveGame.Text = "Сохранить игру";
            this.btnSaveGame.UseVisualStyleBackColor = false;
            this.btnSaveGame.Click += new System.EventHandler(this.btnSaveGame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(206)))), ((int)(((byte)(188)))));
            this.ClientSize = new System.Drawing.Size(2848, 1483);
            this.Controls.Add(this.btnSaveGame);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnDrawFromBank);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panelPlayerTiles);
            this.Controls.Add(this.panelBoard);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Домино";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += Form1_FormClosing;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBoard;
        private System.Windows.Forms.Panel panelPlayerTiles;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnDrawFromBank;
        private System.Windows.Forms.Button btnSaveGame;
    }
}

