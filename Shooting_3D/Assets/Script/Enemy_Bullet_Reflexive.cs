using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Reflexive : MonoBehaviour {
    public float MoveSpeed;
    private int reflex = 1;
    private float[] DestroyXPos, DestroyYPos;

    // Use this for initialization
    void Start()
    {
        DestroyXPos = new float[2];
        DestroyYPos = new float[2];
        Vector2 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
        Vector2 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z));
        DestroyXPos[0] = left.x;
        DestroyXPos[1] = right.x;
        DestroyYPos[0] = left.y;
        DestroyYPos[1] = right.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        if (transform.position.x < DestroyXPos[0] ||
            transform.position.x > DestroyXPos[1])
        {
            if (reflex > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, -transform.eulerAngles.z);
                reflex--;
            }
            else
                Destroy(gameObject);
            //Debug.Log("Destroyed bullet : " + transform.position.x + " " + transform.position.y);
        }


        if (transform.position.y < DestroyYPos[0] ||
            transform.position.y > DestroyYPos[1])
        {
            if(reflex > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180 - transform.eulerAngles.z);
                reflex--;
            }
            else
                Destroy(gameObject);
            //Debug.Log("Destroyed bullet : " + transform.position.x + " " + transform.position.y);
        }
    }
}
