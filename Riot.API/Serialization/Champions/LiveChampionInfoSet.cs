﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiotPls.API.Serialization.General;
using RiotPls.API.Serialization.Transport;

namespace RiotPls.API.Serialization.Champions
{
    internal class LiveChampionInfoSet : RiotSerializable<List<LiveChampionInfo>>
    {
        public override string LocalFileName => "LiveChampionInfo.json";
        public override string RootToken => "champions";
        public override RiotURL URL => new RiotURL(this.apiKey, APISettings.APIVersion, false, "champion?");

        public LiveChampionInfoSet(APIKey key) : base(key)
        {
        }
    }
}
