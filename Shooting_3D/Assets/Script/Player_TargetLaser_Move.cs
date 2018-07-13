using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TargetLaser_Move : MonoBehaviour {
    public float MoveSpeed;
    
    private float[] DestroyXPos, DestroyYPos;

    // Use this for initialization
    void Start () {
        DestroyXPos = new float[2];
        DestroyYPos = new float[2];
        Vector2 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
        Vector2 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z));
        DestroyXPos[0] = left.x;
        DestroyXPos[1] = right.x;
        DestroyYPos[0] = left.y;
        DestroyYPos[1] = right.y;
/*
        Vector3 EnemyPosition = GameObject.FindWithTag("Enemy").transform.position;
        float xDistance = EnemyPosition.x - gameObject.transform.position.x;
        float yDistance = EnemyPosition.y - gameObject.transform.position.y;

        float angle = Mathf.Atan(xDistance / yDistance);

        gameObject.transform.rotation.Set(0, 0, angle, 1);
        */
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        
        if (transform.position.x < DestroyXPos[0] ||
            transform.position.x > DestroyXPos[1] ||
            transform.position.y < DestroyYPos[0] ||
            transform.position.y > DestroyYPos[1])
            Destroy(gameObject);
    }
}
