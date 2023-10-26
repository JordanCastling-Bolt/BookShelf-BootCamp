using Library_Classlib;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PROG7132
{
    /// <summary>
    /// The main form responsible for managing the interaction with the Call Number Manager and the GUI.
    /// </summary>
    public partial class Form : System.Windows.Forms.Form
    {
        public event EventHandler CorrectOrdering;
        private CallNumberManager callNumberManager = new CallNumberManager();
        private int currentCorrectOrders = 0;

        public Form()
        {
            try
            {
                InitializeComponent();

                replacingBooks1.CorrectOrdering += OnCorrectOrdering;


            } catch(Exception e) { MessageBox.Show(e.ToString()); }
        }
        private void OnCorrectOrdering(object sender, EventArgs e)
        {
            currentCorrectOrders++;
            var newCallNumbers = callNumberManager.GenerateCallNumbers();
            replacingBooks1.DisplayCallNumbers(newCallNumbers);
        }

        private void menuIdentifyingAreas_Click(object sender, EventArgs e)
        {
            replacingBooks1.Visible = false;
            identifyingAreas1.Visible = true;
        }

        private void menuReplacingBooks_Click(object sender, EventArgs e)
        {
            replacingBooks1.Visible = true;
            identifyingAreas1.Visible = false;
        }
    }
}