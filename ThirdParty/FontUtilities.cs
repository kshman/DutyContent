using System.Drawing;
using System.Windows.Forms;

namespace DutyContent.ThirdParty
{
	// https://www.codeproject.com/Questions/1080006/How-can-change-application-font-in-runtime

	public static class FontUtilities
	{
		public static void SimpleChangeFont(Control ctrl, string fontname, bool recursive = false)
		{
			ctrl.Font = new Font(fontname, ctrl.Font.Size, ctrl.Font.Style, GraphicsUnit.Point);

			if (recursive)
				InternalRecursiveChangeFont(fontname, ctrl.Controls);
		}

		private static void InternalRecursiveChangeFont(string fontname, Control.ControlCollection ctrls)
		{
			foreach (Control c in ctrls)
			{
				c.Font = new Font(fontname, c.Font.Size, c.Font.Style, GraphicsUnit.Point);

				if (c.Controls.Count > 0)
					InternalRecursiveChangeFont(fontname, c.Controls);
			}
		}
	}
}
