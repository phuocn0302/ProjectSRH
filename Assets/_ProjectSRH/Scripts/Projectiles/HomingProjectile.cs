using UnityEngine;
public class HomingProjectile : Projectile
{
    public GameObject target;
    public GameObject explodeEffect; 
    public float initialSpeed = 20f;
    public float minSpeed = 4f;
    public float speedChange = 1f;
    public float degChange = 180f;
    
    private float moveSpeed;
    public float delayChaseTime = 0.5f;
    private float timer = 0;

    public Vector2 initialVelocity = Vector2.up;
    private Vector2 moveVelocity;

    protected new void Start() 
    {
        base.Start();
        moveVelocity = initialVelocity;
        moveSpeed = initialSpeed;
    }

    private void Update()
    {
        if (timer < delayChaseTime)
        {
            timer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (moveSpeed > minSpeed)
        {
            moveSpeed -= speedChange;
        }
        transform.right = moveVelocity;
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        Vector2 currentPosition = transform.position;
        GameObject _target = GameObject.FindWithTag(target.tag);
        if (_target && timer > delayChaseTime) 
        {
            Vector2 targetPosition = _target.GetComponent<Transform>().position;
            Vector2 directionToTarget = (targetPosition - currentPosition).normalized;
            
            float angle = Vector2.SignedAngle(moveVelocity, directionToTarget);

            moveVelocity = rotate(moveVelocity, ((angle > 0f) ? 1f:-1f) * degChange * Time.deltaTime);
        }

        transform.Translate(Time.deltaTime * moveSpeed * moveVelocity, Space.World);
    }

    private void OnDestroy() 
    {
        Explode();
    }

    private void Explode()
    {
        if (explodeEffect)
            Instantiate(explodeEffect, transform.position, Quaternion.identity);
    }

    private Vector2 rotate(Vector2 v, float delta) {
        delta *= Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
