using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Remoting;
using DCFacade;
using DCBusiness;
using CustomUIControls;
using TrayAlert;
using System.Threading;

namespace DCWinUI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Facade facade;
		private Member member;
		private ArrayList currentConversations;
		private ArrayList storedInvites;
		private Point mousePoint;
		private ConversationMonitor mostRecentlyFocusedMonitor;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.TreeView memberTree;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem7;
		static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
		private bool cancelShutdown = true;
		private bool doCheckInvites = true;
		private System.Windows.Forms.ContextMenu mainTreeCM;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private TrayAlert.TrayAlert trayAlert;

		public Member ConversationMember
		{
			get{return member;}
		}

		public TreeView MemberTree
		{
			get{return memberTree;}
		}

		public TrayAlert.TrayAlert SystemTrayAlert
		{
			get{return trayAlert;}
		}

		public Form1()
		{
			mousePoint = new Point();
			storedInvites = new ArrayList();
			currentConversations = new ArrayList();
			member = null;
			InitializeComponent();
			trayAlert = new TrayAlert.TrayAlert();
			trayAlert.ActionClick += new EventHandler(trayAlert_ActionClick);
			Login login = new Login(this);
			login.Show();
			loop();
		}

		public void initialiseConnection(string conn)
		{
			facade = (Facade)Activator.GetObject(typeof(DCFacade.Facade), conn);
		}

		public void login(string id, bool continuePrevious)
		{
			try
			{
				member = facade.login(id,continuePrevious);
				updateMemberTree();
			}
			catch(ApplicationException re)
			{
				throw re;
			}
			catch(System.Net.Sockets.SocketException se)
			{
				throw se;
			}
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.memberTree = new System.Windows.Forms.TreeView();
			this.mainTreeCM = new System.Windows.Forms.ContextMenu();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem5,
																					  this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "会话";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "开始新的会话";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem6});
			this.menuItem5.Text = "视图";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "刷新用户列表";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "显示";
			// 
			// memberTree
			// 
			this.memberTree.BackColor = System.Drawing.Color.Beige;
			this.memberTree.ContextMenu = this.mainTreeCM;
			this.memberTree.Dock = System.Windows.Forms.DockStyle.Left;
			this.memberTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.memberTree.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.memberTree.HideSelection = false;
			this.memberTree.ImageIndex = -1;
			this.memberTree.Location = new System.Drawing.Point(0, 0);
			this.memberTree.Name = "memberTree";
			this.memberTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				   new System.Windows.Forms.TreeNode("活动用户"),
																				   new System.Windows.Forms.TreeNode("非活动用户")});
			this.memberTree.SelectedImageIndex = -1;
			this.memberTree.Size = new System.Drawing.Size(152, 461);
			this.memberTree.TabIndex = 6;
			// 
			// mainTreeCM
			// 
			this.mainTreeCM.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem8,
																					   this.menuItem9});
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 0;
			this.menuItem8.Text = "Invite Member";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 1;
			this.menuItem9.Text = "New Conversation";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Dot Chat";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem4,
																						 this.menuItem7});
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 0;
			this.menuItem4.Text = "Restore DotChat";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 1;
			this.menuItem7.Text = "Shutdown DotChat";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(512, 461);
			this.Controls.Add(this.memberTree);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.Color.Black;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "聊天客户端";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Form1 instance = new Form1();
			Application.Run(instance);		
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			ConversationMonitor cm = new ConversationMonitor(this, ref facade);
			currentConversations.Add(cm);
			cm.Closing+=new CancelEventHandler(cm_Closing);
			cm.Activated+=new EventHandler(cm_Activated);
			cm.Show();
			mostRecentlyFocusedMonitor = cm;
		}

		public void joinConversation(InviteForm inv)
		{
			inv.Close();
			ConversationMonitor cm = new ConversationMonitor(this, ref facade, inv.Invitation.InvitedConversation.Id);
			cm.ConversationId = inv.Invitation.InvitedConversation.Id;
			currentConversations.Add(cm);
			cm.Closing+=new CancelEventHandler(cm_Closing);
			cm.Activated+=new EventHandler(cm_Activated);
			cm.Show();
			mostRecentlyFocusedMonitor = cm;
		}

		private void updateMemberTree()
		{
			ArrayList al = facade.getMembers();
			memberTree.Nodes[0].Nodes.Clear();
			memberTree.Nodes[1].Nodes.Clear();
			foreach(Member m in al)
			{
				if(m.IsActive) memberTree.Nodes[0].Nodes.Add(m.Id);
				else memberTree.Nodes[1].Nodes.Add(m.Id);
			}
		}

		/// <summary>
		/// Check if the user has been invited to join any conversations
		/// </summary>
		private void checkPendingConversations()
		{
			if(member != null)
			{
				Invite i = facade.getPendingConversation(member.Id);

				//if a pending conversation exists then invite the member
				if(i!=null)
				{
					if(!this.Visible)
					{
						//if app is running in background then notify via the tray
						trayAlert.Title = "New Invitation.";
						trayAlert.Content = "You have just received a new invitation.";
						trayAlert.Show();
						doCheckInvites = false;
						storedInvites.Add(i);	//add the invite to wait for the user to open the app
					}
					else
					{
						InviteForm iForm = new InviteForm(this, ref facade);
						iForm.Invitation = i;
						iForm.initialise();
						iForm.Show();
						currentConversations.Add(iForm);
					}
				}
			}
		}

		/// <summary>
		/// Process the next server message on the queue and handle it appropriately.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
		private void processServerMessages(Object obj, System.EventArgs e)
		{
			if(member != null)
			{
				if(doCheckInvites)checkPendingConversations();
				ServerMessage sm = facade.getServerMessages(member.Id,ServerMessage.UPDATE_MEMBER_TREE);
				if(sm!=null)
				{
					updateMemberTree();
					if(sm.Type != ServerMessage.UPDATE_MEMBER_TREE_NO_ALERT)
					{
						MessageBox.Show(sm.Content,sm.Subject,MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
					}
				}
			}
		}

		private void closeDown()
		{
			myTimer.Stop();
			if(member!=null)
			{
				facade.setMemberToInactive(member.Id);
				facade.removeMemberFromAllConversations(member.Id);
			}
			this.Close();
		}

		private void sendToTray(Object obj, System.ComponentModel.CancelEventArgs e)
		{
			if(cancelShutdown == true)
			{
				e.Cancel = true;
				this.Hide();
			}			
		}

		private void loop()
		{
			//Adds the event and the event handler for the method that will process the timer event to the timer
			myTimer.Tick += new EventHandler(processServerMessages);
			this.Closing += new CancelEventHandler(sendToTray);
			memberTree.MouseUp+=new MouseEventHandler(memberTree_MouseUp);
 
			// Sets the timer.
			myTimer.Interval = 1000;
			myTimer.Start();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			updateMemberTree();
		}

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			if(WindowState == FormWindowState.Minimized)
			{
				this.Hide();
			}
		}

		private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
		{
			Show();
			WindowState = FormWindowState.Normal;
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			cancelShutdown = false;
			closeDown();
		}

		/// <summary>
		/// Event handler which is fired when the content field of the notification balloon is clicked. Simply opens the app.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void taskbarNotifier_ContentClick(object sender, EventArgs e)
		{
			this.Show();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.Show();
		}

		/// <summary>
		/// Event handler which is fired when the user selects to invite a member via the right-click context menu on the main tree.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			string memId = null;
			//get selected member via the mouse point
			memId = memberTree.GetNodeAt(mousePoint.X,mousePoint.Y).Text;
			ConversationMonitor cm = getFocusedConversationMonitor();
			if(cm!=null)
			{
				cm.sendInvite(memId);	//send invite if conversation monitor already exists
			}
			else						//notify user that a conversation must be in place
			{
				MessageBox.Show("You do not have any active conversations. You must start a conversation before inviting members.", "No conversation available",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
			}
//			else					//otherwise create a new conversation monitor
//			{
//				cm = new ConversationMonitor(this,ref facade);
//				currentConversations.Add(cm);
//				cm.Closing+=new CancelEventHandler(cm_Closing);
//				cm.Activated+=new EventHandler(cm_Activated);
//				cm.Show();
//				cm.sendInvite(memId);
//			}
		}

		/// <summary>
		/// Return the ConversationMonitor which currently has the focus
		/// </summary>
		/// <returns>A ConversationMonitor if one currently exists and has focus. Returns null otherwise.</returns>
		private ConversationMonitor getFocusedConversationMonitor()
		{
			if(mostRecentlyFocusedMonitor != null && currentConversations.Count > 0)
			{
				foreach(ConversationMonitor cm in currentConversations)
				{
					if(mostRecentlyFocusedMonitor.Equals(cm))
					{
						return cm;
					}
				}
			}
			return null;
		}

		private void trayAlert_ActionClick(object sender, EventArgs e)
		{
			this.Show();
			//if a stored invite is waiting then process it
			if(storedInvites.Count > 0)
			{
				InviteForm iForm = new InviteForm(this, ref facade);
				iForm.Invitation = (Invite)storedInvites[0];
				iForm.initialise();
				iForm.Show();
				currentConversations.Add(iForm);
				doCheckInvites = true;
				storedInvites.Clear();
			}
		}

		private void cm_Closing(object sender, CancelEventArgs e)
		{
			currentConversations.Remove((ConversationMonitor)sender);
		}

		private string getSelectedMember()
		{
			return memberTree.SelectedNode.Text;
		}

		/// <summary>
		/// Traps the mouseup event on the member tree and stores its x,y co-ords.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void memberTree_MouseUp(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				mousePoint.X = e.X;
				mousePoint.Y = e.Y;
			}
		}

		/// <summary>
		/// Eventhandler raised when a conversation monitor is activated. Set the most recently focused monitor to the one which was just activated.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cm_Activated(object sender, EventArgs e)
		{
			mostRecentlyFocusedMonitor = (ConversationMonitor)sender;
		}

		/// <summary>
		/// Start a new conversation with the selected member.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			string memId = null;
			//get selected member via the mouse point
			memId = memberTree.GetNodeAt(mousePoint.X,mousePoint.Y).Text;

			//check that member has not attempted to invite themselves
			if(memId == member.Id)
			{
				MessageBox.Show("You can not invite yourself to join a conversation.", "Invalid invitation",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
			}
			else
			{
				ConversationMonitor cm = new ConversationMonitor(this,ref facade);
				currentConversations.Add(cm);
				cm.Closing+=new CancelEventHandler(cm_Closing);
				cm.Activated+=new EventHandler(cm_Activated);
				cm.Show();
				cm.sendInvite(memId);
			}
		}
	}
}
