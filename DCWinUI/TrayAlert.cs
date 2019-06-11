using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TrayAlert
{
	/// <summary>
	/// Summary description for TrayAlert.
	/// </summary>
	public class TrayAlert : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private string content;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel panel1;
		private string title;
		public event EventHandler FrameClick = null;
		public event EventHandler ActionClick = null;

		public string Content
		{
			set{content = value;}
		}

		public string Title
		{
			set{title = value;}
		}

		public TrayAlert()
		{
			InitializeComponent();
			//set to hidden
			Hide();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public new void Show()
		{   
			//set location of the pop up
			this.SetBounds((Screen.GetWorkingArea(this).Right - this.Width),
				(Screen.GetWorkingArea(this).Bottom - this.Height),
				this.Width, this.Height, BoundsSpecified.Location);   
			base.Show();
			label1.Text = title;
			label2.Text = content;
			label1.Click +=new EventHandler(label1_Click);
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(10, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(316, 24);
			this.label1.TabIndex = 0;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(316, 86);
			this.label2.TabIndex = 1;
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.SkyBlue;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.Location = new System.Drawing.Point(10, 138);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(67, 25);
			this.button1.TabIndex = 2;
			this.button1.Text = "¹Ø±Õ";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.SkyBlue;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button2.Location = new System.Drawing.Point(86, 138);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(106, 25);
			this.button2.TabIndex = 3;
			this.button2.Text = "´ò¿ª";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(336, 172);
			this.panel1.TabIndex = 4;
			// 
			// TrayAlert
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.AliceBlue;
			this.ClientSize = new System.Drawing.Size(336, 172);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "TrayAlert";
			this.Text = "TrayAlert";
			this.TopMost = true;
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void label1_Click(object sender, EventArgs e)
		{
			FrameClick(this, new EventArgs());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Hide();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			ActionClick(this, new EventArgs());
			Hide();
		}
	}
}
