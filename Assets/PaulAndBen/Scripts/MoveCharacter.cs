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
    private AudioClip turkeysComing;

    [SerializeField]
    private AudioSource myAudioSource;

    private SpriteRenderer mySpriteRenderer;

    private Transform myTransform;

    private Rigidbody2D myRigidbody;

    private int numHousesWarned;

    private bool canWarn;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        canWarn = true;
    }

    void Update()
    {
        CheckForInput();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        Vector2 moveVector = new Vector2(horizontal, vertical);
        moveVector = moveVector.normalized;
        myRigidbody.MovePosition(myTransform.position + (Vector3)moveVector * Time.deltaTime * speed);
    }

    private void FixedUpdate()
    {
        Move();
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
        myAudioSource.PlayOneShot(turkeysComing);
        yield return new WaitForSeconds(1f);
        mySpriteRenderer.sprite = normalHorse;
        dialogueBox.SetActive(false);
        canWarn = true;
    }



}
