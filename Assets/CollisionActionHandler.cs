using System.Collections.Generic;
using UnityEngine;

public class CollisionActionHandler : MonoBehaviour
{
    [SerializeField] private TagCheckList _tags;
    [SerializeField] private ColliderType _colliderType;
    [SerializeField] private CollisionType _collisionType;
    [SerializeField] private Command[] _commands;

    private IList<GameObject> _collisions;

    private void Awake()
    {
        if (_collisions == null)
        {
            _collisions = new List<GameObject>();
        }
        _collisions.Clear();
    }

    private void FixedUpdate()
    {
        if (_collisionType == CollisionType.OnStay)
        {
            for (int i = 0; i < _commands.Length; i++)
            {
                for (int j = 0; j < _collisions.Count; j++)
                {
                    _commands[i].Execute(_collisions[j]);
                }
            }
        }
        else
        {
            _collisions.Clear();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExecuteCommands(ColliderType.Collider, CollisionType.OnEnter, collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ExecuteCommands(ColliderType.Collider, CollisionType.OnExit, collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ExecuteCommands(ColliderType.Trigger, CollisionType.OnEnter, collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ExecuteCommands(ColliderType.Trigger, CollisionType.OnExit, collision.gameObject);
    }

    private void ExecuteCommands(ColliderType currentColliderType, 
        CollisionType currentCollisionType, GameObject actor)
    {
        if (currentColliderType == _colliderType && _tags.ContainsTag(actor.tag))
        {
            if (currentCollisionType == CollisionType.OnEnter &&
                _collisionType == CollisionType.OnStay && 
                !_collisions.Contains(actor))
            {
                _collisions.Add(actor);
            }
            else if (currentCollisionType == CollisionType.OnExit &&
                _collisionType == CollisionType.OnStay)
            {
                _collisions.Remove(actor);
            }
            else if (currentCollisionType == _collisionType && !_collisions.Contains(actor))
            {
                _collisions.Add(actor);

                for (int i = 0; i < _commands.Length; i++)
                {
                    _commands[i].Execute(actor);
                }
            }
        }
    }
}

public enum CollisionType
{
    OnEnter,
    OnStay,
    OnExit,
}

public enum ColliderType
{
    Collider,
    Trigger
}

