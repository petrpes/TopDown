using UnityEngine;

public class SetValueTest : MonoBehaviour
{
    [SerializeField] private InterfaceComponentCache _test;

    //TODO in inspector
    public void SetTest(ITestCache testCache)
    {
        _test.SetValue(testCache);
    }
}


