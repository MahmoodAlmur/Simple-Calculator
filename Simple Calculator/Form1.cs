using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCalc;


namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _LastResult= string.Empty;
        private string _Result = string.Empty;
        private string _strEquation = string.Empty;


        private bool IsWrongeEquation(string strEquation)
        {
            if (strEquation[0] == '*' || strEquation[0] == '/' || strEquation[0] == '-' || strEquation[0] == '+' || strEquation[0] == '.')
            {
                return true;
            }

            if (strEquation[strEquation.Length - 1] == '*' || strEquation[strEquation.Length - 1] == '/' ||
                    strEquation[strEquation.Length - 1] == '-' || strEquation[strEquation.Length - 1] == '+' || 
                    strEquation[strEquation.Length - 1] == '.')
            {
                return true;
            }

            
            bool IsLastsCharOperator = false;

            for (int i = 1; i <= strEquation.Length - 2; i++)
            {
                if (strEquation[i] == '*' || strEquation[i] == '/' || strEquation[i] == '-' || strEquation[i] == '+' || strEquation[i] == '.')
                {
                    if (IsLastsCharOperator)
                        return true;

                    IsLastsCharOperator = true;
                    continue;
                }
                IsLastsCharOperator = false;
            }

            return false;
        }


        private void ChangeEquation(string TheEquation, bool IsBackSpace = false)
        {
            
            if(IsBackSpace)
            {
                _strEquation = TheEquation;
            }
            else
            {
                _strEquation += TheEquation;
            }

            txtEquation.Text = _strEquation;
            if (IsWrongeEquation(_strEquation))
            {
                btnEqual.Enabled = false;
                btnEqual.BackColor = Color.Red;
            }
            else
            {
                btnEqual.Enabled = true;
                btnEqual.BackColor = Color.Teal;
                lblResult.Text = Calculation(_strEquation);
            }
            
        }

        private string  Calculation(string TheEquation)
        {
            Expression e = new Expression(TheEquation);
             _Result = e.Evaluate().ToString();

            return _Result;
        }

        private void Clear()
        {
            _strEquation = string.Empty;
            lblResult.Text = string.Empty;
            txtEquation.Text = string.Empty;
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            _strEquation = _Result;
            txtEquation.Text = _strEquation;
        }

  

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            if (_strEquation.Length == 0)
                return;
            if (_strEquation.Length == 1)
            {
                Clear();
                return;
            }
            _strEquation = _strEquation.Substring(0, _strEquation.Length - 1);
            ChangeEquation(_strEquation, true);
        }

        private void button_Click(object sender, EventArgs e)
        {
            ChangeEquation(((Button)sender).Tag.ToString());
        }

  
    }
}
