using UnityEngine;

public class DeathActionInvoker : MonoBehaviour
{
    [SerializeField] private HealthContainer _healthContainer;
    [SerializeField] private Command[] _deathCommands;

    private void OnEnable()
    {
        _healthContainer.OnDeath += DeathAction;
    }

    private void OnDisable()
    {
        _healthContainer.OnDeath -= DeathAction;
    }

    private void DeathAction()
    {
        for (int i = 0; i < _deathCommands.Length; i++)
        {
            _deathCommands[i].Execute(gameObject);
        }
    }
}

