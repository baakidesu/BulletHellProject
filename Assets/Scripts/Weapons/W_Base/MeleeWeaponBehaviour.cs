using System;
using UnityEngine;
using Lean.Pool;

public class MeleeWeaponBehaviour : MonoBehaviour{

    public float destroyAfterSeconds;
    public WeaponScriptableObject weaponData;
    
    //current stats

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentPierce = weaponData.Pierce;
        currentCooldownDuration = weaponData.CooldownDuration;
    }

    protected virtual void Start()
    {
        Destroy(gameObject,destroyAfterSeconds);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }else if (other.CompareTag("Prop") )
        {
            if (other.gameObject.TryGetComponent(out BreakableProps breakable) )
            {
                breakable.TakeDamage(currentDamage);
            }
        }
    }
}
