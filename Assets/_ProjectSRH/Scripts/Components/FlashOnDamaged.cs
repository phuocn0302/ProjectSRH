using System.Collections;
using UnityEngine;

public class FlashOnDamaged : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private float flashTime = 0.2f;
    private Material material;
    private SpriteRenderer spriteRenderer;

    private Coroutine flashCoroutine;

    private void Awake()
    {
        if (!health) health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    private void OnEnable()
    {
        if (health)
        {
            health.OnHealthDepleted += OnDamaged;
        }
    }

    private void OnDisable()
    {
        if (health)
        {
            health.OnHealthDepleted -= OnDamaged;
        }
    }

    public void OnDamaged()
    {
        if (flashCoroutine != null) StopCoroutine(flashCoroutine); 
        flashCoroutine = StartCoroutine(Flash());
    }

    public IEnumerator Flash()
    {
        if (material.HasProperty("_FlashAmount") && material.GetFloat("_FlashAmount").CompareTo(1) == 1) yield break;
        material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(flashTime);
        material.SetFloat("_FlashAmount", 0);
    }
}
