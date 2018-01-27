using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    public Bullet bullet;
    public Transform hotSpot;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Object.Instantiate(bullet, hotSpot.position, hotSpot.rotation);
    }

}
