using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour {
    private int hit = 0;

	// Use this for initialization
	void Start () {
		
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딛히는 collision을 가진 객체의 태그가 "Player Missile"일 경우
        if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            hit++;
            //Debug.Log("플레이어가 피격되었습니다 - 피격 횟수 : " + hit);
        }
    }
}
