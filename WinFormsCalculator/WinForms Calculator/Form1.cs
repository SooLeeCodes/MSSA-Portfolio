using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Calculator
{
    public partial class Form1 : Form
    {
        Math math = new Math();

        public Form1()
        {
            InitializeComponent();
        }

        private void bttMultiply_Click(object sender, EventArgs e)
        {
            if (txtboxOne.TextLength != 0 && txtboxTwo.TextLength != 0)
            {
                math.Multiply();
                string result = math.numResult.ToString();
                txtboxTotal.Text = result;
            }
        }

        private void txtboxTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxOne_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxOne_Validating(object sender, CancelEventArgs e)
        {
            if (txtboxOne.TextLength != 0)
            {
                double value;

                if (!double.TryParse(txtboxOne.Text, out value))
                {
                    MessageBox.Show("Please enter a number ( ͡ಠ ʖ̯ ͡ಠ )", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    txtboxOne.Clear();
                }
            }
        }

        private void txtboxTwo_Validating(object sender, CancelEventArgs e)
        {
            if (txtboxTwo.TextLength != 0)
            {
                double value;

                if (!double.TryParse(txtboxTwo.Text, out value))
                {
                    MessageBox.Show("Please enter a number ( ͡ಠ ʖ̯ ͡ಠ )", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    txtboxTwo.Clear();
                }
            }
        }

        private void txtboxOne_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(txtboxOne.Text, out double value))
            {
                math.numOne = value;
            }
        }

        private void txtboxTwo_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(txtboxTwo.Text, out double value))
            {
                math.numTwo = value;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtboxOne.TextLength != 0 && txtboxTwo.TextLength !=0)
            {
                math.Add();
                string result = math.numResult.ToString();
                txtboxTotal.Text = result;
            }
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            if (txtboxOne.TextLength != 0 && txtboxTwo.TextLength != 0)
            {
                math.Subtract();
                string result = math.numResult.ToString();
                txtboxTotal.Text = result;
            }
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            if (txtboxTwo.Text == "0")
            {
                MessageBox.Show("You can't divide by \"0\"  ¯\\_( ͠° ͟ʖ ͠°)_/¯", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtboxOne.TextLength != 0 && txtboxTwo.TextLength != 0 && txtboxTwo.Text != "0")
            {
                math.Divide();
                string result = math.numResult.ToString();
                txtboxTotal.Text = result;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtboxOne.Clear();
            txtboxTwo.Clear();
            txtboxTotal.Clear();
        }

        private void btnBeRude_Click(object sender, EventArgs e)
        {
            List<string> messages = new List<string>();

            messages.Add("The best is yet to come.");
            messages.Add("You are capable of amazing things.");
            messages.Add("Every day may not be good, but there is something good in every day.");
            messages.Add("You are stronger than you think.");
            messages.Add("Make today amazing!");
            messages.Add("Happiness is a choice.");
            messages.Add("Dream big, work hard, stay focused.");

            Random random = new Random();

            int index = random.Next(messages.Count);
            MessageBox.Show(messages[index]);
        }
    }
}
