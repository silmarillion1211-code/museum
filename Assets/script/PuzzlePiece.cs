using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("拼图设置")]
    public Vector3 targetPosition;
    public float snapDistance = 0.6f;
    public bool lockRotation = true;

    private Vector3 mouseDragOffset;
    private bool isDragging;
    private bool isPlacedCorrect;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (lockRotation)
            transform.rotation = Quaternion.identity;
    }

    void OnMouseDown()
    {
        if (isPlacedCorrect) return;
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        mouseDragOffset = transform.position - mouseWorldPos;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging || isPlacedCorrect) return;

        Vector3 mouseWorldPos = GetMouseWorldPosition();
        transform.position = mouseWorldPos + mouseDragOffset;

        if (lockRotation)
            transform.rotation = Quaternion.identity;

        // 只计算XY平面距离，忽略Z轴
        float dx = transform.position.x - targetPosition.x;
        float dy = transform.position.y - targetPosition.y;
        float distance = Mathf.Sqrt(dx * dx + dy * dy);

        if (distance <= snapDistance)
        {
            transform.position = targetPosition;
            isPlacedCorrect = true;
            isDragging = false;
            PuzzleManager.Instance.CheckPuzzleComplete();
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (!isPlacedCorrect)
        {
            float dx = transform.position.x - targetPosition.x;
            float dy = transform.position.y - targetPosition.y;
            float distance = Mathf.Sqrt(dx * dx + dy * dy);

            if (distance <= snapDistance)
            {
                transform.position = targetPosition;
                if (lockRotation)
                    transform.rotation = Quaternion.identity;
                isPlacedCorrect = true;
                PuzzleManager.Instance.CheckPuzzleComplete();
            }
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    public bool IsPlaced()
    {
        return isPlacedCorrect;
    }
}