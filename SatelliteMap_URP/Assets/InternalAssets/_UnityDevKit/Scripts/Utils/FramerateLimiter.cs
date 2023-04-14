using UnityDevKit.Utils;
using UnityEngine;

public class FramerateLimiter : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60;

    private void Start()
    {
        VSyncController.RemoveVSync();
        Application.targetFrameRate = targetFrameRate;
    }
}
