using UnityEngine;

public class BackgroundFit : MonoBehaviour
{
    private void Start()
    {
        FitBackground();
    }

    void FitBackground()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr == null) return;

        float worldHeight = Camera.main.orthographicSize * 2;
        float worldWidth = worldHeight * Screen.width / Screen.height;

        Vector2 spriteSize = sr.sprite.bounds.size;

        float scaleX = worldWidth / spriteSize.x;
        float scaleY = worldHeight / spriteSize.y;

        float scale = Mathf.Max(scaleX, scaleY);

        transform.localScale = new Vector3(scale, scale, 1);
    }
}