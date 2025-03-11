using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace DoomsDayDefense
{
    public abstract class TowerBase : MonoBehaviour
    {
        public event System.Action OnDestroyed;

        public int buildCost = 50;
        [SerializeField] protected float range = 5f;
        [SerializeField] protected float fireRate = 1f;
        public GameObject projectilePrefab;
        public Transform firePoint;

        protected Transform target;
        protected bool isActive = true;
        [SerializeField] protected float fireCountdown = 0f;

        // tower UI
        [SerializeField] protected GameObject towerUI;
        //[SerializeField] protected GameObject upgradeBtn;
        //[SerializeField] protected GameObject sellBtn;

        //public virtual void InitializeTower()
        //{
        //    StartCoroutine(AttackRoutine());
        //}

        //protected abstract IEnumerator AttackRoutine();



        public virtual string TowerStats => $"Range: {range}, Fire Rate: {fireRate}";
        public int SellValue => Mathf.FloorToInt(buildCost * 0.7f);

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
            Debug.Log("Tower Clicked");
            towerUI.SetActive(true);
        }

        public void CloseTowerUI()
        {
            towerUI.SetActive(false);
        }

        public void UpgradeTower()
        {
            Debug.Log("Upgrade Tower");
        }

        public virtual void SellTower()
        {
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
