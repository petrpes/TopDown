using UnityEngine;

public class MobViewController : MonoBehaviour
{
    [SerializeField] private HealthChanger _unitHealth;
    [SerializeField] private Command[] _deathCommands;

    private void Awake()
    {
        if (_unitHealth == null)
        {
            _unitHealth = GetComponent<HealthChanger>();
        }
        _unitHealth.OnAfterDeathAction += ExecuteDeathAction;
    }

    private void ExecuteDeathAction()
    {
        for (int i = 0; i < _deathCommands.Length; i++)
        {
            _deathCommands[i].Execute(gameObject);
        }
    }
}

