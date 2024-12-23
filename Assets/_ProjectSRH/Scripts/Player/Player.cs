using System.Collections.Generic;
using PrimeTween;
using UnityEngine;


public class Player : PlayerCore
{

    [field: SerializeField] public PlayerState IdleState {get; private set;}
    [field: SerializeField] public PlayerState MoveState {get; private set;}
    [field: SerializeField] public PlayerState DashState {get; private set;}
    [field: SerializeField] public PlayerState MeleeState {get; private set;}

    public GameObject playerDrone;
    public State CurrentState => stateMachine.state; 

    protected readonly Dictionary<Vector2, string> directionStrs = new()
    {
        {Vector2.up, "Up"},
        {Vector2.down, "Down"},
        {Vector2.left, "Left"},
        {Vector2.right, "Right"}
    };

    private Health health;


    private void Awake()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        SetupInstances();
        
        GameObject playerDroneObj = GameObject.FindGameObjectWithTag("PlayerDrone");
        if (!playerDroneObj) Instantiate(playerDrone);
    }

    private void OnEnable()
    {
        if (health)
        {
            health.OnHealthChange += HandleHealthChange;
            health.OnHealthDepleted += HandleHealthDepleted;
            health.OnObjectDie += HandlePlayerDie;
        }
            
    }

    private void OnDisable() 
    {
        if (health)
        {
            health.OnHealthChange -= HandleHealthChange;
            health.OnHealthDepleted -= HandleHealthDepleted;
            health.OnObjectDie -= HandlePlayerDie;
        }
            
    }

    private void Start()
    {
        FacingDirection = Vector2.down;
        FacingDirectionStr = "Down";
        stateMachine.SetState(IdleState);

        spriteRenderer.transform.localScale = Vector3.zero;
        Tween.Scale(spriteRenderer.transform, 1f, 0.5f);
    }
    
    private void Update()
    {
        UpdateInput();
        UpdateMouseDirection();
        UpdateDirectionVariable();
        UpdateSprite();
        if (stateMachine.state.IsComplete)
        {
            SelectState();
        }
        state.FrameUpdate();
    }

    private void FixedUpdate() 
    {
        state.PhysicsUpdate();
    }

    protected override void SelectState()
    {
        if (moveInput != Vector2.zero)
        {
            stateMachine.SetState(MoveState);
            return;
        }
        stateMachine.SetState(IdleState);
    }

    private void UpdateInput()
    {
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")
            ).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && CurrentState != MeleeState)
        {
            stateMachine.SetState(DashState);
            return;
        }

        if (Input.GetMouseButtonDown(0) && CurrentState != DashState)
        {
            stateMachine.SetState(MeleeState);
            return;
        }
    }
    private void UpdateSprite()
    {
        HandleSpriteFlip();
    }

    private void HandleSpriteFlip()
    {
        if (body.linearVelocity.x < 0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if (body.linearVelocity.x > 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void UpdateDirectionVariable()
    {
        Vector2 _currentFacingDirection = FacingDirection;
        string _currentFacingDirectionStr = FacingDirectionStr;

        Vector2[] directionVectors = {Vector2.up, Vector2.down, Vector2.left, Vector2.right};

        foreach (Vector2 v in directionVectors)
        {
            float delta = Vector2.Dot(body.linearVelocity, v);
            if (delta >= Mathf.Sqrt(2)/ 2)
            {
                FacingDirection = v;
                FacingDirectionStr = directionStrs[v];
                return;
            }
            FacingDirection = _currentFacingDirection;
            FacingDirectionStr = _currentFacingDirectionStr;
        }
    }

    private void UpdateMouseDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        mouseDirection = (mousePos - playerScreenPoint).normalized;
    }

    private void HandleHealthChange(float amount)
    {
        Debug.Log("Health: " + amount);
    }
    
    private void HandleHealthDepleted()
    {
        Debug.Log("Ouch");
        Time.timeScale = 0;
        Tween.GlobalTimeScale(0, 1, 1f);
        Tween.ShakeCamera(GameObject.FindFirstObjectByType<Camera>(), 0.5f);
    }

    private void HandlePlayerDie()
    {
        stateMachine.SetState(IdleState, true);
        this.enabled = false;
        Tween.GlobalTimeScale(1, 0.5f, 1f);
    }
}

