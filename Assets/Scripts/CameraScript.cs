using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraScript : CinemachineExtension
{
    public float currentX = 0f;
    public float minHeightY;
    public bool isLocked = true;
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.RawPosition;

            if (isLocked)
            {
                pos.x = currentX;
            }

            if (pos.y < minHeightY)
            {
                pos.y = minHeightY;
            }

            state.RawPosition = pos;
        }
    }

    public void StartMoveSequence(float waitTime, float targetX)
    {
        StartCoroutine(WaitAndMove(waitTime, targetX));
    }

    IEnumerator WaitAndMove(float waitTime, float targetX)
    {
        isLocked = false;

        while ( Player.movement != 3)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);

        isLocked = true;

        currentX = targetX;
        targetX = targetX + 50f;
    }
}