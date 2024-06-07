using Cinemachine;
using UnityEngine;

namespace CutscenesControl
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get; private set; }
    
        private CinemachineVirtualCamera cinemachineVirtualCamera;
        private float shakeTimer;
    
        private void Awake()
        {
            Instance = this;
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
    
        private void Update()
        {
            if (shakeTimer > 0)
            { 
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0f)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }
        }
    
        public void ShakeCamera(float intensity, float time) //Ints for signal to work
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            shakeTimer = time;
        }
    }
}
