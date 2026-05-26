using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _handle;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _handleRange = 1f;

    private Vector2 _moveInput;

    private void Awake()
    {
        if (_background == null)
            _background = transform as RectTransform;

        if (_canvas == null)
            _canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_background == null) return;

        Camera eventCamera = _canvas != null && _canvas.renderMode != RenderMode.ScreenSpaceOverlay
            ? _canvas.worldCamera
            : null;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_background, eventData.position, eventCamera, out Vector2 localPoint))
            return;

        Vector2 radius = _background.sizeDelta * 0.5f;
        Vector2 input = new(
            radius.x > 0f ? localPoint.x / radius.x : 0f,
            radius.y > 0f ? localPoint.y / radius.y : 0f
        );

        SetMoveInput(input);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ClearMoveInput();
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        _moveInput = Vector2.ClampMagnitude(moveInput, 1f);
        UpdateHandlePosition();
    }

    public void SetMoveInput(float horizontal, float vertical)
    {
        SetMoveInput(new Vector2(horizontal, vertical));
    }

    public void ClearMoveInput()
    {
        _moveInput = Vector2.zero;
        UpdateHandlePosition();
    }

    public Vector2 ReadMoveInput()
    {
        return _moveInput;
    }

    private void UpdateHandlePosition()
    {
        if (_handle == null || _background == null) return;

        Vector2 radius = _background.sizeDelta * 0.5f * _handleRange;
        _handle.anchoredPosition = new Vector2(_moveInput.x * radius.x, _moveInput.y * radius.y);
    }
}
