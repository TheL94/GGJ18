using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpForce;
    public float JumpMovementAmount;

    int SideMovement = 1;

    Rigidbody2D rigid;
    Animator anim;

	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        else if (Input.GetKey(KeyCode.A))
            Move(-1);
        else if (Input.GetKey(KeyCode.D))
            Move(1);
    }

    void Move(int _direction)
    {
        SideMovement = _direction;
        rigid.MovePosition(rigid.position + new Vector2(_direction * MovementSpeed * Time.deltaTime, 0f));
    }

    void Jump()
    {
        rigid.AddForce(new Vector2(JumpMovementAmount * SideMovement, JumpForce), ForceMode2D.Force);
    }

    public void TransmitAction(CharacterAction _action)
    {

    }

    public enum CharacterAction
    {
        GoRight,
        GoLeft,
        Jump
    }
}
