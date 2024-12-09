using System;
using System.Windows.Forms;

namespace Domino
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            cbComputerFirst.Checked = false;
            cbPlayerFirst.Checked = true;
        }

        private void cbPlayerFirst_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPlayerFirst.Checked)
            {
                cbComputerFirst.Checked = false;
            }
            else if (!cbComputerFirst.Checked)
            {
                cbPlayerFirst.Checked = true;
            }
        }

        private void cbComputerFirst_CheckedChanged(object sender, EventArgs e)
        {
            if (cbComputerFirst.Checked)
            {
                cbPlayerFirst.Checked = false;
            }
            else if (!cbPlayerFirst.Checked)
            {
                cbComputerFirst.Checked = true;
            }
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            var gameForm = new Form1
            {
                IsPlayerFirst = cbPlayerFirst.Checked,
                IsNewGame = true
            };

            gameForm.ShowDialog();
            this.Hide();
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Game Save File|*.sav",
                Title = "Загрузить игру"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var gameForm = new Form1();
                gameForm.LoadGame(openFileDialog.FileName);

                gameForm.ShowDialog();
                this.Hide();
            }
        }

        private void MainMenuForm_Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
