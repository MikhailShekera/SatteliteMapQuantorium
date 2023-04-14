using UnityDevKit.Scenarios.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDevKit.Scenarios
{
    public class TaskDisplay : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private TMP_Text text;
        [SerializeField] private GameObject UIOutline;

        public void Init(Task task)
        {
            text.text = task.Data.Name;
            toggle.isOn = task.IsCompleted;
            var outlineLine = UIOutline.GetComponent<Image>();
            outlineLine.enabled = task.IsCompleted;
        }

        public void StatusUpdate(Task task)
        {
            toggle.isOn = task.IsCompleted;
            var outlineLine = UIOutline.GetComponent<Image>();
            outlineLine.enabled = task.IsCompleted;
        }
    }
}