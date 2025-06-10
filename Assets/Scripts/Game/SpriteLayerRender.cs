using System.Collections;
using UnityEngine;

public class SpriteLayerRender : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer storeRenderer;

    private int orderInFront = -1;    // Store behind player
    private int orderBehind = 2;     // Store in front of player

    private void Awake()
    {
        storeRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameManager.Instance?.Player;
            return;
        }

        if (player.transform.position.y > transform.position.y)
        {
            storeRenderer.sortingOrder = orderBehind;
        }
        else
        {
            storeRenderer.sortingOrder = orderInFront;
        }
    }
}
