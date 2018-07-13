using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PatternTest : MonoBehaviour {
    public float startTime = 0f;

    public float time = 0;
    public Transform[] Launchers;
    public GameObject[] SpellLev;
    public GameObject Spell, Spell2, Spell3, Spell4;

    private float hp;
    public int keyHp = 2500;
    
    private bool Level = false, started = false;

    public Texture2D[] images;

    void Start()
    {
        // qhtmrk 0,0, 18 위치에서 대기
//        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(0, 18, 18), "easetype", iTween.EaseType.easeOutCubic, "time", startTime));
        
//        tr = GetComponent<Transform>();
//        for (int i = 0; i == 0; i++)
//            SpellLev[i].SetActive(false);

    }

    void Update()
    {
        hp = GetComponent<Enemy>().enemyData.getHP();
        time = time + Time.deltaTime;
    
        if (startTime < time)
        {
            if(!started)
                SpellShot();
            
            if (Level == false && hp <= keyHp)
            {
                StartCoroutine(SpellClear());
            }
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void SpellShot()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (!Level)
            SpellRun(true);
        else
            SpellRun(false);
    }

    void SpellRun(bool runFirst)
    {
        Debug.Log("Pattern Start");
        started = true;
        if (runFirst)
        {
            Spell.SendMessage("PatternStart", Launchers);
            Spell2.SendMessage("PatternStart", Launchers);
        }
        else
        {
            Spell3.SendMessage("PatternStart", Launchers);
            Spell4.SendMessage("PatternStart", Launchers);
        }
    }

    IEnumerator SpellClear()
    {
        if (!Level)
        {
            Spell.SendMessage("PatternFinish");
            Spell2.SendMessage("PatternFinish");
            Level = true;
        }
        else
        {
            Spell3.SendMessage("PatternFinish");
            Spell4.SendMessage("PatternFinish");
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log("Pattern Cleared");
        started = false;
    }
}
