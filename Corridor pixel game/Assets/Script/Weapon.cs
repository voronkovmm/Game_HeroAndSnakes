using UnityEngine;
using UI;
using System;

public class Weapon : MonoBehaviour
{
    public WeaponType type;
    public string text;
    public Sprite sprite;

    void Start() {
        WeaponUI.i.GetSpriteAndText(type, out sprite, out text);
    }
}

public enum WeaponType
{
    FIST,
    DAGGER
}
