using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f;
    public float speed = 20f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
