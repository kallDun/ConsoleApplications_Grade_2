using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_I
{
    class Program
    {
        static void Main(string[] args)
        {
            ITree<int> tree = new BinaryTree<int>();
            var commands = File.ReadAllLines("input.txt");
            foreach (var command in commands)
            {
                var elems = command.Split();
                switch (elems[0])
                {
                    case "ADD":
                        tree.Add(int.Parse(elems[1]));
                        break;

                    case "DELETE":
                        tree.Delete(int.Parse(elems[1]));
                        break;

                    case "PRINTTREE":
                        tree.Print();
                        break;
                }
            }
        }
    }

    interface ITree<T>
    {
        int Count { get; }
        void Add(T item);
        void Delete(T item);
        void Print();
    }

    public class Node<T>
    {
        public T item;
        public Node<T> parent;
        public Node<T> left, right;
    }

    class BinaryTree<T> : ITree<T>, IEnumerable<T> where T : IComparable<T>
    {
        Node<T> upperNode;

        int count = 0;
        public int Count { get => count; private set => count = value; }

        public void Add(T item)
        {
            Count++;
        }

        public void Delete(T item)
        {
            Count--;
        }

        public void Print()
        {

        }

        public Node<T> GetFirstElement()
        {
            return upperNode;
        }

        public IEnumerator<T> GetEnumerator() => new BinaryTreeIterator<T>(this);
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }

    class BinaryTreeIterator<T> : IEnumerator<T> where T : IComparable<T>
    {
        private BinaryTree<T> tree;
        private int curItter;
        private Node<T> curNode;
        private int last_operation = 0; // 0 is start, 1 is from right, 2 is from left, 3 is from child

        public BinaryTreeIterator(BinaryTree<T> tree)
        {
            this.tree = tree;
            curItter = 0;
            curNode = tree.GetFirstElement();
        }

        public T Current => curNode.item;

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() { }

        public bool MoveNext()
        {
            /*if (curItter++ >= tree.Count) return false;

            if (curNode.right != null)
            {
                curNode = curNode.right;
            }
            else 
            if ()*/
            return false;
        }

        public void Reset() 
        {
            curItter = 0;
            tree.GetFirstElement(); 
        }
    }

}
