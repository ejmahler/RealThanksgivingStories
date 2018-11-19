using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurkeyMovement : MonoBehaviour {


    [SerializeField]
    private Animator myAnimator;

    private void Start()
    {
        myAnimator.SetFloat("Offset", Random.Range(0, 1f));
    }

    public void DestroyTurkey()
    {
        Destroy(this.gameObject);
    }

}
