using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Braking_Script : MonoBehaviour {
    public float MoveSpeed, BrakingRate;
    private float[] DestroyXPos, DestroyYPos;

    // Use this for initialization
    void Start()
    {
        DestroyXPos = new float[2];
        DestroyYPos = new float[2];
        Vector2 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
        Vector2 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z));
        DestroyXPos[0] = left.x * 1.05f;
        DestroyXPos[1] = right.x * 1.05f;
        DestroyYPos[0] = left.y * 1.05f;
        DestroyYPos[1] = right.y * 1.05f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        MoveSpeed -= BrakingRate;
        if (MoveSpeed <= 0)
            Destroy(gameObject);
        else if (transform.position.x < DestroyXPos[0] ||
            transform.position.x > DestroyXPos[1] ||
            transform.position.y < DestroyYPos[0] ||
            transform.position.y > DestroyYPos[1])
        {
            Destroy(gameObject);
            //Debug.Log("Destroyed bullet : " + transform.position.x + " " + transform.position.y);
        }
    }
}
