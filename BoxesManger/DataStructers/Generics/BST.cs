using System;

namespace BoxesManger.DataStructers.Generics
{
    class BST<T> where T : IComparable<T>
    {
        public BST()
        {
            Root = null;
        }
        public Node Root { get; private set; }
        public Node LastUpdate { get; private set; }

        public void Add(T value)
        {
            Node temp = Root;
            if (temp == null)
            {
                Root = new Node(value);
                LastUpdate = Root;
                return;
            }
            Node parent = Root;
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
            LastUpdate = child;
        }  

        public bool Remove(T value)
        {
            Node tmp = Root;
            Node parent = Root;
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
            return RemoveWithTwo(tmp);
        }

        private bool Remove(T value, Node parent, Node tmp)
        {
            if (tmp.left == null)
            {
                if (tmp.Equals(Root))
                {
                    Root = tmp.right;
                    if (Root != null) Root.up = null;
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
                if (tmp == Root)
                {
                    Root = tmp.left;
                    if (Root != null) Root.up = null;
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

        private bool RemoveWithTwo(Node tmp)
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


            if (tmp.left == null)
            {
                parent.right = tmp.right;
                if (parent.right != null) parent.right.up = parent;
            }
            else
            {
                parent.left = tmp.right;
                if (parent.left != null) parent.left.up = parent;
            }

            return true;
        }

        public bool Search(T value, out T founded)
        {
            founded = default(T);
            Node tmp = Root;
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

        internal bool IsEmpty()
        {
            if (Root == null) return true;
            else return false;
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
