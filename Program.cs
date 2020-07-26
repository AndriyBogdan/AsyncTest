using System;
using System.Threading;

namespace ConsoleApp5
{
	class Program
	{
		static void Main(string[] args)
		{
			AsyncTest at = new AsyncTest();

			//===========================================================

			//at.WorkerSync();

			//===========================================================

			//the most frequent and common mechanism of using async programming
			//at.WorkerAsync1().GetAwaiter().GetResult();

			//===========================================================

			//more optimized mechanism for using async
			//at.WorkerAsync2().GetAwaiter().GetResult();

			//===========================================================

			//most optimized use case. Perhaps in this project all advantages are not visible due 
			//to its simplicity but it gives a significant performance increase in complex asynchronous code
			//at.WorkerAsync3().GetAwaiter().GetResult();
		}
	}
}
