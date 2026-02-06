using UnityEngine;

public class MarginForText
{
    #region Positions in Screen Space - Overlay
    public static Vector3 GetTopPosition(GameObject obj, Vector2 offset = default)
    {
        Vector3 screenPos = new Vector3(
                Camera.main.WorldToScreenPoint(obj.GetComponent<BoxCollider2D>().bounds.center).x, // center of the object
                Camera.main.WorldToScreenPoint(obj.GetComponent<BoxCollider2D>().bounds.max).y, 0);
        return screenPos + (Vector3)offset;
    }
    public static Vector3 GetBottomPosition(GameObject obj, Vector2 offset = default)
    {
        Vector3 screenPos = new Vector3(
                Camera.main.WorldToScreenPoint(obj.GetComponent<BoxCollider2D>().bounds.center).x, // center of the object
                Camera.main.WorldToScreenPoint(obj.GetComponent<BoxCollider2D>().bounds.min).y, 0);
        return screenPos - (Vector3)offset;
    }
    #endregion

    #region Canvas Space/ Real Space Conversions
    public static Vector3 GetRealTop(GameObject obj, Vector2 offset = default)
    {
        Vector3 worldPos = new Vector3(
                obj.GetComponent<BoxCollider2D>().bounds.center.x, // center of the object
                obj.GetComponent<BoxCollider2D>().bounds.max.y, 0);
        return worldPos + (Vector3)offset;
    }

    public static Vector3 GetRealBottom(GameObject obj, Vector2 offset = default)
    {
        Vector3 worldPos = new Vector3(
                obj.GetComponent<BoxCollider2D>().bounds.center.x, // center of the object
                obj.GetComponent<BoxCollider2D>().bounds.min.y, 0);
        return worldPos - (Vector3)offset;
    }
    #endregion
}
