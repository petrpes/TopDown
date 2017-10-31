using System;
using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class MobViewController : MonoBehaviour
{
    [SerializeField] private Command[] _deathCommands;

    private UnitHealth _unitHealth;

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

