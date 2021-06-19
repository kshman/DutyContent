using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutyContent.ThirdParty
{
	// https://www.codeproject.com/Questions/1080006/How-can-change-application-font-in-runtime

	public static class FontUtilities
	{
		public static void SimpleChangeFont(this Control ctrl, string fontname, bool recursive = false)
		{
			var font = new Font(fontname, ctrl.Font.Size, ctrl.Font.Style, GraphicsUnit.Point);
			ctrl.Font = font;

			if (recursive)
				RecursiveChangeFont(fontname, ctrl.Controls);
		}

		private static void RecursiveChangeFont(string fontname, Control.ControlCollection ctrls)
		{
			foreach (Control c in ctrls)
			{
				var font = new Font(fontname, c.Font.Size, c.Font.Style, GraphicsUnit.Point);
				c.Font = font;

				if (c.Controls.Count > 0)
					RecursiveChangeFont(fontname, c.Controls);
			}
		}
	}
}
