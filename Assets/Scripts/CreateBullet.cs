using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    public Bullet bullet;
    public Transform hotSpot;
    public float timer = 0.5f;
    private float counter;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (counter < 100)
            counter += Time.deltaTime;
    }

    void OnMouseDown()
    {
        if (counter >= timer)
        {
            //azione
            Instantiate(bullet, hotSpot.position, hotSpot.rotation);
            counter = 0;
            Audio.Play(Audio.Sfx.Shoot);
        }
    }

}
