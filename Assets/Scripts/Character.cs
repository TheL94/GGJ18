using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpForce;
    public float JumpMovementAmount;

    float timer;

    Rigidbody2D rigid;
    Animator anim;

    CharacterAction currentAction;

    int sideMovement;

    bool _isGrounded = false;
    bool isGrounded
    {
        get { return _isGrounded; }
        set
        {
            if (!_isGrounded && value)
            {
                if(sideMovement == 1)
                    currentAction = CharacterAction.GoRight;
                if (sideMovement == -1)
                    currentAction = CharacterAction.GoLeft;
            }
            _isGrounded = value;
        }
    }

    #region API
    public void Kill()
    {

    }
    #endregion

    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
	
	void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            currentAction = CharacterAction.Jump;
        else if (Input.GetKeyDown(KeyCode.A))
            currentAction = CharacterAction.GoLeft;
        else if (Input.GetKeyDown(KeyCode.D))
            currentAction = CharacterAction.GoRight;

        ChooseAction(currentAction);
    }

    void ChooseAction(CharacterAction _characterAction)
    {
        switch (_characterAction)
        {
            case CharacterAction.None:
                break;
            case CharacterAction.GoRight:
                Move(1);
                break;
            case CharacterAction.GoLeft:
                Move(-1);
                break;
            case CharacterAction.Jump:
                Jump();
                break;
        }
    }

    void Move(int _direction)
    {
        sideMovement = _direction;
        rigid.MovePosition(rigid.position + new Vector2(_direction * MovementSpeed * Time.deltaTime, 0f));
    }

    void Jump()
    {
        if(isGrounded)
            rigid.AddForce(new Vector2(JumpMovementAmount * sideMovement, JumpForce), ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void DetectCollisionSide(Collision2D _collision)
    {
        Vector3 points = _collision.contacts[0].point;
        
    }
}

public enum CharacterAction
{
    None = -1,
    GoRight = 0,
    GoLeft = 1,
    Jump = 2
}