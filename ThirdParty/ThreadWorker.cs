using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DutyContent.ThirdParty
{
	public abstract class ThreadWorker<T>
	{
		protected readonly object _thread_lock = new object();
		protected readonly object _sleep_lock = new object();
		private Thread _thread_ptr;

		protected bool IsBreak { get; private set; } = false;

		protected abstract void RunThread(T context);

		public void StartThread(T context, string name = null)
		{
			lock (_thread_lock)
			{
				StopThread();
				_thread_ptr = CreateThread(name);
				_thread_ptr.Start(context);
			}
		}

		public void StopThread()
		{
			lock (_thread_lock)
			{
				while (_thread_ptr!=null && _thread_ptr.IsAlive)
				{
					IsBreak = true;
					lock (_sleep_lock)
						Monitor.Pulse(_sleep_lock);
					Monitor.Wait(_thread_lock, 100);
				}

				_thread_ptr = null;
				IsBreak = false;
			}
		}

		protected virtual Thread CreateThread(string name = null)
		{
			var thd = new Thread(new ParameterizedThreadStart((context) =>
			{
				try
				{
					RunThread((T)context);
				}
				finally
				{
					lock (_thread_lock)
					{
						_thread_ptr = null;
						Monitor.PulseAll(_thread_lock);
					}
				}
			}))
			{
				IsBackground = true,
				Name = !string.IsNullOrEmpty(name) ? name : GetType().FullName
			};

			return thd;
		}

		protected void SafeSleep(TimeSpan ts)
		{
			lock (_sleep_lock)
				Monitor.Wait(_sleep_lock, ts);
		}

		protected void SafeSleep(int milliseconds)
		{
			lock (_sleep_lock)
				Monitor.Wait(_sleep_lock, milliseconds);
		}
	}
}
