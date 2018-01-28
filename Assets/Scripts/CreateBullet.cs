using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    public Bullet bullet;
    public Transform hotSpot;
    public float timer = 0.5f;
    private float counter;
    private PunchingState punchingState;
    private SpriteRenderer sr;
    private Vector3 initialScale;
    private int initialPunchesToDo;

    // Use this for initialization
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        initialScale = sr.transform.localScale;
        initialPunchesToDo = 2;
        punchingState = PunchingState.Resting;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < 100)
        {
            counter += Time.deltaTime;
        }
        if (counter > 1f && initialPunchesToDo > 0)
        {
            Debug.Log("Initial punch");
            punchingState = PunchingState.Growing;
            initialPunchesToDo--;
            counter = 0f;
        }

        if (punchingState == PunchingState.Growing)
        {
            sr.transform.localScale *= 1.1f;
            if (sr.transform.localScale.x > initialScale.x * 1.25)
            {
                punchingState = PunchingState.Shrinking;
            }
        }
        else if (punchingState == PunchingState.Shrinking)
        {
            sr.transform.localScale *= 0.95f;
            if (sr.transform.localScale.x <= initialScale.x)
            {
                punchingState = PunchingState.Resting;
                sr.transform.localScale = initialScale;
            }
        }
    }

    void OnMouseDown()
    {
        if (counter >= timer)
        {
            //azione
            punchingState = PunchingState.Growing;
            Instantiate(bullet, hotSpot.position, hotSpot.rotation);
            counter = 0;
            Audio.Play(Audio.Sfx.Shoot);
        }
    }

    enum PunchingState
    {
        Resting,
        Growing,
        Shrinking
    }

}
