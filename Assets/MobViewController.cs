using UnityEngine;

public class MobViewController : MonoBehaviour
{
    [SerializeField] private HealthChanger _unitHealth;

    private void Awake()
    {
        if (_unitHealth == null)
        {
            _unitHealth = GetComponent<HealthChanger>();
        }
    }
}

