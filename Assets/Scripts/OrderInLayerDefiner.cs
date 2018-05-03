using Components.EventHandler;
using UnityEngine;

public class OrderInLayerDefiner : MonoBehaviour
{
    private static float UnitsCount = 0.03f;

    private SpriteRenderer _spriteRenderer;
    private Transform _transform;

    // Use this for initialization
    void Awake ()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (_transform == null)
        {
            _transform = transform;
        }
    }

    private float _lastPositionY;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float positionY = _transform.position.y;
        if (_lastPositionY != positionY && CurrentRoom != null)
        {
            Rect sceneRect = CurrentRoom.Shape.Rectangle;
            float localPointY = positionY - sceneRect.y;
            _spriteRenderer.sortingOrder = (int)((sceneRect.height - localPointY) / UnitsCount);
            _lastPositionY = positionY;
        }
    }

    private IRoom CurrentRoom
    {
        get
        {
            return LevelAPIs.CurrentRoom;
        }
    }
}
