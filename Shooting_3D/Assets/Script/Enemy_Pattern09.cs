using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern09 : Enemy_Pattern_Base {
    public GameObject WarningLine, Laser;

    private int BaseAmountFactor = 2;
    private float BulletAmountFactor = 0.5f;
    private int BaseSpeedFactor = 30;
    private int BulletSpeedFactor = 1;

    // Use this for initialization
    void Start()
    {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    // 레이저 망 패턴
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        int LaserNum = 4;
        float XPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z)).x;
        float[] YPoint = new float[2];
        YPoint[0] = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, -Camera.main.transform.position.z)).y;
        YPoint[1] = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z)).y;
        float YRange = YPoint[0] - YPoint[1], interval = YRange / LaserNum;
        float FirePointY;
        int OddShot = 1;

        float WarningTime = 2f;

        while (true)
        {
            for(int i = 0; i < LaserNum; i++)
            {
                FirePointY = YPoint[1] + i * interval + (interval / 4);
                if (OddShot == 1)
                    FirePointY += interval / 2;

                WarningLine.GetComponent<Bullet_Laser_Script>().DestroyTime = WarningTime;
                Instantiate(WarningLine, new Vector3(XPoint, FirePointY), Quaternion.Euler(0, 0, -90));
            }
            yield return new WaitForSeconds(WarningTime);
            for (int i = 0; i < LaserNum; i++)
            {
                FirePointY = YPoint[1] + i * interval + (interval / 4);
                if (OddShot == 1)
                    FirePointY += interval / 2;

                Laser.GetComponent<Bullet_Laser_Script>().DestroyTime = 0.1f;
                Laser.GetComponent<Bullet_Laser_Script>().DragDegree = 0;
                Laser.GetComponent<Bullet_Laser_Script>().DragSpeed = 0;
                Laser.transform.localScale = new Vector3((2 + Difficulty_Amount) / 2, Laser.transform.localScale.y, Laser.transform.localScale.z);
                Instantiate(Laser, new Vector3(XPoint, FirePointY), Quaternion.Euler(0, 0, -90));
            }
            if (OddShot == 1)
                OddShot = 0;
            else
                OddShot = 1;
        }
    }

    public int getAmount(int difficulty) { return BaseAmountFactor + (int)(BulletAmountFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
