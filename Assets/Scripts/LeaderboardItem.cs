using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private RectTransform body;

    public bool hasBody { get; private set; } = true;

    public void UnhookBody(Transform parent)
    {
        if (!hasBody) return;
        Debug.Log("UnhookBody");
        hasBody = false;
        body.SetParent(parent);
    }

    public void HookBody()
    {
        if (hasBody) return;
        Debug.Log("HookBody");
        hasBody = true;
        body.SetParent(transform);
        body.localPosition = Vector3.zero;
    }

    public Vector3 GetBodyWorldSize()
    {
        return ScriptUtils.GetWorldBoundsFromRect(body).size;
    }
}
