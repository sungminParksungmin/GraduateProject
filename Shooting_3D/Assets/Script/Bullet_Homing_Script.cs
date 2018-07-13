using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Homing_Script : MonoBehaviour {
    public bool targetPlayer = true;

    private bool started = false;
    public float StartHomingTime = 1f, HomingPeriod = 2f, DiverseAngle = 0;
    private float time = 0f;
    public int HomingCount = 1;
    public float MoveSpeed, DecelerateFactor = 0.5f;

    public bool DestroyAtWall = false;
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
        DestroyYPos[1] = right.y * 1.05f;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        time += Time.deltaTime;
//        Debug.Log("Homing : " + started + ", time : " + time);
        if (started && time > HomingPeriod)
        {
            if (HomingCount > 0)
            {
                Homing();
                MoveSpeed *= DecelerateFactor;
            }
            else
                DestroyAtWall = true;
        }
        else if (!started && time > StartHomingTime)
        {
            Homing();
            started = true;
        }
        else if (DestroyAtWall) {
            if(transform.position.x < DestroyXPos[0] ||
            transform.position.x > DestroyXPos[1] ||
            transform.position.y < DestroyYPos[0] ||
            transform.position.y > DestroyYPos[1])
            Destroy(gameObject);
        }
    }

    public static float getAngle(Vector3 from, Vector3 target)
    {
        float xDistance = target.x - from.x;
        float yDistance = target.y - from.y;
        float angle = Mathf.Atan(xDistance / yDistance) * Mathf.Rad2Deg;
        if (yDistance < 0)
            angle += 180;
        return angle;
    }

    private void Homing()
    {
        GameObject[] Target = null;
        if (targetPlayer)
            Target = GameObject.FindGameObjectsWithTag("Player");
        else
            Target = GameObject.FindGameObjectsWithTag("Enemy");
        if (Target.Length > 0)
        {
            Vector3 TargetPosition = Target[Target.Length - 1].transform.position;
            float angle = getAngle(gameObject.transform.position, TargetPosition);
            float randomRotate = Random.Range(-DiverseAngle, DiverseAngle);
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle + randomRotate));
//            gameObject.transform.rotation.Set(0, 0, -angle + randomRotate, 1);
            time = 0;
            HomingCount--;
        }
    }
}
