using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DutyContent.Libre
{
	class ContentListView : ThirdParty.EXListView
	{
		private int _item_height = 1;

		private Font _font_cat_text;
		private RectangleF _rt_cat_text;
		private StringFormat _sf_cat_text;

		private List<Image> _imglst = new List<Image>();

		//
		public ContentListView()
			: base()
		{
			DoubleBuffered = true;
			GridLines = true;
			ControlPadding = 1;

			MySortBrush = SystemBrushes.ControlLight;
			MyHighlightBrush = Brushes.LightGoldenrodYellow;

			ItemHeight = 40;

			_font_cat_text = new Font(Font.FontFamily, 14.0f, FontStyle.Regular, GraphicsUnit.Pixel);
			_rt_cat_text = new RectangleF(0.0f, 7.0f, 96.0f, 16.0f);
			_sf_cat_text = new StringFormat() { Alignment = StringAlignment.Center };

			Resize += ContentListView_Resize;

			//
			InitializeContentList("ID", "Type", "%", "Name");
		}

		private void ContentListView_Resize(object sender, EventArgs e)
		{
			if (Columns.Count > 0)
				Columns[Columns.Count - 1].Width = -2;
		}

		//
		[Category("ContentListView")]
		public int ItemHeight
		{
			get { return _item_height; }
			set
			{
				if (_item_height != value)
				{
					_item_height = value;

					ImageList il = new ImageList() { ImageSize = new Size(1, value) };
					SmallImageList = il;
				}
			}
		}

		//
		[Category("ContentListView")]
		public Font CategoryTextFont
		{
			get { return _font_cat_text; }
			set { _font_cat_text = value; }
		}

		//
		[Category("ContentListView")]
		public RectangleF CategoryTextRegion
		{
			get { return _rt_cat_text; }
			set { _rt_cat_text = value; }
		}

		//
		[Category("ContentListView")]
		public StringFormat CategoryTextFormat
		{
			get { return _sf_cat_text; }
			set { _sf_cat_text = value; }
		}

		//
		public void InitializeContentList(string id, string type, string percent, string name)
		{
			Items.Clear();
			Columns.Clear();

			Columns.Add(new ThirdParty.EXColumnHeader(id, 50));
			Columns.Add(new ThirdParty.EXColumnHeader(type, 100));
			Columns.Add(new ThirdParty.EXColumnHeader(percent, 40));
			Columns.Add(new ThirdParty.EXColumnHeader(name, 300));
		}

		//
		public void ClearImages()
		{
			_imglst.Clear();
		}

		//
		public int AddImage(Image image)
		{
			_imglst.Add(image);

			return _imglst.Count - 1;
		}

		//
		public int AddCategoryImage(Image baseimage, string text, Brush brush = null)
		{
			if (brush == null)
				brush = Brushes.White;

			Bitmap bmp = new Bitmap(baseimage);

			using (var g = Graphics.FromImage(bmp))
				g.DrawString(text, _font_cat_text, brush, _rt_cat_text, _sf_cat_text);

			return AddImage(bmp);
		}

		//
		public void ResetContentItems()
		{
			BeginUpdate();

			for (var i = Items.Count - 1; i > 0; i--)
				Items.RemoveAt(1);

			EndUpdate();
		}

		//
		public void AddContentItem(int imageindex, string id, string status, string name)
		{
			BeginUpdate();

			ThirdParty.EXListViewItem i = new ThirdParty.EXListViewItem(id);
			i.SubItems.Add(new ThirdParty.EXImageListViewSubItem(_imglst[imageindex], ""));
			i.SubItems.Add(status);
			i.SubItems.Add(name);

			Items.Add(i);

			EndUpdate();
		}

		//
		public void AddContentItem(int imageindex, string name)
		{
			AddContentItem(imageindex, string.Empty, string.Empty, name);
		}

		//
		private int IndexOfContentItem(string id)
		{
			if (Items.Count > 1)
			{
				for (var i = 1; i < Items.Count; i++)
				{
					var v = Items[i];
					if (v.Text.Equals(id))
						return i;
				}
			}

			return -1;
		}

		//
		public void TreatItemFate(int id, int imageindex, int progress, string name)
		{
			var sid = id.ToString();
			var nth = IndexOfContentItem(sid);

			if (nth < 0 && progress >= 0)
			{
				var i = new ThirdParty.EXListViewItem(sid);
				i.SubItems.Add(new ThirdParty.EXImageListViewSubItem(_imglst[imageindex], ""));
				i.SubItems.Add(progress.ToString());
				i.SubItems.Add(name);
				Items.Add(i);
			}
			else if (nth > 0)   // 1~more
			{
				var i = Items[nth];

				if (progress >= 0)
					i.SubItems[2].Text = progress.ToString();
				else
					Items.RemoveAt(nth);
			}
		}

		//
		public void TreatItemCe(int id, int imageindex, string progress, string name)
		{
			var sid = id.ToString();
			var nth = IndexOfContentItem(sid);

			if (nth < 0 && !string.IsNullOrEmpty(progress))
			{
				var i = new ThirdParty.EXListViewItem(sid);
				i.SubItems.Add(new ThirdParty.EXImageListViewSubItem(_imglst[imageindex], ""));
				i.SubItems.Add(progress);
				i.SubItems.Add(name);
				Items.Add(i);
			}
			else if (nth > 0)   // 1~more
			{
				var i = Items[nth];

				if (string.IsNullOrEmpty(progress))
					Items.RemoveAt(nth);
				else
					i.SubItems[2].Text = progress;
			}
		}

		//
		public void TreatItemInstance(int imageindex, int count, string insname)
		{
			if (Items.Count == 0)
				return;

			var i = Items[0];
			(i.SubItems[1] as ThirdParty.EXImageListViewSubItem).MyImage = _imglst[imageindex];
			i.SubItems[2].Text = count <= 0 ? string.Empty : count.ToString();
			i.SubItems[3].Text = insname;
		}
	}
}
