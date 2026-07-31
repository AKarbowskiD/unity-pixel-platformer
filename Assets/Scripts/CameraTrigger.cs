using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraScript cameraScript;
    public float newX = 50f;
    public float bufferTime = 3f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraScript.StartMoveSequence(bufferTime , newX);

            GetComponent<Collider2D>().enabled = false;
        }
    }
}