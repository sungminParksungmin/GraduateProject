using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Laser_Script : MonoBehaviour {
    public float DestroyTime;
    public int startDistance;
    private float time;

    public float DragDegree = 0, DragSpeed = 0;
    private float Dragged = 0;
    public int Direction = 1;

    // Use this for initialization
    void Start () {
        transform.Translate(Vector2.up * startDistance);
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > DestroyTime)
            Destroy(gameObject);
        transform.Translate(Vector2.down * startDistance);
        transform.Rotate(0, 0, DragSpeed * Direction);
        transform.Translate(Vector2.up * startDistance);
        Dragged += DragSpeed;
        if (DragDegree > 0 && Dragged > DragDegree)
            Destroy(gameObject);
    }
}
