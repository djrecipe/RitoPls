﻿using System;
using System.Drawing;
using System.Windows.Forms;
using RiotPls.Tools;
using RiotPls.UI.Interfaces;
using RiotPls.UI.Models;

namespace RiotPls.UI.Views
{
    /// <summary>
    /// Provides a window template with custom styling
    /// </summary>
    public class formTemplate : Form
    {
        #region Constants
        private const int CP_NOCLOSE_BUTTON = 0x200;
        #endregion
        #region Instance Members  
        #region Controls               
        private System.ComponentModel.IContainer components = null;
        protected System.Windows.Forms.PictureBox picLoading;
        #endregion
        private Point lastMouse = Point.Empty;
        protected ITemplateViewModel model = null;
        #endregion
        #region Instance Properties  
        /// <summary>
        /// Enable the built-in Windows Forms close (x) button
        /// </summary>
        /// <remarks>If False, the close button will be visible but disabled</remarks>
        public bool CloseButtonEnabled { get; set; }
        /// <summary>
        /// Show loading gif when form is first opened 
        /// </summary>
        /// <remarks>Typically the loading gif is hidden after data binding is complete</remarks>
        public bool ShowLoading
        {
            get;
            set;
        } = false;
        #region Override Properties
        /// <summary>
        /// Results in a disabled close ('x') button
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams my_cp = base.CreateParams;
                my_cp.ClassStyle = my_cp.ClassStyle;
                if (this.CloseButtonEnabled)
                    my_cp.ClassStyle &= ~CP_NOCLOSE_BUTTON;
                else
                    my_cp.ClassStyle |= CP_NOCLOSE_BUTTON;
                return my_cp;
            }
        }
        #endregion
        #endregion
        #region Instance Methods
        #region Initialization Methods
        protected formTemplate()
        {
            this.InitializeComponent();
            return;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formTemplate));
            this.picLoading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // picLoading
            // 
            this.picLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picLoading.Image = global::RiotPls.Properties.Resources.Loading;
            this.picLoading.InitialImage = null;
            this.picLoading.Location = new System.Drawing.Point(101, 77);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(128, 128);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoading.TabIndex = 7;
            this.picLoading.TabStop = false;
            // 
            // formTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(331, 282);
            this.Controls.Add(this.picLoading);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formTemplate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formTemplate_FormClosing);
            this.Load += new System.EventHandler(this.formTemplate_Load);
            this.SizeChanged += new System.EventHandler(this.formTemplate_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.formTemplate_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.formTemplate_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formTemplate_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        #endregion
        #region Event Methods  
        #region Form Events          
        private void formTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            // save window size & location, regardless of the nature of the window closing
            GeneralSettings.SaveWindowSettings(this);
            // save window visibility, only if window closed due to MDI parent closing
            if(e.CloseReason == CloseReason.MdiFormClosing)
                GeneralSettings.SaveOpenWindow(this);
            return;
        }
        private void formTemplate_Load(object sender, EventArgs e)
        {
            if (this.model != null)
            {
                this.model.DataUpdateStarted += this.Model_DataUpdateStarted;
                this.model.DataUpdateFinished += this.Model_DataUpdateFinished;
            }
            Tools.GeneralSettings.LoadWindowSettings(this);
            return;
        }
        protected void formTemplate_MouseMove(object sender, MouseEventArgs e)
        {
            if (Control.MouseButtons.HasFlag(MouseButtons.Left))
            {
                if (this.lastMouse != Point.Empty)
                    this.Location = new Point(this.Left + (e.X - this.lastMouse.X), this.Top + (e.Y - this.lastMouse.Y));
                else
                    this.lastMouse = e.Location;
            }
            else
                this.lastMouse = Point.Empty;
            return;
        }
        private void formTemplate_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(46, 46, 46), 6.0f), new Rectangle(this.DisplayRectangle.Left, this.DisplayRectangle.Top, this.DisplayRectangle.Width, this.DisplayRectangle.Height));
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(120, 120, 120), 2.0f), new Rectangle(this.DisplayRectangle.Left, this.DisplayRectangle.Top, this.DisplayRectangle.Width, this.DisplayRectangle.Height));
        }
        private void formTemplate_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void formTemplate_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Tools.GeneralSettings.LoadWindowSettings(this);
                this.model?.UpdateData();
            }
            else
                Tools.GeneralSettings.SaveWindowSettings(this);
            return;
        }
        #endregion
        #region Model Events               
        private void Model_DataUpdateFinished(object sender, object e)
        {
            this.picLoading.BeginInvoke(new Action(() => {
                this.picLoading.Visible = false;
            }));
            return;
        }
        private void Model_DataUpdateStarted()
        {
            this.picLoading.BeginInvoke(new Action(() => {
                this.picLoading.Visible = this.ShowLoading;
                this.picLoading.BringToFront();
            }));
            return;
        }
        #endregion
        #endregion
        #region Override Methods
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

    }
}
