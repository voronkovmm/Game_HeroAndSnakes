using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tablet : MonoBehaviour
{
    Animator _anim;
    SpriteRenderer _imageAttack;
    GameObject _canvas;

    [SerializeField] Sprite[] spriteTablet;
    [SerializeField] Sprite[] spriteSticks;

    public static event Action EVENT_readTablet;

    void Awake() {
        _anim = GetComponent<Animator>();
        _imageAttack = transform.Find("image_attack").GetComponent<SpriteRenderer>();
        _canvas = transform.Find("canvas").gameObject;
    }

    void Start() {
        SpriteRenderer srTablet = transform.Find("image_tablet").GetComponent<SpriteRenderer>();
        SpriteRenderer srStick = transform.Find("image_stick").GetComponent<SpriteRenderer>();

        int randSticks = UnityEngine.Random.Range(0, spriteSticks.Length);
        int randTablet = UnityEngine.Random.Range(0, spriteTablet.Length);

        srTablet.sprite = spriteTablet[randTablet];
        srStick.sprite = spriteSticks[randSticks];
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            // нужно отобразить "нажать кнопку"
            Sprite sprite = UI.WeaponUI.i?.slots[0]?.sprite;
            if(sprite != null)
                _imageAttack.sprite = sprite;

            // сделать руку активной
            _imageAttack.gameObject.SetActive(true);

            // запустить анимацию
            // запустить событие - если игрок нажимает кнопку атаки - открывается текст сообщения
            EVENT_readTablet?.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(_canvas.activeSelf)
            _canvas.SetActive(false);

        _imageAttack.gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            if(PlayerControl.i.iAttaking)
            {
                _imageAttack.gameObject.SetActive(false);
                _canvas.SetActive(true);
            }
        }
    }
}
