using UnityEngine;
using System.Collections.Generic;

public class StackUsingQueue<T>
{
    private Queue<T> queue1 = new Queue<T>();
    private Queue<T> queue2 = new Queue<T>();

    // 스택의 크기를 반환하는 메서드
    public int Count()
    {
        return queue1.Count + queue2.Count;
    }

    // 스택에 요소를 추가하는 메서드
    public void Push(T item)
    {
        // queue2를 비우고 queue1에 요소를 모두 옮깁니다.
        while (queue2.Count > 0)
        {
            queue1.Enqueue(queue2.Dequeue());
        }

        // 새로운 요소를 queue1에 추가합니다.
        queue1.Enqueue(item);
    }

    // 스택에서 요소를 제거하고 반환하는 메서드
    public T Pop()
    {
        // queue1에 있는 요소를 모두 queue2로 옮깁니다.
        while (queue1.Count > 1)
        {
            queue2.Enqueue(queue1.Dequeue());
        }

        // 마지막 요소를 제거하고 반환합니다.
        return queue1.Dequeue();
    }

    // 스택의 맨 위에 있는 요소를 반환하는 메서드
    public T Peek()
    {
        // queue1에 있는 요소를 모두 queue2로 옮깁니다.
        while (queue1.Count > 1)
        {
            queue2.Enqueue(queue1.Dequeue());
        }

        // 마지막 요소를 반환합니다.
        T item = queue1.Peek();

        // queue1에 다시 추가합니다.
        queue2.Enqueue(queue1.Dequeue());

        // queue1과 queue2를 스왑합니다.
        Queue<T> temp = queue1;
        queue1 = queue2;
        queue2 = temp;

        return item;
    }

    // 스택이 비어 있는지 여부를 반환하는 메서드
    public bool IsEmpty()
    {
        return queue1.Count == 0 && queue2.Count == 0;
    }
}

public class MissileLauncher : MonoBehaviour
{
    // 총알 프리팹
    public GameObject missilePrefab;

    // 총알 발사되는 순간의 속도
    public float launchSpeed = 10.0f;

    // 총알이 들어갈 스택
    private StackUsingQueue<GameObject> missileStack = new StackUsingQueue<GameObject>();

    // Update is called once per frame
    void Update()
    {
        // 플레이어가 Fire1 버튼을 눌렀는지를 체크
        if (Input.GetButtonDown("Fire1"))
        {
            // 스택 크기가 10개 이하일 때만 발사
            if (missileStack.Count() < 10)
            {
                // 프리팹으로부터 새로운 총알 게임 오브젝트 생성
                GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);

                // 미사일로부터 리지드바디 2D 컴포넌트 가져옴
                Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();

                // 미사일을 전방으로 발사
                rb.AddForce(transform.right * launchSpeed, ForceMode2D.Impulse);

                // 발사한 총알을 스택에 추가
                missileStack.Push(missile);
            }
        }
    }
  
}





      
    

 
    


