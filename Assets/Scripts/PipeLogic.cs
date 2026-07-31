using UnityEngine;

public class SimpleDynamicPipe : MonoBehaviour
{
    public SpriteRenderer pipeRenderer;
    public bool isLeftPipe;
    public LayerMask poleLayer;

    void Start()
    {
        float baseAngle = isLeftPipe ? 180f : 0f;

        float randomOffset = Random.Range(-25f, 25f);
        transform.rotation = Quaternion.Euler(0, 0, baseAngle + randomOffset);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f, poleLayer);

        if (hit.collider != null)
        {
            float distance = hit.distance;
            pipeRenderer.size = new Vector2(distance, pipeRenderer.size.y);
        }
        else
        {
            pipeRenderer.enabled = false;
        }
    }
}