using UnityEngine;

namespace DoomsDayDefense
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] GameObject pointerPrefab;
        [SerializeField] GameObject tutorialPanel;

        public void ShowTutorialPanel()
        {
            tutorialPanel.SetActive(true);
            GameObject pointer = Instantiate(pointerPrefab, transform.position, Quaternion.identity);
            pointer.transform.SetParent(tutorialPanel.transform);
            pointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }

        public void HideTutorialPanel()
        {
            tutorialPanel.SetActive(false);
            Destroy(tutorialPanel.transform.GetChild(0).gameObject);
        }


    }
}
