using Lean.Pool;
using UnityEngine;

public class HealthPotion : MonoBehaviour,ICollectable
{
    public int healthToRestore;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
        LeanPool.Despawn(gameObject);
    }
}
