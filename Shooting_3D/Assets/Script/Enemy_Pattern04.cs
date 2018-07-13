using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern04 : Enemy_Pattern_Base {
    public GameObject Missile;

    private int BaseAmountFactor = 2;
    private float BulletAmountFactor = 0.5f;
    private int BaseSpeedFactor = 25;
    private int BulletSpeedFactor = 2;

    // Use this for initialization
    void Start () {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    override public IEnumerator Pattern(Transform[] Launchers)
    {
        float rand;
        float period = 6f / (Difficulty_Amount + 1);
        int amount = getAmount(Difficulty_Amount);
        float interval = 360f / amount;

        Missile.GetComponent<Enemy_Bullet_Reflexive>().MoveSpeed = getSpeed(Difficulty_Speed);

        while (true)
        {
            Debug.Log("Launcher Position : " + Launchers[0].position.x + " " + Launchers[0].position.y);
            for (int i = 0; i <= amount; i++)
            {
                rand = Random.Range(0, 360);
                Debug.Log("interval : " + interval + " ,result : " + i * interval);
                Instantiate(Missile, Launchers[0].position, Quaternion.Euler(0, 0, rand));
            }
            yield return new WaitForSeconds(period);
        }
    }

    public int getAmount(int difficulty) { return BaseAmountFactor + (int)(BulletAmountFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
