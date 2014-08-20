using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class MCollection<T> where T : IComparable
    {
        class Node
        {
            public T Data;// Data that stored in a node.
            public Node Prev;// Link to the previous node.
            public Node Next;// Link to the next node.
            /// <summary>
            /// Default constructor of the Node Class.
            /// </summary>
            public Node()
            {
                Prev = null;
                Next = null;
            }
            /// <summary>
            /// Constructor with parameters of the Node Class.
            /// </summary>
            /// <param name="_Data">Data that will be stored in a node</param>
            /// <param name="_Prev">Link to the previous node</param>
            /// <param name="_Next">Link to the next node</param>
            public Node(T _Data, Node _Prev, Node _Next)
            {
                Data = _Data;
                Prev = _Prev;
                Next = _Next;
            }
        }

        private Node Begin;// Contains the first element of the collection.
        private Node End;// Contains the last element of the collection.
        public int Count// Contains number of nodes in the collection.
        {
            get;
            private set;
        }
        public bool LastOperation
        {
            get;
            private set;
        }// Contains information about status of the last operation.

        /// <summary>
        /// Get a node from some position
        /// </summary>
        /// <param name="_i">Position of a node</param>
        /// <returns>Node that placed on _i position</returns>
        private Node GoTo(int _i)
        {
            Node Current = Begin;

            for (int j = 0; j != _i; j++)
            {
                Current = Current.Next;
            }
            return Current;
        }
        /// <summary>
        /// Add element to the end of a collection
        /// </summary>
        /// <param name="item">Element that was added</param>
        public void AddToEnd(T item)
        {
            if (Begin == null)
            {
                Begin = new Node(item, null, null);
                End = Begin;
            }
            else
            {
                Node Current = new Node(item, End, null);
                End.Next = Current;
                End = Current;
            }
            Count++;
            LastOperation = true;
        }
        /// <summary>
        /// Add element for some position.
        /// </summary>
        /// <param name="pos">Position where the element will be added</param>
        /// <param name="item">Element that will be added</param>
        /// <returns>"true" if operation is completed, "false" if operation is failed</returns>
        public bool Add(int pos, T item)
        {
            if (pos < Count)
            {
                Node newNext = GoTo(pos);
                Node NewNode = new Node(item, newNext.Prev, newNext);
                if (pos == 0)
                {
                    Begin = NewNode;
                }
                else
                {
                    newNext.Prev.Next = NewNode;
                }
                newNext.Prev = NewNode;
                Count++;
                LastOperation = true;
                return true;
            }
            else
            {
                LastOperation = false;
                return false;
            }
        }
        /// <summary>
        /// Add element to the begin of collection
        /// </summary>
        /// <param name="item">Element that was added</param>
        public void AddToBegin(T item)
        {
            if (Begin == null)
            {
                Begin = new Node(item, null, null);
                End = Begin;
            }
            else
            {
                Node current = new Node(item, null, Begin);
                Begin.Prev = current;
                Begin = current;
            }
            Count++;
            LastOperation = true;
        }

        /// <summary>
        /// Clear the MCollection
        /// </summary>
        public void Clear()
        {
            Begin = null;
            End = null;
            Count = 0;
            LastOperation = true;
        }

        /// <summary>
        /// Delete one element.
        /// </summary>
        /// <param name="position">Position of the element that was deleted</param>
        /// <returns>"true" if operation is completed, "false" if operation is failed</returns>
        public bool Delete(int position)
        {
            try
            {
                Node current = GoTo(position);
                if (position != Count - 1)
                    current.Next.Prev = current.Prev;
                else
                    End = End.Prev;

                if (position != 0)
                    current.Prev.Next = current.Next;
                else
                    Begin = Begin.Next;

                Count--;
                LastOperation = true;
                return true;
            }
            catch
            {
                LastOperation = false;
                return false;
            }
        }
        /// <summary>
        /// Delete last element.
        /// </summary>
        /// <returns>"true" if operation is completed, "false" if operation is failed</returns>
        public bool DeleteFromEnd()
        {
            try
            {
                try
                {
                    End.Prev.Next = null;
                }
                catch
                {
                    Begin = null;
                }

                End = End.Prev;
                Count--;

                LastOperation = true;
                return true;
            }
            catch
            {
                LastOperation = false;
                return false;
            }
        }
        /// <summary>
        /// Delete first element.
        /// </summary>
        /// <returns>"true" if operation is completed, "false" if operation is failed</returns>
        public bool DeleteFromBegin()
        {
            try
            {
                try
                {
                    Begin.Next.Prev = null;
                }
                catch
                {
                    End = null;
                }
                Begin = Begin.Next;
                Count--;
                LastOperation = true;
                return true;
            }
            catch
            {
                LastOperation = false;
                return false;
            }
        }
        /// <summary>
        /// Delete each element that placed on some positions.
        /// </summary>
        /// <param name="startPosition">First element that will be deleted</param>
        /// <param name="endPosition">Last element that will be deleted</param>
        /// <returns>"true" if operation is completed, "false" if operation is failed</returns>
        public bool DeleteFromTo(int startPosition, int endPosition)
        {
            try
            {
                for (int i = startPosition; i <= endPosition; i++)
                {
                    Delete(startPosition);
                }
                LastOperation = true;
                return true;
            }
            catch
            {
                LastOperation = false;
                return false;
            }
        }
        /// <summary>
        /// Iterator for "foreach".
        /// </summary>
        /// <returns>Each element in the collection</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (Node i = Begin; i != null; i = i.Next)
            {
                yield return i.Data;
            }
        }
        /// <summary>
        /// Indexator.
        /// </summary>
        /// <param name="i">Number of the element</param>
        /// <returns>Element that placed on i position</returns>
        public T this[int i]
        {
            get
            {
                if (i >= Count)// Out of range;
                    return default(T);
                else
                    return GoTo(i).Data;
            }
            set
            {
                if (i < Count)// Out of range;
                    GoTo(i).Data = value;
            }
        }

        public override bool Equals(object obj)
        {
            MCollection<T> value = (MCollection<T>)obj;
            if (this.Count != value.Count)
                return false;

            for (int i = 0; i <= Count - 1; i++)
            {
                if (!this[i].Equals(value[i]))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Checks whether the collection contains some element.
        /// </summary>
        /// <param name="obj">Element that is sought in the collections</param>
        /// <returns>"true" if the collection contains this element, else "false"</returns>
        public bool Contains(T obj)
        {
            foreach (T item in this)
            {
                if (item.Equals(obj))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Override of GetHashCode
        /// </summary>
        /// <returns>Hash of the object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Transform collection to array.
        /// </summary>
        /// <returns>Array of elements from the collection</returns>
        public T[] ToArray()
        {
            T[] result = new T[Count];
            for (int i = 0; i <= Count - 1; i++)
            {
                result[i] = this[i];
            }
            return result;
        }
        /// <summary>
        /// Concatenate two collection to one.
        /// </summary>
        /// <param name="obj">Second collection that is cancotenated</param>
        /// <returns>New collection that contains two collections</returns>
        public MCollection<T> Concat(MCollection<T> obj)
        {
            MCollection<T> result = new MCollection<T>();
            foreach (T item in this)
            {
                result.AddToEnd(item);
            }
            foreach (T item in obj)
            {
                result.AddToEnd(item);
            }
            return result;
        }
        /// <summary>
        /// Method that sort the collection.
        /// </summary>
        /// <param name="check"> 
        /// Parameter whic responsible for direct of the sort
        /// If check == true, sort Z..A else A..Z.
        /// </param>
        private void Sort(bool check)
        {
            MCollection<T> extraCollection = new MCollection<T>();
            int Size = Count;
            for (int j = 0; j < Size; j++)
            {
                int i = 0;
                T min = this[0];
                int mIndex = 0;
                foreach (T item2 in this)
                {
                    if ((min.CompareTo(item2) >= 0) ^ check)
                    {
                        min = item2;
                        mIndex = i;
                    }
                    i++;
                }
                extraCollection.AddToEnd(min);
                this.Delete(mIndex);
            }
            this.Begin = extraCollection.Begin;
            this.End = extraCollection.End;
            this.Count = extraCollection.Count;
        }
        /// <summary>
        /// Method that sort the collection from the min element to the max element[A..Z].
        /// </summary>
        public void SortMin()
        {
            Sort(false);
        }
        /// <summary>
        /// Method that sort the collection from the max element the to min element[Z..A].
        /// </summary>
        public void SortMax()
        {
            Sort(true);
        }
        /// <summary>
        /// Method that search objects in the collection.
        /// </summary>
        /// <param name="obj">Object that equal to objects in the collection</param>
        /// <returns>Array of indexes</returns>
        public int[] IndexesOf(T obj)
        { 
            MCollection<int> res = new MCollection<int>();
            int i=0;
            foreach (T item in this)
            {
                if (item.Equals(obj))
                    res.AddToEnd(i);

                i++;
            }
            return res.ToArray();
        }
    }
}
