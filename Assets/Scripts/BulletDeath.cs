using UnityEngine;

class BulletDeath : Death
{
    void Start()
    {
        BaseStart();
        if (GetComponentInChildren<SpriteRenderer>())
        {
            GetComponentInChildren<SpriteRenderer>().sortingOrder += 1000;
        }
        if (GetComponent<Bullet>())
        {
            GetComponent<Bullet>().SlowDown();
        }
    }
}
