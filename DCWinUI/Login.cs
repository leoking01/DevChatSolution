using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Remoting;
using DCBusiness;

namespace DCWinUI
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Button btnOK;
		private Form1 parent;
		private System.Windows.Forms.TextBox tbServer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;

		private System.ComponentModel.Container components = null;

		public Login(Form1 parent)
		{
			InitializeComponent();
			this.parent = parent;	
			parent.Enabled = false;
		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Login));
			this.tbName = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.tbServer = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbName
			// 
			this.tbName.Location = new System.Drawing.Point(10, 26);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(307, 21);
			this.tbName.TabIndex = 0;
			this.tbName.Text = "";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(10, 129);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(90, 25);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "登录";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// tbServer
			// 
			this.tbServer.Location = new System.Drawing.Point(10, 69);
			this.tbServer.Name = "tbServer";
			this.tbServer.Size = new System.Drawing.Size(307, 21);
			this.tbServer.TabIndex = 2;
			this.tbServer.Text = "tcp://localhost:8085/Facade";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "用户名:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "服务器:";
			// 
			// Login
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(326, 160);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbServer);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.btnOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Login";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "登录";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Login_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Login_Load(object sender, System.EventArgs e)
		{
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			//check that id is not blank
			if(tbName.Text == "")
			{
				MessageBox.Show("Blank login ids are not allowed","Error Logging In",
                    MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.ServiceNotification);
			}
			else
			{
				parent.initialiseConnection(tbServer.Text);
				try
				{
					parent.login(tbName.Text, false);
					parent.Enabled = true;
					this.Close();
				}
				catch (ApplicationException re)
				{
					if(re.Message.Equals("Member already exists"))
					{
						MessageBox.Show(re.Message,"Error Logging In",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
					}
					else if(re.Message.Equals("Member does not exist"))
					{
						MessageBox.Show(re.Message,"Error Logging In",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
					}
					else
					{
						MessageBox.Show(re.Message,"Error Logging In",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
					}
				}
				catch (System.Net.Sockets.SocketException se)
				{
					MessageBox.Show("There was an error connecting to the server. \r\n Please make sure the DotChat server is running and that the URL you are using is correct.\r\n FULL DETAILS: \r\n " + se.Message,"Error Connecting to Server",MessageBoxButtons.OK,MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
				}
			}
		}
	}
}
