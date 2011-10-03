using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using bedrock;
using bedrock.net;
using Extensions;
using jabber.client;
using jabber.protocol.client;
using JabberTest;

namespace BuildTower
{
	public class TowerService
	{
		private IMService messagingService;
		private Tower tower;
		private int currentPin;

		public TowerService()
		{
			tower = new Tower();
			currentPin = 0;

			Console.WriteLine("Build Tower Service started...");
			Console.WriteLine("______________________________");

			messagingService = new IMService(new JabberUser("build_monitor", "password", "jabber.org"));
			messagingService.Processor.Client.OnReadText += new TextHandler(OnReceiveText);
			messagingService.Processor.Client.OnConnect += new AsyncSocketHandler(OnConnect);
			messagingService.Processor.Client.OnMessage += new MessageHandler(onMessage);
			messagingService.Connect();
			while (! messagingService.IsConnected);
			while (true);
		}

		private void onMessage(object sender, Message msg)
		{
			Console.WriteLine(msg.Body);
			tower.Write(0);
			tower.Activate(currentPin);
			currentPin ++;
			currentPin %= 7;
		}

		private void OnConnect(object sender, BaseSocket sock)
		{
			Console.WriteLine("Connected...");
		}

		private void OnReceiveText(object sender, string txt)
		{
//			XmlDocument document = new XmlDocument();
//			try
//			{
//				document.Load(new StringReader(txt));
//			}
//			catch (XmlException)
//			{
//				Console.WriteLine("Invalid XML. Ignoring...");
//				return;
//			}
//
//			XmlNodeList nodes = document.SelectNodes("//message/body");
//			if (nodes.Count == 0) return;
//			Debug.Assert(nodes.Count == 1);
//			string actualMessage = nodes.Item(0).InnerXml;
//			if (! actualMessage.StartsWith("EXT:")) return;
//			Console.WriteLine(actualMessage);
		}

		
		~TowerService()
		{
			messagingService.Disconnect();
		}
	}
}
