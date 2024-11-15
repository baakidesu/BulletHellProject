using Lean.Pool;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <=0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        LeanPool.Despawn(gameObject);
    }
}
