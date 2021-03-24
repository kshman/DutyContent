using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutyContent.ThirdParty
{
	class FastGraphLine
	{
		private PictureBox _pbx;
		private Bitmap _bmp;

		private int _midv;
		private int _dcnt;

		public int Width { get; private set; } = -1;
		public int Height { get; private set; } = -1;

		//
		public FastGraphLine(PictureBox pbx)
		{
			_pbx = pbx;
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

				_midv = Height / 2;
				_dcnt = (Width - 40) / 20;

				_bmp = new Bitmap(Width, Height);
			}

			if (_bmp != null)
			{
				using (var g = Graphics.FromImage(_bmp))
				{
					g.Clear(Color.Black);
					g.DrawLine(Pens.DarkBlue, 0, _midv, Width - 1, _midv);
				}
			}
		}

		//
		public void SetValues(List<int> vs, int mindelta = 5, int maxdelta = 5)
		{
			if (_bmp == null && vs.Count >= 2)
				return;

			int min = int.MaxValue, max = int.MinValue;
			int cnt = vs.Count - _dcnt;

			if (cnt < 0)
				cnt = 0;

			for (var i = vs.Count - 1; i >= cnt; i--)
			{
				if (min > vs[i])
					min = vs[i];
				if (max < vs[i])
					max = vs[i];
			}

			min -= mindelta;
			max += maxdelta;

			//
			MesgLog.L("Count: {0}, MM: {1}/{2}", vs.Count, min, max);

			float scale = (float)Height / (float)(max - min);
			int xx = 10;

			using (var g = Graphics.FromImage(_bmp))
			{
				Pen pen = new Pen(Color.White, 2.0f);

				for (var i = vs.Count - 2; i >= cnt; i--)
				{
					var y1 = (int)((vs[i + 1] - min) * scale);
					var y2 = (int)((vs[i] - min) * scale);

					g.DrawLine(pen, xx, y1, xx + 10, y2);

					xx += 10;
				}
			}
		}

		//
		public void Leave()
		{
			_pbx.Image = _bmp;
			_pbx.Refresh();
		}
	}
}
