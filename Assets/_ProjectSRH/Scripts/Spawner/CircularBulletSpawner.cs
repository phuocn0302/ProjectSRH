using System.Collections;
using UnityEngine;

public class CircularBulletSpawner : MonoBehaviour
{
    public int numberOfBullets;
    public GameObject bulletType;

    public float shootDelay = 1f;

    public bool spin;
    public float spinDegree = 10;


    private void Awake()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if(spin)
            transform.Rotate(new (0,0, spinDegree));
        
        float degree = 360f / numberOfBullets;
        Vector2 direction = transform.right;
        for(int i = 0; i < numberOfBullets; i++)
        {
            var spawnedBullet = Instantiate(bulletType, transform.position, Quaternion.identity);
            spawnedBullet.transform.right = direction;
            direction = rotateVector(direction, degree * Mathf.Deg2Rad);
        }

        yield return new WaitForSeconds(shootDelay);
        StartCoroutine(Shoot());
    }

    private Vector2 rotateVector(Vector2 v, float delta) {
        return new Vector2(
        v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
        v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
    );
}
}
