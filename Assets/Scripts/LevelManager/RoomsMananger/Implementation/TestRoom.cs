using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestRoom : MonoBehaviour, IRoom
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private DoorsHolder _doorsHolder;
    [HideInInspector]
    [SerializeField] private ComponentsCache _roomObjects = new ComponentsCache(typeof(PublicComponentCache), true);
    [Header("Drag a floor sprite here to adjust it's size to room")]
    [SerializeField] private SpriteRenderer _floorSprite;
    [SerializeField] private bool _lockSprite;

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

    public void AdjustSprite()
    {
        if (_floorSprite == null || _lockSprite)
        {
            return;
        }

        var floorTransform = _floorSprite.transform;
        if (!floorTransform.localScale.Equals(Vector3.one))
        {
            Debug.LogWarning("It would be better if " + _floorSprite.gameObject.name + " scale would be 1,1,1");
        }

        if (Shape.Rectangle.width == 0 ||
                Shape.Rectangle.height == 0)
        {
            Debug.LogError("Size of the room should not be nullable");
            return;
        }
        floorTransform.localPosition = Vector3.zero;

        if (_floorSprite.drawMode.Equals(SpriteDrawMode.Simple))
        {
            var size = _floorSprite.sprite.GetSize();

            size.x = Shape.Rectangle.width / size.x;
            size.y = Shape.Rectangle.height / size.y;

            floorTransform.localScale = size;
        }
        else
        {
            _floorSprite.size = Shape.Rectangle.size;
        }

        _floorSprite.size = Shape.Rectangle.size;
    }
#endif
}


[CustomEditor(typeof(TestRoom))]
public class TestRoomEditor : Editor
{
    private int _previousCount = 0;
    private IWallsColliderCreator _collidersCreator = new WallsColliderCreator();
    private TestRoom BasicObject { get { return this.BasicComponent<TestRoom>(); } }
    private Rect _prevRect;

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

        BasicObject.AdjustSprite();
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

    private void OnSceneGUI()
    {
        Handles.DrawSolidRectangleWithOutline(BasicObject.Shape.Rectangle, new Color(1,1,1,0), Color.red);
    }
}

