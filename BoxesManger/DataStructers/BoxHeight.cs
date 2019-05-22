using BoxesManger.DataStructers.Generics;
using System;

namespace BoxesManger.DataStructers
{
    internal class BoxHeight : IComparable<BoxHeight>
    {
        public double Height { get; set; }
        internal MyLinkedList<BoxData>.Node DataNode { get; set; }
        public int Count { get; set; }

        internal BoxHeight(double y, bool isDummy = true, int count = 0 , MyLinkedList<BoxData>.Node node=null)
        {
            Height = y;
            if (!isDummy)
            {
                Count = count;
                DataNode = node;
            };
        }
        public int CompareTo(BoxHeight other)
        {
            return Height.CompareTo(other.Height);
        }
    }
}