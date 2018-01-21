using UnityEngine;

public class DeathActionInvoker : MonoBehaviour
{
    [SerializeField] private HealthContainer _healthContainer;
    [SerializeField] private Command[] _deathCommands;

    private void OnEnable()
    {
        _healthContainer.Death += DeathAction;
    }

    private void OnDisable()
    {
        _healthContainer.Death -= DeathAction;
    }

    private void DeathAction()
    {
        _deathCommands.Execute(gameObject);
    }
}

