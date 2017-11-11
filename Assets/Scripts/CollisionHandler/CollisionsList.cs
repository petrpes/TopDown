using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsList : ICollection<GameObject>
{
    private Dictionary<GameObject, int> _dictionary;

    public CollisionsList()
    {
        _dictionary = new Dictionary<GameObject, int>();
    }

    public int Count { get; private set; }

    public bool IsReadOnly { get { return false; } }

    public void Add(GameObject item)
    {
        if (Contains(item))
        {
            _dictionary[item]++;
        }
        else
        {
            _dictionary.Add(item, 1);
            Count++;
        }
    }

    public void Clear()
    {
        _dictionary.Clear();
    }

    public bool Contains(GameObject item)
    {
        return _dictionary.ContainsKey(item);
    }

    public void CopyTo(GameObject[] array, int arrayIndex)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator<GameObject> GetEnumerator()
    {
        return _dictionary.Keys.GetEnumerator();
    }

    public bool Remove(GameObject item)
    {
        if (Contains(item))
        {
            _dictionary[item]--;
            if (_dictionary[item] <= 0)
            {
                _dictionary.Remove(item);
                Count--;
            }
            return true;
        }
        return false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _dictionary.Keys.GetEnumerator();
    }
}

