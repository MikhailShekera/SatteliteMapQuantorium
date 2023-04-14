using MyBox;
using System.Collections;
using UnityEngine;

namespace UnityDevKit.Utils.Environment
{
    public class WallClockController : MonoBehaviour
    {
        [Separator("Rotation Axis")] [SerializeField]
        private GameObject secondRotationAxis;

        [SerializeField] private GameObject minuteRotationAxis;
        [SerializeField] private GameObject hourRotationAxis;

        private const int secondHandUpdateRate = 1;

        private const float SECOND_HAND_UPDATE_RATE = -6f;
        private const float MINUTE_HAND_UPDATE_RATE = -0.1f;
        private const float HOUR_HAND_UPDATE_RATE = -360 / 86400f;
        private const float LERP_DURATION = 2f;

        private float elapsedTime;

        private void Start()
        {
            SetHands();

            VirtualTimeHandler.Instance.OnTimeChange.AddListener(SetHands);

            StartCoroutine(nameof(SecondHandRotation));
        }

        private void SetHands()
        {
            StopAllCoroutines();
            StartCoroutine(nameof(HandLerp));
        }

        private IEnumerator SecondHandRotation()
        {
            while (true)
            {
                secondRotationAxis.transform.localEulerAngles = new Vector3(0, -SECOND_HAND_UPDATE_RATE, 0) *
                                                                VirtualTimeHandler.Instance.CurrentTime.Seconds;
                minuteRotationAxis.transform.Rotate(0, -MINUTE_HAND_UPDATE_RATE, 0);
                hourRotationAxis.transform.Rotate(0, -HOUR_HAND_UPDATE_RATE, 0);
                yield return new WaitForSeconds(secondHandUpdateRate);
            }
        }

        private IEnumerator HandLerp()
        {
            Vector3 hourFinalPos = new Vector3(0f, HourAngleReset(), 0f);
            Vector3 minuteFinalPos = new Vector3(0f, MinuteAngleReset(), 0f);

            float elapsedTime = 0;
            while (elapsedTime < LERP_DURATION)
            {
                elapsedTime += Time.deltaTime;
                var progress = elapsedTime / LERP_DURATION;
                secondRotationAxis.transform.localEulerAngles =
                    Vector3.Slerp(secondRotationAxis.transform.localEulerAngles, Vector3.zero, progress);
                hourRotationAxis.transform.localEulerAngles = Vector3.Slerp(hourRotationAxis.transform.localEulerAngles,
                    hourFinalPos, progress);
                minuteRotationAxis.transform.localEulerAngles =
                    Vector3.Slerp(minuteRotationAxis.transform.localEulerAngles, minuteFinalPos, progress);
                yield return null;
            }

            StartCoroutine(nameof(SecondHandRotation));
        }

        #region Функции вычисления угла наклона стрелок

        private float HourAngleReset()
        {
            var angle = VirtualTimeHandler.Instance.CurrentTime.Hours * 30 +
                        VirtualTimeHandler.Instance.CurrentTime.Minutes * 0.25;
            while (angle >= 360)
                angle -= 360;
            return (float) angle;
        }

        private float MinuteAngleReset()
        {
            return VirtualTimeHandler.Instance.CurrentTime.Minutes * 6;
        }

        #endregion
    }
}