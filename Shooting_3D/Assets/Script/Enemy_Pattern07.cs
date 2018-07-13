using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern07 : Enemy_Pattern_Base {
    public GameObject Missile;

    private int BaseAmountFactor = 2;
    private float BulletAmountFactor = 0.5f;
    private int BaseSpeedFactor = 25;
    private int BulletSpeedFactor = 1;

    // Use this for initialization
    void Start () {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    // 직선 유도 패턴
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        float period = 3f - Difficulty_Speed * 0.1f;
        int amount = getAmount(Difficulty_Amount);
        float interval = 60f / amount;
        float degree;

        while (true)
        {
            for(int i = 0; i < amount; i++)
            {
                Missile.GetComponent<Bullet_Homing_Script>().StartHomingTime = 0.4f + (0.1f * i);
                degree = 60f + interval * i;

                Missile.GetComponent<Bullet_Homing_Script>().targetPlayer = true;
                Missile.GetComponent<Bullet_Homing_Script>().HomingCount = getHomingCount(Difficulty_Amount);
                Missile.GetComponent<Bullet_Homing_Script>().HomingPeriod = getHomingPeriod(Difficulty_Speed);
                Missile.GetComponent<Bullet_Homing_Script>().DiverseAngle = getDiverseAngle(Difficulty_Amount);
                Missile.GetComponent<Bullet_Homing_Script>().MoveSpeed = getSpeed(Difficulty_Speed);
                Missile.GetComponent<Bullet_Homing_Script>().DecelerateFactor = getDecelerateFactor(Difficulty_Speed);
                Missile.GetComponent<Bullet_Homing_Script>().DestroyAtWall = false;
                Instantiate(Missile, Launchers[0].position, Quaternion.Euler(0, 0, degree));
                Instantiate(Missile, Launchers[0].position, Quaternion.Euler(0, 0, -degree));
            }
            yield return new WaitForSeconds(period);
        }
    }

    public int getAmount(int difficulty) { return BaseAmountFactor + (int)(BulletAmountFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
    public int getHomingCount(int difficulty){ return (difficulty / 4) + 2; }
    public float getHomingPeriod(int difficulty) { return 2f - (difficulty * 0.05f); }
    public float getDiverseAngle(int difficulty)
    {
        if (difficulty >= 5)
            return (difficulty - 4) * 2;
        return 0;
    }
    public float getDecelerateFactor(int difficulty) { return 0.73f + (difficulty * 0.03f); }
}
