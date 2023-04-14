using MyBox;
using System.Collections;
using UnityEngine;

namespace UnityDevKit.Utils.Environment
{
    public class SunRotationController : MonoBehaviour
    {
        [Separator("Settings")]
        [SerializeField] private Light directionalLight;
        [SerializeField] private int zeroIntensityPoint;
        [SerializeField] private int updateRate;

        private const float LERP_DURATION = 5f;
         
        private float rotationAngle;

        private float elapsedTime = 0;

        private void Start()
        {
            ChooseLightTemperature();
            directionalLight.transform.eulerAngles = new Vector3(axisXSet(), axisYSet(), axisZSet());

            rotationAngle = 360 * updateRate / 86400f;

            VirtualTimeHandler.Instance.OnTimeChange.AddListener(TimeSet);

            StartCoroutine(nameof(SetAngle));
        }

        private IEnumerator SetAngle()
        {
            while (true)
            {
                directionalLight.transform.eulerAngles = new Vector3(axisXSet(), axisYSet(), axisZSet());
                yield return new WaitForSeconds(updateRate);
            }
        }

        private void TimeSet()
        {
            StopCoroutine(nameof(SunLerp));
            elapsedTime = 0;
            StartCoroutine(nameof(SunLerp));
        }

        private IEnumerator SunLerp()
        {
            StopCoroutine(nameof(SetAngle));

            if (axisXSet() < zeroIntensityPoint)
                directionalLight.intensity = 0;

            ChooseLightTemperature();

            Quaternion newPos = Quaternion.Euler(axisXSet(), axisYSet(), axisZSet());

            while (elapsedTime < LERP_DURATION)
            {
                elapsedTime += Time.deltaTime;

                directionalLight.transform.rotation = Quaternion.Slerp(directionalLight.transform.rotation, newPos, elapsedTime / LERP_DURATION);

                yield return null;
            }
            StartCoroutine(nameof(SetAngle));
        }

        #region Функции установки углов
        private float axisXSet()
        {
            return -40 * Mathf.Cos((float)(VirtualTimeHandler.Instance.CurrentTime.TotalSeconds / updateRate * rotationAngle * Mathf.PI / 180)) - 10;
        }

        private float axisYSet()
        {
            return rotationAngle * (float)VirtualTimeHandler.Instance.CurrentTime.TotalSeconds/updateRate;
        }

        private float axisZSet()
        {

            return 40 * Mathf.Cos((float)(VirtualTimeHandler.Instance.CurrentTime.TotalSeconds / updateRate * rotationAngle * Mathf.PI/180));
        }
        #endregion

        private void ChooseLightTemperature()
        {
            if (VirtualTimeHandler.Instance.CurrentTime.TotalHours > 6 && VirtualTimeHandler.Instance.CurrentTime.TotalHours < 10)
                directionalLight.colorTemperature = 3500;
            else if (VirtualTimeHandler.Instance.CurrentTime.TotalHours > 10 && VirtualTimeHandler.Instance.CurrentTime.TotalHours < 18)
                directionalLight.colorTemperature = 6000;
        }
    }
}
