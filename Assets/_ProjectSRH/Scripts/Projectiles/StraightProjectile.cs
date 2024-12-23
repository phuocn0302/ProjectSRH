using UnityEngine;

public class StraightBullet : Projectile
{
    private void FixedUpdate()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.right);
    }
}
