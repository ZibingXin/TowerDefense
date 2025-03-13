using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace DoomsDayDefense
{
    public abstract class TowerBase : MonoBehaviour
    {
        public event System.Action OnDestroyed;

        public CrystalType crystalType;
        public int buildCost = 50;

        [SerializeField] protected int level = 1;
        [SerializeField] protected float range = 5f;
        [SerializeField] protected float fireRate = 1f;
        public GameObject projectilePrefab;
        public Transform firePoint;

        protected Transform target;
        protected bool isActive = true;
        [SerializeField] protected float fireCountdown = 0f;

        // tower UI
        [SerializeField] private GameObject towerUIRoot;
        [SerializeField] private TextMeshProUGUI towerInfoText;

        public virtual string TowerStats => $"Level: {level}, Range: {range}, Fire Rate: {fireRate}";
        public int SellValue => Mathf.FloorToInt(buildCost * 0.6f);

        private void Start()
        {
            if (towerUIRoot == null)
                towerUIRoot = GetComponentInChildren<TowerUI>(true).gameObject;

            if (towerInfoText == null)
                towerInfoText = GetComponentInChildren<TextMeshProUGUI>(true);

            towerUIRoot.SetActive(false);
        }
        private void Update()
        {
            UpdateTarget();
            if (target != null)
            {
                AimTarget(target.position);
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    AudioManager.Instance.PlaySFX("TowerAttack", transform.position);
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }

            if (towerUIRoot.activeSelf)
            {
                towerUIRoot.transform.rotation = Quaternion.Euler(0f, -transform.rotation.y, 0f);
                if (Input.GetMouseButtonDown(1))
                {
                    AudioManager.Instance.PlaySFX("ButtonClick", transform.position);
                    CloseTowerUI();
                }
            }
        }


        protected void UpdateTarget()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    float distance = col.GetComponentInParent<Enemy>().distance;
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestEnemy = col.gameObject;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }

        }

        protected void AimTarget(Vector3 position)
        {
            if (position == null) return;
            Vector3 dir = position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        protected virtual void Shoot() { }

        private void OnMouseDown()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", transform.position);
            towerUIRoot.SetActive(true);
            towerInfoText.text = TowerStats;
        }

        public void CloseTowerUI()
        {
            towerUIRoot.SetActive(false);
        }

        public void UpgradeTower()
        {
            if (GameManager.Instance.PurchaseTower(50) && level < 3)
            {
                AudioManager.Instance.PlaySFX("ButtonClick", transform.position);
                level++;
                range += 1f;
                fireRate += 0.5f;
                towerInfoText.text = TowerStats;
                CloseTowerUI();
            }
        }

        public virtual void SellTower()
        {
            AudioManager.Instance.PlaySFX("ButtonClick", transform.position);
            CloseTowerUI();
            switch (crystalType)
            {
                case CrystalType.Red:
                    GameManager.Instance.redCrystals += SellValue;
                    break;
                case CrystalType.Blue:
                    GameManager.Instance.blueCrystals += SellValue;
                    break;
                case CrystalType.Green:
                    GameManager.Instance.greenCrystals += SellValue;
                    break;
            }
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
