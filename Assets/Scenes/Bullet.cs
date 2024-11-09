using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 15f;
    public float angle = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
        this.transform.position += bulletSpeed * Time.deltaTime * direction; // direction;
    }
}
