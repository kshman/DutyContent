using System.Reflection;
using System.Windows.Forms;

namespace DutyContent.ThirdParty
{
	static class WinFormSupp
	{
        public static void DoubleBuffered(Control control, bool enabled)
        {
            var prop = control.GetType().GetProperty(
                "DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);

            prop.SetValue(control, enabled, null);
        }
    }
}
