using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpForce;
    public float JumpMovementAmount;

    public GameObject FirePrefab;
    public Transform FireSpawn;

    public float FireTime = 0.5f;
    public float StepNoisePeriod = 0.5f;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRend;

    CharacterAction previousAction;
    CharacterAction _currentAction = CharacterAction.None;
    public CharacterAction currentAction
    {
        get { return _currentAction; }
        private set
        {
            previousAction = _currentAction;
            _currentAction = value;
        }
    }

    int sideMovement = 0;

    bool _isGrounded = false;
    bool isGrounded
    {
        get { return _isGrounded; }
        set
        {
            if (!_isGrounded && value)
            {
                if (sideMovement == 1)
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

    void SetFireSpawn(bool right)
    {
        float x = Mathf.Abs(FireSpawn.localPosition.x);
        if (!right)
        {
            x = -x;
        }
        FireSpawn.localPosition = new Vector3(x, FireSpawn.localPosition.y, FireSpawn.localPosition.z);
    }

    void ChooseAction(CharacterAction _characterAction)
    {
        switch (_characterAction)
        {
            case CharacterAction.GoRight:
                Move(1);
                SetFireSpawn(true);
                spriteRend.flipX = true;
                break;
            case CharacterAction.GoLeft:
                Move(-1);
                SetFireSpawn(false);
                spriteRend.flipX = false;
                break;
            case CharacterAction.Jump:
                Jump();
                break;
            case CharacterAction.Fire:
                Fire();
                break;
            case CharacterAction.Idle:
            case CharacterAction.None:
                SetFireSpawn(true);
                break;
        }

        if (currentAction != CharacterAction.None)
        {
            anim.enabled = true;
            anim.SetInteger("AnimState", (int)currentAction);
        }
        else
        {
            anim.enabled = false;
        }
    }

    void Move(int _direction)
    {
        if (isGrounded)
        {
            sideMovement = _direction;
            rigid.MovePosition(rigid.position + new Vector2(_direction * MovementSpeed * Time.deltaTime, 0f));
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            Audio.Play(Audio.Sfx.Jump);            
            rigid.AddForce(new Vector2(JumpMovementAmount * sideMovement, JumpForce), ForceMode2D.Force);
        }
    }

    void Fire()
    {
        GameObject newfire = Instantiate(FirePrefab, FireSpawn.position, FireSpawn.rotation, transform);
        if (spriteRend.flipX) {
            newfire.transform.localEulerAngles = new Vector3(0, 180, 0);
        } else
        {
            newfire.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        Audio.Play(Audio.Sfx.Fire);
        Destroy(newfire, FireTime);
        currentAction = previousAction;
    }

    CollisionDirection DetectCollisionSide(Collider2D bullet)
    {
        CollisionDirection direction = CollisionDirection.None;

        if(bullet.transform.up == Vector3.right)
            direction = CollisionDirection.FromLeft;
        else if (bullet.transform.up == Vector3.left)
            direction = CollisionDirection.FromRight;
        else if (bullet.transform.up == Vector3.up)
            direction = CollisionDirection.FromDown;
        else if (bullet.transform.up == Vector3.down)
            direction = CollisionDirection.FromUp;
        else
            direction = CollisionDirection.None;

        return direction;
    }

    private void SetGrounded(Collision2D groundCollision)
    {
        foreach (ContactPoint2D contact in groundCollision.contacts)
        {
            if (contact.normal.y > Mathf.Abs(contact.normal.x))
            {
                if (currentAction == CharacterAction.Jump)
                {
                    Debug.Log("Landing");
                    Audio.Play(Audio.Sfx.Land);
                }
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            SetGrounded(collision);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            if (currentAction == CharacterAction.GoRight)
            {
                currentAction = CharacterAction.GoLeft;
            }
            else if (currentAction == CharacterAction.GoLeft)
            {
                currentAction = CharacterAction.GoRight;
            }
        }
        else if (collision.gameObject.tag == "Goal")
        {
            GameManager.I.GoToNextLevel();
            if (currentAction != CharacterAction.Jump)
            {
                currentAction = CharacterAction.Jump;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            SetGrounded(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null)
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