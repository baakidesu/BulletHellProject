using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    
    #region Public

    public float moveSpeed;
    
    [HideInInspector]
    public Vector2 moveDir;

    [HideInInspector] 
    public float lastHorizontalVector;
    
    [HideInInspector] 
    public float lastVerticalVector;
    
    #endregion

    #region Private

    private Rigidbody2D rb;

    private float moveY;
    private float moveX;
    
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        InputManagment(); 
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagment()
    { 
        moveX = Input.GetAxisRaw("Horizontal"); 
        moveY = Input.GetAxisRaw("Vertical");
        
        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

  
}
