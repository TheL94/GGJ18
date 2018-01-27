using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpForce;

    Rigidbody2D rigid;

	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Move(Vector2.left);
        if (Input.GetKeyDown(KeyCode.D))
            Move(Vector2.right);
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();        
    }

    void Move(Vector2 _direction)
    {
        //rigid.MovePosition(_direction * MovementSpeed);
        transform.Translate(_direction * MovementSpeed);
    }

    void Jump()
    {
        rigid.AddForce(Vector2.up * JumpForce, ForceMode2D.Force);
    }
}
