using System;
using System.Collections;
using System.Threading;
using SpeechLib;

namespace BuildTower
{
	public class Tower
	{
		private readonly int portAddress = 0x378;
		private PinoutMap _pinout;
		private string currentProjectName;
		private static Hashtable projects = new Hashtable();

// 4 and 7 currently out of commission

		public Tower() : this(new PinoutMap())
		{
		}

		public Tower(PinoutMap map)
		{
			_pinout = map;
			Write(0);
		}

		public void Set(int zeroBasedPinNumber, int i)
		{
			if (i == 0) Deactivate(zeroBasedPinNumber);
			else
			{
				Activate(zeroBasedPinNumber);
			}
		}

		public void Activate(int zeroBasedPinNumber)
		{
			int actualPinNumber = Translate(zeroBasedPinNumber);
			int portValue = InteropService.ReadFrom(portAddress);
			int valueToOr = (int) Math.Pow(2, actualPinNumber);
			int newPortValue = portValue | valueToOr;
			Write(newPortValue);
		}

		private int Translate(int zeroBasedPinNumber)
		{
			return _pinout.ActualPin(zeroBasedPinNumber);
		}

		public void Deactivate(int zeroBasedPinNumber)
		{
			int actualPinNumber = Translate(zeroBasedPinNumber);
			int portValue = InteropService.ReadFrom(portAddress);
			int valueToXor = (int) Math.Pow(2, actualPinNumber);
			int allOn = 255;
			int mask = valueToXor ^ allOn;
			int newPortValue = portValue & mask;
			Write(newPortValue);
		}

		public int ReadFrom(int zeroBasedPinNumber)
		{
			int portValue = Read();
			int valueToAnd = (int) Math.Pow(2, zeroBasedPinNumber);
			int result = portValue & valueToAnd;
			return (result > 0) ? 1 : 0;
		}

		public void Write(int value)
		{
			InteropService.WriteTo(portAddress, value);
		}

		public int Read()
		{
			return InteropService.ReadFrom(portAddress);
		}

		public void Blink(int pinIndex, string projectName)
		{
			Write(0);
			if (pinIndex.Equals(projects[projectName]))
			{
				Activate(pinIndex);
				return;
			}
			
			currentProjectName = projectName;
			projects[currentProjectName] = pinIndex;

			Thread lightThread = new Thread(new ThreadStart(BlinkOn1));
			Thread speechThread = new Thread(new ThreadStart(SpeakUp));

			lightThread.Start();
			speechThread.Start();
		}

		private void BlinkOn1()
		{
			Activate((int) projects["Server"]);
		}

		public void Reset()
		{
			Write(0);
			foreach (DictionaryEntry entry in projects)
			{
				if (! projects.ContainsKey(entry)) continue;
				projects[entry] = -1;
			}
			projects = new Hashtable();
		}

		public void Say(string message)
		{
			SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
			new SpVoice().Speak(message, SpFlags);
		}

		private void SpeakUp()
		{
			try 
			{
				SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
				SpVoice Voice = new SpVoice();
				int zeroBasedPinIndex = (int) projects[currentProjectName];
				if (zeroBasedPinIndex == 0)
				{
					Voice.Speak("Someone broke the build for " + currentProjectName + "...", SpFlags);
				}
//				if (zeroBasedPinIndex == 1)
//				{
//					Voice.Speak("Build is green for " + currentProjectName + "...", SpFlags);
//				}
//				if (zeroBasedPinIndex == 2)
//				{
//					Voice.Speak("Building now for " + currentProjectName + "...", SpFlags);
//				}
			}
			catch(Exception error)
			{
			}
		}

//		private void BlinkOn()
//		{
//			Write(0);
//			for (int i = 1; i <= 5; ++ i)
//			{
//				Activate(zeroBasedPinIndex);
//				Thread.Sleep(700);
//				Write(0);
//				Thread.Sleep(700);
//			}
//			Activate(zeroBasedPinIndex);
//		}
//
		~Tower()
		{
//			Write(0);
		}
	}
}