using System;
using UnityEngine;
using Lean.Pool;
public class ExperienceGem : Pickup
{
    public int experienceGranted;
    
    
    public override void Collect()
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
        player.IncreaseExperience(experienceGranted);
    }
    
}
