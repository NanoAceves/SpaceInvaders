using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvadersManager : MonoBehaviour
{
    public int rows;
    public int cols;
    public Invader[] prefabs;
    public AnimationCurve speed;

    private Vector3 _direction=Vector3.right;

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    public int totalInvaders => rows * cols;
    public int amountKilled { get; private set; }
    public float percentKilled=>(float)amountKilled/(float)totalInvaders;


    void Awake()
    {
        for(int i =0; i<rows; i++)
        {
            float width = 2.0f * (cols-1);
            float height = 2.0f * (rows - 1);
            Vector2 center = new Vector2((-width/2), (-height/2));
            Vector3 rowPosition = new Vector3(center.x, center.y+i*2.0f, 0.0f);
            for (int j=0; j<cols; j++)
            {
                Invader invader = Instantiate(prefabs[i], transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += j * 2.0f;
                invader.transform.localPosition=position;
            }
        }
    }

    void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        amountKilled = 0;
    }


    void Update()
    {
        transform.position += _direction * speed.Evaluate(percentKilled) * Time.deltaTime;
        foreach(Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (_direction==Vector3.right && invader.position.x>=rightEdge.x-1.0f)
            {
                AdvanceRow();
            }
            else if(_direction==Vector3.left && invader.position.x <= leftEdge.x+1.0f)
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        _direction *= -1;
        Vector3 position = transform.position;
        position.y-=1.0f;
        transform.position = position;
    }

    private void InvaderKilled()
    {
        amountKilled++;
    }
}
