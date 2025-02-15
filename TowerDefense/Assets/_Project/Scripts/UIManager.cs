using UnityEngine;
using UnityEngine.UI;

namespace DoomsDayDefense
{
    public class UIManager : MonoBehaviour
    {
        public Text goldText;
        public Text levesText;

        void Update()
        {
            goldText.text = "Gold: " + GameManager.Instance.gold;
            levesText.text = "Leves: " + GameManager.Instance.lives;
        }
    }
}
