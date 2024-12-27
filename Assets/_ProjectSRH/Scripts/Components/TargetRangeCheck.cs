using System;
using UnityEngine;

public class TargetRangeCheck : MonoBehaviour
{
    public GameObject target;

    public event Action<bool> OnTargetEnter;

    private  void OnTriggerEnter2D(Collider2D other)
    {
        if (target && target.CompareTag(other.tag))
        {
            OnTargetEnter?.Invoke(true);
        }
    }

    private  void OnTriggerExit2D(Collider2D other)
    {
        if (target && target.CompareTag(other.tag))
        {
            OnTargetEnter?.Invoke(false);
        }
    }
}
