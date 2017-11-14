using UnityEngine;

public class AddForceCommand : Command
{
    [SerializeField] private bool _useVelocity;
    [SerializeField] private float _mass;

    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private float Impulse { get { return _mass * (_useVelocity ? _rigidbody.velocity.magnitude : 1); } }

    public override void ExecuteCommand(GameObject actor)
    {
        if (_transform == null)
        {
            _transform = transform;
        }
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        DamageSkill actorDamageSkill = actor.GetComponent<DamageSkill>();
        if (actorDamageSkill != null && actorDamageSkill.MovableBody != null)
        {
            DirectionVector direction = new DirectionVector()
            {
                Value = _useVelocity ?
                    (Vector3)_rigidbody.velocity :
                    (actor.transform.position - _transform.position)
            };
            //actorDamageSkill.MovableBody.AddForce(direction.Value * Impulse);
            actorDamageSkill.MovableBody.velocity += (Vector2)direction.Value * Impulse;
        }
    }
}

