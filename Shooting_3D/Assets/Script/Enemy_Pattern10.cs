using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern10 : Enemy_Pattern_Base {
    public GameObject Missile;
    public int ShootingType = 1;

    private float BaseAmountFactor = 15;
    private float BulletAmountFactor = 5;
    private float BaseSpeedFactor = 20;
    private float BulletSpeedFactor = 1.75f;


    // Use this for initialization
    void Start()
    {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    // 뒤에서 쏘기 패턴
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        float amount = getAmount(Difficulty_Amount);
        float period = 3 / amount;
        float BaseSpeed = getSpeed(Difficulty_Speed);
        float SpeedRange = getSpeed(Difficulty_Speed);
        float Speed;
        Missile.GetComponent<Bullet_Braking_Script>().BrakingRate = 0.5f;

        Vector3 ShootPoint;
        
        while (true)
        {
            for (int i = 1; i < amount; i++)
            {
                if(ShootingType == 1)
                    ShootPoint = Camera.main.ViewportToWorldPoint(new Vector3(i / amount, 0, -Camera.main.transform.position.z));
                else
                    ShootPoint = Camera.main.ViewportToWorldPoint(new Vector3(1 - (i / amount), 0, -Camera.main.transform.position.z));
                Speed = Random.Range(BaseSpeed, BaseSpeed + SpeedRange);
                Missile.GetComponent<Bullet_Braking_Script>().MoveSpeed = Speed;
                Instantiate(Missile, ShootPoint, Quaternion.Euler(0, 0, 0));
                yield return new WaitForSeconds(period);
            }
        }
    }

    public float getAmount(int difficulty) { return BaseAmountFactor + (BulletAmountFactor / 2f * difficulty); }
    public float getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
