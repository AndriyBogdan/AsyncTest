using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
	public class AsyncTest
	{

		static Stopwatch sw = new Stopwatch();

		public void WorkerSync()
		{
			sw.Start();

			var val1 = ActionOneSync();
			var val2 = ActionTwoSync();
			var val3 = ActionThreeSync(val1);

			sw.Stop();
			Console.WriteLine($"from 2 - {val2}\nfrom 3 - {val3}");
			Console.WriteLine($"elapsed time - {sw.ElapsedMilliseconds}");
		}



		public async Task WorkerAsync1()
		{
			sw.Start();

			var val1 = await ActionOne();
			var val2 = await ActionTwo();
			var val3 = await ActionThree(val1);

			sw.Stop();
			Console.WriteLine($"from 2 - {val2}\nfrom 3 - {val3}");
			Console.WriteLine($"elapsed time - {sw.ElapsedMilliseconds}");
		}



		public async Task WorkerAsync2()
		{
			sw.Restart();
			Task<int> t1 = ActionOne();

			Task<string> t2 = ActionTwo();

			int val1 = await t1;
			Task<int> t3 = ActionThree(val1);


			var val2 = await t2;
			var val3 = await t3;

			sw.Stop();
			Console.WriteLine($"from 2 - {val2}\nfrom 3 - {val3}");
			Console.WriteLine($"elapsed time - {sw.ElapsedMilliseconds}");
		}



		public async Task WorkerAsync3()
		{
			sw.Restart();

			Task t2 = ActionTwoWrapper();
			Task t3 = ActionThreeWrapper();

			var tasks = new List<Task>() { t2, t3 };

			while (tasks.Count > 0)
			{
				Task finishedTask = await Task.WhenAny(tasks);
				tasks.Remove(finishedTask);
			}

			sw.Stop();
			Console.WriteLine($"elapsed time - {sw.ElapsedMilliseconds}");
		}

		#region ACTION METHODS

		//SYNC MODE

		public int ActionOneSync()
		{
			Thread.Sleep(10000);
			return 1111;
		}
		public string ActionTwoSync()
		{
			Thread.Sleep(3000);
			return "from ActionTwo Method";
		}
		public int ActionThreeSync(int val)
		{
			Thread.Sleep(500);
			return val + 2222;
		}

		//ASYNC MODE

		public async Task<int> ActionOne()
		{
			await Task.Delay(10000);
			return 1111;
		}

		public async Task<string> ActionTwo()
		{
			await Task.Delay(3000);
			return "from ActionTwo Method";
		}

		public async Task<int> ActionThree(int value)
		{
			await Task.Delay(500);
			return value + 2222;
		}

		#endregion


		#region FOR WORKER 3

		public async Task ActionTwoWrapper()
		{
			var res = await ActionTwo();
			Console.WriteLine($"from 2 - {res}");
		}
		public async Task ActionThreeWrapper()
		{
			var res = await ActionThreeModified();
			Console.WriteLine($"from 3 - {res}");
		}
		public async Task<int> ActionThreeModified()
		{
			await Task.Delay(500);
			var res = await ActionOne();
			return res + 2222;
		}

		#endregion
	}
}
