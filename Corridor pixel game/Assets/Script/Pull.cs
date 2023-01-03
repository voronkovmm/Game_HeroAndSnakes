using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour
{
    public static Pull i;

    [SerializeField] GameObject _pfBlood;
    [SerializeField] GameObject _pfBloodCircle;
    [SerializeField] GameObject _pfBloodstain;
    [SerializeField] GameObject _pfBloodSplash;
    [SerializeField] GameObject _pfSnakeHead;
    [SerializeField] GameObject _pfMineralEffect;
    [SerializeField] GameObject _pfMineral;
    [SerializeField] GameObject _pfSnakeAtkSpray;
    Transform parent;
    Queue<GameObject> _blood;
    Queue<GameObject> _bloodCircle;
    Queue<GameObject> _bloodstain;
    Queue<GameObject> _bloodSplash;
    Queue<GameObject> _snakeHead;
    Queue<GameObject> _mineralEffect;
    Queue<GameObject> _mineral;
    Queue<GameObject> _snakeAtkSpray;

    public enum Item 
    {
        BLOOD, BLOOD_CIRCLE, BLOODSTAIN, BLOODSPLASH,
        SNAKE_HEAD,
        MINERAL_EFFECT, MINERAL,
        SNAKE_ATK_SPRAY
    }

    void Awake() {
        if(i == null) i = this;    
        parent = new GameObject("pull").transform;
    }

    void Start() {
        FillBlood();
        FillBloodCircle();
        FillBloodstain();
        FillBloodSplash();
        FillSnakeHead();
        FillMineralEffect();
        FillMineral();
        FillSnakeAtkSpray();
    }

    void FillBlood()
    {
        int size = 5;
        _blood = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(_pfBlood, Vector2.zero, Quaternion.identity, parent);
            _blood.Enqueue(go);
            go.SetActive(false);
        }
    }
    public GameObject GetBlood(Vector2 pos, float timeGoBack = 10.5f, bool flip = false)
    {
        if(_blood.Count == 0)
            _blood.Enqueue(Instantiate(_pfBlood, Vector2.zero, Quaternion.identity, parent));

        GameObject go = _blood.Dequeue();
        go.transform.position = pos;
        if(flip) go.transform.rotation = Quaternion.Euler(0,180,0);
        go.SetActive(true);

        StartCoroutine(Return(go, Item.BLOOD, timeGoBack));
        return go;
    }

    void FillBloodCircle()
    {
        int size = 1;
        _bloodCircle = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(_pfBloodCircle, Vector2.zero, Quaternion.identity, Player.i.transform);
            _bloodCircle.Enqueue(go);
            go.SetActive(false);
        }
    }
    public GameObject GetBloodCircle(Vector2 pos, float timeGoBack = 10.5f)
    {
        if(_bloodCircle.Count == 0)
        {
            _bloodCircle.Enqueue(Instantiate(_pfBloodCircle, Vector2.zero, Quaternion.identity, Player.i.transform));
        }

        GameObject go = _bloodCircle.Dequeue();
        go.transform.position = pos;
        go.SetActive(true);

        StartCoroutine(Return(go, Item.BLOOD_CIRCLE, timeGoBack));
        return go;
    }

    void FillBloodstain()
    {
        int size = 1;
        _bloodstain = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(_pfBloodstain, Vector2.zero, Quaternion.identity, parent);
            go.SetActive(false);
            _bloodstain.Enqueue(go);
        }
    }
    public GameObject GetBloodstain(Vector2 pos, float timeGoBack = 10.5f)
    {
        if(_bloodstain.Count == 0)
            _bloodstain.Enqueue(Instantiate(_pfBloodstain, Vector2.zero, Quaternion.identity, parent));

        GameObject go = _bloodstain.Dequeue();
        go.transform.Rotate(new Vector3(0,0, Random.Range(0, 360)));
        go.transform.position = pos;
        float scale = Random.Range(0.75f, 2f);
        go.transform.localScale = new Vector3(scale, scale, 0);
        go.SetActive(true);

        StartCoroutine(Return(go, Item.BLOODSTAIN, timeGoBack));
        
        return go;
    }

    void FillBloodSplash()
    {
        int size = 1;
        _bloodSplash = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(_pfBloodSplash, Vector2.zero, Quaternion.identity, parent);
            go.SetActive(false);
            _bloodSplash.Enqueue(go);
        }
    }
    public GameObject GetBloodSplash(Vector2 pos, float timeGoBack = 10.5f, bool flip = false)
    {
        if(_bloodSplash.Count == 0)
            _bloodSplash.Enqueue(Instantiate(_pfBloodSplash, Vector2.zero, Quaternion.identity, parent));

        GameObject go = _bloodSplash.Dequeue();
        go.transform.position = pos;
        if(flip) go.transform.rotation = Quaternion.Euler(0,180,0);
        go.SetActive(true);

        StartCoroutine(Return(go, Item.BLOODSPLASH, timeGoBack));
        
        return go;
    }

    void FillSnakeHead()
    {
        int size = 1;
        _snakeHead = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(_pfSnakeHead, Vector2.zero, Quaternion.identity, parent);
            go.SetActive(false);
            _snakeHead.Enqueue(go);
        }
    }
    public GameObject GetSnakeHead(Vector2 pos, float timeGoBack = 10.5f, bool needBack = true)
    {
        if(_snakeHead.Count == 0)
            _snakeHead.Enqueue(Instantiate(_pfSnakeHead, Vector2.zero, Quaternion.identity, parent));

        GameObject go = _snakeHead.Dequeue();
        go.transform.position = pos;
        go.SetActive(true);

        if(needBack) StartCoroutine(Return(go, Item.SNAKE_HEAD, timeGoBack));
        
        return go;
    }

    void FillMineralEffect()
    {
        int size = 2;
        _mineralEffect = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(_pfMineralEffect, Vector2.zero, Quaternion.identity, Player.i.transform);
            go.SetActive(false);
            _mineralEffect.Enqueue(go);
        }
    }
    public GameObject GetMineralEffect(Vector2 pos, float timeGoBack = 0.5f, bool needBack = true)
    {
        if(_mineralEffect.Count <= 0)
            _mineralEffect.Enqueue(Instantiate(_pfMineralEffect, Vector2.zero, Quaternion.identity, Player.i.transform));

        GameObject go = _mineralEffect.Dequeue();
        go.transform.position = pos;
        go.SetActive(true);

        if(needBack) StartCoroutine(Return(go, Item.MINERAL_EFFECT, timeGoBack));
        
        return go;
    }

    void FillMineral()
    {
        int size = 1;
        _mineral = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(_pfMineral, Vector2.zero, Quaternion.identity, parent);
            go.SetActive(false);
            _mineral.Enqueue(go);
        }
    }
    public void GetMineral(Vector2 pos, int count)
    {
        if(_mineral.Count < count)
        {
            for (int i = 0; i < count; i++)
                _mineral.Enqueue(Instantiate(_pfMineral, Vector2.zero, Quaternion.identity, parent));
        }

        for (int i = 0; i < count; i++)
        {
            GameObject go = _mineral.Dequeue();
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            int randX = Random.Range(-1, 2);
            float randForce = Random.Range(0.5f, 2f);
            rb.AddForce(new Vector2(randX, 1) * randForce, ForceMode2D.Impulse);
            go.transform.position = pos;
            go.SetActive(true);
        }
    }

    void FillSnakeAtkSpray()
    {
        int size = 1;
        _snakeAtkSpray = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(_pfSnakeAtkSpray, Vector2.zero, Quaternion.identity, parent);
            go.SetActive(false);
            _snakeAtkSpray.Enqueue(go);
        }
    }
    public GameObject GetSnakeAtkSpray(Vector2 pos)
    {
        if(_snakeAtkSpray.Count <= 0)
            _snakeAtkSpray.Enqueue(Instantiate(_pfSnakeAtkSpray, Vector2.zero, Quaternion.identity, parent));

        GameObject go = _snakeAtkSpray.Dequeue();
        go.transform.position = pos;
        go.SetActive(true);

        return go;
    }

    public IEnumerator Return(GameObject go, Item item, float timer)
    {
        yield return new WaitForSeconds(timer);
        go.SetActive(false);
        
        yield return new WaitForSeconds(0.1f);

        switch (item) {
            case Item.BLOOD:
                go.transform.rotation = Quaternion.Euler(0,0,0);
                _blood.Enqueue(go);
                break;

            case Item.BLOOD_CIRCLE:
                _bloodCircle.Enqueue(go);
                break;

            case Item.BLOODSTAIN:
                _bloodstain.Enqueue(go);
                break;

            case Item.BLOODSPLASH:
                go.transform.rotation = Quaternion.Euler(0,0,0);
                _bloodSplash.Enqueue(go);
                break;

            case Item.SNAKE_HEAD:
                go.transform.rotation = Quaternion.Euler(0,0,0);
                _snakeHead.Enqueue(go);
                break;
            
            case Item.MINERAL_EFFECT:
                _mineralEffect.Enqueue(go);
                break;

            case Item.MINERAL:
                _mineral.Enqueue(go);
                break;
        }
        
    }

    public IEnumerator DisappearingAndGoBack(SpriteRenderer sr, float timer)
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

        StartCoroutine(Return(sr.gameObject, Item.SNAKE_HEAD, 0));
    }

}
