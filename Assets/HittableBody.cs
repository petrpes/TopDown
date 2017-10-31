using System.Collections.Generic;
using UnityEngine;

public class HittableBody : MonoBehaviour
{
    [SerializeField] private TagCheckList _tagsHittable;
    [SerializeField] private HitOn _hitOn;
    [SerializeField] private Command[] _afterHitCommands;

    public HealthPoints Damage { get; set; }

    private Dictionary<GameObject, UnitHealth> _currentlyHitable;

    private void Awake()
    {
        if (_currentlyHitable == null)
        {
            _currentlyHitable = new Dictionary<GameObject, UnitHealth>();
        }
        else
        {
            _currentlyHitable.Clear();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Hit(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Hit(other.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        LeaveCollider(collision.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        LeaveCollider(other.gameObject);
    }

    private void FixedUpdate()
    {
        foreach (UnitHealth unitHealth in _currentlyHitable.Values)
        {
            Hit(unitHealth);
        }
    }

    private void Hit(GameObject gameObject)
    {
        if (_tagsHittable.ContainsTag(gameObject.tag))
        {
            UnitHealth unitHealth = gameObject.GetComponent<UnitHealth>();
            if (_hitOn == HitOn.OnEnter)
            {
                Hit(unitHealth);
            }
            else
            {
                if (!_currentlyHitable.ContainsKey(gameObject))
                {
                    _currentlyHitable.Add(gameObject, unitHealth);
                }
            }
        }
    }

    private void Hit(UnitHealth unitHealth)
    {
        if (unitHealth.Hit(Damage))
        {
            for (int i = 0; i < _afterHitCommands.Length; i++)
            {
                _afterHitCommands[i].Execute(unitHealth.gameObject);
            }
        }
    }

    private void LeaveCollider(GameObject gameObject)
    {
        if (_tagsHittable.ContainsTag(gameObject.tag) && _hitOn == HitOn.OnStay && 
            _currentlyHitable.ContainsKey(gameObject))
        {
            _currentlyHitable.Remove(gameObject);
        }
    }
}

public enum HitOn
{
    OnEnter,
    OnStay
}

