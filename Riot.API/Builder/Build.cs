﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RiotPls.API.Serialization.Attributes;
using RiotPls.API.Serialization.Champions;
using RiotPls.API.Serialization.Items;
using RiotPls.Tools;

namespace RiotPls.API.Builder
{
    /// <summary>
    /// Serializable collection of entities which describe an in-game build
    /// </summary>
    /// <remarks>Instantiation is accomplished via the <see cref="BuildManager"/> class</remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class Build
    {
        #region Types
        public delegate void BuildDelegate(Build build);
        #endregion
        #region Instance Members
        private CombinedStatsInfo stats = new CombinedStatsInfo();
        /// <summary>
        /// Notifies when build details change (i.e. champion or item changes)
        /// </summary>
        public event StringDelegate BuildChanged;
        /// <summary>
        /// Notifies when the SelectRow property value changes
        /// </summary>
        public event IntegerDelegate SelectedRowChanged;
        #endregion
        #region Instance Properties   
        /// <summary>
        /// Buys associated with this build
        /// </summary>               
        [JsonProperty("Buys", ItemIsReference = true, ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        public BuySetCollection Buys { get; private set; } = new BuySetCollection();
        /// <summary>
        /// Champion associated with this build
        /// </summary>              
        [JsonProperty("Champion", ItemIsReference = true, ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        public ChampionInfo Champion { get; private set; }
        /// <summary>
        /// Total number of valid items in the build
        /// </summary>
        public int ItemCount => this.Items.Count(i => i != null);
        /// <summary>
        /// Six items associated with this build
        /// </summary>            
        [JsonProperty("Items", ItemIsReference = true, ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        private ItemInfo[] Items { get; set; } = new ItemInfo[6];
        /// <summary>
        /// Build name
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; } = null;
        private int _SelectedRow = 0; 
        /// <summary>
        /// Currently selected row in the stats table
        /// </summary>
        public int SelectedRow
        {
            get { return this._SelectedRow; }
            set
            {
                if (this.SelectedRow != value)
                {
                    this._SelectedRow = value;
                    if (this.SelectedRowChanged != null)
                        this.SelectedRowChanged(value);
                }
            }
        }
        /// <summary>
        /// Table of combined champion & item stats from champion levels 1 to 18
        /// </summary>
        public StatsTable Table => this.stats.Table;
        #endregion
        #region Instance Methods
        [JsonConstructor]
        private Build()
        {
            
        }
        internal Build(string name)
        {
            this.Name = name;
            return;
        }

        private void FireUpdate(string name)
        {
            this.stats = new CombinedStatsInfo();
            if(this.Champion != null)
                this.stats += this.Champion.Stats;
            foreach (ItemInfo item in this.Items.Where(i => i != null))
            {
                this.stats += item.Stats;
            }
            if (this.BuildChanged != null)
                this.BuildChanged(name);
            return;
        }
        /// <summary>
        /// Find and retrieve the item indices of a given item name
        /// </summary>
        /// <param name="name">Name of the item to search for</param>
        /// <returns>List of indices of item in the build</returns>
        public List<int> GetItemIndices(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Item name must be valid", "name");
            List<int> list = new List<int>();
            for (int i = 0; i < this.Items.Length; i++)
            {
                if (this.Items[i]?.Name == name)
                    list.Add(i);
            }
            return list;
        }
        /// <summary>
        /// Retrieve the item at a specified index
        /// </summary>
        /// <param name="index">Item index to check</param>
        /// <returns>Item at the specified index, if any</returns>
        public ItemInfo GetItem(int index)
        {
            return index >= 0 && index < this.Items.Length ? this.Items[index] : null;
        }

        /// <summary>
        /// Assign a champion to this build
        /// </summary>
        /// <param name="champion_in">Champion to assign to this build</param>
        public void SetChampion(ChampionInfo champion_in)
        {
            if (this.Champion != champion_in)
            {
                this.Champion = champion_in;
                this.FireUpdate(this.Name);
            }
            return;
        }
        /// <summary>
        /// Assign an item to this build
        /// </summary>
        /// <param name="index">Index at which the item will be assigned (0-5)</param>
        /// <param name="item">Item to assign at the specified index</param>
        public void SetItem(int index, ItemInfo item)
        {
            if (index < 0 || index >= 6)
                throw new ArgumentOutOfRangeException("Item index must be 0 or greater and less than six", "index");
            this.Items[index] = item;
            this.FireUpdate(this.Name);
            return;
        }
        /// <summary>
        /// Assign a level to the item at the specified index
        /// </summary>
        /// <remarks>The item's stats will only be applied to stat rows at or above the specified level</remarks>
        /// <param name="index">Index of the item to be affected</param>
        /// <param name="level">Desired level (1-18)</param>
        public void SetItemLevel(int index, int level)
        {
            if (index < 0 || index >= 6)
                throw new ArgumentOutOfRangeException("Item index must be 0 or greater and less than six", "index");
            if (this.Items[index] == null)
                return;
            this.Items[index].LevelObtained = level;
            this.FireUpdate(this.Name);
            return;
        }
        #endregion
    }
}
