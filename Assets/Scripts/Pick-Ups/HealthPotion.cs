using Lean.Pool;
using UnityEngine;

public class HealthPotion : Pickup,ICollectable
{
    public int healthToRestore;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }
}
