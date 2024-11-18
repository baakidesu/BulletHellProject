using System;
using UnityEngine;
using Lean.Pool;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    //current stats
    [HideInInspector]
    public float currentHealth; 
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public float currentMoveSpeed;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats player = other.gameObject.GetComponentInParent<PlayerStats>();
            player.TakeDamage(currentDamage);//oyuncuya hasar ver
        }
    }
}
