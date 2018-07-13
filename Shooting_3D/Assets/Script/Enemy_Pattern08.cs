using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern08 : Enemy_Pattern_Base
{
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
    
    // 레이저 긁기 패턴
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        GameObject[] Target = GameObject.FindGameObjectsWithTag("Player");

        float Angle, DirectionAngle;

        float WarningTime = 2f;

        int Direction = 1;

        while (true)
        {
            Angle = Bullet_Homing_Script.getAngle(Launchers[0].position, Target[Target.Length - 1].transform.position);

            WarningLine.GetComponent<Bullet_Laser_Script>().DestroyTime = WarningTime;

            Instantiate(WarningLine, Launchers[0].position, Quaternion.Euler(0, 0, -Angle));
            Debug.Log("Player Position : " + Target[Target.Length - 1].transform.position.x + " " + Target[Target.Length - 1].transform.position.y
                + ", Angle : " + Angle);
            yield return new WaitForSeconds(WarningTime);

            DirectionAngle = Bullet_Homing_Script.getAngle(Launchers[0].position, Target[Target.Length - 1].transform.position);
            if (DirectionAngle - Angle < 0 || DirectionAngle - Angle > 180)
                Laser.GetComponent<Bullet_Laser_Script>().Direction = 1;
            else
                Laser.GetComponent<Bullet_Laser_Script>().Direction = -1;
            Laser.GetComponent<Bullet_Laser_Script>().DestroyTime = 999;
            Laser.GetComponent<Bullet_Laser_Script>().DragDegree = 4 * Difficulty_Amount;
            Laser.GetComponent<Bullet_Laser_Script>().DragSpeed = 0.05f * Difficulty_Speed;
            Laser.transform.localScale = new Vector3(1 + Difficulty_Amount, Laser.transform.localScale.y, Laser.transform.localScale.z);

            Instantiate(Laser, Launchers[0].position, Quaternion.Euler(0, 0, -Angle));
            Debug.Log("Player Position : " + Target[Target.Length - 1].transform.position.x + " " + Target[Target.Length - 1].transform.position.y
                + ", Angle : " + DirectionAngle + ", Direction : " + Direction);
            yield return new WaitForSeconds(1.8f - 0.2f * Difficulty_Amount);
        }
    }

    public int getAmount(int difficulty) { return BaseAmountFactor + (int)(BulletAmountFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}