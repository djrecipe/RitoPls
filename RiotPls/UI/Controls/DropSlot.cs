﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using RiotPls.API.Serialization.Champions;
using RiotPls.API.Serialization.Interfaces;
using RiotPls.API.Serialization.Items;
using RiotPls.UI.Views;

namespace RiotPls.UI.Controls
{
    /// <summary>
    /// Receives and displays a drag-and-droppable Riot API entity
    /// </summary>
    public class DropSlot : UserControl
    {
        #region Types                                                                     
        public delegate void DropOccurredDelegate(DropSlot slot, IRiotDroppable drop);
        public delegate void LevelObtainedChangedDelegate(DropSlot slot, IRiotDroppable drop, int level);
        public delegate void PricingChangedDelegate(DropSlot slot, IRiotDroppable drop, bool full_pricing);
        /// <summary>
        /// Type of Riot entity being represented
        /// </summary>
        public enum DataTypes : uint
        {
            Champion = 0,
            Item = 1,
            ItemBuy = 2
        }
        #endregion
        #region Instance Members
        #region Controls           
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip cmenItem;
        private ContextMenuStrip cmenChampion;
        private ToolStripMenuItem mnuitmRemoveItem;
        private ToolStripMenuItem mnuitmItemLevelObtained;
        private ToolStripMenuItem mnuitmRemoveChampion;
        private ToolStripTextBox mnuitmLevelObtainedValue;
        private Label lblMain;
        private PictureBox picMain;
        private ContextMenuStrip cmenItemBuy;
        private ToolStripMenuItem mnuitmRemoveItemBuy;
        private ToolStripMenuItem mnuitmPricing;
        private ToolStripMenuItem mnuitmFullPricing;
        private ToolStripMenuItem mnuitmUpgradePricing;
        private formItemComponents componentsForm = null;
        private Button btnComponents;
        #endregion
        #region Events       
        /// <summary>
        /// An entity has been dropped onto the control
        /// </summary>
        public event DropOccurredDelegate DropOccurred;
        /// <summary>
        /// Level obtained value for the current entity has changed
        /// </summary>
        public event LevelObtainedChangedDelegate LevelObtainedChanged;
        #endregion
        private IRiotDroppable drop = null;
        #endregion
        #region Instance Properties
        private string _NullText = "";
        /// <summary>
        /// Displayed text when no entity is being represented
        /// </summary>
        public string NullText
        {
            get { return this._NullText; }
            set
            {
                this._NullText = value ?? "";
                this.UpdateData();
            }
        }
        private DataTypes _Type = DataTypes.Champion;
        /// <summary>
        /// Type of entity being represented by this control
        /// </summary>
        public DataTypes Type
        {
            get { return this._Type; }
            set
            {
                this._Type = value;
                this.UpdateContextMenu();
            }
        }

