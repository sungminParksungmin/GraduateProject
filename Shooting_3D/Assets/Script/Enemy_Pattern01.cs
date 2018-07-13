using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Pattern01 : Enemy_Pattern_Base {
    public GameObject Missile;

    private int BaseAmountFactor = 8;
    private int BulletAmountFactor = 3;
    private int BaseSpeedFactor = 15;
    private int BulletSpeedFactor = 2;
    
	// Use this for initialization
	void Start () {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
	}

    // 원형 발사 패턴
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        float rand;
        float period = 6f / (Difficulty_Amount + 1);
        int amount = getAmount(Difficulty_Amount);
        float interval = 360f / amount;
        Missile.GetComponent<Bullet_Straight_Script>().MoveSpeed = getSpeed(Difficulty_Speed);
        
        while (true)
        {
            Debug.Log("Launcher Position : " + Launchers[0].position.x + " " + Launchers[0].position.y);
            rand = Random.Range(0, interval);
            for (int i = 0; i <= amount; i++)
            {
                Debug.Log("interval : " + interval + " ,result : " + i * interval);
                Instantiate(Missile, Launchers[0].position, Quaternion.Euler(0, 0, (i * interval) + rand));
            }
            yield return new WaitForSeconds(period);
        }
    }

    public int getAmount(int difficulty) { return BaseAmountFactor + (BulletAmountFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
