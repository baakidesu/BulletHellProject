using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAnimator : MonoBehaviour
{
    #region Privates
    
    private PlayerMovement pM;
    private Animator am;
    private SpriteRenderer sr;

    
    #endregion
    
    [Inject]
    public void Construct(PlayerMovement playerM)
    {
      pM = playerM;  
    }
    void Start()
    {
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        if (pM.moveDir.x !=0 || pM.moveDir.y !=0)
        {
            am.SetBool("Move", true);        
            SpriteDirChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    }

    void SpriteDirChecker()
    {
        if (pM.lastHorizontalVector <0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
