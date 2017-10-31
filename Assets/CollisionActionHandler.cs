using System.Collections.Generic;
using UnityEngine;

public class CollisionActionHandler : MonoBehaviour
{
    [SerializeField] private TagCheckList _tags;
    [SerializeField] private CollisionType _collisionType;
    [SerializeField] private Command[] _commands;

    private List<GameObject> _collided;

    private void FixedUpdate()
    {
        if (_collided == null)
        {
            _collided = new List<GameObject>();
        }
        _collided.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExecuteCommands(CollisionType.OnCollisionEnter, collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ExecuteCommands(CollisionType.OnCollisionExit, collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ExecuteCommands(CollisionType.OnTriggerEnter, collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ExecuteCommands(CollisionType.OnTriggerExit, collision.gameObject);
    }

    private void ExecuteCommands(CollisionType currentCollisionType, GameObject actor)
    {
        if (CommandsDictionary.ContainsKey(currentCollisionType))
        {
            if (_collided.Contains(actor))
            {
                return;
            }
            _collided.Add(actor);//todo complex

            for (int i = 0; i < CommandsDictionary[currentCollisionType].Count; i++)
            {
                CommandsDictionary[currentCollisionType][i].Execute(actor);
            }
        }
    }
}


public enum CollisionType : byte
{
    OnTriggerEnter = 0,
    OnTriggerStay = 1,
    OnTriggerExit = 2,
    OnCollisionEnter = 3,
    OnCollisionStay = 4,
    OnCollisionExit = 5
}

