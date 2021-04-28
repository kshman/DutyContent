using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DutyContent.ThirdParty
{
	class NativeMethods
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

		public static void ScrollToBottom(RichTextBox richTextBox)
		{
			SendMessage(richTextBox.Handle, 277, (IntPtr)7, IntPtr.Zero);
			richTextBox.SelectionStart = richTextBox.Text.Length;
		}

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowLong(IntPtr hwnd, int index);

		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowLong(IntPtr hwnd, int index, IntPtr dwNewLong);

		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(uint access_flag, bool inherit_handle, int process_id);

		[DllImport("kernel32.dll")]
		public static extern int CloseHandle(IntPtr handle);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowThreadProcessId(IntPtr handle, out int process_id);

		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		public class ProcessHandle
		{
			public Process Process { get; private set; }
			public IntPtr Handle { get; private set; }
			public System.Net.IPEndPoint EndPoint { get; set; }

			public ProcessHandle(Process p)
			{
				Process = p;

				if (p != null && !p.HasExited)
					Handle = OpenProcess(16, false, p.Id);
			}

			~ProcessHandle()
			{
				if (Handle != IntPtr.Zero)
					CloseHandle(Handle);
			}
		}

		[DllImport("Iphlpapi.dll", SetLastError = true)]
		public static extern uint GetExtendedTcpTable(IntPtr tcpTable, ref int tcpTableLength, bool sort, AddressFamily ipVersion, int tcpTableType, int reserved = 0);

		[StructLayout(LayoutKind.Sequential)]
		public struct TcpTable
		{
			public uint entries;
			[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
			public TcpRow[] row;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TcpRow : IEquatable<TcpRow>
		{
			public TcpState state;
			public uint __localAddr;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] __localPort;
			public uint __remoteAddr;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] __remotePort;
			public uint owningPid;

			public IPAddress LocalAddress => new IPAddress(__localAddr);
			public ushort LocalPort => BitConverter.ToUInt16(new byte[2] { __localPort[1], __localPort[0] }, 0);

			public IPAddress RemoteAddress => new IPAddress(__remoteAddr);
			public ushort RemotePort => BitConverter.ToUInt16(new byte[2] { __remotePort[1], __remotePort[0] }, 0);

			public bool Equals(TcpRow right)
			{
				return
					__localAddr == right.__localAddr && LocalPort == right.LocalPort &&
					__remoteAddr == right.__remoteAddr && RemotePort == right.RemotePort &&
					owningPid == right.owningPid;
			}

			public override bool Equals(object obj)
			{
				return !(obj is null) && obj is TcpRow && Equals((TcpRow)obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					var h = (int)__localAddr;
					h = (h * 397) ^ LocalPort;
					h = (h * 397) ^ (int)__remoteAddr;
					h = (h * 397) ^ RemotePort;
					h = (h * 397) ^ (int)owningPid;
					return h;
				}
			}
		}
	}
}
