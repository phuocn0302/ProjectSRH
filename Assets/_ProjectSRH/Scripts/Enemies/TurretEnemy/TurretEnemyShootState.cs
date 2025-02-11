using System.Collections;
using UnityEngine;

public class TurretEnemyShootState : EnemyState
{
    public AnimationClip anim;
    [field: SerializeField] public GameObject shootProjectile {get; private set;}
    [SerializeField] private float shootCooldownTime = 1f;
    private bool isShooting;

    public override void Enter()
    {
        if (!isShooting)
            StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        if (anim)
            animator.Play(anim.name);

        if (shootProjectile)
            Instantiate(shootProjectile, transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(shootCooldownTime);
        StartCoroutine(Shoot());
    }

    public override void Exit()
    {
        base.Exit();
        isShooting = false;
        StopAllCoroutines();
    }
}