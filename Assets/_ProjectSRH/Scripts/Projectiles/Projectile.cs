using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f;
    public float speed = 20f;

    protected void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
