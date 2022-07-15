using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example08_MyBinaryTree
{
    internal class MyBinaryTree<T>
    {
        public class Node<K>
        {
            public K value;
            public Node<K> left;
            public Node<K> right;
        }
        Node<T> root, tmp, tmp2;


        // O(logN)
        public Node<T> Find(T item)
        {
            if (root != null)
            {
                tmp = root;

                while (tmp != null)
                {
                    if (Comparer<T>.Default.Compare(item, tmp.value) < 0)
                        tmp = tmp.left;
                    else if (Comparer<T>.Default.Compare(item, tmp.value) > 0)
                        tmp = tmp.right;
                    else
                        return tmp;
                }
            }
            return null;
        }

        // O(logN)
        public void Add(T item)
        {
            if (root != null)
            {
                tmp = root;
                while (tmp != null)
                {
                    if (Comparer<T>.Default.Compare(item, tmp.value) < 0)
                    {
                        if (tmp.left != null)
                            tmp = tmp.left;
                        else
                        {
                            tmp.left = new Node<T>();
                            tmp.left.value = item;
                            return;
                        }

                    }
                    else if (Comparer<T>.Default.Compare(item, tmp.value) > 0)
                    {
                        if (tmp.right != null)
                            tmp = tmp.right;
                        else
                        {
                            tmp.right = new Node<T>();
                            tmp.right.value = item;
                            return;
                        }
                    }
                    else
                        throw new Exception("해당 값의 노드가 이미 존재함");
                }
            }
            else
            {
                root = new Node<T>();
                root.value = item;
            }
        }

        // O(logN)
        public bool Delete(T item)
        {
            bool isOK = false;
            if (root != null)
            {
                tmp = root;

                while (tmp != null)
                {
                    if (Comparer<T>.Default.Compare(item, tmp.value) < 0)
                        tmp = tmp.left;
                    else if (Comparer<T>.Default.Compare(item, tmp.value) > 0)
                        tmp = tmp.right;
                    else // found
                    {
                        isOK = true;
                        break;
                    }
                }

                if (isOK)
                {
                    if (tmp.left == null && tmp.right == null)
                        tmp = null;
                    else if (tmp.left == null && tmp.right != null)
                        tmp = tmp.right;
                    else if (tmp.left != null && tmp.right == null)
                        tmp = tmp.left;
                    else
                    {
                        // 오른쪽 자식노드로부터 가장 왼쪽 노드찾기
                        tmp2 = tmp.right;
                        while (tmp2.left != null)
                        {
                            tmp2 = tmp2.left;
                        }
                        tmp2.left = tmp.left;
                        tmp2.right = tmp.right;
                        tmp = tmp2;
                    }
                }

            }
            return isOK;
        }
    }
}
