using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private Transform bodyHolder;
    [SerializeField] private LeaderboardItem item;

    private Bounds bounds;

    private void Awake()
    {
        scroll.onValueChanged.AddListener(OnScrolling);
    }

    private IEnumerator Start()
    {
        // Если не пропустить первый кадр, то расчет границ и позиции Item может быть не точным.
        yield return new WaitForEndOfFrame();

        InitBounds();
        InitItemBodyPosition();
    }

    private void InitBounds()
    {
        // Определеяем границы Viewport и размер Item в мировых координатах.
        Bounds viewportBounds = ScriptUtils.GetWorldBoundsFromRect(scroll.viewport);
        Vector3 itemSize = ScriptUtils.GetWorldBoundsFromRect(item.rect).size;

        // offset - половина размера Item по Y. 
        Vector3 offset = new Vector3(0, itemSize.y / 2f, 0);

        // Уменьшаем границы Viewport на величину offset сверху и снизу, чтобы Item не уходил на половину за Viewport.
        Vector3 min = viewportBounds.min + offset;
        Vector3 max = viewportBounds.max - offset;

        // Сохраняем рассчитаные границы.
        bounds.SetMinMax(min, max);
    }

    private void InitItemBodyPosition()
    {
        // Если Item изначально находится за границами и его не видно в скролле,
        // то закрепляем его Body к границе.
        var itemPos = item.transform.position;

        if (!bounds.Contains(itemPos))
            item.UnhookBody(bodyHolder, bounds);
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
            item.UnhookBody(bodyHolder, bounds);
        }
    }
}
