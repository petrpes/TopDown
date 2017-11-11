using System.Collections.Generic;
using UnityEngine;

public class CollisionActionHandler : MonoBehaviour
{
    [SerializeField] private TagCheckList _tags;
    [SerializeField] private ColliderType _colliderType;
    [SerializeField] private CollisionType _collisionType;
    [SerializeField] private Command[] _commands;

    private ICollection<GameObject> _collisions;
    private ICollection<GameObject> _updateCollisions;

    private void Awake()
    {
        if (_collisions == null)
        {
            _collisions = new CollisionsList();
        }
        if (_updateCollisions == null)
        {
            _updateCollisions = new CollisionsList();
        }
    }

    private void OnDisable()
    {
        _collisions.Clear();
        _updateCollisions.Clear();
    }

    private void FixedUpdate()
    {
        if (_collisionType == CollisionType.OnStay)
        {
            for (int i = 0; i < _commands.Length; i++)
            {
                foreach (GameObject collision in _collisions)
                {
                    _commands[i].Execute(collision);
                }
            }
        }

        _updateCollisions.Clear();
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
        if (actor.name == "HittableBody" && currentColliderType == _colliderType)
        {
            Debug.Log(111);
        }
        if (currentColliderType == _colliderType && _tags.ContainsTag(actor.tag) &&
            !_updateCollisions.Contains(actor))
        {

            bool shouldExecute = false;
            if (currentCollisionType == CollisionType.OnEnter)
            {
                shouldExecute = !_collisions.Contains(actor);
                _collisions.Add(actor);
            }
            else if (currentCollisionType == CollisionType.OnExit)
            {
                _collisions.Remove(actor);
                shouldExecute = !_collisions.Contains(actor);
            }

            if (currentCollisionType == _collisionType && shouldExecute)
            {
                _updateCollisions.Add(actor);

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

