using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class Tools : MonoBehaviour
{
    public static IEnumerator Delay(Action method, float time)
    {
        yield return new WaitForSeconds(time);

        method.Invoke();
    }

    public static IEnumerator DisappearingAndDestroy(SpriteRenderer sr, float timer)
    {
        yield return new WaitForSeconds(timer);

        float alpha = sr.color.a;
        Color color = sr.color;

        while(alpha > 0)
        {
            alpha -= Time.deltaTime/2;
            sr.color = new Color(color.r, color.b, color.b, alpha);
            yield return null;
        }

        Destroy(sr.gameObject,1);
    }

    public static IEnumerator DisappearingAndReturn(Queue<GameObject> queue ,SpriteRenderer sr, float timer)
    {
        yield return new WaitForSeconds(timer);

        float alpha = sr.color.a;
        Color color = sr.color;

        while(alpha > 0)
        {
            alpha -= Time.deltaTime/2;
            sr.color = new Color(color.r, color.b, color.b, alpha);
            yield return null;
        }

        GameObject go = sr.gameObject;
        go.SetActive(false);
        queue.Enqueue(go);
    }
    
}
