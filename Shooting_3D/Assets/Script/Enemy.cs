using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    public Enemy_Data enemyData;

    // Use this for initialization
    void Start()
    {
        enemyData = new Enemy_Data(HP);
        Debug.Log(gameObject.name + "의 체력 : " + enemyData.getHP());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딛히는 collision을 가진 객체의 태그가 "Player Missile"일 경우
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            
            // 총알의 공격력만큼 체력을 감소
            enemyData.decreaseHP(collision.gameObject.GetComponent<Player_Shots>().power);
            //Debug.Log(gameObject.name + "의 현재 체력 : " + enemyData.getHP());

            if (enemyData.getHP() <= 0)
                Destroy(gameObject);
        }
    }
}