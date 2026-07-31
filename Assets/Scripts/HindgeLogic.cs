using UnityEngine;

public class SmartHingeScaler : MonoBehaviour
{
    public LayerMask poleLayer;
    public float maxDetectionRange = 15f;
    public Transform cableTransform;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        AdjustHinge();
    }

    [ContextMenu("Test")]
    public void AdjustHinge()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();

        if (cableTransform == null)
        {
            if (transform.parent != null)
            {
                foreach (Transform child in transform.parent)
                {
                    if (child.name.Contains("Cabel") || child.name.Contains("Cable"))
                    {
                        cableTransform = child;
                        break;
                    }
                }
            }
        }

        if (cableTransform == null) return;

        float cableRotY = cableTransform.localEulerAngles.y;
        if (cableRotY > 180) cableRotY -= 360;

        float genX = PolesGeneration.GeneralX;
        float targetX = Mathf.Abs(cableTransform.position.x - genX) ;
        

        float offsetX = 0f;
        float forcedWidth = -1f;

        if (Mathf.Abs(targetX - 5f) < 0.1f) { offsetX = 0.7f; forcedWidth = 4.5f; }
        else if (Mathf.Abs(targetX - 4f) < 0.1f) { offsetX = 0.95f; forcedWidth = 6f; }
        else if (Mathf.Abs(targetX - 3f) < 0.1f) { offsetX = 1.2f; forcedWidth = 7f; }
        else if (Mathf.Abs(targetX - 2f) < 0.1f) { offsetX = 1.6f; forcedWidth = 8f; }
        else if (Mathf.Abs(targetX - 1f) < 0.1f) { offsetX = 1.9f; forcedWidth = 9.5f; }
        else if (Mathf.Abs(targetX - 0f) < 0.1f) { offsetX = 2.2f; forcedWidth = 10.8f; }

        if (cableTransform.position.x > 0f+genX)
        {
            if (Mathf.Approximately(Mathf.Abs(cableRotY), 180f)) transform.localRotation = Quaternion.Euler(0, 0, 0);
            else transform.localRotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            if (Mathf.Approximately(Mathf.Abs(cableRotY), 180f)) transform.localRotation = Quaternion.Euler(0, 180, 0);
            else transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        float finalOffsetX;
        float currentHingeRot = transform.localEulerAngles.y;
        if (currentHingeRot > 180) currentHingeRot -= 360;

        if (cableTransform.position.x > 0f+ genX)
            finalOffsetX = Mathf.Abs(offsetX);
        else
            finalOffsetX = -Mathf.Abs(offsetX);

        transform.position = new Vector3(
            cableTransform.position.x + finalOffsetX,
            transform.position.y,
            transform.position.z
        );

        sr.drawMode = SpriteDrawMode.Sliced;

        if (forcedWidth > 0)
        {
            sr.size = new Vector2(forcedWidth, sr.size.y);
        }
        else
        {
            bool isLeft = cableTransform.position.x <= 0f + genX;
            Vector2 direction = isLeft ? Vector2.left : Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDetectionRange, poleLayer);

            if (hit.collider != null)
            {
                sr.size = new Vector2(
                    Vector2.Distance(transform.position, hit.point),
                    sr.size.y
                );
            }
        }
    }
}