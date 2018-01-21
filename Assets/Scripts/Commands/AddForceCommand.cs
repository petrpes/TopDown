using UnityEngine;

public class AddForceCommand : Command
{
    [SerializeField] private bool _useVelocity;
    [SerializeField] private float _force;

    private Transform _transform;
    private Rigidbody2D _rigidbody;

    public override void Execute(GameObject actor)
    {
        if (_transform == null)
        {
            _transform = transform;
        }
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        var actorMover = actor.GetMover();
        if (actorMover != null)
        {
            DirectionVector direction = new DirectionVector()
            {
                Value = _useVelocity ?
                    (Vector3)_rigidbody.velocity :
                    (actor.transform.position - _transform.position)
            };
            actorMover.AddForce(direction.Value * _force);
        }
    }
}

