using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestRoom : MonoBehaviour, IRoom
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private DoorsHolder _doorsHolder;
    [HideInInspector]
    [SerializeField] private ComponentsCache _roomObjects = new ComponentsCache(typeof(PublicComponentCache), true);

    private Transform _transform;
    private Rect _rectangle;
    private Vector2 _perviousSize;
    private Vector2 _perviousPosition;
    private IShape _shape;

    public IShape Shape
    {
        get
        {
            if (_transform == null)
            {
                _transform = transform;
            }
            if (_shape == null || _rectangle == Rect.zero || 
                !_size.Equals(_perviousSize) || !_perviousPosition.Equals(_transform.position))
            {
                _perviousSize = _size;
                _perviousPosition = _transform.position;

                _rectangle = new Rect(_perviousPosition.x - _size.x / 2,
                                      _perviousPosition.y - _size.y / 2,
                                      _size.x,
                                      _size.y);
                _shape = new ShapeRectangle(_rectangle);
            }

            return _shape;
        }
    }

    public IEnumerable<PublicComponentCache> DefaultObjects
    {
        get
        {
            return _roomObjects.GetCachedComponets<PublicComponentCache>();
        }
    }

    public IDoorsHolder DoorsHolder
    {
        get
        {
            return _doorsHolder;
        }
#if UNITY_EDITOR
        set
        {
            _doorsHolder = value as DoorsHolder;
        }
#endif
    }

#if UNITY_EDITOR
    public void SubscribeObjects()
    {
        _roomObjects.RecalculateComponents(gameObject, this);
        EditorUtility.SetDirty(this);
        if (_roomObjects.Count > 0)
        {
            Debug.Log(_roomObjects.Count + " objects was successfully added to the room");
        }
    }
#endif
}


[CustomEditor(typeof(TestRoom))]
public class TestRoomEditor : Editor
{
    private int _previousCount = 0;
    private IWallsColliderCreator _collidersCreator = new WallsColliderCreator();
    private TestRoom BasicObject { get { return this.BasicComponent<TestRoom>(); } }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Subscribe objects to room"))
        {
            BasicObject.SubscribeObjects();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Build Walls"))
        {
            BuildWalls();
        }
    }

    private void BuildWalls()
    {
        var walls = this.Property("_walls");
        var room = target as TestRoom;

        if (room.DoorsHolder == null)
        {
            var doorsHolder = BasicObject.gameObject.GetComponent<DoorsHolder>();

            if (doorsHolder != null && 
                EditorUtility.DisplayDialog("DoorsHolder", 
                    "There is seems to be a DoorsHolder component attached to this object. " +
                    "Do you like to set it to this room's DoorsHolder property?",
                    "OK", "Nevermind"))
            {
                room.DoorsHolder = doorsHolder;
            }
        }

        var gameObject = CreateGameObject(room.gameObject);
        _collidersCreator.CreateColliders(room.Shape, room.DoorsHolder, gameObject);
    }

    private GameObject CreateGameObject(GameObject parent)
    {
        var wallGameObject = new GameObject("Walls");

        wallGameObject.transform.parent = parent.transform;
        wallGameObject.tag = "Wall";
        wallGameObject.layer = LayerMask.NameToLayer("Walls");

        var rigidbody = wallGameObject.AddComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Static;

        return wallGameObject;
    }
}

