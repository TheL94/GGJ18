using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Burning burnable = collider.gameObject.GetComponent<Burning>();
        if (burnable) {
            burnable.enabled = true;
        }
    }
}