        #endregion
        #region Instance Methods
        public DropSlot()
        {
            this.InitializeComponent();
            this.UpdateContextMenu();
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblMain = new System.Windows.Forms.Label();
            this.cmenItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuitmRemoveItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuitmItemLevelObtained = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuitmLevelObtainedValue = new System.Windows.Forms.ToolStripTextBox();
            this.cmenChampion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuitmRemoveChampion = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenItemBuy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuitmRemoveItemBuy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuitmPricing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuitmFullPricing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuitmUpgradePricing = new System.Windows.Forms.ToolStripMenuItem();
            this.btnComponents = new System.Windows.Forms.Button();
            this.picMain = new System.Windows.Forms.PictureBox();
            this.cmenItem.SuspendLayout();
            this.cmenChampion.SuspendLayout();
            this.cmenItemBuy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMain
            // 
            this.lblMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMain.Location = new System.Drawing.Point(0, 79);
            this.lblMain.Margin = new System.Windows.Forms.Padding(0);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(80, 30);
            this.lblMain.TabIndex = 1;
            this.lblMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmenItem
            // 
            this.cmenItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuitmRemoveItem,
            this.mnuitmItemLevelObtained});
            this.cmenItem.Name = "cmenItem";
            this.cmenItem.Size = new System.Drawing.Size(154, 48);
            // 
            // mnuitmRemoveItem
            // 
            this.mnuitmRemoveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuitmRemoveItem.Name = "mnuitmRemoveItem";
            this.mnuitmRemoveItem.Size = new System.Drawing.Size(153, 22);
            this.mnuitmRemoveItem.Text = "Remove";
            this.mnuitmRemoveItem.Click += new System.EventHandler(this.mnuitmRemove_Click);
            // 
            // mnuitmItemLevelObtained
            // 
            this.mnuitmItemLevelObtained.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuitmItemLevelObtained.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuitmLevelObtainedValue});
            this.mnuitmItemLevelObtained.Name = "mnuitmItemLevelObtained";
            this.mnuitmItemLevelObtained.Size = new System.Drawing.Size(153, 22);
            this.mnuitmItemLevelObtained.Text = "Level Obtained";
            this.mnuitmItemLevelObtained.DropDownOpened += new System.EventHandler(this.mnuitmItemLevelObtained_DropDownOpened);
            // 
            // mnuitmLevelObtainedValue
            // 
            this.mnuitmLevelObtainedValue.Name = "mnuitmLevelObtainedValue";
            this.mnuitmLevelObtainedValue.Size = new System.Drawing.Size(100, 23);
            this.mnuitmLevelObtainedValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mnuitmLevelObtainedValue_KeyDown);
            this.mnuitmLevelObtainedValue.TextChanged += new System.EventHandler(this.mnuitmLevelObtainedValue_TextChanged);
            // 
            // cmenChampion
            // 
            this.cmenChampion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuitmRemoveChampion});
            this.cmenChampion.Name = "cmenChampion";
            this.cmenChampion.Size = new System.Drawing.Size(118, 26);
            // 
            // mnuitmRemoveChampion
            // 
            this.mnuitmRemoveChampion.Name = "mnuitmRemoveChampion";
            this.mnuitmRemoveChampion.Size = new System.Drawing.Size(117, 22);
            this.mnuitmRemoveChampion.Text = "Remove";
            this.mnuitmRemoveChampion.Click += new System.EventHandler(this.mnuitmRemove_Click);
            // 
            // cmenItemBuy
            // 
            this.cmenItemBuy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuitmRemoveItemBuy,
            this.mnuitmPricing});
            this.cmenItemBuy.Name = "cmenItem";
            this.cmenItemBuy.Size = new System.Drawing.Size(118, 48);
            // 
            // mnuitmRemoveItemBuy
            // 
            this.mnuitmRemoveItemBuy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuitmRemoveItemBuy.Name = "mnuitmRemoveItemBuy";
            this.mnuitmRemoveItemBuy.Size = new System.Drawing.Size(117, 22);
            this.mnuitmRemoveItemBuy.Text = "Remove";
            this.mnuitmRemoveItemBuy.Click += new System.EventHandler(this.mnuitmRemove_Click);
            // 
            // mnuitmPricing
            // 
            this.mnuitmPricing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuitmPricing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuitmFullPricing,
            this.mnuitmUpgradePricing});
            this.mnuitmPricing.Name = "mnuitmPricing";
            this.mnuitmPricing.Size = new System.Drawing.Size(117, 22);
            this.mnuitmPricing.Text = "Pricing";
            this.mnuitmPricing.DropDownOpening += new System.EventHandler(this.mnuitmPricing_DropDownOpening);
            // 
            // mnuitmFullPricing
            // 
            this.mnuitmFullPricing.CheckOnClick = true;
            this.mnuitmFullPricing.Name = "mnuitmFullPricing";
            this.mnuitmFullPricing.Size = new System.Drawing.Size(119, 22);
            this.mnuitmFullPricing.Text = "Full";
            this.mnuitmFullPricing.CheckedChanged += new System.EventHandler(this.mnuitmFullPricing_CheckedChanged);
            // 
            // mnuitmUpgradePricing
            // 
            this.mnuitmUpgradePricing.CheckOnClick = true;
            this.mnuitmUpgradePricing.Name = "mnuitmUpgradePricing";
            this.mnuitmUpgradePricing.Size = new System.Drawing.Size(119, 22);
            this.mnuitmUpgradePricing.Text = "Upgrade";
            this.mnuitmUpgradePricing.CheckedChanged += new System.EventHandler(this.mnuitmUpgradePricing_CheckedChanged);
            // 
            // btnComponents
            // 
            this.btnComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComponents.BackColor = System.Drawing.Color.Transparent;
            this.btnComponents.BackgroundImage = global::RiotPls.Properties.Resources.DropDownIcon;
            this.btnComponents.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnComponents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComponents.ForeColor = System.Drawing.Color.White;
            this.btnComponents.Location = new System.Drawing.Point(53, 54);
            this.btnComponents.Margin = new System.Windows.Forms.Padding(0);
            this.btnComponents.Name = "btnComponents";
            this.btnComponents.Size = new System.Drawing.Size(17, 16);
            this.btnComponents.TabIndex = 3;
            this.btnComponents.UseVisualStyleBackColor = false;
            this.btnComponents.MouseHover += new System.EventHandler(this.btnComponents_MouseHover);
            // 
            // picMain
            // 
            this.picMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picMain.Location = new System.Drawing.Point(10, 10);
            this.picMain.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(60, 60);
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            this.picMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseDown);
            this.picMain.MouseHover += new System.EventHandler(this.picMain_MouseHover);
            // 
            // DropSlot
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnComponents);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.picMain);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(80, 110);
            this.Name = "DropSlot";
            this.Size = new System.Drawing.Size(80, 110);
            this.LocationChanged += new System.EventHandler(this.DropSlot_LocationChanged);
            this.cmenItem.ResumeLayout(false);
            this.cmenChampion.ResumeLayout(false);
            this.cmenItemBuy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.ResumeLayout(false);

        }
        /// <summary>
        /// Removes the currently represented entity
        /// </summary>
        public void Clear()
        {
            this.drop = null;
            this.UpdateData();
            this.FireDropOccurredEvent(this.drop);
        }

        private void FireDropOccurredEvent(IRiotDroppable drop)
        {
            if (this.DropOccurred != null)
                this.DropOccurred(this, drop);
            return;
        }
        /// <summary>
        /// Sets the currently represented entity
        /// </summary>
        /// <param name="new_drop">Entity to represent</param>
        public void Set(IRiotDroppable new_drop)
        {
            if (this.drop != new_drop)
            {
                this.drop = new_drop;
                this.UpdateData();
                this.FireDropOccurredEvent(this.drop);
            }
            return;
        }

        private void ShowComponents()
        {
            // do not show over top of context menu
            if (this.ContextMenuStrip?.Visible ?? false)
                return;
            // only show item components if content is an item
            if (this.Type == DataTypes.ItemBuy || this.Type == DataTypes.Item)
            {
                ItemInfo item = this.drop as ItemInfo;
                if (item != null && item.ComponentIDs.Length > 0)
                {
                    // drop slot area
                    Rectangle rect = this.ClientRectangle;
                    rect.Location = this.PointToScreen(rect.Location);
                    // show form
                    this.componentsForm = new formItemComponents(item, rect);
                    this.componentsForm.VisibleChanged += this.formItemComponents_VisibleChanged;
                    this.componentsForm.FormClosing += this.formItemComponents_FormClosing;
                    this.componentsForm.Show(this);
                    this.componentsForm.Location = this.Parent.PointToScreen(new Point(this.Left - this.componentsForm.Width/2 + this.Width/2, this.Bottom-1));
                }
            }
            return;
        }


        private void UpdateContextMenu()
        {
            switch (this.Type)
            {
                case DataTypes.Champion:
                    this.ContextMenuStrip = this.cmenChampion;
                    break;
                case DataTypes.Item:
                    this.ContextMenuStrip = this.cmenItem;
                    break;
                case DataTypes.ItemBuy:
                    this.ContextMenuStrip = this.cmenItemBuy;
                    break;
            }
            this.UpdateHoverButtonVisibility();
            return;
        }
        private void UpdateData()
        {
            this.picMain.BackgroundImage = this.drop?.Image;
            this.lblMain.Text = this.drop?.Name ?? this.NullText;
            this.mnuitmLevelObtainedValue.Text = (this.drop?.LevelObtained ?? 1).ToString();
            this.UpdateHoverButtonVisibility();
            return;
        }

        private void UpdateHoverButtonVisibility()
        {
            ItemInfo item = this.drop as ItemInfo;
            if ((this.Type == DataTypes.Item || this.Type == DataTypes.ItemBuy) && item != null)
            {
                this.btnComponents.Visible = item.ComponentIDs.Length > 0;
            }
            else
            {
                this.btnComponents.Visible = false;
            }
            return;
        }

        private void UpdateOwner()
        {
            formItemComponents owner = this.Parent as formItemComponents;
            if (owner != null)
                owner.ChildrenVisible = this.componentsForm != null && this.componentsForm.Visible;
            return;
        }
        private void UpdatePrice()
        {
            ItemInfo item = this.drop as ItemInfo;
            if (item != null)
            {
                item.PricingStyle = this.mnuitmFullPricing.Checked
                    ? ItemInfo.PricingStyles.Full
                    : ItemInfo.PricingStyles.Upgrade;
            }
        }
        #region Event Methods   
        #region Button Events
        private void btnComponents_MouseHover(object sender, EventArgs e)
        {
            this.ShowComponents();
            return;
        }
        #region DropSlot Events     
        private void DropSlot_LocationChanged(object sender, EventArgs e)
        {
            if (this.componentsForm != null)
            {
                Rectangle rect = this.ClientRectangle;
                rect.Location = this.PointToScreen(rect.Location);
                this.componentsForm.ParentRectangle = rect;
            }
            return;
        }
        #endregion
        #region Form Events 
        private void formItemComponents_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BeginInvoke((MethodInvoker) delegate
            {
                this.Invalidate();
                this.UpdateOwner();
            });
            return;
        }
        private void formItemComponents_VisibleChanged(object sender, EventArgs e)
        {
            this.Invalidate();
            this.UpdateOwner();
            return;
        }
        #endregion
        #endregion
        #region Menu Events       
        private void mnuitmFullPricing_CheckedChanged(object sender, EventArgs e)
        {
            this.mnuitmUpgradePricing.Checked = !this.mnuitmFullPricing.Checked;
            this.UpdatePrice();
            return;
        }
        private void mnuitmItemLevelObtained_DropDownOpened(object sender, EventArgs e)
        {
            this.mnuitmLevelObtainedValue.Focus();
            this.mnuitmLevelObtainedValue.SelectionStart = 0;
            this.mnuitmLevelObtainedValue.SelectionLength = this.mnuitmLevelObtainedValue.TextLength;
        }
        private void mnuitmLevelObtainedValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmenItem.Close();
                e.Handled = true;
            }
            return;
        }
        private void mnuitmLevelObtainedValue_TextChanged(object sender, EventArgs e)
        {
            if (this.drop == null)
                return;
            int level = 1;
            if (int.TryParse(this.mnuitmLevelObtainedValue.Text, out level))
            {
                this.drop.LevelObtained = level;
                if (this.LevelObtainedChanged != null)
                    this.LevelObtainedChanged(this, this.drop, this.drop.LevelObtained);
            }
            return;
        }
        private void mnuitmPricing_DropDownOpening(object sender, EventArgs e)
        {
            ItemInfo item = this.drop as ItemInfo;
            if (item == null)
                return;
            this.mnuitmFullPricing.Checked = item.PricingStyle == ItemInfo.PricingStyles.Full;
            this.mnuitmUpgradePricing.Checked = item.PricingStyle == ItemInfo.PricingStyles.Upgrade;
            return;
        }
        private void mnuitmRemove_Click(object sender, EventArgs e)
        {
            if (this.DropOccurred != null)
                this.DropOccurred(this, null);
        }
        private void mnuitmUpgradePricing_CheckedChanged(object sender, EventArgs e)
        {
            this.mnuitmFullPricing.Checked = !this.mnuitmUpgradePricing.Checked;
            this.UpdatePrice();
            return;
        }
        #endregion
        #region PictureBox Events
        private void picMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || this.drop == null)
                return;
            this.BeginInvoke((MethodInvoker)delegate { this.DoDragDrop(this.drop.Clone() as IRiotDroppable, DragDropEffects.Copy); });
            return;
        }
        private void picMain_MouseHover(object sender, EventArgs e)
        {
            this.ShowComponents();
            return;
        }
        #endregion
        #endregion
        #endregion
        #region Override Methods
        protected override void OnDragDrop(DragEventArgs e)
        {                                                                                 
            base.OnDragDrop(e);
            switch (this.Type)
            {
                default:
                case DataTypes.Champion:
                    this.drop = (IRiotDroppable)e.Data.GetData(typeof(ChampionInfo));
                    break;
                case DataTypes.ItemBuy:
                case DataTypes.Item:
                    this.drop = (IRiotDroppable)e.Data.GetData(typeof(ItemInfo));
                    break;
            }
            this.UpdateData();
            this.FireDropOccurredEvent(this.drop);
            return;
        }

        protected override void OnDragEnter(DragEventArgs e)               
        {
            base.OnDragEnter(e);
            switch (this.Type)
            {
                default:
                case DataTypes.Champion:
                    if (e.Data.GetDataPresent(typeof(ChampionInfo)))
                        e.Effect = DragDropEffects.Copy;
                    break;
                case DataTypes.ItemBuy:
                case DataTypes.Item:
                    if (e.Data.GetDataPresent(typeof(ItemInfo)))
                        e.Effect = DragDropEffects.Copy;
                    break;
            }
            return;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.componentsForm != null && this.componentsForm.Visible)
            {
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawLine(Pens.Blue, new Point(this.picMain.Left, this.picMain.Bottom), new Point(0, this.Height));
                e.Graphics.DrawLine(Pens.Blue, new Point(this.picMain.Right, this.picMain.Bottom), new Point(this.Width, this.Height));
            }
            return;
        }
        #endregion

    }
}
