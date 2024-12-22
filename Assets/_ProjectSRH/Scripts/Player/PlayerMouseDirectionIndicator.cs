using UnityEngine;

public class PlayerMouseDirectionIndicator : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        this.transform.right = player.mouseDirection;
    }
}
