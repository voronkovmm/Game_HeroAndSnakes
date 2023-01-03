using UnityEngine;
using UnityEngine.UI;

namespace UI {
public class WeaponUI : MonoBehaviour
{
    public static WeaponUI i;

    bool _isChoiceOpen;
    Text _textWeapon;

    public Weapon[] slots;
    int unlockedSlots = 0;

    public Sprite spriteFist;
    public Sprite spriteDagger;

    void Awake() {
        if(i == null) i = this;
        _textWeapon = transform.Find("text").GetComponent<Text>();
    }

    void Start() {
        TakeMe.WeaponTaken += HANDLER_WeaponTaken;
        _textWeapon.text = Json.i.myText.weapon[0];
    }

    void HANDLER_WeaponTaken(WeaponType type)
    {
        switch(type)
        {
            case WeaponType.DAGGER:
            {
                unlockedSlots++;

                Weapon slot = WeaponUI.i.slots[unlockedSlots];
                slot.GetComponent<Weapon>().type = WeaponType.DAGGER;
                
                Image image = slot.transform.Find("image").GetComponent<Image>();
                image.sprite = spriteDagger;
                image.SetNativeSize();
                break;
            }
        }
    }

    public void OpenChoiceButton()
    {
        if(unlockedSlots == 0) return;

        _isChoiceOpen = _isChoiceOpen ? false : true;

        _textWeapon.enabled = _isChoiceOpen ? false : true;

        for (int i = 1; i <= unlockedSlots; i++)
        {
            if(_isChoiceOpen)
                slots[i].gameObject.SetActive(true);
            else
                slots[i].gameObject.SetActive(false);
        }
    }

    public void BUTTON_ChangeWeapon(Weapon weapon)
    {
        Sprite newSprite = weapon.sprite;
        Sprite oldSprite = slots[0].sprite;

        WeaponType newType = weapon.type;
        WeaponType oldType = slots[0].type;

        string newText = weapon.text;
        string oldText = slots[0].text;

        slots[0].sprite = newSprite;
        slots[0].text = newText;
        slots[0].type = newType;

        weapon.sprite = oldSprite;
        weapon.text = oldText;
        weapon.type = oldType;

        Image imageOtherSlot = weapon.transform.Find("image").GetComponent<Image>();
        imageOtherSlot.sprite = oldSprite;
        imageOtherSlot.SetNativeSize();

        Image imageMainSlot = slots[0].transform.Find("image").GetComponent<Image>();
        imageMainSlot.sprite = newSprite;
        imageMainSlot.SetNativeSize();
        _textWeapon.text = newText;

        OpenChoiceButton();
    }

    public void GetSpriteAndText(WeaponType type, out Sprite sprite, out string text)
    {
        sprite = null;
        text = null;

        switch(type)
        {
            case WeaponType.FIST:
            {
                sprite = spriteFist;
                text = Json.i.myText.weapon[0];
                break;
            }
            case WeaponType.DAGGER:
            {
                sprite = spriteDagger;
                text = Json.i.myText.weapon[1];
                break;
            }
        }
    }
}
}
