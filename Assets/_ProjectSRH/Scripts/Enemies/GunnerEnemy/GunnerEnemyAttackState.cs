using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GunnerEnemyAttackState : EnemyState
{
    public GameObject bullet;
    public AnimationClip anim;
    public float shootCooldown;

    public override void Enter()
    {
        if (anim) animator.Play(anim.name);
        StartCoroutine(Shoot());

    }

    private IEnumerator Shoot()
    {
        SpawnBullet();
        if (anim) animator.Play(anim.name);
        yield return new WaitForSeconds(shootCooldown + anim.length);
        StartCoroutine(Shoot());
    }

    private void SpawnBullet()
    {
        Vector3 position = transform.position + new Vector3(directionToTarget.x, 0, 0).normalized;
        Instantiate(bullet, 
        position, 
        Quaternion.identity).transform.right = ((Vector3)targetPosition - position).normalized ;
    }
}
