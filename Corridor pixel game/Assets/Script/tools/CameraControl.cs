using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl i;

    float _shakeTimer;
    float _startIntensity;
    float _shakeTimerTotal;
    CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    
    void Awake() {
         _cinemachineBasicMultiChannelPerlin = 
            GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Start() {
        if(i == null) i = this;
        
    }

    void Update() {
        ShakeCameraInner(); 
    }

    void ShakeCameraInner()
    {
        if(_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;

            if(_shakeTimer <= 0)
            {
                // Time is over
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 
                    Mathf.Lerp(_startIntensity, 0f, (1 - (_shakeTimer / _shakeTimerTotal)));
            }   
        }
    }
    public void ShakeCamera(float intensity, float time)
    {
        _shakeTimer = _shakeTimerTotal = time;
        _startIntensity = intensity;
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }

}
