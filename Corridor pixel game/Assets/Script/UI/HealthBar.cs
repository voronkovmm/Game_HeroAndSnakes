using System.Collections;
using UnityEngine;

namespace UI {
public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject[] health;

    void Start() {
         Player.iTakeDamageEvent += PlayerTakeDamageHandler;
         Player.iRestHealthEvent += PlayerRestHealthHandler;
    }

    void PlayerTakeDamageHandler(int currentHealth)
    {
        for (int i = currentHealth; i < health.Length; i++)
        {
            health[i].SetActive(false);
        }
    }

    void PlayerRestHealthHandler(int currentHealth)
    {
        IEnumerator RestHealth()
        {
            for (int i = 0; i < health.Length; i++)
            {
                if(health[i].activeSelf) continue;
                health[i].SetActive(true);

                yield return new WaitForSeconds(0.3f);
            }

            Player.i.CurrentHealth = Player.i.Health;
        }

        StartCoroutine(RestHealth());
    }
}
}
