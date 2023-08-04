using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpPower = 16f;
    private bool isFaceRight = true;

    private bool isWallSlide;
    public float wallSlideSpeed = 2f;

    private bool isWallJump;
    private float wallJumpDirection;
    public float wallJumpTime = 0.2f;
    private float wallJumpCounter;
    public float wallJumpDuration = 0.2f;
    private Vector2 wallJumpPower = new Vector2(8f, 16f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal1");

        if (Input.GetButtonDown("Jump1") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        WallSlide();
        WallJump();

        if (!isWallJump)
        {
            Flip();
        }

        //Debug.Log(isGrounded());
    }

    private void FixedUpdate()
    {
        if (!isWallJump)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (isWalled() && !isGrounded() && horizontal != 0f)
        {
            isWallSlide = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSlide = false;
        }
    }

    private void WallJump()
    {
        if (isWallSlide)
        {
            isWallJump = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpCounter = wallJumpTime;

            CancelInvoke(nameof(StopWallJump));
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump1") && wallJumpCounter > 0f)
        {
            isWallJump = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpCounter = 0f;

            if (transform.localScale.x != wallJumpDirection)
            {
                isFaceRight = !isFaceRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJump), wallJumpDuration);
        }
    }

    private void StopWallJump()
    {
        isWallJump = false;
    }

    private void Flip()
    {
        if (isFaceRight && horizontal < 0f || !isFaceRight && horizontal > 0f)
        {
            isFaceRight = !isFaceRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }
}
