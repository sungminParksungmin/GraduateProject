using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern05 : Enemy_Pattern_Base {
    public GameObject Missile;

    private float BaseSizeFactor = 3.5f;
    private float BulletSizeFactor = 0.3f;
    private int BaseSpeedFactor = 30;
    private int BulletSpeedFactor = 3;


    // Use this for initialization
    void Start()
    {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    // 대형 탄환 패턴
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        float period = 4f / (Difficulty_Amount + 1);
        float size = getSize(Difficulty_Amount);
        float rand;

        Missile.transform.localScale = new Vector3(size, size, size);
        Missile.GetComponent<Bullet_Straight_Script>().MoveSpeed = getSpeed(Difficulty_Speed);

        while (true)
        {
            for (int i = 0; i < 1; i++)
            {
                rand = Random.Range(120, 240);
                Instantiate(Missile, Launchers[0].position, Quaternion.Euler(0, 0, rand));
            }
            yield return new WaitForSeconds(period);
        }
    }

    public float getSize(int difficulty) { return BaseSizeFactor + (BulletSizeFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
