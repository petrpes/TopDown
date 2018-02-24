using UnityEditor;
using UnityEngine;

public class SetValueTest : MonoBehaviour
{
    public InterfaceComponentCache _test;

    //TODO in inspector
    public void SetTest(ITestCache testCache)
    {
        _test.SetValue(testCache);
    }
}

[CustomEditor(typeof(SetValueTest))]
public class SetValueTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Set"))
        {
            var baseObj = target as SetValueTest;
            baseObj.SetTest(baseObj.gameObject.GetComponent<ITestCache>());
        }
    }
}

