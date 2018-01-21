using UnityEngine;

public class PublicComponentCache : PublicComponentsCacheBase
{
    [SerializeField] private BuffHandler _buffHandler;
    [SerializeField] private Mover _mover;
    [SerializeField] private HealthChanger _healthChanger;
    [SerializeField] private ClassInformation _classInformation;
    [SerializeField] private ObjectStatesContainerBase _statesContainer;

    public override BuffHandler BuffHandler { get { return _buffHandler; } }

    public override Mover Mover { get { return _mover; } }

    public override HealthChanger HealthChanger { get { return _healthChanger; } }

    public override ClassInformation ClassInformation { get { return _classInformation; } }

    public override ObjectStatesContainerBase StatesContainer { get { return _statesContainer; } }
}

