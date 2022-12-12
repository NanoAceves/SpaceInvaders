using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;


    private SpriteRenderer _spriteRender;
    private int _animationFrame;

    public System.Action killed;

    void Awake()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _animationFrame = 0;
    }

    void Start()
    {
        InvokeRepeating(nameof(animateInvader), animationTime, animationTime);
    }

    private void animateInvader()
    {
        _animationFrame++;
        if (_animationFrame>=animationSprites.Length)
        {
            _animationFrame = 0;
        }

        _spriteRender.sprite = animationSprites[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Laser"))
        {
            killed.Invoke();
            gameObject.SetActive(false);
            
        }
    }
}
