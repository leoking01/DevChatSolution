using System;

namespace DCBusiness
{
	public class DCMessage : MarshalByRefObject
	{
		private string content;
		private Member owner;

		public string Content
		{
			get{return content;}
			set{content = value;}
		}

		public Member Owner
		{
			get{return owner;}
			set{owner = value;}
		}

		public DCMessage(string s, Member m)
		{
			content = s;
			owner = m;
		}
	}
}
