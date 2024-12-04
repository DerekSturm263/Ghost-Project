using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EntropicList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICollection
{
    public enum FilterMode
    {
        In,
        Out
    }

    [SerializeField] private List<T> _possibilities;

    public EntropicList()
    {
        _possibilities = new();
    }

    public EntropicList(List<T> possibilities)
    {
        _possibilities = new(possibilities);
    }

    public int Count => ((ICollection<T>)_possibilities).Count;
    public bool IsReadOnly => ((ICollection<T>)_possibilities).IsReadOnly;
    public bool IsSynchronized => ((ICollection)_possibilities).IsSynchronized;
    public object SyncRoot => ((ICollection)_possibilities).SyncRoot;
    public bool IsFixedSize => ((IList)_possibilities).IsFixedSize;

    public void Add(T item) => ((ICollection<T>)_possibilities).Add(item);
    public int Add(object value) => ((IList)_possibilities).Add(value);

    public bool Contains(T item) => ((ICollection<T>)_possibilities).Contains(item);
    public bool Contains(object value) => ((IList)_possibilities).Contains(value);

    public void CopyTo(T[] array, int arrayIndex) => ((ICollection<T>)_possibilities).CopyTo(array, arrayIndex);
    public void CopyTo(Array array, int index) => ((ICollection)_possibilities).CopyTo(array, index);

    public int IndexOf(T item) => ((IList<T>)_possibilities).IndexOf(item);
    public int IndexOf(object value) => ((IList)_possibilities).IndexOf(value);

    public void Insert(int index, T item) => ((IList<T>)_possibilities).Insert(index, item);
    public void Insert(int index, object value) => ((IList)_possibilities).Insert(index, value);

    public bool Remove(T item) => ((ICollection<T>)_possibilities).Remove(item);
    public void Remove(object value) => ((IList)_possibilities).Remove(value);

    public void RemoveAt(int index) => ((IList<T>)_possibilities).RemoveAt(index);

    public void Clear() => ((ICollection<T>)_possibilities).Clear();

    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_possibilities).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_possibilities).GetEnumerator();

    public T Get(System.Random random)
    {
        if (_possibilities.Count > 0)
            _possibilities = new() { _possibilities[random.Next(0, _possibilities.Count)] };

        return _possibilities.FirstOrDefault();
    }

    public void Filter(Func<T, bool> predicate, T defaultT, FilterMode filterMode)
    {
        if (_possibilities.Count == 1)
            return;

        List<T> possibilities = new();

        foreach (var item in _possibilities)
        {
            switch (filterMode)
            {
                case FilterMode.In:
                    if (predicate(item))
                        possibilities.Add(item);
                    break;

                case FilterMode.Out:
                    if (!predicate(item))
                        possibilities.Add(item);
                    break;
            }
        }

        _possibilities = possibilities.Count > 0 ? possibilities : new() { defaultT };
    }
}
