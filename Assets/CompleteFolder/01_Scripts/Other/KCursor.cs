using UnityEngine;

public class KCursor : MonoBehaviour
{
    public Texture2D _cursor;
    public Vector2 _hotspot;
    private void Awake()
    {
        Cursor.SetCursor(_cursor,_hotspot,CursorMode.ForceSoftware);
    }
}
