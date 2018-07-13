using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern02 : Enemy_Pattern_Base {
    public GameObject Missile;

    private int BaseAmountFactor = 0;
    private int BulletAmountFactor = 1;
    private int BaseSpeedFactor = 15;
    private int BulletSpeedFactor = 3;

    // Use this for initialization
    void Start()
    {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    // 랜덤 비내리기 패턴
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        float period = (0.03f * Difficulty_Amount) + 4f / (Difficulty_Amount + 1);
        float amount = getAmount(Difficulty_Amount);
        Missile.GetComponent<Bullet_Straight_Script>().MoveSpeed = getSpeed(Difficulty_Speed);

        float rand = 0, xPosition, yPosition;
        while (true)
        {
            for (int i = 1; i <= amount / 2; i++)
            {
                rand = Random.Range(0, 0.5f);
                xPosition = Camera.main.ViewportToWorldPoint(new Vector3(rand, 1, -Camera.main.transform.position.z)).x;
                yPosition = Camera.main.ViewportToWorldPoint(new Vector3(rand, 1, -Camera.main.transform.position.z)).y;
                Instantiate(Missile, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 0, 180));
                rand = Random.Range(0.5f, 1);
                xPosition = Camera.main.ViewportToWorldPoint(new Vector3(rand, 1, -Camera.main.transform.position.z)).x;
                yPosition = Camera.main.ViewportToWorldPoint(new Vector3(rand, 1, -Camera.main.transform.position.z)).y;
                Instantiate(Missile, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 0, 180));
            }
            if(amount % 1 != 0)
            {
                if (Random.Range(0f, 1f) >= amount % 1)
                {
                    rand = Random.Range(0f, 1f);
                    xPosition = Camera.main.ViewportToWorldPoint(new Vector3(rand, 1, -Camera.main.transform.position.z)).x;
                    yPosition = Camera.main.ViewportToWorldPoint(new Vector3(rand, 1, -Camera.main.transform.position.z)).y;
                    Instantiate(Missile, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 0, 180));
                }
            }
            yield return new WaitForSeconds(period);
        }
    }

    public float getAmount(int difficulty) { return BaseAmountFactor + (BulletAmountFactor / 2f * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
