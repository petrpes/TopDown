using UnityEngine;

public class UnitSpawnOnEnable : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private void OnEnable()
    {
        SpawnManager.Instance.Spawn(_prefab);
    }
}

