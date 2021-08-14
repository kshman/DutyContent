using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DutyContent.Libre
{
	class PingGrapher
	{
		private PictureBox _pbx;
		private Bitmap _bmp;
		private Font _fnt;

		private int _count_per_line;

		public int Width { get; private set; } = -1;
		public int Height { get; private set; } = -1;

		public int Step { get; set; } = 20;

		//
		public PingGrapher(PictureBox container_pbx)
		{
			_pbx = container_pbx;

			_fnt = new Font(FontFamily.GenericSansSerif, 7.0f, FontStyle.Regular, GraphicsUnit.Pixel);
		}

		//
		public void Enter()
		{
			if (_pbx == null)
				return;

			if (Width != _pbx.ClientSize.Width || Height != _pbx.ClientSize.Height)
			{
				Width = _pbx.ClientSize.Width;
				Height = _pbx.ClientSize.Height;

				_count_per_line = (Width - Step + Step) / Step + 1;

				_bmp = new Bitmap(Width, Height);
			}

			if (_bmp != null)
			{
				using (var g = Graphics.FromImage(_bmp))
					g.Clear(Color.Black);
			}
		}

		//
		class CalcData
		{
			public int[] Values { get; set; }
			public int Count { get; set; }

			public int Min { get; set; } = int.MaxValue;
			public int Max { get; set; } = int.MinValue;

			public int RangeMax { get; set; }
			public int RangeStep { get; set; }

			public CalcData(List<int> lst, int count_per_line, int range_delta)
			{
				var at = Math.Max(lst.Count - count_per_line, 0);

				Count = lst.Count - at;
				Values = new int[Count];

				for (int j = 0, i = lst.Count - 1; i >= at; i--)
				{
					if (Min > lst[i]) Min = lst[i];
					if (Max < lst[i]) Max = lst[i];

					Values[j++] = lst[i];
				}

				var rf = (float)(Max + range_delta) / 10.0f;
				RangeMax = ((int)Math.Round(rf) + 1) * 10;

				RangeStep =
					RangeMax < 100 ? 20 :
					RangeMax < 300 ? 40 :
					RangeMax < 1000 ? 100 : 200;
			}
		}

		public void DrawValues(List<int> values, DrawType drawtype = DrawType.Linear)
		{
			if (_bmp == null || values.Count < 2)
				return;

			var cd = new CalcData(values, _count_per_line, 5);
			var height = (float)Height;
			var scale = height / cd.RangeMax;

			using (var g = Graphics.FromImage(_bmp))
			{
				float f;
				int u;

				for (var i = cd.RangeStep; i < cd.RangeMax; i += cd.RangeStep)
				{
					f = height - i * scale;
					u = (int)f;

					g.DrawLine(Pens.DarkBlue, 0, u, Width - 1, u);
					g.DrawString(i.ToString(), _fnt, Brushes.DarkGray, 0.0f, f);
				}

				var pen = new Pen(Color.White, 2.0f);

#if false
				u = 0;

				// linear
				for (var i = 0; i < cd.Count - 1; i++)
				{
					var y1 = height - cd.Values[i] * scale;
					var y2 = height - cd.Values[i + 1] * scale;

					g.DrawLine(pen, u, y1, u + Step, y2);

					u += Step;
				}
#else
				// curved
				if (cd.Count > 2)
				{
					u = 10;
					var pts = new PointF[cd.Count - 1];
					for (var i = 0; i < cd.Count - 1; i++, u += Step)
						pts[i] = new PointF(u, height - cd.Values[i] * scale);

					if (drawtype == DrawType.Linear)
						g.DrawLines(pen, pts);
					else if (drawtype == DrawType.Curved)
						g.DrawCurve(pen, pts, 0.6f);
				}
#endif
			}
		}

		//
		public void Leave()
		{
			_pbx.Image = _bmp;
			_pbx.Refresh();
		}

		//
		public enum DrawType : int
		{
			Linear = 0,
			Curved = 1
		}
	}
}
