using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Material _mat;

    float _offset_X;
    [SerializeField] float _speedOffset_X;

    float oldValueX;

    void Start() {
        _mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        oldValueX = Player.i.GetPos().x;

        if(PlayerControl.isCanMove && oldValueX != Player.i.GetPos().x)
            _offset_X += (PlayerControl.i._dirMove.x * _speedOffset_X) / 10;
        _mat.SetVector("_offset", new Vector4(_offset_X, 0,0,0));
    }
}