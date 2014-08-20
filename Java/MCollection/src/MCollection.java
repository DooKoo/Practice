import java.util.Iterator;

public class MCollection<T extends Comparable> implements Iterable<T> {

    private int Count;// Contains number of nodes in the collection.
    private boolean LastOperation;// Contains information about status of the last operation.
    private Node Begin;// Contains the first element of the collection.
    private Node End;// Contains the last element of the collection.

    public int getCount() {
        return Count;
    }

    // Method which that status of last operation.
    public boolean isLastOperation() {
        return LastOperation;
    }

    // Inside method that can posible move to the _i Node.
    private Node GoTo(int _i) {
        Node CurrentNode = Begin;

        for (int j = 0; j != _i; j++) {
            CurrentNode = CurrentNode.Next;
        }
        return CurrentNode;
    }

    // Method that added element("item") to end of the collection.
    public void AddToEnd(T item) {
        if (Begin == null) {
            Begin = new Node(item, null, null);
            End = Begin;
        } else {
            Node Current = new Node(item, End, null);
            End.Next = Current;
            End = Current;
        }
        Count++;
        LastOperation = true;
    }

    // Method that added element("item") to the some position("pos") in the collection.
    public boolean Add(int pos, T item) {
        if (pos < Count) {
            Node newNext = GoTo(pos);
            Node NewNode = new Node(item, newNext.Prev, newNext);
            if (pos == 0) {
                Begin = NewNode;
            } else {
                newNext.Prev.Next = NewNode;
            }
            newNext.Prev = NewNode;
            Count++;
            LastOperation = true;
            return true;
        } else {
            LastOperation = false;
            return false;
        }
    }

    // Method that added element("item") to begin of the collection.
    public void AddToBegin(T item) {
        if (Begin == null) {
            Begin = new Node(item, null, null);
            End = Begin;
        } else {
            Node current = new Node(item, null, Begin);
            Begin.Prev = current;
            Begin = current;
        }
        Count++;
        LastOperation = true;
    }

    // Method that delete all elements from the collection.
    public void Clear() {
        Begin = null;
        End = null;
        Count = 0;
        LastOperation = true;
    }

    //Method that delete element for some position("position").
    public boolean Delete(int position) {
        if(Count>0 && position<Count)
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
        else
        {
            LastOperation = false;
            return false;
        }
    }

    // Method that delete element from the end of the collection.
    public boolean DeleteFromEnd() {
        if(Count > 0) {
            if(End.Prev != null)
            {
                End.Prev.Next = null;
            } else
            {
                Begin = null;
            }

            End = End.Prev;
            Count--;

            LastOperation = true;
            return true;
        }
        else
        {
            LastOperation = false;
            return false;
        }
    }

    // Method that delete element form the begin of the collection.
    public boolean DeleteFromBegin() {
        try {
            try {
                Begin.Next.Prev = null;
            } catch (Exception e) {
                End = null;
            }
            Begin = Begin.Next;
            Count--;
            LastOperation = true;
            return true;
        }
        catch (Exception e)
        {
            LastOperation = false;
            return false;
        }
    }

    // Method that delete all elements form startPosition to endPosition;
    public boolean DeleteFromTo(int startPosition, int endPosition) {
        try {
            for (int i = startPosition; i <= endPosition; i++) {
                Delete(startPosition);
            }
            LastOperation = true;
            return true;
        } catch (Exception e) {
            LastOperation = false;
            return false;
        }
    }

    // Method that return element that placed on "i" position.
    public T GetElement(int i) {

        if (i >= Count)// Out of range;
            return null;
        else
            return GoTo(i).Data;
    }

    // Method that make posible construction for(T item : MCollection).
    @Override
    public Iterator<T> iterator() {
        return new Iterator<T>() {
            private int count=0;

            @Override
            public boolean hasNext() {
                if (Count < count)
                    return false;

                if (GoTo(count)==null)
                    return false;
                else
                    return true;
            }

            @Override
            public T next() {
                count++;
                return GoTo(count-1).Data;
            }

            @Override
            public void remove() {
                throw new UnsupportedOperationException();
            }
        };
    }


    @Override
    public boolean equals(Object obj) {
        MCollection<T> value = (MCollection<T>) obj;
        if (this.Count != value.Count)
            return false;

        for (int i = 0; i <= Count - 1; i++) {
            if (!GetElement(i).equals(value.GetElement(i)))
                return false;
        }
        return true;
    }

    // Method that return true if collection consist "obj", and false if collection not consist "obj".
    public boolean Contains(T obj) {
        for (T item : this) {
            if (item.equals(obj))
                return true;
        }
        return false;
    }

    @Override
    public int hashCode() {
        return super.hashCode();
    }

    public MCollection<T> Concat(MCollection<T> obj) {
        MCollection<T> result = new MCollection<T>();
        for(T item:this)
        {
            result.AddToEnd(item);
        }
        for(T item : obj)
        {
            result.AddToEnd(item);
        }
        return result;
    }

    // Inside method that used in public SortMin, SortMax.
    // if check = true, sort Z..A, else sort A..Z.
    private void Sort(boolean check)
    {
        MCollection<T> extraCollection = new MCollection<T>();
        int Size = Count;
        for (int j = 0; j < Size; j++) {
            T item = this.GetElement(0);
            int Index = 0;
            for(int i=0; i<getCount(); i++)
            {
                if ((item.compareTo(GetElement(i)) >= 0) ^ check) {
                    item = GetElement(i);
                    Index = i;
                }
            }
            extraCollection.AddToEnd(item);
            this.Delete(Index);
        }
        this.Begin = extraCollection.Begin;
        this.End = extraCollection.End;
        this.Count = extraCollection.Count;
    }
    public void SortMin() {
        Sort(false);
    }

    public void SortMax() {
        Sort(true);
    }

    // Method that search and return all elements that equals to "element".
    public MCollection<T> Find(T element)
    {
        MCollection<T> extraCollection = new MCollection<T>();
        for(T item:this)
        {
            if(element.equals(item))
                extraCollection.AddToEnd(item);
        }
        return extraCollection;
    }

    // Class that define structure of the Node.
    class Node {
        public T Data;// Data that stored in a node.
        public Node Prev;// Link to the previous node.
        public Node Next;// Link to the next node.

        public Node() {
            Prev = null;
            Next = null;
        }

        public Node(T _Data, Node _Prev, Node _Next) {
            Data = _Data;
            Prev = _Prev;
            Next = _Next;
        }
    }
}