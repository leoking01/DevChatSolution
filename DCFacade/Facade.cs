using System;
using System.Collections;
using System.Runtime.Remoting;
using DCBusiness;

namespace DCFacade
{
	public class Facade : MarshalByRefObject
	{
		private Mediator mediator;

		public Facade()
		{
			mediator = new Mediator();
		}

		public int initialiseConversation(string memberId)
		{
			return mediator.initialiseConversation(memberId);
		}

		public void inviteMember(string memberId, int conversationId)
		{
			mediator.inviteMember(memberId, conversationId);
		}

		public void addMemberToConversation(string memberId, int conversationId)
		{
		}

		public void sendMessage(string s, int i, string id)
		{
			mediator.sendMessage(s,i,id);
		}

		public Member login(string id,bool continuePrevious)
		{
			try
			{
				return mediator.login(id, continuePrevious);
			}
			catch(ApplicationException se)		
			{
				throw se;
			}
		}

		public ArrayList getMembers()
		{
			return mediator.MembersLoggedIn;
		}

		public Conversation getConversationById(int id)
		{
			return mediator.getConversationById(id);
		}

		public Invite getPendingConversation(string memberId)
		{
			return mediator.getPendingConversation(memberId);
		}

		public ServerMessage getServerMessages(string memberId)
		{
			return mediator.getServerMessage(memberId);
		}

		public ServerMessage getServerMessages(string memberId,int type)
		{
			return mediator.getServerMessage(memberId,type);
		}

		public void acceptInvitation(string memberId, int invitationId)
		{
			mediator.acceptInvitation(memberId,invitationId);
		}

		public void rejectInvitation(string memberId, int invitationId)
		{
			mediator.rejectInvitation(memberId, invitationId);
		}

		public void removeMemberFromConversation(string memberId, int conversationId)
		{
			mediator.removeMemberFromConversation(memberId,conversationId);
		}

		public void removeMemberFromAllConversations(string memberId)
		{
			mediator.removeMemberFromAllConversations(memberId);
		}

		public void setMemberToInactive(string memberId)
		{
			mediator.setMemberToInactive(memberId);
		}
	}
}
