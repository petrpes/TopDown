using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckUnitCommand : Command
{
    [SerializeField] private HealthPoints _damage;

    public override void ExecuteCommand(GameObject actor)
    {
        SpriteRenderer spriteRenderer = actor.GetComponent<SpriteRenderer>();
        Rigidbody2D rigidbody = actor.GetComponent<Rigidbody2D>();
        UnitHealth health = actor.GetComponent<UnitHealth>();
        if (health == null)
        {
            health = actor.GetComponentInChildren<UnitHealth>();
        }

        Vector3 velocity = rigidbody.velocity;
        rigidbody.velocity = Vector3.zero;
        health.Hit(_damage, gameObject);

        StartCoroutine(Fall(spriteRenderer, rigidbody, health, velocity));
    }

    private IEnumerator Fall(SpriteRenderer spriteRenderer, Rigidbody2D rigidbody, UnitHealth health, Vector3 velocity)
    {
        yield return new WaitForSeconds(1);
        rigidbody.transform.position += -velocity.normalized;
    }
}

