using Lean.Pool;
using UnityEngine;

public class HealthPotion : Pickup
{
    public int healthToRestore;
    public void Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }
}
