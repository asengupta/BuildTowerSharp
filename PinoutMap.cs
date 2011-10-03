using System;
using System.Collections;

namespace BuildTower
{
	public class PinoutMap
	{
		private Hashtable _pinouts;

		public PinoutMap()
		{
			_pinouts = new Hashtable();

			for (int i = 0; i <= 7; ++ i)
			{
				Configure(i, i);
			}
		}

		public void Configure(int actualPin, int virtualPin)
		{
			_pinouts[virtualPin] = actualPin;
		}

		public int ActualPin(int virtualPin)
		{
			return (int) _pinouts[virtualPin];
		}

		public void ConfigureFromUI()
		{
			Tower tower = new Tower();

			for (int i = 0; i <= 7; ++ i)
			{
				tower.Write(0);
				tower.Activate(i);
				Console.WriteLine("This is pin {0}. What number do you want to assign it ?", i);
				int virtualPin = Int32.Parse(Console.ReadLine());
				Configure(i, virtualPin);
			}
		}
	}
}
