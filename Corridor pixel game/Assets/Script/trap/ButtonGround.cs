using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class ButtonGround : MonoBehaviour
{
    Animator _anim;
    GameObject _canvas;

    public UnityEvent EVENT_buttonPressed;
    
    void Awake() {
        _anim = GetComponent<Animator>();
        _canvas = transform.Find("canvas").gameObject;

    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            _anim.SetTrigger("activate");
            EVENT_buttonPressed?.Invoke();

            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(0.5f);

        _canvas.SetActive(true);

        yield return new WaitForSeconds(1);

        _canvas.SetActive(false);
    }
}
