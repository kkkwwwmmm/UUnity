using UnityEngine;
using System.Collections.Generic;

public class MissileLauncher : MonoBehaviour
{
    // 총알 프리팹
    public GameObject missilePrefab;

    // 총알 발사되는 순간의 속도
    public float launchSpeed = 10.0f;

    // 총알이 들어갈 큐
    private Queue<GameObject> missileQueue = new Queue<GameObject>();

    // Update is called once per frame
    void Update()
    {
        // 플레이어가 Fire1 버튼을 눌렀는지를 체크
        if (Input.GetButtonDown("Fire1"))
        {
            // 총알이 10발 이하일 때만 발사
            if (missileQueue.Count < 10)
            {
                // 프리팹으로부터 새로운 총알 게임 오브젝트 생성
                GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);

                // 미사일로부터 리지드바디 2D 컴포넌트 가져옴
                Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();

                // 미사일을 전방으로 발사
                rb.AddForce(transform.right * launchSpeed, ForceMode2D.Impulse);

                // 발사한 총알을 큐에 추가
                missileQueue.Enqueue(missile);
            }
        }

        // 총알이 빨간 상자에 충돌했는지 확인하여 처리
        CheckMissileCollision();
    }

    // 충돌 감지 함수
    void CheckMissileCollision()
    {
        // 모든 총알에 대해 확인
        foreach (var missile in missileQueue)
        {
            // 총알이 존재하지 않으면 다음으로 넘어감
            if (missile == null)
                continue;

            // 총알과 충돌한 물체의 레이어
            LayerMask collisionLayer = LayerMask.GetMask("RedBox");

            // 총알이 빨간 상자에 충돌했는지 확인
            if (Physics2D.OverlapCircle(missile.transform.position, 0.1f, collisionLayer))
            {
                //  총알 파괴
                Destroy(missile);
                // 큐에서 해당 총알 제거
                missileQueue.Dequeue();
            }
        }
    }

    
}

