using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GarlicBehaviour : MeleeWeaponBehaviour
{
    private List<GameObject> markedEnemies;
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !markedEnemies.Contains(other.gameObject))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
            
            markedEnemies.Add(other.gameObject);
        }else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(other.gameObject))
            {
                breakable.TakeDamage(GetCurrentDamage());
                markedEnemies.Add(other.gameObject);
            }
        }
    }
}
