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

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !markedEnemies.Contains(other.gameObject))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            
            markedEnemies.Add(other.gameObject);
        }
    }
}
