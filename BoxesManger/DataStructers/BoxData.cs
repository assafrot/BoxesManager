using System;

namespace BoxesManger.DataStructers
{
    internal class BoxData
    {
        public DateTime LastUpdate { get; set; }
        public BoxBase BaseData { get; set; }
        public BoxHeight HeightData { get; set; }

        internal BoxData(BoxBase boxBase)
        {
            LastUpdate = DateTime.Now;
            BaseData = boxBase;
        }
    }
}
