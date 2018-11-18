using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HouseScript : MonoBehaviour {

    [SerializeField]
    private Sprite houseNormal;

    [SerializeField]
    private Sprite houseWarned;

    private SpriteRenderer mySpriteRenderer;
   
    private bool isWarnable;

    private bool isAlreadyWarned;

    public Action OnHouseWarned;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void WarnHouse()
    {
        if (isWarnable && !isAlreadyWarned)
        {

            if (OnHouseWarned != null)
            {
                OnHouseWarned();
            }

            mySpriteRenderer.sprite = houseWarned;
            isAlreadyWarned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            isWarnable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            isWarnable = false;
        }
    }
}
