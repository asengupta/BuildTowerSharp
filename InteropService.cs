using System.Runtime.InteropServices;

namespace BuildTower
{
	public class InteropService
	{
		[DllImport("inpout32.dll", EntryPoint="Out32")]
		public static extern void WriteTo(int address, int value);

		[DllImport("inpout32.dll", EntryPoint="Inp32")]
		public static extern int ReadFrom(int address);
	}
}