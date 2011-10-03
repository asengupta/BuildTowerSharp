using System;
using System.Threading;

namespace BuildTower
{
	public class MainEntry
	{
		[STAThread]
		public static void Main(string[] args)
		{
//			new TowerService();
			Tower tower = new Tower();
			tower.Write(0);
			
//			tower.Activate(0);
//			while (true);
//			tower.Blink(0);
			int i = 0;
			while (true)
			{
				tower.Write(0);
				tower.Activate(i);
				Thread.Sleep(800);
				++ i;

				i %= 3;
			}
//			InteropService.WriteTo(0x378, 0);

//			int i = 0;
//			for (i = 0; i <= 7; ++ i)
//			{
//				tower.Set(i, i % 2);
//			}
//			tower.Write(255);
//			while (true);

//			tower.Activate(0);
//			tower.Deactivate(1);
//			tower.Activate(2);
//			while (true);
//			int i = 0;
//			while (true)
//			{
//				tower.Write(0);
//				tower.Activate(i);
//				i ++;
//				i %= 8;
//				Thread.Sleep(100);
//			}
//			PinoutMap map = new PinoutMap();
//			map.ConfigureFromUI();
//			Tower tower = new Tower(map);
//
//			while (true)
//			{
//				for (int x = 0; x <= 6; ++ x)
//				{
//					tower.Write(0);
//					tower.Activate(x);
//					Thread.Sleep(100);
//				}
//			}
//			int i = 0;
//			while(true)
//			{
//				for (int x = 0; x <= 6; ++ x)
//				{
//					tower.Write(0);
//					tower.Activate(x);
//					Thread.Sleep(500);
//				}
////				Console.WriteLine("Setting to " + i % 2);
////				tower.Write((i % 2) * 255);
////				Thread.Sleep(500);
////				++ i;
//			}
		}
	}
}