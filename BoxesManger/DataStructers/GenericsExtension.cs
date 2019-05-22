using BoxesManger.DataStructers.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesManger.DataStructers
{
    internal static class GenericsExtension
    {
        internal static bool SearchBiggerOrEqueleThan<T>(this BST<T> bst, T value, out T founded) where T : IComparable<T>
        {
            founded = default(T);
            if (bst.Search(value, out founded))
                return true;
            if (bst.SearchBiggerThan(value, out founded))
                return true;
            return false;
        }

        internal static bool SearchBiggerThan<T>(this BST<T> bst, T value, out T founded) where T : IComparable<T>
        {
            founded = default(T);
            var tmp = bst.Root;
            while (tmp != null)
            {
                if (tmp.data.CompareTo(value) > 0)
                {
                    founded = tmp.data;
                    tmp = tmp.left;
                }
                else
                {
                    tmp = tmp.right;
                }
            }
            if (founded != null) return true;
            else return false;
        }
    }
}
