using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationAmount = 90f; // 회전 각도
    public float animationDuration = 1f; // 애니메이션 지속 시간
    private bool isRotating = false; // 회전 중인지 여부
    private Quaternion targetRotation; // 목표 회전 각도
    private float rotationTimer = 0f; // 회전 타이머

    void Update()
    {
        // 회전 중이 아니고 Q 키를 누르면 회전 시작
        if (!isRotating && Input.GetKeyDown(KeyCode.O))
        {
            StartRotation(Vector3.forward, rotationAmount);
        }
        // 회전 중이 아니고 E 키를 누르면 회전 시작
        else if (!isRotating && Input.GetKeyDown(KeyCode.P))
        {
            StartRotation(Vector3.forward, -rotationAmount);
        }

        // 회전 중일 때
        if (isRotating)
        {
            // 회전 진행
            rotationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(rotationTimer / animationDuration);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);

            // 애니메이션 종료 후 상태 초기화
            if (rotationTimer >= animationDuration)
            {
                isRotating = false;
                rotationTimer = 0f;
            }
        }
    }

    // 회전 시작
    void StartRotation(Vector3 axis, float angle)
    {
        isRotating = true;
        targetRotation = transform.rotation * Quaternion.AngleAxis(angle, axis);
    }
}

