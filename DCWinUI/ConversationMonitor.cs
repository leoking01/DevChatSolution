using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using DCBusiness;
using DCFacade;

namespace DCWinUI
{
	/// <summary>
	/// Summary description for ConversationMonitor.
	/// </summary>
	public class ConversationMonitor : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnSend;
		private int conversationId;
		private Facade f;
		private System.Windows.Forms.TreeView tvConversationMembers;
		private System.Windows.Forms.Button btnAddMember;
		private Member m;
		private System.Windows.Forms.MainMenu mainMenu1;
		private Form1 parent;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnLeave;
		static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.TextBox tbMessage;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbConversation;
		private bool hasLeftConversation = false;
		private int numMessages = 0;

		public int ConversationId
		{
			get{return conversationId;}
			set{conversationId = value;}
		}

		/// <summary>
		/// Constructor to start a blank conversation.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="f"></param>
		public ConversationMonitor(Form1 parent, ref Facade f)
		{
			InitializeComponent();
			this.parent = parent;
			this.MdiParent = parent;
			this.f = f;
			m = parent.ConversationMember;
			conversationId = this.f.initialiseConversation(m.Id);	
			initaliseTimer();
		}

		/// <summary>
		/// Constructor to join an existing conversation.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="f"></param>
		/// <param name="conversationId"></param>
		public ConversationMonitor(Form1 parent, ref Facade f, int conversationId)
		{
			InitializeComponent();
			this.parent = parent;
			this.MdiParent = parent;
			this.f = f;
			m = parent.ConversationMember;
			conversationId = conversationId;
			initaliseTimer();
		}

