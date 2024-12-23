using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public float speed;

    private float elapsedTime = 0;

    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifeTime) Destroy(gameObject);
        transform.Translate(Time.deltaTime * speed * Vector3.right);
    }
}
