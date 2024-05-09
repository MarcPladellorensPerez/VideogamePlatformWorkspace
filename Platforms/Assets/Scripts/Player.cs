using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform player; // Player object (make sure to assign it in the Inspector)
    public float moveSpeed = 1f;
    public int moveHorizontal;
    public int moveVertical;


    private Animator anim;

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("VelY", moveVertical);
        anim.SetFloat("VelX", moveHorizontal);

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        player.Translate(moveDirection * Time.deltaTime * moveSpeed);

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            moveVertical = 2;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1;
        }
        else
        {
            moveVertical = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1;
        }
        else
        {
            moveHorizontal = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (moveVertical <= 1)
            {
                anim.SetInteger("AttackNum", Random.Range(1,4));
                anim.SetTrigger("Attack");
            }
        }
    }
}
