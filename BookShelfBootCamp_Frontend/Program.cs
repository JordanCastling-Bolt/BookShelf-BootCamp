﻿using System;
using System.Windows.Forms;

namespace PROG7132
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalExceptionHandler);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form());
        }


        private static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Error");
        }
    }
}
