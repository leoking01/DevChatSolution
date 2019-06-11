using System;

namespace DCBusiness
{
	public class ServerMessage : MarshalByRefObject
	{
		private string content;
		private string subject;
		public static int MESSAGE_TYPE;
		private Member targetMember;
		private bool isDelivered;
		public static int UPDATE_MEMBER_TREE = 1;
		public static int UPDATE_CONVERSATION_MONITOR = 2;
		public static int UPDATE_MEMBER_TREE_NO_ALERT = 3;

		public string Content
		{
			get{return content;}
			set{content = value;}
		}
		public string Subject
		{
			get{return subject;}
			set{subject = value;}
		}
		public Member TargetMember
		{
			get{return targetMember;}
			set{targetMember = value;}
		}
		public bool IsDelivered
		{
			get{return isDelivered;}
			set{isDelivered = value;}
		}
		public int Type
		{
			get{return MESSAGE_TYPE;}
		}

		public ServerMessage(string content, string subject, int type, Member targetMember)
		{
			isDelivered = false;
			this.content = content;
			this.subject = subject;
			MESSAGE_TYPE = type;
			this.targetMember = targetMember;
		}
	}
}
