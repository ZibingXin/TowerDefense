using UnityEngine;

namespace DoomsDayDefense
{
    public enum CrystalType
    {
        Red,
        Blue,
        Green
    }
    public class CrystalCapturerTower : MonoBehaviour
    {
        private float cooldown = 3f;
        private float timer;

        [SerializeField] private int captureAmount = 10;
        [SerializeField] private CrystalType crystalType;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0;
                CaptureCrystal();
            }
        }

        private void CaptureCrystal()
        {
            GameManager.Instance.AddCrystal(crystalType, captureAmount);
        }

    }
}
