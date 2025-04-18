using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private LeaderboardItem item;

    private Bounds bounds;

    private void Awake()
    {
        scroll.onValueChanged.AddListener(OnScrolling);
    }

    private void Start()
    {
        InitBounds();
    }

    private void InitBounds()
    {
        Bounds viewportBounds = ScriptUtils.GetWorldBoundsFromRect(scroll.viewport);
        Vector3 itemBodySize = item.GetBodyWorldSize();

        Vector3 offset = new Vector3(0, itemBodySize.y / 2f, 0);
        Vector3 min = viewportBounds.min + offset;
        Vector3 max = viewportBounds.max - offset;

        bounds.SetMinMax(min, max);
    }

    private void OnScrolling(Vector2 value)
    {
        var itemPos = item.transform.position;

        if (bounds.Contains(itemPos))
        {
            item.HookBody();
        }
        else
        {
            item.UnhookBody(scroll.viewport);
        }
    }
}
