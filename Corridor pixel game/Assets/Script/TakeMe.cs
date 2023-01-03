using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using UnityEngine.UI;

public class TakeMe : MonoBehaviour
{
    [SerializeField] Item item;

    enum Item
    {
        WEAPON_DAGGER,
        BAG,
        MINERAL,
    }

    public static event Action<WeaponType> WeaponTaken;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            switch (item)
            {
                case Item.BAG:
                    Bag.i.gameObject.SetActive(true);
                    Bag.i.OpenBag();
                    Destroy(gameObject);
                    break;


                case Item.WEAPON_DAGGER:
                    WeaponTaken?.Invoke(WeaponType.DAGGER);
                    Destroy(gameObject);
                    break;


                case Item.MINERAL:
                    Score.TotalScore++;
                    Pull.i.GetMineralEffect(Player.i.GetCenterPos());
                    StartCoroutine(Pull.i.Return(gameObject, Pull.Item.MINERAL, timer: 0));
                    break;
            }
        }
    }
}


