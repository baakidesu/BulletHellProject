using UnityEngine;
using Lean.Pool;
public class ExperienceGem : MonoBehaviour, ICollectable
{
    public int experienceGranted;
    
    
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        LeanPool.Despawn(gameObject);
    }
}
