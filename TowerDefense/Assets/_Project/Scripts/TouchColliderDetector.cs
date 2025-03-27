using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DoomsDayDefense
{
    public class TouchColliderDetector : MonoBehaviour
    {
        [Header("Input Actions")]
        [SerializeField] private InputActionReference touchPressAction;
        [SerializeField] private InputActionReference touchPositionAction;

        [Header("Settings")]
        [SerializeField] private LayerMask interactableLayers;
        [SerializeField] private float maxRayDistance = 100f;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
            EnableInputActions();
        }

        private void EnableInputActions()
        {
            touchPressAction.action.Enable();
            touchPositionAction.action.Enable();
        }

        private void OnEnable()
        {
            touchPressAction.action.performed += HandleTouch;
        }

        private void OnDisable()
        {
            touchPressAction.action.performed -= HandleTouch;
        }

        private void HandleTouch(InputAction.CallbackContext context)
        {
            if (EventSystem.current.IsPointerOverGameObject(GetPointerId())) return;

            Vector2 touchPos = touchPositionAction.action.ReadValue<Vector2>();
            CheckColliderAtPosition(touchPos);
        }

        private int GetPointerId()
        {
            return touchPressAction.action.activeControl.device.deviceId;
        }

        private void CheckColliderAtPosition(Vector2 screenPos)
        {
            Ray ray = mainCamera.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, interactableLayers))
            {
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log($"Hit: {hitObject.name}", hitObject);

                if (hitObject.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.OnTapped();
                }
            }
        }
    }
}
