using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public int ghostPoolSize = 10;
    public Material ghostMaterial;

    private SpriteRenderer spriteRenderer;
    private Queue<GameObject> ghostPool = new Queue<GameObject>();
    private Queue<GameObject> spawnedObject = new Queue<GameObject>();

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AddToPool(ghostPoolSize);
    }

    public void AddToPool(int _numberOfGhost)
    {
        for (int i = 0; i < _numberOfGhost; i++)
        {
            GameObject ghost = new GameObject("GhostPoolObject" + gameObject.name);
            ghost.AddComponent<SpriteRenderer>();
            ghost.SetActive(false);
            ghostPool.Enqueue(ghost);

            spawnedObject.Enqueue(ghost);
        }
    }

    public IEnumerator ShowGhost(int _numberOfGhost, float _duration, float _ghostFadeTime = 0.4f)
    {
        if (_numberOfGhost > ghostPool.Count) AddToPool(_numberOfGhost - ghostPool.Count);

        for (int i = 0; i < _numberOfGhost; i++)
        {
            GameObject g = ghostPool.Dequeue();
            g.SetActive(true);
            g.transform.position = transform.position;
            g.transform.localScale = Vector3.one;
            SpriteRenderer s = g.GetComponent<SpriteRenderer>();

            if (ghostMaterial) s.material = ghostMaterial;

            s.flipX = spriteRenderer.flipX;
            s.flipY = spriteRenderer.flipY;
            
            s.sprite = spriteRenderer.sprite;
            s.color = Color.white;

            Tween.Alpha(s, 0, _ghostFadeTime).OnComplete(() => {
                g.SetActive(false);
                ghostPool.Enqueue(g);
            });
            
            yield return new WaitForSeconds(_duration / _numberOfGhost);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < ghostPool.Count; i++)
        {
            Destroy(spawnedObject.Dequeue());
        }
    }
}
