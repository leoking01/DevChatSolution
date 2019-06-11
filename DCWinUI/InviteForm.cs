using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DCBusiness;
using DCFacade;

namespace DCWinUI
{
	/// <summary>
	/// Summary description for InviteForm.
	/// </summary>
	public class InviteForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.Label lblMessage;
		private System.ComponentModel.Container components = null;
		private Invite invitation;
		private Facade f;
		private Form1 parent;


		public Invite Invitation
		{
			get{return invitation;}
			set{invitation = value;}
		}
		public InviteForm(Form1 parent, ref Facade f)
		{
			InitializeComponent();
			this.MdiParent = parent;
			this.parent = parent;
			this.f = f;
		}
		public void initialise()
		{
			lblMessage.Text = "You have been invited to join a conversation with:\n";
			foreach(Member m in invitation.InvitedConversation.Members)
			{
				lblMessage.Text += m.Id;
				lblMessage.Text += "\n";
			}
			lblMessage.Text += "Would you like to join?";
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InviteForm));
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnYes = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblMessage.Location = new System.Drawing.Point(0, 0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(220, 203);
			this.lblMessage.TabIndex = 0;
			// 
			// btnYes
			// 
			this.btnYes.Location = new System.Drawing.Point(10, 164);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(90, 24);
			this.btnYes.TabIndex = 1;
			this.btnYes.Text = "½ÓÊÜ";
			this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
			// 
			// btnNo
			// 
			this.btnNo.Location = new System.Drawing.Point(115, 164);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(90, 24);
			this.btnNo.TabIndex = 2;
			this.btnNo.Text = "¾Ü¾ø";
			this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
			// 
			// InviteForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(220, 203);
			this.Controls.Add(this.btnNo);
			this.Controls.Add(this.btnYes);
			this.Controls.Add(this.lblMessage);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InviteForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ÑûÇë";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnYes_Click(object sender, System.EventArgs e)
		{
			//accept the invitation
			f.acceptInvitation(parent.ConversationMember.Id,invitation.Id);
			parent.joinConversation(this);
		}

		private void btnNo_Click(object sender, System.EventArgs e)
		{
			f.rejectInvitation(parent.ConversationMember.Id,invitation.Id);
			this.Close();
		}
	}
}
