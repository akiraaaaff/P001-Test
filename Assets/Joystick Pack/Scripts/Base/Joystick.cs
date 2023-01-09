using Lockstep.Math;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static LFloat Horizontal { get { return (snapX) ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x; } }
    public static LFloat Vertical { get { return (snapY) ? SnapFloat(input.y, AxisOptions.Vertical) : input.y; } }
    public static LVector2 Direction { get { return new (Horizontal, Vertical); } }

    public AxisOptions AxisOptions { get { return AxisOptions; } set { axisOptions = value; } }
    public static bool SnapX { get { return snapX; } set { snapX = value; } }
    public static bool SnapY { get { return snapY; } set { snapY = value; } }

    private readonly float handleRange = 1;
    private readonly float deadZone = 0;
    private static AxisOptions axisOptions = AxisOptions.Both;
    private static bool snapX = true;
    private static bool snapY = true;

    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;
    private RectTransform baseRect = null;

    private Canvas canvas;
    private Camera cam;

    public static LVector2 input = LVector2.zero;

    protected virtual void Start()
    {
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        var floatInput = (eventData.position - position) / (radius * canvas.scaleFactor);
        input = floatInput.ToLVector2();
        FormatInput();
        HandleInput(floatInput.magnitude, floatInput.normalized, radius, cam);
        handle.anchoredPosition = input.ToVector2() * radius * handleRange;
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised.ToLVector2();
        }
        else
            input = LVector2.zero;
    }

    private void FormatInput()
    {
        if (axisOptions == AxisOptions.Horizontal)
            input = new LVector2(input.x, LFloat.zero);
        else if (axisOptions == AxisOptions.Vertical)
            input = new LVector2(LFloat.zero, input.y);
    }

    private static LFloat SnapFloat(LFloat value, AxisOptions snapAxis)
    {
        if (value == 0)
            return value;

        if (axisOptions == AxisOptions.Both)
        {
            LFloat angle = LMath.AngleInt(input, LVector2.up);
            if (snapAxis == AxisOptions.Horizontal)
            {
                if (angle < new LFloat(true,22500) || angle > new LFloat(true, 157500))
                    return LFloat.zero;
                else
                    return (value > 0) ? LFloat.one : -LFloat.one;
            }
            else if (snapAxis == AxisOptions.Vertical)
            {
                if (angle > new LFloat(true, 67500) && angle < new LFloat(true, 112500))
                    return LFloat.zero;
                else
                    return (value > 0) ? LFloat.one : -LFloat.one;
            }
            return value;
        }
        else
        {
            if (value > 0)
                return LFloat.one;
            if (value < 0)
                return -LFloat.one;
        }
        return LFloat.zero;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        input = LVector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }
        return Vector2.zero;
    }
}

public enum AxisOptions { Both, Horizontal, Vertical }