using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern06 : Enemy_Pattern_Base {
    public GameObject Missile;

    public int ShootingType = 1;

    private int BaseAmountFactor = 3;
    private float BulletAmountFactor = 1f;
    private int BaseSpeedFactor = 25;
    private int BulletSpeedFactor = 2;

    // Use this for initialization
    void Start()
    {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    // 좌우 분할 발사
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        float period = 6f / (Difficulty_Amount + 1);
        int amount = getAmount(Difficulty_Amount);
        float interval = 120f / amount;
        float degree;

        Missile.GetComponent<Bullet_Straight_Script>().MoveSpeed = getSpeed(Difficulty_Speed);

        while (true)
        {
            Debug.Log("Launcher Position : " + Launchers[1].position.x + " " + Launchers[1].position.y);
            for (int i = 0; i <= amount; i++)
            {
                degree = 150 + i * interval;
                Instantiate(Missile, Launchers[1].position, Quaternion.Euler(0, 0, degree));
                Instantiate(Missile, Launchers[2].position, Quaternion.Euler(0, 0, -degree));
                if(ShootingType == 1)
                    yield return new WaitForSeconds(0.02f);
            }
            if (ShootingType == 1)
                yield return new WaitForSeconds(period - amount * 0.02f);
            else if (ShootingType == 2)
                yield return new WaitForSeconds(period);
        }
    }

    public int getAmount(int difficulty) { return BaseAmountFactor + (int)(BulletAmountFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
