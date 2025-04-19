using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private RectTransform _body;

    public RectTransform rect => _rect;
    public RectTransform body => _body;

    private bool isBodyHooked = true;

    public void UnhookBody(Transform parent, Bounds bounds)
    {
        if (!isBodyHooked) return;

        isBodyHooked = false;
        _body.SetParent(parent);

        bool isBottom = transform.position.y < bounds.center.y;
        var bodyPos = _body.transform.position;

        bodyPos.y = isBottom ? bounds.min.y : bounds.max.y;
        _body.transform.position = bodyPos;
    }

    public void HookBody()
    {
        if (isBodyHooked) return;
        
        isBodyHooked = true;
        _body.SetParent(transform);
        _body.localPosition = Vector3.zero;
    }
}
