using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject); // 자기자신 삭제
        
    }
}
