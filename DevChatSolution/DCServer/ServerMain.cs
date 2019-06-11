using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;

namespace DCServer
{
	class ServerMain
	{
		/// <summary>
		/// The main entry point for the dot chat server
		/// </summary>
		/// <param name="args">The first two arguments specify the TCP and HTTP ports to use respectively. ie. arguments of 8085 and 8086 will instruct the server to listen for requests on TCP:8085 and HTTP:8086 </param>
		[STAThread]
		static void Main(string[] args)
		{
			int tPort = 0;
			int hPort = 0;

			if(args.Length != 2)
			{
				System.Console.WriteLine("Port numbers not specified for TCP and HTTP. Using default configuration of 8085 and 8086...");
				tPort = 8085;
				hPort = 8086;
			}
			else
			{	
				tPort = Convert.ToInt32(args[0]);
				hPort = Convert.ToInt32(args[1]);
			}
			TcpChannel chan = new TcpChannel(tPort);
			ChannelServices.RegisterChannel(chan);
			HttpChannel hChan = new HttpChannel(hPort);
			ChannelServices.RegisterChannel(hChan);
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(DCFacade.Facade), "Facade", WellKnownObjectMode.Singleton);
			System.Console.WriteLine("TCP Server started and listening for requests on port " + tPort + "..." );
			System.Console.WriteLine("HTTP Server started and listening for requests on port " + hPort + "...");
			System.Console.ReadLine();
		}
	}
}
