using UnityEngine;

public interface IBuiltLocally
{
#if UNITY_EDITOR
    void Build(GameObject parent);
#endif
}

