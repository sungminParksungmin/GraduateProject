using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pattern_Control : MonoBehaviour {
    public float startTime = 5.0f;

    public float time = 0;
    public Transform[] Launchers;
    public GameObject[] SpellLev;
    public GameObject Spell, Spell2;

    private float hp;
    public int[] keyHp;

    public int level = 0;
    private int maxLevel;

    public Texture2D[] images;

    void Start()
    {
        // qhtmrk 0,0, 18 위치에서 대기
//        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(0, 18, 18), "easetype", iTween.EaseType.easeOutCubic, "time", startTime));
        
//        tr = GetComponent<Transform>();
        maxLevel = keyHp.Length + 1;
        Debug.Log("MaxLevel : " + maxLevel);
        for (int i = 0; i == (maxLevel - 1); i++)
            SpellLev[i].SetActive(false);
    }

    void Update()
    {
        hp = GetComponent<Enemy>().enemyData.getHP();
        time = time + Time.deltaTime;
    
        if (startTime < time)
        {
            if (level == maxLevel)
            {
//              GetComponent<Enemy>().BossClear();
            }
            else if (Spell == null && Spell2 == null)
                // 스펠 오브젝트가 비어 있을때 스펠 샷 함수 발동.
                SpellShot();
            
            if (keyHp.Length > level)
            {
                if (hp <= keyHp[level])
                {
                    //StartCoroutine(SpellClear());
                    PatternClear();
                }
            }
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void SpellShot()
    {
        int[] rand = new int[2];
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        rand[0] = Random.Range(0, SpellLev.Length);
        rand[1] = Random.Range(0, SpellLev.Length);
        while(rand[0] == rand[1])
            rand[1] = Random.Range(0, 7);

        Spell = SpellLev[rand[0]];
        Spell2 = SpellLev[rand[1]];
        Spell.SendMessage("PatternStart", Launchers);
        Spell2.SendMessage("PatternStart", Launchers);
    }
    
    void PatternClear()
    {
        level++;
        Spell.SendMessage("PatternFinish");
        Spell2.SendMessage("PatternFinish");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("PatternRelpace", 2.0f);
    }
    void PatternRelpace()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Spell = null;
        Spell2 = null;
        Debug.Log("Spell Cleared");
    }
}