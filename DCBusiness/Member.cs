using System;
using System.Drawing;

namespace DCBusiness
{
	public class Member : MarshalByRefObject
	{
		private string id;
		public static int COLOUR_ID = 0;
		public static Color[] COLOUR_LIST = {Color.DarkGreen, Color.DarkMagenta, Color.DarkBlue};
		private Color textColour;
		private bool isActive;

		public string Id
		{
			get{return id;}
		}
		public Color TextColour
		{
			get{return textColour;}
			set{textColour = value;}
		}
		public bool IsActive
		{
			get{return isActive;}
			set{isActive = value;}
		}

		public Member(string id)
		{
			this.id = id;
			textColour = COLOUR_LIST[COLOUR_ID];
			if(COLOUR_ID == COLOUR_LIST.Length-1)
			{
				COLOUR_ID = 0;
			}
			else
			{
				COLOUR_ID++;
			}
			isActive = true;
		}
	}
}
