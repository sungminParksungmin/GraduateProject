using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern11 : Enemy_Pattern_Base {
    public GameObject Missile;

    private int BaseAmountFactor = 5;
    private int BulletAmountFactor = 1;
    private int BaseSpeedFactor = 25;
    private int BulletSpeedFactor = 2;


    // Use this for initialization
    void Start()
    {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    // 원형 발사 패턴
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        int amount = getAmount(Difficulty_Amount);
        Missile.GetComponent<Bullet_Straight_Script>().MoveSpeed = getSpeed(Difficulty_Speed);

        GameObject[] Target = GameObject.FindGameObjectsWithTag("Player");
        float[] Angle = new float[2], AngleStore = {999, 999};
        float interval;
        float period = 0.12f - 0.005f * Difficulty_Amount;

        Angle[0] = Bullet_Homing_Script.getAngle(Launchers[0].position, Target[Target.Length - 1].transform.position) - 45;
        Angle[1] = Angle[0] + 90;
        while (true)
        {
            interval = (Angle[1] - Angle[0]) / amount;
            for(int i = 0; i < amount; i++)
            {
                if(i >= 2)
                    Instantiate(Missile, Launchers[0].position, Quaternion.Euler(0, 0, Angle[0] + (i - 2) * interval));
                else if(AngleStore[i] < 512)
                    Instantiate(Missile, Launchers[0].position, Quaternion.Euler(0, 0, AngleStore[i]));
                Instantiate(Missile, Launchers[0].position, Quaternion.Euler(0, 0, Angle[0] + i * interval));
                yield return new WaitForSeconds(period);
            }
            AngleStore[0] = Angle[0] + (amount - 2) * interval;
            AngleStore[1] = Angle[0] + (amount - 1) * interval;
            Angle[0] = Angle[1];
            Angle[1] = 360 - Bullet_Homing_Script.getAngle(Launchers[0].position, Target[Target.Length - 1].transform.position);
            if (Angle[1] > 360) Angle[1] -= 360;
            if (Angle[1] - Angle[0] > 0)
                Angle[1] += 45;
            else
                Angle[1] -= 45;
        }
    }

    public int getAmount(int difficulty) { return BaseAmountFactor + (BulletAmountFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
