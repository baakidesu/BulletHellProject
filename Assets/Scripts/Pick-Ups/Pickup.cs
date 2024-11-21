using UnityEngine;
using Lean.Pool;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); //lean pool patladı
        }
    }
}
