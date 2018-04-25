using UnityEditor;
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

[CustomEditor(typeof(TestCache))]
public class TestCacheEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Set"))
        {
            var baseObj = target as TestCache;
            baseObj.gameObject.GetComponentInChildren<SetValueTest>().SetTest(baseObj);
        }
    }
}
