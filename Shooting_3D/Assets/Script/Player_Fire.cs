using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fire : MonoBehaviour {
    public GameObject Player_Missile;
    public GameObject Player_TargetLaser;
    public Transform MissileLauncher_Left;
    public Transform MissileLauncher_Mid;
    public Transform MissileLauncher_Right;
    public float FireDelay_Missile, FireDelay_Laser;
    private bool FireState_Missile, FireState_Laser, temp;
    

	// Use this for initialization
	void Start () {
        FireState_Missile = true;
        FireState_Laser = true;
	}
	
	// Update is called once per frame
	void Update () {
        playerFire();
	}

    private void playerFire()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (FireState_Missile)
            {

                StartCoroutine(FireCycleControl(FireState_Missile, FireDelay_Missile, (temp) => FireState_Missile = temp));
                //                Instantiate(Player_Missile, MissileLauncher_Mid.position, MissileLauncher_Mid.rotation);
                Instantiate(Player_Missile, MissileLauncher_Mid.position, Quaternion.Euler(0, 0, 2));
                Instantiate(Player_Missile, MissileLauncher_Mid.position, Quaternion.Euler(0, 0, -2));
                // 좌측으로 발사되는 총알
                Instantiate(Player_Missile, MissileLauncher_Left.position, Quaternion.Euler(0, 0, 5));
                // 우측으로 발사되는 총알
                Instantiate(Player_Missile, MissileLauncher_Right.position, Quaternion.Euler(0, 0, -5));
            }
            if (FireState_Laser)
            {
                StartCoroutine(FireCycleControl(FireState_Laser, FireDelay_Laser, (temp) => FireState_Laser = temp));
                /*                GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
                                if (Enemies.Length > 0)
                                {

                                    Vector3 EnemyPosition = Enemies[Enemies.Length - 1].transform.position;
                                    //                Vector3 EnemyPosition = GameObject.FindWithTag("Enemy").transform.position;
                                    float angle = HomingScript.getAngle(MissileLauncher_Left.position, EnemyPosition);
                                    Instantiate(Player_TargetLaser, MissileLauncher_Left.position, Quaternion.Euler(0, 0, -angle));
                                    angle = HomingScript.getAngle(MissileLauncher_Right.position, EnemyPosition);
                                    Instantiate(Player_TargetLaser, MissileLauncher_Right.position, Quaternion.Euler(0, 0, -angle));
                                }
                                else
                                {
                                    Instantiate(Player_TargetLaser, MissileLauncher_Left.position, Quaternion.Euler(0, 0, 30));
                                    Instantiate(Player_TargetLaser, MissileLauncher_Right.position, Quaternion.Euler(0, 0, -30));
                                }
                //                Debug.Log("player : " + transform.position.x + " " + transform.position.y + " enemy : " + EnemyPosition.x + " " + EnemyPosition.y + " angle : " + angle);
                  */
                Instantiate(Player_TargetLaser, MissileLauncher_Left.position, Quaternion.Euler(0, 0, 30));
                Instantiate(Player_TargetLaser, MissileLauncher_Right.position, Quaternion.Euler(0, 0, -30));

            }
        }
    }
    IEnumerator FireCycleControl(bool state, float delay, System.Action<bool> action)
    {
        // 처음에 FireState를 false로 만들고
        state = false;
        action(state);
        // FireDelay_Missile초 후에
        yield return new WaitForSeconds(delay);
        // FireState를 true로 만든다.
        state = true;
        action(state);
    }
}
