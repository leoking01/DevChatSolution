using System;
using System.Collections;
using System.Runtime.Remoting;

namespace DCBusiness
{
	public class Mediator : MarshalByRefObject
	{
		private ArrayList membersLoggedIn;
		private ArrayList conversations;
		private ArrayList pendingInvites;
		private ArrayList serverMessages;
		private static int lastId = 0;
		private static int lastInviteId = 0;

		public ArrayList MembersLoggedIn
		{
			get{return membersLoggedIn;}
		}

		public ArrayList Conversations
		{
			get{return conversations;}
		}

		public Mediator()
		{
			membersLoggedIn = new ArrayList();
			conversations = new ArrayList();
			pendingInvites = new ArrayList();
			serverMessages = new ArrayList();
		}

		public void sendMessage(string s, int id, string memId)
		{
			Conversation c = getConversationById(id);
			Member m = getMemberById(memId);
			c.Messages.Add(new DCMessage(s,m));
		}

		/// <summary>
		/// Logs the member into the system.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Member login(string id, bool continuePrevious)
		{
			if(continuePrevious)
			{
				//check that member does exist
				Member m = getMemberById(id);
				if(m==null)
				{
					throw new ApplicationException("Member does not exist");
				}
				else
				{
					m.IsActive = true;
					foreach(Member m2 in membersLoggedIn)
					{
						ServerMessage sm = new ServerMessage(null,null,ServerMessage.UPDATE_MEMBER_TREE_NO_ALERT,m2);
						serverMessages.Add(sm);
					}
					return m;
				}
			}
			else
			{
				//check that a member with that id is not already logged in
				Member m = getMemberById(id);
				if(m!=null)
				{
					throw new ApplicationException("Member already exists");
					//throw new DCServerException("Member already exists");
				}
				else
				{
					m = new Member(id);
					membersLoggedIn.Add(m);
					foreach(Member m2 in membersLoggedIn)
					{
						ServerMessage sm = new ServerMessage(null,null,ServerMessage.UPDATE_MEMBER_TREE_NO_ALERT,m2);
						serverMessages.Add(sm);
					}
					return m;
				}
			}
		}

		public int initialiseConversation(string memberId)
		{
			Conversation c = new Conversation();
			conversations.Add(c);
			c.Id = lastId;
			lastId++;
			c.Members.Add(getMemberById(memberId));
			return c.Id;
		}

		public Conversation getConversationById(int id)
		{
			foreach(Conversation c in conversations)
			{
				if(c.Id == id) return c;
			}
			return null;
		}

		public Member getMemberById(string id)
		{
			foreach(Member m in membersLoggedIn)
			{
				if(m.Id == id) return m;
			}
			return null;
		}

		public Invite getInviteById(int id)
		{
			foreach(Invite i in pendingInvites)
			{
				if(i.Id == id) return i;
			}
			return null;
		}

		/// <summary>
		/// Invite a member by adding their id to the pendingInvites list
		/// </summary>
		/// <param name="memberId"></param>
		/// <param name="conversationId"></param>
		public void inviteMember(string memberId, int conversationId)
		{
			Conversation c = getConversationById(conversationId);
			Member m = getMemberById(memberId);

			//first check if member is already active in conversation
			if(!c.Members.Contains(m))
			{
				Invite i = new Invite(m,c);
				i.Id = lastInviteId;
				lastInviteId++;
				pendingInvites.Add(i);
			}
		}

		/// <summary>
		/// Check whether the specified member has been invited to any meetings they have not already joined.
		/// </summary>
		/// <param name="memberId"></param>
		/// <returns></returns>
		public Invite getPendingConversation(string memberId)
		{
			foreach(Invite i in pendingInvites)
			{
				//if member is invited
				if(i.InvitedMember.Id == memberId && i.IsSent == false)
				{
					i.IsSent = true;
					return i;
				}
			}
			return null;	//no invites pending
		}

