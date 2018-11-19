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


    private bool isClopping;

    [SerializeField]
    private GameObject stuffedCanvas;

    [SerializeField]
    private CanvasGroup stuffedGroup;

    [SerializeField]
    private AudioSource stuffedSource;

    [SerializeField]
    private AudioSource gruntsSource;

    [SerializeField]
    private AudioClip stuffedClip;

    [SerializeField]
    private AudioClip gruntsLoopClip;

    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private float stuffedTime;


    private bool fadeInStuffed;

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

        if (moveVector != Vector2.zero && !isClopping)
        {
            myAnimator.SetBool("Run", true);
            gruntsSource.Play();
            isClopping = true;
        }
        else if (moveVector == Vector2.zero && isClopping)
        {
            myAnimator.SetBool("Run", false);
            gruntsSource.Stop();
            isClopping = false;
        }

        moveVector = moveVector.normalized;
        myRigidbody.MovePosition(myTransform.position + (Vector3)moveVector * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Turkey"))
        {
            _collision.GetComponent<TurkeyMovement>().DestroyTurkey();
            numTurkeysStuffed++;
            StartCoroutine(TurkeyCaught());
            
        }
    }

    private void Flip()
    {
        Vector3 scale = myTransform.localScale;
        scale.x *= -1;
        myTransform.localScale = scale;
    }

    private IEnumerator TurkeyCaught()
    {
        speed = 0.2f;
        stuffedSource.pitch = Random.Range(0.95f, 1.05f);
        stuffedSource.Stop();
        stuffedSource.clip = stuffedClip;
        stuffedSource.Play();
        stuffedGroup.alpha = 0;
        fadeInStuffed = true;
        yield return new WaitForSeconds(2f);
        fadeInStuffed = false;
        if (numTurkeysStuffed == 6)
        {
            SceneManager.LoadScene("FinalCard");
        }
        yield return new WaitForSeconds(1f);
        stuffedGroup.alpha = 0;
        speed = 5f;
    }

    private void Update()
    {
        if (fadeInStuffed)
        {
            stuffedGroup.alpha = Mathf.Clamp01(stuffedGroup.alpha + Time.deltaTime / stuffedTime);
        }
        else
        {
            stuffedGroup.alpha = Mathf.Clamp01(stuffedGroup.alpha - Time.deltaTime / stuffedTime);
        }
    }

}
