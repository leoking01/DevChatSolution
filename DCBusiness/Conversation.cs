using System;
using System.Collections;

namespace DCBusiness
{
	public class Conversation : MarshalByRefObject
	{
		private ArrayList members;
		private ArrayList messages;
		private int id;

		public int Id
		{
			get{return id;}
			set{this.id = value;}
		}

		public ArrayList Messages
		{
			get{return messages;}
			set{messages = value;}
		}

		public ArrayList Members
		{
			get{return members;}
		}

		public Conversation()
		{
			members = new ArrayList();
			messages = new ArrayList();
		}
	}
}
