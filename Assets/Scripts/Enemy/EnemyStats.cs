using System;
using UnityEngine;
using Lean.Pool;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    //current stats
    float currentHealth; 
    float currentDamage;
    float currentMoveSpeed;

    public void Awake()
    {
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <=0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
