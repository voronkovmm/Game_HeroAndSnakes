using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public abstract Vector2 GetPos();
    public abstract void Damage(int damage, Vector2 dirImpact);
}
