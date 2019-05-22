using System;
using System.Collections;
using System.Collections.Generic;

namespace BoxesManger.DataStructers.Generics
{
    delegate void Invoke<T>(T item);

    class MyLinkedList<T> : IEnumerable<T>
    {
        public Node Start;
        public Node End { get; private set; }

        public void AddLast(T newData)
        {
            Node tmp = new Node(newData);
            if (Start == null)
            {
                Start = End = tmp;
                return;
            }
            End.next = tmp;
            tmp.prev = End;
            End = tmp;
        }

        internal void ModifyNode(Node node, Invoke<Node> modify)
        {
            if (End != Start)
            {
                RemoveNode(node, out Node tmp);
                modify(tmp);
                End.next = tmp;
                tmp.prev = End;
                End = tmp;
            }
            else
                modify(End);
        }

        internal bool RemoveFirst(out Node saveDeleted)
        {
            if (Start != null && Start.next == null)
            {
                saveDeleted = Start;
                Start = null;
                End = null;
                return true;
            }

            if (Start != null)
            {
                saveDeleted = Start;
                Start = Start.next;
                Start.prev = null;
                return true;
            }
            saveDeleted = null;
            return false;
        }

        private bool RemoveLast(out Node saveDeleted)
        {
            if (End != null && End.prev == null)
            {
                saveDeleted = End;
                Start = End = null;
                return true;
            }
            if (End != null)
            {
                saveDeleted = End;
                End = End.prev;
                End.next = null;
                return true;
            }
            saveDeleted = null;
            return false;
        }

        internal bool RemoveNode(Node node, out Node saveDeleted)
        {
            saveDeleted = null;
            if (node == null)
                return false;
            if (node == Start) return RemoveFirst(out saveDeleted);
            if (node == End) return RemoveLast(out saveDeleted);
            if (node.next != null && node.prev != null)
            {
                saveDeleted = node;
                node.next.prev = node.prev;
                node.prev.next = node.next;
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node tmp = Start;
            while (tmp.next != null)
            {
                yield return tmp.data;
                tmp = tmp.next;
            }
            yield return tmp.data;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        internal class Node
        {
            public T data;
            public Node next;
            public Node prev;

            public Node(T data)
            {
                this.data = data;
                next = null;
            }
        }
    }
}
