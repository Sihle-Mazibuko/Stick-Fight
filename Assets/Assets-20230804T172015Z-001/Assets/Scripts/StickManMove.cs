using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManMove : MonoBehaviour
{
    [Header("Movement")]
    public float movementForce;
    public float jumpForce;
    [Space(5)]
    [Range(0f, 100f)] public float raycastDistance = 1.5f;
    public LayerMask whatIsGround;

    [Header("Animation")]
    public Animator anim;
    public Transform hips;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        float xDir = Input.GetAxisRaw("Horizontal2");
        rb.velocity = new Vector2(xDir * (movementForce * Time.deltaTime), rb.velocity.y);
        if (xDir != 0) 
        { 
           hips.localScale = new Vector3(xDir, hips.localScale.y, 1f);
        }
    }

    private void Jump()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, whatIsGround);
        return hit.collider != null;
    }
}
