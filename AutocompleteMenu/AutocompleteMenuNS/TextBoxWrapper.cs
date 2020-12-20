using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
    public class TextBoxWrapper : ITextBoxWrapper
    {
        private Control target;

        private PropertyInfo selectionStart;

        private PropertyInfo selectionLength;

        private PropertyInfo selectedText;

        private PropertyInfo readonlyProperty;

        private MethodInfo getPositionFromCharIndex;

        [method: CompilerGenerated]
        private event ScrollEventHandler RTBScroll;

        public virtual event EventHandler LostFocus
        {
            add
            {
                this.target.LostFocus += value;
            }
            remove
            {
                this.target.LostFocus -= value;
            }
        }

        public virtual event ScrollEventHandler Scroll
        {
            add
            {
                bool flag = this.target is RichTextBox;
                if (flag)
                {
                    this.RTBScroll += value;
                }
                else
                {
                    bool flag2 = this.target is ScrollableControl;
                    if (flag2)
                    {
                        (this.target as ScrollableControl).Scroll += value;
                    }
                }
            }
            remove
            {
                bool flag = this.target is RichTextBox;
                if (flag)
                {
                    this.RTBScroll -= value;
                }
                else
                {
                    bool flag2 = this.target is ScrollableControl;
                    if (flag2)
                    {
                        (this.target as ScrollableControl).Scroll -= value;
                    }
                }
            }
        }

        public virtual event KeyEventHandler KeyDown
        {
            add
            {
                this.target.KeyDown += value;
            }
            remove
            {
                this.target.KeyDown -= value;
            }
        }

        public virtual event PreviewKeyDownEventHandler PreviewKeyDown
        {
            add
            {
                this.target.PreviewKeyDown += value;
            }
            remove
            {
                this.target.PreviewKeyDown -= value;
            }
        }


        public virtual event MouseEventHandler MouseDown
		{
			add
			{
				this.target.MouseDown += value;
			}
			remove
			{
				this.target.MouseDown -= value;
			}
		}

		public virtual string Text
		{
			get
			{
				return this.target.Text;
			}
			set
			{
				this.target.Text = value;
			}
		}

		public virtual string SelectedText
		{
			get
			{
				return (string)this.selectedText.GetValue(this.target, null);
			}
			set
			{
				this.selectedText.SetValue(this.target, value, null);
			}
		}

		public virtual int SelectionLength
		{
			get
			{
				return (int)this.selectionLength.GetValue(this.target, null);
			}
			set
			{
				this.selectionLength.SetValue(this.target, value, null);
			}
		}

		public virtual int SelectionStart
		{
			get
			{
				return (int)this.selectionStart.GetValue(this.target, null);
			}
			set
			{
				this.selectionStart.SetValue(this.target, value, null);
			}
		}

		public virtual Control TargetControl
		{
			get
			{
				return this.target;
			}
		}

		public bool Readonly
		{
			get
			{
				return (bool)this.readonlyProperty.GetValue(this.target, null);
			}
		}

		private TextBoxWrapper(Control targetControl)
		{
			this.target = targetControl;
			this.Init();
		}

		protected virtual void Init()
		{
			Type t = this.target.GetType();
			this.selectedText = t.GetProperty("SelectedText");
			this.selectionLength = t.GetProperty("SelectionLength");
			this.selectionStart = t.GetProperty("SelectionStart");
			this.readonlyProperty = t.GetProperty("ReadOnly");
			this.getPositionFromCharIndex = (t.GetMethod("GetPositionFromCharIndex") ?? t.GetMethod("PositionToPoint"));
			bool flag = this.target is RichTextBox;
			if (flag)
			{
				(this.target as RichTextBox).VScroll += new EventHandler(this.TextBoxWrapper_VScroll);
			}
		}

		private void TextBoxWrapper_VScroll(object sender, EventArgs e)
		{
			bool flag = this.RTBScroll != null;
			if (flag)
			{
				this.RTBScroll(sender, new ScrollEventArgs(ScrollEventType.EndScroll, 0, 1));
			}
		}

		public static TextBoxWrapper Create(Control targetControl)
		{
			TextBoxWrapper result = new TextBoxWrapper(targetControl);
			bool flag = result.selectedText == null || result.selectionLength == null || result.selectionStart == null || result.getPositionFromCharIndex == null;
			TextBoxWrapper result2;
			if (flag)
			{
				result2 = null;
			}
			else
			{
				result2 = result;
			}
			return result2;
		}

		public virtual Point GetPositionFromCharIndex(int pos)
		{
			return (Point)this.getPositionFromCharIndex.Invoke(this.target, new object[]
			{
				pos
			});
		}

		public virtual Form FindForm()
		{
			return this.target.FindForm();
		}
	}
}
