using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    private float horizontalInput; // 수평 입력
    private float verticalInput; // 수직 입력
    private Vector3 moveDirection; // 이동 방향

    void Update()
    {
        // 이동 입력
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // O 키를 눌렀을 때 회전
        if (Input.GetKeyDown(KeyCode.O))
        {
            transform.Rotate(Vector3.up, -90f);
        }
         if (Input.GetKeyDown(KeyCode.P))
        {
            transform.Rotate(Vector3.up, 90f);
        }

        // 현재 카메라의 전방 방향으로 이동 방향 설정
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection.Normalize(); // 정규화하여 이동 방향 벡터를 단위 벡터로 만듦

        // 이동 적용
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}





