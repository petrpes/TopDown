using UnityEditor;
using UnityEngine;

public class PrefabsSpawnPoint : SpawnPointBase
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private int _count;
    [SerializeField] private bool _rand = false;
    [SerializeField] private float _variance = 1;

    public override void Spawn()
    {
        for (int i = 0; i < _count; i++)
        {
            var offsetPosition = _rand ? 
                GeometryExtentions.RandomVector(_variance) : 
                Vector3.zero;
            var spawnedObject = SpawnManager.Instance.Spawn(_prefab);
            var objectTranform = spawnedObject.transform;

            objectTranform.parent = _parentTransform.parent;
            objectTranform.position = _parentTransform.position + offsetPosition;
        }
    }
}