		/// <summary>
		/// Set up the timer and start the timer
		/// </summary>
		private void initaliseTimer()
		{
			myTimer.Tick += new EventHandler(processTimer);
			myTimer.Interval = 1000;
			myTimer.Start();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{	
				if(!hasLeftConversation) leaveConversation();
				myTimer.Stop();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ConversationMonitor));
			this.btnSend = new System.Windows.Forms.Button();
			this.tvConversationMembers = new System.Windows.Forms.TreeView();
			this.btnAddMember = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.label2 = new System.Windows.Forms.Label();
			this.btnLeave = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblInfo = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.tbMessage = new System.Windows.Forms.TextBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.tbConversation = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSend
			// 
			this.btnSend.BackColor = System.Drawing.Color.Khaki;
			this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSend.ForeColor = System.Drawing.Color.Black;
			this.btnSend.Location = new System.Drawing.Point(0, 0);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(96, 23);
			this.btnSend.TabIndex = 3;
			this.btnSend.Text = "发送消息";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// tvConversationMembers
			// 
			this.tvConversationMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvConversationMembers.BackColor = System.Drawing.Color.Beige;
			this.tvConversationMembers.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.tvConversationMembers.ImageIndex = -1;
			this.tvConversationMembers.Location = new System.Drawing.Point(0, 16);
			this.tvConversationMembers.Name = "tvConversationMembers";
			this.tvConversationMembers.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																							  new System.Windows.Forms.TreeNode("活动"),
																							  new System.Windows.Forms.TreeNode("非活动")});
			this.tvConversationMembers.SelectedImageIndex = -1;
			this.tvConversationMembers.Size = new System.Drawing.Size(152, 283);
			this.tvConversationMembers.TabIndex = 4;
			// 
			// btnAddMember
			// 
			this.btnAddMember.BackColor = System.Drawing.Color.Beige;
			this.btnAddMember.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAddMember.ForeColor = System.Drawing.Color.DimGray;
			this.btnAddMember.Location = new System.Drawing.Point(104, 0);
			this.btnAddMember.Name = "btnAddMember";
			this.btnAddMember.Size = new System.Drawing.Size(144, 23);
			this.btnAddMember.TabIndex = 5;
			this.btnAddMember.Text = "邀请成员";
			this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(398, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "消息:";
			// 
			// btnLeave
			// 
			this.btnLeave.BackColor = System.Drawing.Color.Beige;
			this.btnLeave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnLeave.ForeColor = System.Drawing.Color.DimGray;
			this.btnLeave.Location = new System.Drawing.Point(256, 0);
			this.btnLeave.Name = "btnLeave";
			this.btnLeave.Size = new System.Drawing.Size(120, 23);
			this.btnLeave.TabIndex = 9;
			this.btnLeave.Text = "离开会话";
			this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tvConversationMembers);
			this.panel1.Controls.Add(this.lblInfo);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(5, 10);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(144, 299);
			this.panel1.TabIndex = 10;
			// 
			// lblInfo
			// 
			this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblInfo.Location = new System.Drawing.Point(0, 0);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(144, 24);
			this.lblInfo.TabIndex = 11;
			this.lblInfo.Text = "会话参与人员:";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.tbMessage);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(5, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(398, 112);
			this.panel3.TabIndex = 12;
			// 
			// tbMessage
			// 
			this.tbMessage.BackColor = System.Drawing.Color.White;
			this.tbMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tbMessage.Location = new System.Drawing.Point(0, 16);
			this.tbMessage.Multiline = true;
			this.tbMessage.Name = "tbMessage";
			this.tbMessage.Size = new System.Drawing.Size(398, 96);
			this.tbMessage.TabIndex = 13;
			this.tbMessage.Text = "";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.panel3);
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel4.DockPadding.Left = 5;
			this.panel4.DockPadding.Right = 5;
			this.panel4.Location = new System.Drawing.Point(0, 333);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(408, 152);
			this.panel4.TabIndex = 13;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.btnSend);
			this.panel5.Controls.Add(this.btnLeave);
			this.panel5.Controls.Add(this.btnAddMember);
			this.panel5.Location = new System.Drawing.Point(8, 120);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(376, 24);
			this.panel5.TabIndex = 14;
			// 
			// panel6
			// 
			this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel6.Controls.Add(this.panel1);
			this.panel6.Controls.Add(this.panel2);
			this.panel6.DockPadding.Left = 5;
			this.panel6.DockPadding.Right = 5;
			this.panel6.DockPadding.Top = 10;
			this.panel6.Location = new System.Drawing.Point(0, 8);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(408, 309);
			this.panel6.TabIndex = 14;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.Color.DarkGray;
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.tbConversation);
			this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.panel2.ForeColor = System.Drawing.Color.White;
			this.panel2.Location = new System.Drawing.Point(157, 10);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(246, 299);
			this.panel2.TabIndex = 15;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(246, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "会话记录:";
			// 
			// tbConversation
			// 
			this.tbConversation.AcceptsReturn = true;
			this.tbConversation.AcceptsTab = true;
			this.tbConversation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbConversation.BackColor = System.Drawing.Color.Ivory;
			this.tbConversation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tbConversation.ForeColor = System.Drawing.Color.DimGray;
			this.tbConversation.Location = new System.Drawing.Point(0, 16);
			this.tbConversation.MaxLength = 100000;
			this.tbConversation.Multiline = true;
			this.tbConversation.Name = "tbConversation";
			this.tbConversation.ReadOnly = true;
			this.tbConversation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbConversation.Size = new System.Drawing.Size(246, 283);
			this.tbConversation.TabIndex = 0;
			this.tbConversation.Text = "";
			// 
			// ConversationMonitor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 13);
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.DarkGray;
			this.ClientSize = new System.Drawing.Size(408, 485);
			this.Controls.Add(this.panel6);
			this.Controls.Add(this.panel4);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "ConversationMonitor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "会话";
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Update the conversation box with the latest version of the conversation
		/// </summary>
		private void updateConversation()
		{
			StringBuilder sb = new StringBuilder();
			Conversation c = f.getConversationById(conversationId);
			foreach(DCMessage m in c.Messages)
			{
				sb.Append(m.Owner.Id);
				sb.Append(":\t");
				sb.Append(m.Content);
				sb.Append("\r\n");
			}
			tbConversation.Text = sb.ToString();

			//scroll box to bottom
			if(!tbConversation.IsDisposed)	//important check! (seems like a MS bug to me)
			{
				tbConversation.SelectionStart = tbConversation.Text.Length;
				tbConversation.SelectionLength = 0;
				tbConversation.ScrollToCaret();
			}

			//see if a message has been added. If so, send an alert to the tray
			if(numMessages < c.Messages.Count)
			{
				if(!parent.Visible)	//check the app is not running in the foreground
				{
					DCMessage dcm = (DCMessage)c.Messages[c.Messages.Count -1];
					parent.SystemTrayAlert.Title = "New Message Received.";
					parent.SystemTrayAlert.Content = "You have received a new message.";
					parent.SystemTrayAlert.Show();
				}
			}
			numMessages = c.Messages.Count;	//reset numMessages to check for future new messages
		}

		/// <summary>
		/// Update the member tree in the conversation monitor
		/// </summary>
		private void updateMemberList()
		{
			tvConversationMembers.Nodes[0].Nodes.Clear();
			tvConversationMembers.Nodes[1].Nodes.Clear();
			Conversation c = f.getConversationById(conversationId);
			foreach(Member m in c.Members)
			{
				if(m.IsActive) tvConversationMembers.Nodes[0].Nodes.Add(m.Id);
				else tvConversationMembers.Nodes[1].Nodes.Add(m.Id);
			}
		}

		/// <summary>
		/// Poll the server for any undelivered messages for this member. Then display them.
		/// </summary>
		private void processServerMessages()
		{
			ServerMessage sm = f.getServerMessages(m.Id,ServerMessage.UPDATE_CONVERSATION_MONITOR);
			if(sm!=null)
			{
				MessageBox.Show(sm.Content,sm.Subject,MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
			}
		}

		/// <summary>
		/// Member has chosen to leave the conversation;
		/// </summary>
		private void leaveConversation()
		{
			myTimer.Stop();
			f.removeMemberFromConversation(m.Id,conversationId);
			hasLeftConversation = true;
			this.Close();
		}

		private void btnSend_Click(object sender, System.EventArgs e)
		{
			f.sendMessage(tbMessage.Text, conversationId, m.Id);
			updateConversation();
		}

		/// <summary>
		/// Event handler which sends an invite to the selected member
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddMember_Click(object sender, System.EventArgs e)
		{
			sendInvite();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			updateMemberList();
		}

		/// <summary>
		/// Event handler to handle the timer events.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
		private void processTimer(Object obj, System.EventArgs e)
		{
			updateConversation();
			if(!tvConversationMembers.IsDisposed)
			{
				updateMemberList();
			}
			processServerMessages();
		}

		/// <summary>
		/// Event handler which fires when the user wishes to leave the conversation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLeave_Click(object sender, System.EventArgs e)
		{
			leaveConversation();
		}

		public void sendInvite()
		{
			//get the selected member from the treeview
			TreeView tv = parent.MemberTree;
			string memberId = null;
			for(int i=0; i<tv.Nodes[0].Nodes.Count; i++)
			{
				TreeNode tn = tv.Nodes[0].Nodes[i];
				if(tn.IsSelected)
				{
					memberId = tn.Text;
				}
			}

			//check that one active member has been selected
			if(memberId != null)
			{
				//check that member is not self inviting
				if(memberId == m.Id)
				{
					MessageBox.Show("You can not invite yourself to join a conversation.", "Invalid invitation",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
				}
				else
				{

					//check that the member is not already in the conversation
					bool memFound = false;
					Conversation c = f.getConversationById(conversationId);
					foreach(Member mem in c.Members)
					{
						if(mem.Id == memberId) memFound = true;
					}
					if(memFound)
					{
						MessageBox.Show("That member is already participating in this conversation","Member already exists",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
					}
					else
					{
						f.inviteMember(memberId, conversationId);
					}
				}
			}
			else
			{
				MessageBox.Show("You must select a currently active member from the list","Not a valid selection",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
			}
		}

		public void sendInvite(string memberId)
		{
			if(memberId != null)
			{
				//check that the member is not already in the conversation
				bool memFound = false;
				Conversation c = f.getConversationById(conversationId);
				foreach(Member m in c.Members)
				{
					if(m.Id == memberId) memFound = true;
				}
				if(memFound)
				{
					MessageBox.Show("That member is already participating in this conversation","Member already exists",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
				}
				else
				{
					f.inviteMember(memberId, conversationId);
				}
			}
			else
			{
				MessageBox.Show("You must select a currently active member from the list","Not a valid selection",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
			}
		}
	}
}
