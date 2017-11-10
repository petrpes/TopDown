using UnityEngine;

public class OrderInLayerDefiner : MonoBehaviour
{
    private static float UnitsCount = 0.03f;

    [SerializeField] private Rect _sceneSize;

    private SpriteRenderer _spriteRenderer;
    private Sprite _sprite;
    private Transform _transform;

    // Use this for initialization
    void Awake ()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (_sprite == null)
        {
            _sprite = _spriteRenderer.sprite;
        }

        if (_transform == null)
        {
            _transform = transform;
        }
    }

    private float LocalPointOffset
    {
        get
        {
            return (_sprite.rect.height / _sprite.pixelsPerUnit) * _transform.localScale.y / 2f;
        }
    }

    private float _lastPositionY;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float positionY = _transform.position.y;
        if (_lastPositionY != positionY)
        {
            Rect sceneRect = _sceneSize;//SceneManager.Instance.Size;//todo 
            float localPointY = positionY - sceneRect.y - LocalPointOffset;
            _spriteRenderer.sortingOrder = (int)((sceneRect.height - localPointY) / UnitsCount);
            _lastPositionY = positionY;
        }
	}
}
