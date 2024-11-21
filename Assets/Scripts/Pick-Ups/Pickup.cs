using UnityEngine;
using Lean.Pool;

public class Pickup : MonoBehaviour, ICollectable
{
    protected bool hasBeenCollected;
    
    public virtual void Collect()
    {
        hasBeenCollected = true;    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); //lean pool patladı
        }
    }
}
