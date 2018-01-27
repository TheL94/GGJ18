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

    CharacterAction currentAction = CharacterAction.None;

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
        else if (Input.GetKeyDown(KeyCode.F))
            currentAction = CharacterAction.Fire;

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
            case CharacterAction.Fire:
                Fire();
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

    void Fire()
    {

    }

    CollisionDirection DetectCollisionSide(Collider2D _collider)
    {
        CollisionDirection direction = CollisionDirection.None;

        Vector2 difference = transform.position - _collider.transform.position;
        
        if(Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
        {
            if (difference.x > 0)
                direction = CollisionDirection.FromRight;
            else
                direction = CollisionDirection.FromLeft;
        }
        else
        {
            if (difference.y > 0)
                direction = CollisionDirection.FromUp;
            else
                direction = CollisionDirection.FromDown;
        }


        Debug.Log(direction);
        return direction;
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
        if(collision.GetComponent<Bullet>() != null)
        {
            switch (DetectCollisionSide(collision))
            {
                case CollisionDirection.FromUp:
                    ChooseAction(CharacterAction.Fire);
                    break;
                case CollisionDirection.FromDown:
                    ChooseAction(CharacterAction.Jump);
                    break;
                case CollisionDirection.FromLeft:
                    ChooseAction(CharacterAction.GoRight);
                    break;
                case CollisionDirection.FromRight:
                    ChooseAction(CharacterAction.GoLeft);
                    break;
            }
        }
    }

}

public enum CollisionDirection
{
    None,
    FromUp,
    FromDown,
    FromLeft,
    FromRight
}

public enum CharacterAction
{
    None = -1,
    GoRight = 0,
    GoLeft = 1,
    Jump = 2,
    Fire = 3
}