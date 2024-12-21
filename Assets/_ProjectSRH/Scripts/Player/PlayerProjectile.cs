using UnityEngine;

public class PlayerProjectile : MonoBehaviour 
{
    public float lifeTime = 3f;
    public float speed = 50f;

    private void Start()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.right);
    }    

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
