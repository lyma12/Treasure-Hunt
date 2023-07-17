using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class JumpButton : MonoBehaviour

{
    private float horizontal;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Player player;

    void Update()
    {

        Flip();
    }
    void Start()
    {
        

    }
    public void turnLeft(bool left)
    {
        if (left) horizontal = -1;
        else horizontal = 1;
        player.run(horizontal);
    }
    public void buttonDownJump()
    {
        
            player.jump();

    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = player.transform.localScale;
            localScale.x *= -1f;
            player.transform.localScale = localScale;
        }
    }
}

