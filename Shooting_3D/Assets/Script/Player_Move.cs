using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {
    public float CharacterMoveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float movespeed = CharacterMoveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            movespeed = CharacterMoveSpeed / 2;
        else
            movespeed = CharacterMoveSpeed;

        move(movespeed);
        moveClamp();
    }

    private void move(float movespeed)
    {
        if (Input.GetKey(KeyCode.UpArrow))
            this.transform.Translate(new Vector3(0, 1) * movespeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            this.transform.Translate(new Vector3(0, -1) * movespeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            ;
        else if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.Translate(new Vector3(-1, 0, 0) * movespeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            this.transform.Translate(new Vector3(1, 0, 0) * movespeed * Time.deltaTime);
    }

    private void moveClamp()
    {
        // 메인 카메라의 왼쪽하단 월드 좌표를 뷰포트로부터 얻기
        Vector2 left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
        // 메인 카메라의 오른쪽상단 월드 좌표를 뷰포트로부터 얻기
        Vector2 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z));
        // 현재 플레이어의 좌표를 얻기
        Vector2 player = transform.position;

        // 플레이어의 X좌표를 카메라에서 왼쪽으로 0.8F만큼, 오른쪽으로 0.8F만큼 간격을 주고 제한하기
        player.x = Mathf.Clamp(player.x, left.x + 3F, right.x - 3F);

        // 플레이어의 Y좌표를 카메라에서 위로부터 3만큼, 아래로부터 3만큼 간격을 주고 제한하기
        player.y = Mathf.Clamp(player.y, left.y + 3, right.y - 3);

        // 제한된 값을 플레이어의 좌표에 적용
        transform.position = player;

    }
}
