using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DoomsDayDefense
{
    public class BuildButton : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private Button button;

        public void Initialize(Sprite icon, string name, string cost, UnityAction onClick)
        {
            iconImage.sprite = icon;
            nameText.text = name;
            costText.text = cost;
            button.onClick.AddListener(onClick);

            UpdateButtonState(int.Parse(cost));
            GameManager.Instance.OnGoldChanged += (gold) => UpdateButtonState(int.Parse(cost));
        }

        void UpdateButtonState(int requiredGold)
        {
            button.interactable = GameManager.Instance.CurrentGold >= requiredGold;
            costText.color = GameManager.Instance.CurrentGold >= requiredGold ?
                Color.white : Color.red;
        }
    }
}
