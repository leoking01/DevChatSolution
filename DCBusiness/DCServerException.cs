using System;

namespace DCBusiness
{
	public class DCServerException : System.Runtime.Remoting.RemotingException
	{
		public DCServerException() : base()
		{
		}

		public DCServerException(string s) : base(s)
		{
		}
	}
}
