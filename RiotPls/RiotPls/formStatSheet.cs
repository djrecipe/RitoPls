﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiotPls
{
    public partial class formStatSheet : formTemplate
    {
        private ChampionInfo _Champion = null;
        public ChampionInfo Champion
        {
            get
            {
                return this._Champion;
            }
            set
            {
                this._Champion = value;
                this.picChampion.BackgroundImage = this.Champion.Image;
                return;
            }
        }
        public formStatSheet()
        {
            this.InitializeComponent();
            this.picChampion.AllowDrop = true;
            this.picChampion.DragEnter += this.picChampion_DragEnter;
            this.picChampion.DragDrop += this.picChampion_DragDrop;
        }
        private void formStatSheet_DragDrop(object sender, DragEventArgs e)
        {
            ChampionInfo target_info = null;
            try
            {
                target_info = e.Data.GetData(typeof(ChampionInfo)) as ChampionInfo;
            }
            catch
            {
                target_info = null;
            }
            this.Champion = target_info;
            return;
        }
        private void picChampion_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Copy;
            return;
        }
        private void picChampion_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                this.picChampion.BackgroundImage = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            }
            catch
            {
            }
            return;
        }
    }
}
