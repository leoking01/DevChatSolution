using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.IO;

namespace DCServerService
{
	public class Service1 : System.ServiceProcess.ServiceBase
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Service1()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();
		}

		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new Service1() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Service1
			// 
			this.ServiceName = "DotChatServer";

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			RemotingConfiguration.Configure("c:\\server.config");
			TcpChannel chan = new TcpChannel(8086);
			ChannelServices.RegisterChannel(chan);
			HttpChannel hChan = new HttpChannel(8087);
			ChannelServices.RegisterChannel(hChan);
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(DCFacade.Facade), "Facade", WellKnownObjectMode.Singleton);
		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			// TODO: Add code here to perform any tear-down necessary to stop your service.
		}
	}
}
