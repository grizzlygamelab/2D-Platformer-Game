using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _doubleJumpForce;
    [SerializeField] private bool _canDoubleJump;

    [Header("Wall Interactions")]
    [SerializeField] private float _wallJumpDuraction = 0.6f;
    [SerializeField] private Vector2 _wallJumpForce;
    private bool _isWallJumping;

    [Header("Knockback")]
    [SerializeField] private float _knockbackDuration = 1f;
    [SerializeField] private Vector2 _knockbackPower;
    private bool _isKnocked;
    private bool _canBeKnocked;

    [Header("Collisions")]
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private float _ceilingCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isAirBorne;
    [SerializeField] private bool _isWallDetected;

    private float _xInput;
    private float _yInput;
    private int _facingDirection = 1;
    private bool _facingRight = true;
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;
    //private SpriteRenderer _playerSpriteRenderer;

    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Knockback();
        }

        UpdateAirborneStatus();
        
        if(_isKnocked)
        {
            return;
        }

        HandleInput();
        HandleWallSlide();
        Movement();
        HandleFlip();
        DetectCollisions();
        Animations();
        
    }

    private void HandleInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _yInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
    }

    private void HandleWallSlide()
    {
        bool canWallSlide = _isWallDetected && _playerRigidbody.velocity.y < 0;
        float yModifier = _yInput < 0 ? 1 : 0.5f;

        if(canWallSlide == false)
        {
            return;
        }

        _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _playerRigidbody.velocity.y * yModifier);
    }

    private void UpdateAirborneStatus()
    {
        if(_isAirBorne && _isGrounded)
        {
            HandleLanding();
        }

        if(!_isGrounded && !_isAirBorne)
        {
            BecomeAirborne();
        }
    }

    private void JumpButton()
    {
        if(_isGrounded)
        {
            Jump();
        }
        else if(_isWallDetected)
        {
            WallJump();
        }
        else if(_canDoubleJump)
        {
            DoubleJump();
        }
    }

    private void Jump()
    {
        _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpForce);
    }

    private void DoubleJump()
    {
        _canDoubleJump = false;
        _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _doubleJumpForce);
        _playerAnimator.SetTrigger("canDoubleJump");
    }

    private void WallJump()
    {
        //_isWallJumping = true;
        _playerRigidbody.velocity = new Vector2(_wallJumpForce.x -_facingDirection, _wallJumpForce.y);
        StartCoroutine(WallJumpRoutine());
    }

    private void TrampolineBounce(float amount)
    {
        _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, amount);
    }

    private void HandleLanding()
    {
        _isAirBorne = false;
        _canDoubleJump = true;
    }

    private void BecomeAirborne()
    {
        _isAirBorne = true;
    }

    private void Movement()
    {
        if(_isWallDetected)
        {
            return;
        }

        if(_isWallJumping)
        {
            return;
        }
       
        _playerRigidbody.velocity = new Vector2(_xInput * _moveSpeed, _playerRigidbody.velocity.y);
    }

    private void Flip()
    {
        _facingDirection = _facingDirection * -1;
        transform.Rotate(0, 180, 0);
        _facingRight = !_facingRight;
    }

    private void HandleFlip()
    {
        if(_xInput < 0 && _facingRight || _xInput > 0 && !_facingRight)
        {
            Flip();
        }
    }

    public void Knockback()
    {
        if(_isKnocked)
        {
            return;
        }

        StartCoroutine(KnockbackRoutine());
        _playerAnimator.SetTrigger("knockback");
        _playerRigidbody.velocity = new Vector2(_knockbackPower.x * -_facingDirection, _knockbackPower.y);
        FindObjectOfType<UIManager>().SetPlayerLives(1);
    }

    private void Animations()
    {
        _playerAnimator.SetFloat("xVelocity", _playerRigidbody.velocity.x);
        _playerAnimator.SetFloat("yVelocity",_playerRigidbody.velocity.y);
        _playerAnimator.SetBool("isGrounded", _isGrounded);
        _playerAnimator.SetBool("isWallDetected", _isWallDetected);
    }

    private void DetectCollisions()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down,_groundCheckDistance, _whatIsGround);
        _isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * _facingDirection, _wallCheckDistance, _whatIsGround);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - _groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (_wallCheckDistance * _facingDirection), transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + _ceilingCheckDistance));
    }

    // *** All of the Coroutines ***
    private IEnumerator WallJumpRoutine()
    {
        _isWallJumping = true;

        yield return new WaitForSeconds(_wallJumpDuraction);

        _isWallJumping = false;
    }

    private IEnumerator KnockbackRoutine()
    {
        _canBeKnocked = false;
        _isKnocked = true;

        yield return new WaitForSeconds(_knockbackDuration);

        _canBeKnocked = true;
        _isKnocked = false;
    }

    // *** All public access ***
    public void TrampolineJump(float amount)
    {
        float bounceForce = _jumpForce * amount;

        TrampolineBounce(bounceForce);
    }
}
