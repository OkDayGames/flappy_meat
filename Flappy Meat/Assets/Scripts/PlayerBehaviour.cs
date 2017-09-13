using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public enum PlayerState
{
    ALIVE, DEAD, NONE
}
public class PlayerBehaviour : MonoBehaviour
{
    //---------------------------------------
    
    private PlayerState _currntPlayerState;
    private Rigidbody2D _rb2d;
    private Transform _transform;
    private bool isFlap;
    private Vector3 velocity = Vector2.zero;
    private Vector2 previousPosition = Vector2.zero;
    
    //-------------------
    
    public PlayerState CurrntPlayerState
    {
        get { return _currntPlayerState; }
        set { _currntPlayerState = value; }
    }
    
    //---------------------------------------
    
    public Vector3 gravity;
    public Vector3 velocityFlapp;
    public float maxSpeed;
    
    //---------------------------------------
    
    public void Initialize()
    {
        _rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    
    public void Jump()
    {
        isFlap = true;
        Rotate();
        Rotate();
        previousPosition.y = transform.position.y;
    }

    public void Moved()
    {
        velocity += gravity;
        Flapp();     
        if (velocity.y > 0)
        {
            velocity = Vector2.ClampMagnitude(velocity, maxSpeed); 
        }
        transform.position += velocity * Time.deltaTime;
        
        if (previousPosition.y < transform.position.y)
        {
            previousPosition.y = transform.position.y;
        }
        else
        {
            RotateBack();
        }
    }

    public void OnCheckpointExit()
    {
        GameManager.Instance.Score++;
    }

    public void OnDead()
    {
        CurrntPlayerState = PlayerState.DEAD;
    }

    //-------------------
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Barrier")
        {
            OnDead();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            OnCheckpointExit();       
        }
    }

    //---------------------------------------
    
    private void Flapp()
    {
        if (isFlap)
        {
            if (velocity.y < 0) {
                velocity.y = 0;
            }
            isFlap = false;
            velocity += velocityFlapp;
        }
    }
    
    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, -25f);
    }

    private void RotateBack()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
