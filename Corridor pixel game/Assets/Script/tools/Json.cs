using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Json : MonoBehaviour
{
    public static Json i;

    public MyText myText;

    void Awake() {
        if(i == null) i = this;
    }

    void Start() {
        TextAsset file = Resources.Load("text") as TextAsset;
        string content = file.ToString();
        myText = JsonUtility.FromJson<MyText>(content);
    }

    [System.Serializable]
    public struct MyText
    {
        public string deadWindow;
        public string[] weapon;
    }
}
