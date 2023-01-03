using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Player i;

    public int Health {get; private set;}
    public int CurrentHealth {get; set;}
    public int Damage {get; private set;}

    public static event Action<int> iTakeDamageEvent;
    public static event Action<int> iRestHealthEvent;

    void Awake() {
        if(i == null) i = this;
    }

    void Start() {
        Health = 50;
        Damage = 1;
        CurrentHealth = Health; 
    }

    void IDamageable.Damage(int damage, Vector2 dirImpact)
    {
        CurrentHealth--;
        iTakeDamageEvent?.Invoke(CurrentHealth);

        Pull.i.GetBloodCircle(pos: GetCenterPos());
        Pull.i.GetBloodstain(pos: GetPos());
        Pull.i.GetBlood(pos: GetCenterPos(), flip: dirImpact.x < GetPos().x ? true : false);
        
        CameraControl.i.ShakeCamera(7.5f, .2f);
    }

    public void RestHealth()
    {
        CurrentHealth++;
        iRestHealthEvent?.Invoke(CurrentHealth);
    }

    public Vector2 GetPos() => transform.position;
    public Vector2 GetCenterPos() => transform.position + (Vector3.up * 0.4f);
}
