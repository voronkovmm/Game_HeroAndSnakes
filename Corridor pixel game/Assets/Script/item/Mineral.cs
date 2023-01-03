using UnityEngine;

public class Mineral : MonoBehaviour
{
    Animator _anim;

    void Awake() {
        _anim = transform.Find("image").GetComponent<Animator>();
    }

    void Start() {
        float speedAnim = Random.Range(0.7f, 1);
        _anim.speed = speedAnim;
    }
}
