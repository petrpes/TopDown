using System;
using Components.Timer;
using UnityEngine;

[RequireComponent(typeof(DamageSkill))]
[RequireComponent(typeof(Rigidbody2D))]
public class SimpleProjectile : Projectile, IAllRoomsEventListener
{
    private DamageSkill _damageSkill;
    private Rigidbody2D _rigidbody2D;
    private ClassInformation _classInformation;
    private ExpirationTimer _timer;

    public Action<IRoom> this[RoomEventType eventType]
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    private void Awake()
    {
        RoomEventHandler.Instance.SubscribeListener(this);
    }

    private void OnDestroy()
    {
        RoomEventHandler.Instance.UnsubscribeListener(this);
    }

    public override void Shoot(Vector3 position, float rotation, float timeFloat, Vector3 speed, HealthPoints damageAddition,
        Fraction currentFraction)
    {
        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        if (_damageSkill == null)
        {
            _damageSkill = GetComponent<DamageSkill>();
        }
        if (_classInformation == null)
        {
            _classInformation = GetComponent<ClassInformation>();
        }
        if (_timer == null)
        {
            _timer = new ExpirationTimer(timeFloat);
            _timer.OnExpiredTimer += DespawnAction;
        }
        if (_timer.ExpirationTime != timeFloat)
        {
            _timer.ExpirationTime = timeFloat;
        }
        _timer.Start();

        _damageSkill.DamageValue = damageAddition;
        _classInformation.CurrentFraction = currentFraction;

        _rigidbody2D.position = position;
        _rigidbody2D.rotation = rotation;
        _rigidbody2D.velocity = speed;
    }

    private void DespawnAction()
    {
        SpawnManager.Instance.Despawn(this);
    }

    public void OnRoomEvent(IRoom room, RoomEventType eventType)
    {
        if (eventType == RoomEventType.OnBeforeClose && SpawnManager.Instance.IsSpawned(this))
        {
            DespawnAction();
        }
    }
}

