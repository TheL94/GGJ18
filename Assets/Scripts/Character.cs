using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Character : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpForce;
    public float JumpMovementAmount;

    public GameObject FirePrefab;
    public Transform FireSpawn;

    public float FireTime;

    float timer;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRend;

    bool spriteFlip;

    CharacterAction previousAction;
    CharacterAction _currentAction = CharacterAction.None;
    CharacterAction currentAction
    {
        get { return _currentAction; }
        set
        {
            previousAction = _currentAction;
            _currentAction = value;
        }
    }

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
                else if (sideMovement == -1)
                    currentAction = CharacterAction.GoLeft;
                else
                    currentAction = CharacterAction.Idle;
            }
            _isGrounded = value;
        }
    }

    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.F))
            currentAction = CharacterAction.Fire;
        if (Input.GetKeyDown(KeyCode.D))
            currentAction = CharacterAction.GoRight;
        if (Input.GetKeyDown(KeyCode.A))
            currentAction = CharacterAction.GoLeft;
        if (Input.GetKeyDown(KeyCode.Space))
            currentAction = CharacterAction.Jump;
        ChooseAction(currentAction);
    }

    void ChooseAction(CharacterAction _characterAction)
    {
        switch (_characterAction)
        {
            case CharacterAction.GoRight:
                Move(1);
                FireSpawn.position = new Vector3(Mathf.Abs(FireSpawn.position.x), FireSpawn.position.y, FireSpawn.position.z);
                if(!spriteRend.flipX)
                    spriteRend.flipX = true;
                break;
            case CharacterAction.GoLeft:
                Move(-1);
                FireSpawn.position = new Vector3(-FireSpawn.position.x, FireSpawn.position.y, FireSpawn.position.z);
                if (spriteRend.flipX)
                    spriteRend.flipX = false;
                break;
            case CharacterAction.Jump:
                Jump();
                break;
            case CharacterAction.Fire:
                Fire();
                break;
        }

        anim.SetInteger("AnimState", (int)currentAction);
    }

    void Move(int _direction)
    {
        if (isGrounded)
        {
            sideMovement = _direction;
            //rigid.velocity = rigid.velocity + Vector2.right * (_direction * MovementSpeed);
            rigid.MovePosition(rigid.position + new Vector2(_direction * MovementSpeed * Time.deltaTime, 0f));
        }
    }

    void Jump()
    {
        if(isGrounded)
            rigid.AddForce(new Vector2(JumpMovementAmount * sideMovement, JumpForce), ForceMode2D.Force);
    }

    void Fire()
    {
        GameObject newfire = Instantiate(FirePrefab, FireSpawn.position, FireSpawn.rotation);
        Destroy(newfire, FireTime);
        currentAction = previousAction;
    }

    CollisionDirection DetectCollisionSide(Collider2D _collider)
    {
        CollisionDirection direction = CollisionDirection.None;

        if(_collider.transform.up == Vector3.right)
            direction = CollisionDirection.FromLeft;
        else if (_collider.transform.up == Vector3.left)
            direction = CollisionDirection.FromRight;
        else if (_collider.transform.up == Vector3.up)
            direction = CollisionDirection.FromDown;
        else if (_collider.transform.up == Vector3.down)
            direction = CollisionDirection.FromUp;
        else
            direction = CollisionDirection.None;

        return direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Tock");
            if (currentAction == CharacterAction.GoRight)
            {
                currentAction = CharacterAction.GoLeft;
            }
            else if (currentAction == CharacterAction.GoLeft)
            {
                currentAction = CharacterAction.GoRight;
            }
        }
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

    private void OnDestroy()
    {
        // SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Bullet>() != null)
        {
            Destroy(collision.gameObject);
            switch (DetectCollisionSide(collision))
            {
                case CollisionDirection.FromUp:
                    currentAction = CharacterAction.Fire;
                    break;
                case CollisionDirection.FromDown:
                    currentAction = CharacterAction.Jump;
                    break;
                case CollisionDirection.FromLeft:
                    currentAction = CharacterAction.GoRight;
                    break;
                case CollisionDirection.FromRight:
                    currentAction = CharacterAction.GoLeft;
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
    Idle = 0,
    GoRight = 1,
    GoLeft = 2,
    Jump = 3,
    Fire = 4
}