		/// <summary>
		/// This method is called when a member accepts an invitation
		/// </summary>
		/// <param name="memberId"></param>
		/// <param name="invitationId"></param>
		public void acceptInvitation(string memberId, int invitationId)
		{
			//add the member to the conversation
			Invite i = getInviteById(invitationId);
			Member m = getMemberById(memberId);
			i.InvitedConversation.Members.Add(m);
			i.IsAccepted = true;

			//post a server message notifying all other members
			foreach(Member mb in i.InvitedConversation.Members)
			{
				if(mb.Id != m.Id)
				{
					ServerMessage sm = new ServerMessage(m.Id + " has accepted an invitation to join this conversation.", "A new member has joined", ServerMessage.UPDATE_CONVERSATION_MONITOR, mb);
					//add the server message to the list
					serverMessages.Add(sm);
				}
			}
		}

		public void rejectInvitation(string memberId, int invitationId)
		{
			Invite i = getInviteById(invitationId);

			foreach(Member mb in i.InvitedConversation.Members)
			{
				ServerMessage sm = new ServerMessage(memberId + " has declined an invitation to join a conversation.", "A member has declined", ServerMessage.UPDATE_CONVERSATION_MONITOR, mb);
				serverMessages.Add(sm);
			}
		}

		/// <summary>
		/// Remove a member from the specified conversation.
		/// </summary>
		/// <param name="memberId"></param>
		/// <param name="conversationId"></param>
		public void removeMemberFromConversation(string memberId, int conversationId)
		{
			Conversation c = getConversationById(conversationId);
			Member mem = getMemberById(memberId);
			c.Members.Remove(mem);

			//post a message to the other members
			foreach(Member m in c.Members)
			{
				ServerMessage sm = new ServerMessage(mem.Id + " has left this conversation", "A member has left the conversation", ServerMessage.UPDATE_CONVERSATION_MONITOR, m);
				serverMessages.Add(sm);
			}
		}

		/// <summary>
		/// Remove a member from all active conversations.
		/// </summary>
		/// <param name="memberId"></param>
		/// <param name="conversationId"></param>
		public void removeMemberFromAllConversations(string memberId)
		{
			Member m = getMemberById(memberId);
			//check every conversation for this member
			foreach(Conversation c in conversations)
			{
				if(c.Members.Contains(m))
				{
					c.Members.Remove(m);
				}
			}
		}

		/// <summary>
		/// return a pending server message to the specified member
		/// </summary>
		/// <param name="memberId"></param>
		/// <returns></returns>
		public ServerMessage getServerMessage(string memberId)
		{
			foreach(ServerMessage sm in serverMessages)
			{
				if(sm.TargetMember != null)
				{
					if(sm.TargetMember.Id == memberId && !sm.IsDelivered)
					{
						sm.IsDelivered = true;
						return sm;
					}
				}
			}
			return null;
		}

		public ServerMessage getServerMessage(string memberId, int type)
		{
			foreach(ServerMessage sm in serverMessages)
			{
				if(!sm.IsDelivered && type == ServerMessage.UPDATE_MEMBER_TREE && sm.TargetMember.Id == memberId)
				{
					sm.IsDelivered = true;
					return sm;
				}
			}
			return null;
		}

		/// <summary>
		/// Deactivate a member but do not destroy their id.
		/// </summary>
		/// <param name="memberId"></param>
		public void setMemberToInactive(string memberId)
		{
			Member m = getMemberById(memberId);
			m.IsActive = false;
			postMessageToAllMembers(memberId + " has left the chat and is now inactive until further notice.","Member is now inactive",ServerMessage.UPDATE_MEMBER_TREE,membersLoggedIn,true);
		}

		private void postMessageToAllMembers(string content, string title, int type, ArrayList mems, bool postToActiveOnly)
		{
			//post a server message notifying all other members
			foreach(Member mb in mems)
			{
				if(postToActiveOnly)
				{
					if(mb.IsActive)
					{
						ServerMessage sm = new ServerMessage(content, title, type, mb);
						//add the server message to the list
						serverMessages.Add(sm);
					}
				}
			}
		}
	}
}
