using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] BoxCollider2D _spikeCollider;
    
    Light2D _light;
    Animator _anim;
    CapsuleCollider2D _playerCollider;

    void Awake() {
        _light = transform.Find("light").GetComponent<Light2D>();
        _anim = GetComponent<Animator>();
        _playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            _light.enabled = true;
            _anim.SetTrigger("activate");
        }
    }

    void OffLightEvent() => _light.enabled = false;
    void CheckPlayerColliderEvent()
    {
        if(Physics2D.IsTouching(_spikeCollider, _playerCollider))
            ((IDamageable)Player.i).Damage(1, transform.position);
    }
}
