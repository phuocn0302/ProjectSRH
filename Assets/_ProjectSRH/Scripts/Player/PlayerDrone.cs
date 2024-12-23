using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class PlayerDrone : MonoBehaviour
{
    public Transform playerDronePos;
    public float angularSpeed = 1f;
    public float circleRad = 0.2f;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float shootDelay = 0.1f;
    public PlayerProjectile projectile;

    private float currentAngle;
    public GameObject playerObj;
    public Player player;

    private Vector2 defaultPosition;
    private SpriteRenderer spriteRenderer;
    private Dictionary<Vector2, Sprite> spriteMap; 
    private Health playerHealth;

    private bool isShooting;
    private bool canShoot = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        playerHealth = playerObj.GetComponent<Health>(); 
    }

    private void OnEnable()
    {
        if (playerHealth)
        {
            playerHealth.OnObjectDie += Die;
        }
    }

    private void Start()
    {
        spriteRenderer.sprite = downSprite;
        spriteMap = new()
        {
            {Vector2.zero, downSprite},
            {Vector2.up, upSprite},
            {Vector2.down, downSprite},
            {Vector2.left, leftSprite},
            {Vector2.right, rightSprite}
        };
        spriteRenderer.transform.localScale = Vector2.zero;
        Tween.Scale(spriteRenderer.transform, 1f, 0.5f);
    }

    private void Update()
    {
        if (!player) return;
        UpdateSprite();
        if (Input.GetMouseButton(1))
        {   isShooting = true;
            Shoot();
        }
        else
        {
            isShooting = false;
        };
    }

    private void FixedUpdate()
    {
        if (!player) return;
        currentAngle += angularSpeed * Time.deltaTime;
        Vector2 offset = new Vector2 (Mathf.Sin(currentAngle), Mathf.Cos(currentAngle)) * circleRad;
        
        this.transform.position = Vector2.Lerp(
            this.transform.position,
            (Vector2)player.transform.position + new Vector2(-1,0.5f) + offset,
            Time.deltaTime * 2
        );
    }
    
    private void UpdateSprite()
    {
        spriteRenderer.sprite = (isShooting) ? 
            spriteMap[MouseDirectionToUnitVector()] :
            spriteMap[player.FacingDirection];
    }

    private void Shoot()
    {
        if (!canShoot) return;
        canShoot = false;
        
        //Shoot
        PlayerProjectile p = Instantiate(projectile, transform.position, Quaternion.identity);
        p.GetComponent<PlayerProjectile>().transform.right = GetMouseDirection();
        StartCoroutine(ShootCooldown());
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 droneScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        return (mousePos - droneScreenPoint).normalized;
    }

    private Vector2 MouseDirectionToUnitVector()
    {
        Vector2[] directionVectors = {Vector2.up, Vector2.down, Vector2.left, Vector2.right};

        foreach (Vector2 v in directionVectors)
        {
            float delta = Vector2.Dot(GetMouseDirection(), v);
            if (delta >= Mathf.Sqrt(2)/ 2)
            {
                return v;
            }
        }
        return Vector2.down;
    }

    private void Die()
    {
        Tween.Scale(this.transform, 1.2f, 0f, 0.5f).OnComplete(() => {
            Destroy(gameObject);
        });
    }

}

