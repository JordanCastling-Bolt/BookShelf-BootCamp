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
        ReplacingBooks callNumberControl = new ReplacingBooks();
        public event EventHandler CorrectOrdering;
        private CallNumberManager callNumberManager = new CallNumberManager();
        private int currentCorrectOrders = 0;
        IdentifyingAreas identifyingAreas = new IdentifyingAreas();


        public Form()
        {
            try
            {
                InitializeComponent();

                callNumberControl.CorrectOrdering += OnCorrectOrdering;

                this.Controls.Add(callNumberControl);
                callNumberControl.Dock = DockStyle.Right;
                callNumberControl.Visible = true;


                this.Controls.Add(identifyingAreas);
                identifyingAreas.Dock = DockStyle.Right;
                identifyingAreas.Visible = false;

            } catch(Exception e) { MessageBox.Show(e.ToString()); }
        }
        private void OnCorrectOrdering(object sender, EventArgs e)
        {
            currentCorrectOrders++;
            var newCallNumbers = callNumberManager.GenerateCallNumbers();
            callNumberControl.DisplayCallNumbers(newCallNumbers);
        }

        private void menuIdentifyingAreas_Click(object sender, EventArgs e)
        {
            callNumberControl.Visible = false;
            identifyingAreas.Visible = true;
        }

        private void menuReplacingBooks_Click(object sender, EventArgs e)
        {
            callNumberControl.Visible = true;
            identifyingAreas.Visible = false;
        }
    }
}