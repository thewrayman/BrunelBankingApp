using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrunelServer
{
    public partial class Form1 : Form
    {
        static Form1 instance;
        public static Form1 Instance { get { return instance; } }
        
        public Form1()
        {
            InitializeComponent();
        }

        public void UpdateStatus(string update)
        {
            //RichTextBox rtb;
            //if (this.StatusTextBox.InvokeRequired){
            //    //SetTextCallback d = new SetTextCallback(UpdateStatus);
            //    //this.Invoke(d, new object[] { update });
            //    Console.WriteLine("invoke required");
            //    this.Invoke(new MethodInvoker(delegate { rtb = this.StatusTextBox; }));
            //}
            //else
            //{
            //    Console.WriteLine("no invoke required");
            //    StatusTextBox.AppendText(update + "\n");
            //}
            this.Invoke((MethodInvoker)delegate {
                this.StatusTextBox.AppendText(update + "\n");// runs on UI thread
            });

        }

        public void UpdateAccountBalance(double amount, string account)
        {
            string balance = amount.ToString();
            RichTextBox accountbox = null;
            if (account == "A") accountbox = this.AccountBoxA;
            if (account == "B") accountbox = this.AccountBoxB;
            if (account == "C") accountbox = this.AccountBoxC;
            this.Invoke((MethodInvoker)delegate
            {
                accountbox.Text = balance;
            });
        }
        delegate void SetTextCallback(string text);

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    
}
