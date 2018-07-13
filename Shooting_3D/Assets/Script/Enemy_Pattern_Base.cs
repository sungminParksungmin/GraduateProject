using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Pattern_Base : MonoBehaviour {
    public int Difficulty_Amount, Difficulty_Speed;
    IEnumerator coroutine;

    // Use this for initialization
    void Start () {

    }

    public void PatternStart(Transform[] Launchers)
    {
        coroutine = Pattern(Launchers);
        StartCoroutine(coroutine);
    }

    public abstract IEnumerator Pattern(Transform[] Launchers);

    public void PatternFinish()
    {
        Debug.Log("Let's Stop it!");
        StopCoroutine(coroutine);
    }
}
