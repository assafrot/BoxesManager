using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataStructers
{
    public delegate void Invoke<T>(T item);

    public class BST<T> where T : IComparable<T>
    {
        Node root = null;
        public void Add(T value)
        {
            Node temp = root;
            if (temp == null)
            {
                root = new Node(value);
                return;
            }
            Node parent = root;
            while (temp != null)
            {
                parent = temp;
                if (value.CompareTo(temp.data) < 0)
                    temp = temp.left;
                else
                    temp = temp.right;
            }

            Node child = new Node(value);
            if (value.CompareTo(parent.data) < 0) parent.left = child;
            else parent.right = child;
            child.up = parent;
        }

        public bool Remove(T value)
        {
            Node tmp = root;
            Node parent = root;
            while (tmp != null && tmp.data.CompareTo(value) != 0)
            {
                parent = tmp;
                if (value.CompareTo(tmp.data) < 0)
                    tmp = tmp.left;
                else
                    tmp = tmp.right;
            }
            //didn't found data
            if (tmp == null)
                return false;
            //leaf or node with one children (2 cases)
            if (Remove(value, parent, tmp)) return true;
            //node with two children
            return RemoveWithTwo(value, tmp);
        }

        private bool Remove(T value, Node parent, Node tmp)
        {
            if (tmp.left == null)
            {
                if (tmp.Equals(root))
                {
                    root = tmp.right;
                    if (root != null) root.up = null;
                }
                else if (parent.left != null && parent.left.data.CompareTo(value) == 0)
                {
                    parent.left = tmp.right;
                    if (parent.left != null) parent.left.up = parent;
                }
                else
                {
                    parent.right = tmp.right;
                    if (parent.right != null) parent.right.up = parent;
                }
                return true;
            }
            else if (tmp.right == null)
            {
                if (tmp == root)
                {
                    root = tmp.left;
                    if (root != null) root.up = null;
                }
                else if (parent.left != null && parent.left.data.CompareTo(value) == 0)
                {
                    parent.left = tmp.left;
                    if (parent.left != null) parent.left.up = parent;
                }
                else
                {
                    parent.right = tmp.left;
                    if (parent.right != null) parent.right.up = parent;
                }
                return true;
            }
            return false;
        }

        private bool RemoveWithTwo(T value, Node tmp)
        {
            Node founded = tmp;
            Node parent = tmp;
            tmp = tmp.right;
            while (tmp.left != null)
            {
                parent = tmp;
                tmp = tmp.left;
            }
            founded.data = tmp.data;
            if (parent.left != null && parent.left.data.CompareTo(value) == 0)
            {
                parent.left = tmp.right;
                if (parent.left != null) parent.left.up = parent;
            }
            else
            {
                parent.right = tmp.right;
                if (parent.right != null) parent.right.up = parent;
            }
            return true;
        }

        private T MinVal(Node node)
        {
            while (node.left != null) node = node.left;
            return node.data;
        }

        public bool Search(T value, out T founded)
        {
            founded = default(T);
            Node tmp = root;
            while (tmp != null && tmp.data.CompareTo(value) != 0)
            {
                if (value.CompareTo(tmp.data) < 0)
                    tmp = tmp.left;
                else
                    tmp = tmp.right;
            }
            if (tmp == null) return false;
            else
            {
                founded = tmp.data;
                return true;
            }
        }

        public bool IsEmpty()
        {
            if (root == null) return true;
            else return false;
        }



        public void InOrder(Invoke<T> callback)
        {
            InOrder(root, callback);
        }

        private void InOrder(Node tmp, Invoke<T> callback) //InOrder
        {
            if (tmp == null) return;
            InOrder(tmp.left, callback);
            callback(tmp.data);
            InOrder(tmp.right, callback);
        }

        public int GetDepth()
        {
            return GetDepth(root);
        }

        private int GetDepth(Node tmp)
        {
            if (tmp == null)
                return 0;
            else
            {
                return Math.Max(GetDepth(tmp.left), GetDepth(tmp.right)) + 1;
            }
        }

        internal class Node
        {
            public T data;
            public Node left;
            public Node right;
            public Node up;

            public Node(T data)
            {
                this.data = data;
            }
        }
    }

}

