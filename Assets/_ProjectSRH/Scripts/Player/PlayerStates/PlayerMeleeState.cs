using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : PlayerState 
{   
    public float hitDelayTime = 0.2f;
    public float hitDeltaTime = 0.2f;

    public Animator slashAnimator;
    public GameObject slashContainer;
    private int maxHitCombo;
    private int hitCount;
    
    protected readonly Dictionary<Vector2, string> directionStrs = new()
    {
        {Vector2.up, "Up"},
        {Vector2.down, "Down"},
        {Vector2.left, "Right"},
        {Vector2.right, "Right"}
    };

    private void Start()
    {
        maxHitCombo = slashAnimator.runtimeAnimatorController.animationClips.Length;
    }

    public override void Enter()
    {
        hitCount = 1;
        StartCoroutine(Attack());
    }

    public override void PhysicsUpdate()
    {
        // body.velocity = Time.fixedDeltaTime * moveSpeed * 0.1f * moveInput; 
    }

    public override void Exit()
    {
        IsComplete = true;
    }

    private IEnumerator Attack()
    {
        if (hitCount >= maxHitCombo) 
        {
            hitCount = 1;
        }

        slashContainer.transform.up = -mouseDirection;

        string playerAnim = "Player";

        string[] slashType = {"Slash", "Chop"};
        string randomType = slashType[Random.Range(0,2)];
        playerAnim += randomType;

        if (randomType == "Slash" && hitCount % 2 != 0) 
            playerAnim += "Revert";
            
        playerAnim += MouseDirectionToString();
        
        animator.Play(playerAnim);
        slashAnimator.Play("Slash" + hitCount++.ToString());
        
        body.linearVelocity = Time.fixedDeltaTime * moveSpeed * mouseDirection;
        yield return new WaitForSeconds(0.05f);
        body.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(hitDelayTime);

        float _elapsedTime = 0;
        while (_elapsedTime < hitDeltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Attack());
                yield break;
            }

            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        Exit();
    }

    private string MouseDirectionToString()
    {
        Vector2[] directionVectors = {Vector2.up, Vector2.down, Vector2.left, Vector2.right};

        foreach (Vector2 v in directionVectors)
        {
            float delta = Vector2.Dot(mouseDirection, v);
            if (delta >= Mathf.Sqrt(2)/ 2)
            {
                return directionStrs[v];
            }
        }
        return directionStrs[Vector2.down];
    }

}
