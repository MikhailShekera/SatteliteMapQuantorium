using TMPro;
using UnityEngine;

namespace UnityDevKit.UI.Descriptions
{
    public class Description : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private string _currentDescription;

        public void SetupNew(string description)
        {
            _currentDescription = description;
            Show(_currentDescription); // TODO -- add apply effect
        }

        public void Show(string description)
        {
            text.text = description;
        }

        public void Reset()
        {
            text.text = _currentDescription;
        }
    }
}