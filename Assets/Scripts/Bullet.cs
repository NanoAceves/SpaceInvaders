using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 _direction;
    public float speed;
    public System.Action destroyed;

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        destroyed.Invoke();
        Destroy(this.gameObject);
    }
}
