using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BenMover : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidbody;

    private Transform myTransform;

    private int numTurkeysStuffed;

    private bool isFacingRight;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        isFacingRight = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        if (horizontal < 0 && isFacingRight)
        {
            isFacingRight = false;
            Flip();
        }
        else if (horizontal > 0 && !isFacingRight)
        {
            isFacingRight = true;
            Flip();
        }

        Vector2 moveVector = new Vector2(horizontal, vertical);
        moveVector = moveVector.normalized;
        myRigidbody.MovePosition(myTransform.position + (Vector3)moveVector * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Turkey"))
        {
            _collision.GetComponent<TurkeyMovement>().DestroyTurkey();
            numTurkeysStuffed++;
            CheckEnding();
        }
    }

    private void Flip()
    {
        Vector3 scale = myTransform.localScale;
        scale.x *= -1;
        myTransform.localScale = scale;
    }

    private void CheckEnding()
    {
        if (numTurkeysStuffed == 6)
        {
            SceneManager.LoadScene("FinalCard");
        }
    }

}
