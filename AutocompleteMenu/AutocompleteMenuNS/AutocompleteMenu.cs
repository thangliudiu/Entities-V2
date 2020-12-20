using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
    [ProvideProperty("AutocompleteMenu", typeof(Control))]
    public class AutocompleteMenu : Component, IExtenderProvider
    {
        private static readonly Dictionary<Control, AutocompleteMenu> AutocompleteMenuByControls = new Dictionary<Control, AutocompleteMenu>();

        private static readonly Dictionary<Control, ITextBoxWrapper> WrapperByControls = new Dictionary<Control, ITextBoxWrapper>();

        private ITextBoxWrapper targetControlWrapper;

        private readonly Timer timer = new Timer();

        private IEnumerable<AutocompleteItem> sourceItems = new List<AutocompleteItem>();

        private Size maximumSize;

        private Form myForm;

        private bool forcedOpened = false;

        [Description("Called when user selected the control and needed wrapper over it. You can assign own Wrapper for target control.")]
        [method: CompilerGenerated]
        public event EventHandler<WrapperNeededEventArgs> WrapperNeeded;

        [Description("Occurs when user selects item.")]
        [method: CompilerGenerated]
        public event EventHandler<SelectingEventArgs> Selecting;

        [Description("Occurs after user selected item.")]
        [method: CompilerGenerated]
        public event EventHandler<SelectedEventArgs> Selected;

        [Description("Occurs when user hovered item.")]
        [method: CompilerGenerated]

        public event EventHandler<HoveredEventArgs> Hovered;
        [method: CompilerGenerated]
        public event EventHandler<CancelEventArgs> Opening;

        [Browsable(false)]
        public IList<AutocompleteItem> VisibleItems
        {
            get
            {
                return this.Host.ListView.VisibleItems;
            }
            private set
            {
                this.Host.ListView.VisibleItems = value;
            }
        }

        [DefaultValue(3000), Description("Duration (ms) of tooltip showing")]
        public int ToolTipDuration
        {
            get
            {
                return this.Host.ListView.ToolTipDuration;
            }
            set
            {
                this.Host.ListView.ToolTipDuration = value;
            }
        }

        [Browsable(false)]
        public int SelectedItemIndex
        {
            get
            {
                return this.Host.ListView.SelectedItemIndex;
            }
            internal set
            {
                this.Host.ListView.SelectedItemIndex = value;
            }
        }

        internal AutocompleteMenuHost Host
        {
            get;
            set;
        }

        [Browsable(false)]
        public ITextBoxWrapper TargetControlWrapper
        {
            get
            {
                return this.targetControlWrapper;
            }
            set
            {
                this.targetControlWrapper = value;
                bool flag = value != null && !AutocompleteMenu.WrapperByControls.ContainsKey(value.TargetControl);
                if (flag)
                {
                    AutocompleteMenu.WrapperByControls[value.TargetControl] = value;
                    this.SetAutocompleteMenu(value.TargetControl, this);
                }
            }
        }

        [DefaultValue(typeof(Size), "180, 200"), Description("Maximum size of popup menu")]
        public Size MaximumSize
        {
            get
            {
                return this.maximumSize;
            }
            set
            {
                this.maximumSize = value;
                (this.Host.ListView as Control).MaximumSize = this.maximumSize;
                (this.Host.ListView as Control).Size = this.maximumSize;
                this.Host.CalcSize();
            }
        }

        public Font Font
        {
            get
            {
                return (this.Host.ListView as Control).Font;
            }
            set
            {
                (this.Host.ListView as Control).Font = value;
            }
        }

        [DefaultValue(18), Description("Left padding of text")]
        public int LeftPadding
        {
            get
            {
                bool flag = this.Host.ListView is AutocompleteListView;
                int result;
                if (flag)
                {
                    result = (this.Host.ListView as AutocompleteListView).LeftPadding;
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            set
            {
                bool flag = this.Host.ListView is AutocompleteListView;
                if (flag)
                {
                    (this.Host.ListView as AutocompleteListView).LeftPadding = value;
                }
            }
        }

        [Browsable(true), Description("Colors of foreground and background."), TypeConverter(typeof(ExpandableObjectConverter))]
        public Colors Colors
        {
            get
            {
                return this.Host.ListView.Colors;
            }
            set
            {
                this.Host.ListView.Colors = value;
            }
        }

        [DefaultValue(true), Description("AutocompleteMenu will popup automatically (when user writes text). Otherwise it will popup only programmatically or by Ctrl-Space.")]
        public bool AutoPopup
        {
            get;
            set;
        }

        [DefaultValue(false), Description("AutocompleteMenu will capture focus when opening.")]
        public bool CaptureFocus
        {
            get;
            set;
        }

        [DefaultValue(typeof(RightToLeft), "No"), Description("Indicates whether the component should draw right-to-left for RTL languages.")]
        public RightToLeft RightToLeft
        {
            get
            {
                return this.Host.RightToLeft;
            }
            set
            {
                this.Host.RightToLeft = value;
            }
        }

        public ImageList ImageList
        {
            get
            {
                return this.Host.ListView.ImageList;
            }
            set
            {
                this.Host.ListView.ImageList = value;
            }
        }

        [Browsable(false)]
        public Range Fragment
        {
            get;
            internal set;
        }

        [DefaultValue("[\\w\\.]"), Description("Regex pattern for serach fragment around caret")]
        public string SearchPattern
        {
            get;
            set;
        }

        [DefaultValue(2), Description("Minimum fragment length for popup")]
        public int MinFragmentLength
        {
            get;
            set;
        }

        [DefaultValue(false), Description("Allows TAB for select menu item")]
        public bool AllowsTabKey
        {
            get;
            set;
        }

        [DefaultValue(500), Description("Interval of menu appear (ms)")]
        public int AppearInterval
        {
            get;
            set;
        }

        [DefaultValue(null)]
        public string[] Items
        {
            get
            {
                bool flag = this.sourceItems == null;
                string[] result;
                if (flag)
                {
                    result = null;
                }
                else
                {
                    List<string> list = new List<string>();
                    foreach (AutocompleteItem item in this.sourceItems)
                    {
                        list.Add(item.ToString());
                    }
                    result = list.ToArray();
                }
                return result;
            }
            set
            {
                this.SetAutocompleteItems(value);
            }
        }

        [Browsable(false)]
        public IAutocompleteListView ListView
        {
            get
            {
                return this.Host.ListView;
            }
            set
            {
                bool flag = this.ListView != null;
                if (flag)
                {
                    Control ctrl = value as Control;
                    value.ImageList = this.ImageList;
                    ctrl.RightToLeft = this.RightToLeft;
                    ctrl.Font = this.Font;
                    ctrl.MaximumSize = this.MaximumSize;
                }
                this.Host.ListView = value;
                this.Host.ListView.ItemSelected += new EventHandler(this.ListView_ItemSelected);
                this.Host.ListView.ItemHovered += new EventHandler<HoveredEventArgs>(this.ListView_ItemHovered);
            }
        }

        [DefaultValue(true)]
        public bool Enabled
        {
            get;
            set;
        }

        public bool Visible
        {
            get
            {
                return this.Host != null && this.Host.Visible;
            }
        }

        public AutocompleteMenu()
        {
            this.Host = new AutocompleteMenuHost(this);
            this.Host.ListView.ItemSelected += new EventHandler(this.ListView_ItemSelected);
            this.Host.ListView.ItemHovered += new EventHandler<HoveredEventArgs>(this.ListView_ItemHovered);
            this.VisibleItems = new List<AutocompleteItem>();
            this.Enabled = true;
            this.AppearInterval = 200;
            this.timer.Tick += new EventHandler(this.timer_Tick);
            this.MaximumSize = new Size(180, 200);
            this.AutoPopup = true;
            this.SearchPattern = "[\\w\\.]";
            this.MinFragmentLength = 2;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.timer.Dispose();
                this.Host.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ListView_ItemSelected(object sender, EventArgs e)
        {
            this.OnSelecting();
        }

        private void ListView_ItemHovered(object sender, HoveredEventArgs e)
        {
            this.OnHovered(e);
        }

        public void OnHovered(HoveredEventArgs e)
        {
            bool flag = this.Hovered != null;
            if (flag)
            {
                this.Hovered(this, e);
            }
        }

        protected void OnWrapperNeeded(WrapperNeededEventArgs args)
        {
            bool flag = this.WrapperNeeded != null;
            if (flag)
            {
                this.WrapperNeeded(this, args);
            }
            bool flag2 = args.Wrapper == null;
            if (flag2)
            {
                args.Wrapper = TextBoxWrapper.Create(args.TargetControl);
            }
        }

        private ITextBoxWrapper CreateWrapper(Control control)
        {
            bool flag = AutocompleteMenu.WrapperByControls.ContainsKey(control);
            ITextBoxWrapper result;
            if (flag)
            {
                result = AutocompleteMenu.WrapperByControls[control];
            }
            else
            {
                WrapperNeededEventArgs args = new WrapperNeededEventArgs(control);
                this.OnWrapperNeeded(args);
                bool flag2 = args.Wrapper != null;
                if (flag2)
                {
                    AutocompleteMenu.WrapperByControls[control] = args.Wrapper;
                }
                result = args.Wrapper;
            }
            return result;
        }

        public void Update()
        {
            this.Host.CalcSize();
        }

        public Rectangle GetItemRectangle(int itemIndex)
        {
            return this.Host.ListView.GetItemRectangle(itemIndex);
        }

        bool IExtenderProvider.CanExtend(object extendee)
        {
            bool flag = base.Container != null;
            bool result;
            if (flag)
            {
                foreach (object comp in base.Container.Components)
                {
                    bool flag2 = comp is AutocompleteMenu;
                    if (flag2)
                    {
                        bool flag3 = comp.GetHashCode() < this.GetHashCode();
                        if (flag3)
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }
            bool flag4 = !(extendee is Control);
            if (flag4)
            {
                result = false;
            }
            else
            {
                TextBoxWrapper temp = TextBoxWrapper.Create(extendee as Control);
                result = (temp != null);
            }
            return result;
        }

        public void SetAutocompleteMenu(Control control, AutocompleteMenu menu)
        {
            bool flag = menu != null;
            if (flag)
            {
                bool flag2 = AutocompleteMenu.WrapperByControls.ContainsKey(control);
                if (!flag2)
                {
                    ITextBoxWrapper wrapper = menu.CreateWrapper(control);
                    bool flag3 = wrapper == null;
                    if (!flag3)
                    {
                        bool isHandleCreated = control.IsHandleCreated;
                        if (isHandleCreated)
                        {
                            menu.SubscribeForm(wrapper);
                        }
                        else
                        {
                            control.HandleCreated += delegate (object o, EventArgs e)
                            {
                                menu.SubscribeForm(wrapper);
                            };
                        }
                        AutocompleteMenu.AutocompleteMenuByControls[control] = this;
                        wrapper.LostFocus += new EventHandler(menu.control_LostFocus);
                        wrapper.Scroll += new ScrollEventHandler(menu.control_Scroll);
                        wrapper.KeyDown += new KeyEventHandler(menu.control_KeyDown);
                        wrapper.MouseDown += new MouseEventHandler(menu.control_MouseDown);
                    }
                }
            }
            else
            {
                AutocompleteMenu.AutocompleteMenuByControls.TryGetValue(control, out menu);
                AutocompleteMenu.AutocompleteMenuByControls.Remove(control);
                ITextBoxWrapper wrapper2 = null;
                AutocompleteMenu.WrapperByControls.TryGetValue(control, out wrapper2);
                AutocompleteMenu.WrapperByControls.Remove(control);
                bool flag4 = wrapper2 != null && menu != null;
                if (flag4)
                {
                    wrapper2.LostFocus -= new EventHandler(menu.control_LostFocus);
                    wrapper2.Scroll -= new ScrollEventHandler(menu.control_Scroll);
                    wrapper2.KeyDown -= new KeyEventHandler(menu.control_KeyDown);

                    wrapper2.MouseDown -= new MouseEventHandler(menu.control_MouseDown);
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            bool flag = this.TargetControlWrapper != null;
            if (flag)
            {
                this.ShowAutocomplete(false);
            }
        }

        private void SubscribeForm(ITextBoxWrapper wrapper)
        {
            bool flag = wrapper == null;
            if (!flag)
            {
                Form form = wrapper.TargetControl.FindForm();
                bool flag2 = form == null;
                if (!flag2)
                {
                    bool flag3 = this.myForm != null;
                    if (flag3)
                    {
                        bool flag4 = this.myForm == form;
                        if (flag4)
                        {
                            return;
                        }
                        this.UnsubscribeForm(wrapper);
                    }
                    this.myForm = form;
                    form.LocationChanged += new EventHandler(this.form_LocationChanged);
                    form.ResizeBegin += new EventHandler(this.form_LocationChanged);
                    form.FormClosing += new FormClosingEventHandler(this.form_FormClosing);
                    form.LostFocus += new EventHandler(this.form_LocationChanged);
                }
            }
        }

        private void UnsubscribeForm(ITextBoxWrapper wrapper)
        {
            bool flag = wrapper == null;
            if (!flag)
            {
                Form form = wrapper.TargetControl.FindForm();
                bool flag2 = form == null;
                if (!flag2)
                {
                    form.LocationChanged -= new EventHandler(this.form_LocationChanged);
                    form.ResizeBegin -= new EventHandler(this.form_LocationChanged);
                    form.FormClosing -= new FormClosingEventHandler(this.form_FormClosing);
                    form.LostFocus -= new EventHandler(this.form_LocationChanged);
                }
            }
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        private void form_LocationChanged(object sender, EventArgs e)
        {
            this.Close();
        }

        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private ITextBoxWrapper FindWrapper(Control sender)
        {
            ITextBoxWrapper result;
            while (sender != null)
            {
                bool flag = AutocompleteMenu.WrapperByControls.ContainsKey(sender);
                if (flag)
                {
                    result = AutocompleteMenu.WrapperByControls[sender];
                    return result;
                }
                sender = sender.Parent;
            }
            result = null;
            return result;
        }

        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            this.TargetControlWrapper = this.FindWrapper(sender as Control);
            bool backspaceORdel = e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete;
            bool visible = this.Host.Visible;
            if (visible)
            {
                bool flag = this.ProcessKey((char)e.KeyCode, Control.ModifierKeys);
                if (flag)
                {
                    e.SuppressKeyPress = true;
                }
                else
                {
                    bool flag2 = !backspaceORdel;
                    if (flag2)
                    {
                        this.ResetTimer(1);
                    }
                    else
                    {
                        this.ResetTimer();
                    }
                }
            }
            else
            {
                bool flag3 = !this.Host.Visible;
                if (flag3)
                {
                    Keys keyCode = e.KeyCode;
                    if (keyCode != Keys.ControlKey)
                    {
                        switch (keyCode)
                        {
                            case Keys.Prior:
                            case Keys.Next:
                            case Keys.End:
                            case Keys.Home:
                            case Keys.Left:
                            case Keys.Up:
                            case Keys.Right:
                            case Keys.Down:
                            case Keys.F5:
                                break;
                            default:
                                {
                                    bool flag4 = Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Space;
                                    if (flag4)
                                    {
                                        this.ShowAutocomplete(true);
                                        e.SuppressKeyPress = true;
                                        return;
                                    }
                                    
                                    goto IL_10C;
                                }
                        }
                    }
                    this.timer.Stop();
                    return;
                }
                IL_10C:
                this.ResetTimer();
            }
        }

        private void ResetTimer()
        {
            this.ResetTimer(-1);
        }

        private void ResetTimer(int interval)
        {
            bool flag = interval <= 0;
            if (flag)
            {
                this.timer.Interval = this.AppearInterval;
            }
            else
            {
                this.timer.Interval = interval;
            }
            this.timer.Stop();
            this.timer.Start();
        }

        private void control_Scroll(object sender, ScrollEventArgs e)
        {
            this.Close();
        }

        private void control_LostFocus(object sender, EventArgs e)
        {
            bool flag = !this.Host.Focused;
            if (flag)
            {
                this.Close();
            }
        }

        public AutocompleteMenu GetAutocompleteMenu(Control control)
        {
            bool flag = AutocompleteMenu.AutocompleteMenuByControls.ContainsKey(control);
            AutocompleteMenu result;
            if (flag)
            {
                result = AutocompleteMenu.AutocompleteMenuByControls[control];
            }
            else
            {
                result = null;
            }
            return result;
        }

        internal void ShowAutocomplete(bool forced)
        {
            if (forced)
            {
                this.forcedOpened = true;
            }
            bool flag = this.TargetControlWrapper != null && this.TargetControlWrapper.Readonly;
            if (flag)
            {
                this.Close();
            }
            else
            {
                bool flag2 = !this.Enabled;
                if (flag2)
                {
                    this.Close();
                }
                else
                {
                    bool flag3 = !this.forcedOpened && !this.AutoPopup;
                    if (flag3)
                    {
                        this.Close();
                    }
                    else
                    {
                        this.BuildAutocompleteList(this.forcedOpened);
                        bool flag4 = this.VisibleItems.Count > 0;
                        if (flag4)
                        {
                            bool flag5 = forced && this.VisibleItems.Count == 1 && this.Host.ListView.SelectedItemIndex == 0;
                            if (flag5)
                            {
                                this.OnSelecting();
                                this.Close();
                            }
                            else
                            {
                                this.ShowMenu();
                            }
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
            }
        }

        private void ShowMenu()
        {
            bool flag = !this.Host.Visible;
            if (flag)
            {
                CancelEventArgs args = new CancelEventArgs();
                this.OnOpening(args);
                bool flag2 = !args.Cancel;
                if (flag2)
                {
                    this.TargetControlWrapper.TargetControl.Location.Offset(2, this.TargetControlWrapper.TargetControl.Height + 2);
                    Point point = this.TargetControlWrapper.GetPositionFromCharIndex(this.Fragment.Start);
                    point.Offset(2, this.TargetControlWrapper.TargetControl.Font.Height + 2);
                    this.Host.Show(this.TargetControlWrapper.TargetControl, point);
                    bool captureFocus = this.CaptureFocus;
                    if (captureFocus)
                    {
                        (this.Host.ListView as Control).Focus();
                    }
                }
            }
            else
            {
                (this.Host.ListView as Control).Invalidate();
            }
        }

        private void BuildAutocompleteList(bool forced)
        {
            List<AutocompleteItem> visibleItems = new List<AutocompleteItem>();
            bool foundSelected = false;
            int selectedIndex = -1;
            Range fragment = this.GetFragment(this.SearchPattern);
            string text = fragment.Text;
            bool isShowAll = false;
            if(string.IsNullOrEmpty(text))
            {
                Range fragmentDot = this.GetFragment(@"\.");
                if(fragmentDot.End == fragment.End)
                {
                    if(fragmentDot.Text ==".")
                    isShowAll = true;
                }
            }
        

            bool flag = this.sourceItems != null;
            if (flag)
            {
                bool flag2 = forced || text.Length >= this.MinFragmentLength || isShowAll;
                if (flag2)
                {
                    this.Fragment = fragment;
                    foreach (AutocompleteItem item in this.sourceItems)
                    {
                        item.Parent = this;
                        CompareResult res = item.Compare(text);
                        bool flag3 = res > CompareResult.Hidden;
                        if (flag3)
                        {
                            visibleItems.Add(item);
                        }
                        bool flag4 = res == CompareResult.VisibleAndSelected && !foundSelected;
                        if (flag4)
                        {
                            foundSelected = true;
                            selectedIndex = visibleItems.Count - 1;
                        }
                    }
                }
            }

            var tables = visibleItems.OrderByDescending(x =>
            {
                var number = LevenshteinDistance.Compute2(text, x.Text);
                if (number < 5) number = 0;
                return number;
            });

            this.VisibleItems = tables.ToList();


            bool flag5 = foundSelected;
            if (flag5)
            {
                this.SelectedItemIndex = selectedIndex;
            }
            else
            {
                this.SelectedItemIndex = 0;
            }
            this.Host.ListView.HighlightedItemIndex = -1;
            this.Host.CalcSize();
        }

        internal void OnOpening(CancelEventArgs args)
        {
            bool flag = this.Opening != null;
            if (flag)
            {
                this.Opening(this, args);
            }
        }

        private Range GetFragment(string searchPattern)
        {
            ITextBoxWrapper tb = this.TargetControlWrapper;
            bool flag = tb.SelectionLength > 0;
            Range result2;
            if (flag)
            {
                result2 = new Range(tb);
            }
            else
            {
                string text = tb.Text;
                Regex regex = new Regex(searchPattern);
                Range result = new Range(tb);
                int startPos = tb.SelectionStart;
                int i = startPos;
                while (i >= 0 && i < text.Length)
                {
                    bool flag2 = !regex.IsMatch(text[i].ToString());
                    if (flag2)
                    {
                        break;
                    }
                    i++;
                }
                result.End = i;
                i = startPos;
                while (i > 0 && i - 1 < text.Length)
                {
                    bool flag3 = !regex.IsMatch(text[i - 1].ToString());
                    if (flag3)
                    {
                        break;
                    }
                    i--;
                }
                result.Start = i;
                result2 = result;
            }
            return result2;
        }

        public void Close()
        {
            this.Host.Close();
            this.forcedOpened = false;
        }

        public void SetAutocompleteItems(IEnumerable<string> items)
        {
            List<AutocompleteItem> list = new List<AutocompleteItem>();
            bool flag = items == null;
            if (flag)
            {
                this.sourceItems = null;
            }
            else
            {
                foreach (string item in items)
                {
                    list.Add(new AutocompleteItem(item));
                }
                this.SetAutocompleteItems(list);
            }
        }

        public void SetAutocompleteItems(IEnumerable<AutocompleteItem> items)
        {
            this.sourceItems = items;
        }

        public void AddItem(string item)
        {
            this.AddItem(new AutocompleteItem(item));
        }

        public void AddItem(AutocompleteItem item)
        {
            bool flag = this.sourceItems == null;
            if (flag)
            {
                this.sourceItems = new List<AutocompleteItem>();
            }
            bool flag2 = this.sourceItems is IList;
            if (flag2)
            {
                (this.sourceItems as IList).Add(item);
                return;
            }
            throw new Exception("Current autocomplete items does not support adding");
        }

        public void Show(Control control, bool forced)
        {
            this.SetAutocompleteMenu(control, this);
            this.TargetControlWrapper = this.FindWrapper(control);
            this.ShowAutocomplete(forced);
        }

        internal virtual void OnSelecting()
        {
            bool flag = this.SelectedItemIndex < 0 || this.SelectedItemIndex >= this.VisibleItems.Count;
            if (!flag)
            {
                AutocompleteItem item = this.VisibleItems[this.SelectedItemIndex];
                SelectingEventArgs args = new SelectingEventArgs
                {
                    Item = item,
                    SelectedIndex = this.SelectedItemIndex
                };
                this.OnSelecting(args);
                bool cancel = args.Cancel;
                if (cancel)
                {
                    this.SelectedItemIndex = args.SelectedIndex;
                    (this.Host.ListView as Control).Invalidate(true);
                }
                else
                {
                    bool flag2 = !args.Handled;
                    if (flag2)
                    {
                        Range fragment = this.Fragment;

                        this.ApplyAutocomplete(item, fragment);
                    }
                    this.Close();
                    SelectedEventArgs args2 = new SelectedEventArgs
                    {
                        Item = item,
                        Control = this.TargetControlWrapper.TargetControl
                    };
                    item.OnSelected(args2);
                    this.OnSelected(args2);
                }
            }
        }

        private void ApplyAutocomplete(AutocompleteItem item, Range fragment)
        {
            string newText = item.GetTextForReplace();
            fragment.Text = newText;
            fragment.TargetWrapper.TargetControl.Focus();
        }

        internal void OnSelecting(SelectingEventArgs args)
        {
            bool flag = this.Selecting != null;
            if (flag)
            {
                this.Selecting(this, args);
            }
        }

        public void OnSelected(SelectedEventArgs args)
        {
            bool flag = this.Selected != null;
            if (flag)
            {
                this.Selected(this, args);
            }
        }

        public void SelectNext(int shift)
        {
            this.SelectedItemIndex = Math.Max(0, Math.Min(this.SelectedItemIndex + shift, this.VisibleItems.Count - 1));
            (this.Host.ListView as Control).Invalidate();
        }

        public bool ProcessKey(char c, Keys keyModifiers)
        {
            int page = this.Host.Height / (this.Font.Height + 4);
            bool flag = keyModifiers == Keys.None;
            bool result;
            if (flag)
            {
                if (c <= '\r')
                {
                    if (c != '\t')
                    {
                        if (c == '\r')
                        {
                            this.OnSelecting();
                            result = true;
                            return result;
                        }
                    }
                    else
                    {
                        bool flag2 = !this.AllowsTabKey;
                        if (!flag2)
                        {
                            this.OnSelecting();
                            result = true;
                            return result;
                        }
                    }
                }
                else
                {
                    if (c == '\u001b')
                    {
                        this.Close();
                        result = true;
                        return result;
                    }
                    switch (c)
                    {
                        case '!':
                            this.SelectNext(-page);
                            result = true;
                            return result;
                        case '"':
                            this.SelectNext(page);
                            result = true;
                            return result;
                        case '%':
                        case '\'':
                            this.Close();
                            result = false;
                            return result;
                        case '&':
                            this.SelectNext(-1);
                            result = true;
                            return result;
                        case '(':
                            this.SelectNext(1);
                            result = true;
                            return result;
                    }
                }
            }
            result = false;
            return result;
        }
    }
}
