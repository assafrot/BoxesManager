using BoxesManger.DataStructers.Generics;
using System;

namespace BoxesManger.DataStructers
{
    internal class BoxBase : IComparable<BoxBase>
    {
        public double BaseSize { get; set; }
        public BST<BoxHeight> HeightTree { get; set; }

        public BoxBase(double x, bool isDummy = true)
        {
            BaseSize = x;
            if (!isDummy) HeightTree = new BST<BoxHeight>();
        }

        public int CompareTo(BoxBase other)
        {
            return BaseSize.CompareTo(other.BaseSize);
        }

    }
}
