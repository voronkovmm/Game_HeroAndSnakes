using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    bool isDown;

    public void MoveDown(float y) => StartCoroutine(COROUTINE_MoveDown(y));

    IEnumerator COROUTINE_MoveDown(float y)
    {
        if(isDown) yield break;

        Vector2 newPos = new Vector2(transform.position.x, transform.position.y - y);

        while((transform.position.y > newPos.y))
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, 0.5f * Time.deltaTime);
            yield return null;
        }

        isDown = true;
    }
}
