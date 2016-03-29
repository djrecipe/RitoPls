﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RiotPls.API.Builder;
using RiotPls.API.Serialization.Champions;
using RiotPls.API.Serialization.Items;
using RiotPls.Controls;

namespace RiotPls.Forms
{
    public class formBuilder : formTemplate
    {
        #region Controls
        private System.ComponentModel.IContainer components = null;
        private DropSlot[] itemDrops = null;
        #endregion
        private Build build = null;
        private DropSlot dropChampion;
        private DropSlot dropItem1;
        private DropSlot dropItem2;
        private DropSlot dropItem3;
        private DropSlot dropItem4;
        private DropSlot dropItem5;
        private DropSlot dropItem6;
        public formBuilder()
        {
            this.InitializeComponent();
            this.InitializeDragDrop();
            Build.BuildChanged += this.BuildManager_BuildChanged;
            return;
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formBuilder));
            this.dropChampion = new RiotPls.Controls.DropSlot();
            this.dropItem1 = new RiotPls.Controls.DropSlot();
            this.dropItem2 = new RiotPls.Controls.DropSlot();
            this.dropItem3 = new RiotPls.Controls.DropSlot();
            this.dropItem4 = new RiotPls.Controls.DropSlot();
            this.dropItem5 = new RiotPls.Controls.DropSlot();
            this.dropItem6 = new RiotPls.Controls.DropSlot();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Location = new System.Drawing.Point(811, 9);
            // 
            // btnSettings
            // 
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.Visible = false;
            // 
            // picLoading
            // 
            this.picLoading.Location = new System.Drawing.Point(356, 186);
            // 
            // dropChampion
            // 
            this.dropChampion.AllowDrop = true;
            this.dropChampion.BackColor = System.Drawing.Color.Transparent;
            this.dropChampion.DataType = RiotPls.API.Serialization.Interfaces.DataType.Champion;
            this.dropChampion.Location = new System.Drawing.Point(29, 29);
            this.dropChampion.Margin = new System.Windows.Forms.Padding(20);
            this.dropChampion.MinimumSize = new System.Drawing.Size(80, 110);
            this.dropChampion.Name = "dropChampion";
            this.dropChampion.NullText = "Champion";
            this.dropChampion.Size = new System.Drawing.Size(124, 151);
            this.dropChampion.TabIndex = 22;
            this.dropChampion.DropOccurred += new RiotPls.Controls.DropSlot.delDropOccurred(this.dropChampion_DropOccurred);
            // 
            // dropItem1
            // 
            this.dropItem1.AllowDrop = true;
            this.dropItem1.BackColor = System.Drawing.Color.Transparent;
            this.dropItem1.DataType = RiotPls.API.Serialization.Interfaces.DataType.Item;
            this.dropItem1.Location = new System.Drawing.Point(183, 59);
            this.dropItem1.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.dropItem1.MinimumSize = new System.Drawing.Size(80, 110);
            this.dropItem1.Name = "dropItem1";
            this.dropItem1.NullText = "Item 1";
            this.dropItem1.Size = new System.Drawing.Size(96, 121);
            this.dropItem1.TabIndex = 23;
            this.dropItem1.DropOccurred += new RiotPls.Controls.DropSlot.delDropOccurred(this.dropItem_DropOccurred);
            // 
            // dropItem2
            // 
            this.dropItem2.AllowDrop = true;
            this.dropItem2.BackColor = System.Drawing.Color.Transparent;
            this.dropItem2.DataType = RiotPls.API.Serialization.Interfaces.DataType.Item;
            this.dropItem2.Location = new System.Drawing.Point(289, 59);
            this.dropItem2.Margin = new System.Windows.Forms.Padding(5);
            this.dropItem2.MinimumSize = new System.Drawing.Size(80, 110);
            this.dropItem2.Name = "dropItem2";
            this.dropItem2.NullText = "Item 2";
            this.dropItem2.Size = new System.Drawing.Size(96, 121);
            this.dropItem2.TabIndex = 24;
            this.dropItem2.DropOccurred += new RiotPls.Controls.DropSlot.delDropOccurred(this.dropItem_DropOccurred);
            // 
            // dropItem3
            // 
            this.dropItem3.AllowDrop = true;
            this.dropItem3.BackColor = System.Drawing.Color.Transparent;
            this.dropItem3.DataType = RiotPls.API.Serialization.Interfaces.DataType.Item;
            this.dropItem3.Location = new System.Drawing.Point(395, 59);
            this.dropItem3.Margin = new System.Windows.Forms.Padding(5);
            this.dropItem3.MinimumSize = new System.Drawing.Size(80, 110);
            this.dropItem3.Name = "dropItem3";
            this.dropItem3.NullText = "Item 3";
            this.dropItem3.Size = new System.Drawing.Size(96, 121);
            this.dropItem3.TabIndex = 25;
            this.dropItem3.DropOccurred += new RiotPls.Controls.DropSlot.delDropOccurred(this.dropItem_DropOccurred);
            // 
            // dropItem4
            // 
            this.dropItem4.AllowDrop = true;
            this.dropItem4.BackColor = System.Drawing.Color.Transparent;
            this.dropItem4.DataType = RiotPls.API.Serialization.Interfaces.DataType.Item;
            this.dropItem4.Location = new System.Drawing.Point(501, 59);
            this.dropItem4.Margin = new System.Windows.Forms.Padding(5);
            this.dropItem4.MinimumSize = new System.Drawing.Size(80, 110);
            this.dropItem4.Name = "dropItem4";
            this.dropItem4.NullText = "Item 4";
            this.dropItem4.Size = new System.Drawing.Size(96, 121);
            this.dropItem4.TabIndex = 26;
            this.dropItem4.DropOccurred += new RiotPls.Controls.DropSlot.delDropOccurred(this.dropItem_DropOccurred);
            // 
            // dropItem5
            // 
            this.dropItem5.AllowDrop = true;
            this.dropItem5.BackColor = System.Drawing.Color.Transparent;
            this.dropItem5.DataType = RiotPls.API.Serialization.Interfaces.DataType.Item;
            this.dropItem5.Location = new System.Drawing.Point(607, 59);
            this.dropItem5.Margin = new System.Windows.Forms.Padding(5);
            this.dropItem5.MinimumSize = new System.Drawing.Size(80, 110);
            this.dropItem5.Name = "dropItem5";
            this.dropItem5.NullText = "Item 5";
            this.dropItem5.Size = new System.Drawing.Size(96, 121);
            this.dropItem5.TabIndex = 27;
            this.dropItem5.DropOccurred += new RiotPls.Controls.DropSlot.delDropOccurred(this.dropItem_DropOccurred);
            // 
            // dropItem6
            // 
            this.dropItem6.AllowDrop = true;
            this.dropItem6.BackColor = System.Drawing.Color.Transparent;
            this.dropItem6.DataType = RiotPls.API.Serialization.Interfaces.DataType.Item;
            this.dropItem6.Location = new System.Drawing.Point(713, 59);
            this.dropItem6.Margin = new System.Windows.Forms.Padding(5);
            this.dropItem6.MinimumSize = new System.Drawing.Size(80, 110);
            this.dropItem6.Name = "dropItem6";
            this.dropItem6.NullText = "Item 6";
            this.dropItem6.Size = new System.Drawing.Size(96, 121);
            this.dropItem6.TabIndex = 28;
            this.dropItem6.DropOccurred += new RiotPls.Controls.DropSlot.delDropOccurred(this.dropItem_DropOccurred);
            // 
            // formBuilder
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 500);
            this.Controls.Add(this.dropItem6);
            this.Controls.Add(this.dropItem5);
            this.Controls.Add(this.dropItem4);
            this.Controls.Add(this.dropItem3);
            this.Controls.Add(this.dropItem2);
            this.Controls.Add(this.dropItem1);
            this.Controls.Add(this.dropChampion);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formBuilder";
            this.Text = "formStatSheet";
            this.VisibleChanged += new System.EventHandler(this.formStatSheet_VisibleChanged);
            this.Controls.SetChildIndex(this.dropChampion, 0);
            this.Controls.SetChildIndex(this.btnSettings, 0);
            this.Controls.SetChildIndex(this.picLoading, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.dropItem1, 0);
            this.Controls.SetChildIndex(this.dropItem2, 0);
            this.Controls.SetChildIndex(this.dropItem3, 0);
            this.Controls.SetChildIndex(this.dropItem4, 0);
            this.Controls.SetChildIndex(this.dropItem5, 0);
            this.Controls.SetChildIndex(this.dropItem6, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);

        }

        private void InitializeDragDrop()
        {
            this.itemDrops = new DropSlot[] { this.dropItem1, this.dropItem2, this.dropItem3,
                this.dropItem4, this.dropItem5, this.dropItem6 };
            return;
        }
        private void UpdateBuild()
        {
            if (!this.Visible)
                return;
            this.build = Build.GetBuild(0);
            // update champion
            ChampionInfo champion = this.build.GetChampion();
            this.dropChampion.Set(champion);
            // update items
            for (int i = 0; i < 6; i++)
                this.itemDrops[i].Set(this.build.GetItem(i));
            return;
        }
        private void BuildManager_BuildChanged(int index)
        {
            this.UpdateBuild();
            return;
        }
        private void formStatSheet_VisibleChanged(object sender, System.EventArgs e)
        {
            this.UpdateBuild();
            return;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dropChampion_DropOccurred(DropSlot slot, API.Serialization.Interfaces.IRiotDroppable drop)
        {
            ChampionInfo champ = drop as ChampionInfo;
            this.build.SetChampion(champ);
        }

        private void dropItem_DropOccurred(DropSlot slot, API.Serialization.Interfaces.IRiotDroppable drop)
        {
            int index = this.itemDrops.ToList().IndexOf(slot);
            ItemInfo item = drop as ItemInfo;
            this.build.SetItem(index, item);
        }
    }
}
