using UnityEngine;

public class TestCache : MonoBehaviour, ITestCache
{
    public string TestValue
    {
        get
        {
            return name;
        }
    }
}

