using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{

    Animator animator;

    [SerializeField] float blinkMinCooldown;
    [SerializeField] float blinkMaxCooldown;

    float blinkCD;
    float lastBlink;

    #region Unity Functions

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        blinkCD = Random.Range(blinkMinCooldown, blinkMaxCooldown);
    }


    void Update()
    {
        if (Time.time - lastBlink >= blinkCD)
        {
            Blink();
        }
    }

    #endregion

    #region Public Functions

    public void Feed()
    {
        animator.SetTrigger("Eat");
    }

    #endregion

    #region Private Functions
    void Blink()
    {
        animator.SetTrigger("Blink");
        blinkCD = Random.Range(blinkMinCooldown, blinkMaxCooldown);
        lastBlink = Time.time;
    }

    #endregion

}
