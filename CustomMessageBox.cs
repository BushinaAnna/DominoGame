using System;
using System.Windows.Forms;

namespace Domino
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
        }

        public static DialogResult Show(string message)
        {
            using (var messageBox = new CustomMessageBox(message))
            {
                return messageBox.ShowDialog();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
