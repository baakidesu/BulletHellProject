using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private PlayerStats player;
    CircleCollider2D playerCollector;

    public float pullSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>(); //todo zenject
        playerCollector = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        playerCollector.radius = player.CurrentMagnet;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //ICollectable varsa topla yavrum
        if (other.gameObject.TryGetComponent(out ICollectable collectable))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - other.transform.position).normalized;
            rb.AddForce(forceDirection * pullSpeed);
            collectable.Collect();
        }
    }
}
