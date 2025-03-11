using System.Collections;
using UnityEngine;

namespace DoomsDayDefense
{
    
    public class CrystalCapturerTower : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float cooldown = 3f;
        [SerializeField] private int captureAmount = 10;
        public CrystalType crystalType;

        private bool isInitialized = false;

        public void Initialize(CrystalType ct)
        {
            if (isInitialized) return;

            crystalType = ct;
            isInitialized = true;

            StartCoroutine(CaptureRoutine());
            Debug.Log("Initialized " + crystalType + " crystal capturer");
        }

        private IEnumerator CaptureRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(cooldown);
                CaptureCrystal();
            }
        }

        private void CaptureCrystal()
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager is null");
                return;
            }

            GameManager.Instance.AddCrystal(crystalType, captureAmount);
            //Debug.Log("Captured " + captureAmount + " " + crystalType + " crystals\nCurrent: " + GameManager.Instance.redCrystals);
        }

    }
}
