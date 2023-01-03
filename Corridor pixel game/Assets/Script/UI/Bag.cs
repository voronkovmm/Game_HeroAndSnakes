using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
public class Bag : MonoBehaviour
{
    public static Bag i;

    [SerializeField] GameObject UI_Bag;
    
    void Awake() {
        if(i == null) i = this;
        gameObject.SetActive(false);
    }

    public void OpenBag() 
    {
        UI_Bag.SetActive(true);
        PlayerControl.StopAllMove();
    }
    public void CloseBagEvent()
    {
        UI_Bag.SetActive(false);
        PlayerControl.isCanMove = true;
    }


}
}
