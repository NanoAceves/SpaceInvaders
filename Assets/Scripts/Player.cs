using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float sideSeparation=0.5f;
    public Bullet laser;

    private Collider2D PlayerCollider;
    private float HorizontalMovement;
    private float minX, maxX;
    private float restrictX;

    private Vector3 newPosition;
    private float bulletPosY;
    private bool _laserActive;
    

    void Start()
    {
        PlayerCollider = GetComponent<Collider2D>();
        minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x+PlayerCollider.bounds.extents.x+sideSeparation;
        maxX= Camera.main.ViewportToWorldPoint(Vector3.right).x - PlayerCollider.bounds.extents.x - sideSeparation;
        bulletPosY = transform.position.y + PlayerCollider.bounds.extents.y;
        _laserActive = false;
    }


    void Update()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        newPosition = transform.position += Vector3.right * HorizontalMovement * speed * Time.deltaTime;
        restrictX=Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = new Vector3(restrictX, transform.position.y, transform.position.z);

        if (Input.GetButtonDown("Jump"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!_laserActive)
        {
            Bullet laserCopy = Instantiate(laser, new Vector3(transform.position.x, bulletPosY, transform.position.z), Quaternion.identity);
            laserCopy.destroyed += LaserDestroyed;
            _laserActive = true;
        }
        
    }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }
}
