using UnityEngine;

public class MobViewController : MonoBehaviour
{
    [SerializeField] private UnitHealth _unitHealth;
    [SerializeField] private Command[] _deathCommands;

    private void Awake()
    {
        if (_unitHealth == null)
        {
            _unitHealth = GetComponent<UnitHealth>();
        }
        _unitHealth.DeathAction += ExecuteDeathAction;
    }

    private void ExecuteDeathAction()
    {
        for (int i = 0; i < _deathCommands.Length; i++)
        {
            _deathCommands[i].Execute(gameObject);
        }
    }
}

