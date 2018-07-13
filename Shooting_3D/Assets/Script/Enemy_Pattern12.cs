using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern12 : Enemy_Pattern_Base {
    public GameObject Missile;

    private float BaseSizeFactor = 2f;
    private float BulletSizeFactor = 0.3f;
    private int BaseSpeedFactor = 15;
    private int BulletSpeedFactor = 0;
    
    // Use this for initialization
    void Start()
    {
        Difficulty_Amount = 5;
        Difficulty_Speed = 5;
    }

    // 대형 탄환 - 회전 발사!
    override public IEnumerator Pattern(Transform[] Launchers)
    {
        float period = 4;
        float size = getSize(Difficulty_Amount);
        float rand;

        while (true)
        {
            rand = Random.Range(140, 220);
            Missile.transform.localScale = new Vector3(size, size, size);
            Missile.GetComponent<Bullet_Shooter_Round_Script>().MoveSpeed = getSpeed(Difficulty_Speed);
            Instantiate(Missile, Launchers[0].position, Quaternion.Euler(0, 0, rand));
            yield return new WaitForSeconds(period);
        }
    }

    public float getSize(int difficulty) { return BaseSizeFactor + (BulletSizeFactor * difficulty); }
    public int getSpeed(int difficulty) { return BaseSpeedFactor + (BulletSpeedFactor * difficulty); }
}
