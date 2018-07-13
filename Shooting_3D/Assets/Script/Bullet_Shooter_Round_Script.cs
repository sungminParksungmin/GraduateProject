using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shooter_Round_Script : MonoBehaviour {
    public float MoveSpeed;
    private float[] DestroyXPos, DestroyYPos;

    public GameObject Missile;
    public float StartTime;
    private float time;
    private bool Shooting = false;
    public int RoundAmount = 1;
    public float RoundSpeed = 1, period = 0.1f;

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

        if (transform.position.x < DestroyXPos[0] ||
            transform.position.x > DestroyXPos[1] ||
            transform.position.y < DestroyYPos[0] ||
            transform.position.y > DestroyYPos[1])
        {
            Shooting = false;
            Destroy(gameObject);
        }

        time += Time.deltaTime;
        if(!Shooting && time > StartTime)
        {
            Shooting = true;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        float angle = 0;
        float interval = 360 / RoundAmount;
        while (Shooting)
        {
            for(int i = 0; i < RoundAmount; i++)
            {
                Instantiate(Missile, transform.position, Quaternion.Euler(0, 0, i * interval + angle));
            }
            angle += RoundSpeed;
            if (angle > interval)
                angle -= interval;
            yield return new WaitForSeconds(period);
        }
    }
}
