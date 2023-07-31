using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
	public partial class Form4 : Form
	{
		static bool CheckIfBrowserIsRunning()
		{
			Process[] Processes = Process.GetProcesses();
			
			if (Processes.Length <= 0)
				return false;
			if (!Processes.Any(d => d.MainWindowHandle != IntPtr.Zero))
			   return false;

			return true;
		}


		public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

		[DllImport("user32.dll")]
		protected static extern bool EnumWindows(Win32Callback enumProc, IntPtr lParam);

		private static bool EnumWindow(IntPtr handle, IntPtr pointer)
		{
			List<IntPtr> pointers = GCHandle.FromIntPtr(pointer).Target as List<IntPtr>;
			pointers.Add(handle);
			return true;
		}
		[DllImport("User32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr windowHandle, StringBuilder stringBuilder, int nMaxCount);

		[DllImport("user32.dll", EntryPoint = "GetWindowTextLength", SetLastError = true)]
		internal static extern int GetWindowTextLength(IntPtr hwnd);
		private static string GetTitle(IntPtr handle)
		{
			int length = GetWindowTextLength(handle);
			StringBuilder sb = new StringBuilder(length + 1);
			GetWindowText(handle, sb, sb.Capacity);
			return sb.ToString();
		}
		private static List<IntPtr> GetAllWindows()
		{
			Win32Callback enumCallback = new Win32Callback(EnumWindow);
			List<IntPtr> pointers = new List<IntPtr>();
			GCHandle listHandle = GCHandle.Alloc(pointers);
			try
			{
				EnumWindows(enumCallback, GCHandle.ToIntPtr(listHandle));
			}
			finally
			{
				if (listHandle.IsAllocated) listHandle.Free();
			}
			return pointers;
		}
		/*private static string GetUrlInternal()
		{
			string sURL = null;
			Process[] procsChrome = Process.GetProcessesByName("chrome");
			foreach (Process chrome in procsChrome)
			{
				if (chrome.MainWindowHandle == IntPtr.Zero)
				{
					continue;
				}
				AutomationElement element = AutomationElement.FromHandle(chrome.MainWindowHandle);
				AutomationElement elm1 = element.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Google Chrome"));
				AutomationElement elm2 = TreeWalker.RawViewWalker.GetLastChild(elm1);
				AutomationElement elm3 = elm2.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, ""));
				AutomationElement elm4 = elm3.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ToolBar));
				AutomationElement elementx = elm1.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
				if (!(bool)elementx.GetCurrentPropertyValue(AutomationElement.HasKeyboardFocusProperty))
				{
					sURL = ((ValuePattern)elementx.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
				}
			}
			return sURL;
		}*/


		public Form4()
		{
			InitializeComponent();
		}

		private void Form4_Load(object sender, EventArgs e)
		{
			foreach (var item in GetAllWindows())
			{
				string title = GetTitle(item);

				if (title.Contains("Google Chrome"))
				{
					listBox1.Items.Add(title);
				}
				else if (title.Contains("Edge"))
				{
					listBox1.Items.Add(title);
				}
				else if (title.Contains("firefox"))
				{
					listBox1.Items.Add(title);
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			List<string> list = new List<string>();	
			foreach (var item in GetAllWindows())
			{
				string title = GetTitle(item);
				
				if (title.Contains("Google Chrome"))
				{
					if(!listBox1.Items.Contains(title))
					{
						listBox1.Items.Add(title);
					}
					
				}
				else if (title.Contains("Edge"))
				{
					if (!listBox1.Items.Contains(title))
					{
						listBox1.Items.Add(title);
					}
				}
				else if (title.Contains("firefox"))
				{
					if (!listBox1.Items.Contains(title))
					{
						listBox1.Items.Add(title);
					}
				}
				
			}
		}
	}
}
