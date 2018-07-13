using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern03 : Enemy_Pattern_Base {
    public GameObject Missile;

    private int BaseAmountFactor = 4;
    private float BulletAmountFactor = 0.5f;
    private int BaseSpeedFactor = 25;
    private int BulletSpeedFactor = 2;
    
    // Use this for initialization
    void Start()
    {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    // 랜덤 비내리기 패턴
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        float period = 4f / (Difficulty_Amount + 1);
        int amount = getAmount(Difficulty_Amount);

        Missile.GetComponent<Bullet_Straight_Script>().MoveSpeed = getSpeed(Difficulty_Speed);

        float[] xRanges = new float[2];
        xRanges[0] = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, -Camera.main.transform.position.z)).x;
        xRanges[1] = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z)).x;
        float interval = (xRanges[1] - xRanges[0]) / amount;
        float xPosition, yPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z)).y;

        int round = 0;
        while (true)
        {
            for (int i = 0; i <= amount; i++)
            {
                xPosition = xRanges[0] + ((i + round * 0.25f) * interval);
                Instantiate(Missile, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 0, 180));
            }
            round++;
            if (round >= 4) round = 0;
            yield return new WaitForSeconds(period);
        }
    }

    public int getAmount(int difficulty) { return BaseAmountFactor + (int)(BulletAmountFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
