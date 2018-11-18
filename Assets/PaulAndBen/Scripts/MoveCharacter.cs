using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{

    [SerializeField]
    private List<HouseScript> houses = new List<HouseScript>();

    [SerializeField]
    private float speed;

    [SerializeField]
    private Sprite normalHorse;

    [SerializeField]
    private Sprite wheelieHorse;

    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private List<AudioClip> turkeysComingClips = new List<AudioClip>();

    [SerializeField]
    private AudioSource myAudioSource;

    [SerializeField]
    private AudioSource clipclopSource;

    private SpriteRenderer mySpriteRenderer;

    private Transform myTransform;

    private Rigidbody2D myRigidbody;

    private int numHousesWarned;

    private bool canWarn;

    private bool isClopping;

    private bool isFacingRight;

    private bool canMove;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        canWarn = true;
        isFacingRight = true;
    }

    void Update()
    {
        if (canMove)
        {
            CheckForInput();
        }
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
            clipclopSource.Play();
            isClopping = true;
        }
        else if (moveVector == Vector2.zero && isClopping)
        {
            clipclopSource.Stop();
            isClopping = false;
        }
        moveVector = moveVector.normalized;
        myRigidbody.MovePosition(myTransform.position + (Vector3)moveVector * Time.deltaTime * speed);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Flip()
    {
        Vector3 scale = myTransform.localScale;
        scale.x *= -1;
        myTransform.localScale = scale;
    }


    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WarnNearbyHouses();
        }
    }

    private void WarnNearbyHouses()
    {
        if (canWarn)
        {
            for (int i = 0; i < houses.Count; i++)
            {
                houses[i].WarnHouse();
            }
            StartCoroutine(Warning());
        }
    }

    private IEnumerator Warning()
    {
        canWarn = false;
        mySpriteRenderer.sprite = wheelieHorse;
        dialogueBox.SetActive(true);
        myAudioSource.pitch = Random.Range(0.95f, 1.05f);
        myAudioSource.Stop();
        int clipNum = Random.Range(0, turkeysComingClips.Count);
        myAudioSource.clip = turkeysComingClips[clipNum];
        myAudioSource.Play();
        yield return new WaitForSeconds(turkeysComingClips[clipNum].length);
        mySpriteRenderer.sprite = normalHorse;
        dialogueBox.SetActive(false);
        canWarn = true;
    }

    public void MovementStatus(bool _canMove)
    {
        canMove = _canMove;
        clipclopSource.Stop();
        isClopping = false;
    }





}
