using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isMobileDevice;

    [Header("Mobile UI")]
    [SerializeField] GameObject MobileUI;

    [Header("MakeActiveBeforeGameStart")]
    [SerializeField] GameObject _UIBag;

    void Awake() {
        Application.targetFrameRate = 60;

        isMobileDevice = SystemInfo.deviceType == DeviceType.Handheld;

        MobileOrientation();
        MobileUISetActive();

        MakeActiveBeforeStart();
    }

    void Start() {
        MakeInactiveBeforeStart();
    }

    void MobileOrientation()
    {
        if(isMobileDevice)
        {
            Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
            Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;
            Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
        }
    }

    public void MobileUISetActive()
    {   
        if(GameManager.isMobileDevice)
            MobileUI.SetActive(true);
    }

    void MakeActiveBeforeStart()
    {
        _UIBag.gameObject.SetActive(true);
    }

    void MakeInactiveBeforeStart()
    {
        _UIBag.gameObject.SetActive(false);
    }
}
