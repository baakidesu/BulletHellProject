using System;
using UnityEngine;
using Lean.Pool;
public class ExperienceGem : Pickup, ICollectable
{
    public int experienceGranted;
    
    
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
    }
    
}
