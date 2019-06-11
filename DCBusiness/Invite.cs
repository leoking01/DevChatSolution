using System;

namespace DCBusiness
{
	public class Invite : MarshalByRefObject
	{
		private int id;
		private Member member;
		private Conversation conversation;
		private bool isAccepted;
		private bool isSent;

		public Member InvitedMember
		{
			get{return member;}
		}
		public Conversation InvitedConversation
		{
			get{return conversation;}
		}
		public bool IsAccepted
		{
			get{return isAccepted;}
			set{isAccepted = value;}
		}

		public bool IsSent
		{
			get{return isSent;}
			set{isSent = value;}
		}
		public int Id
		{
			get{return id;}
			set{id = value;}
		}

		public Invite(Member m, Conversation c)
		{
			member = m;
			conversation = c;
			isAccepted = false;
			isSent = false;
		}
	}
}
