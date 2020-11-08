using UnityEngine;

public class ScreenBounds
{
	public Vector2 ScreenSize()
	{
		float height = Camera.main.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;

		return new Vector2(width, height);
	}

	public Vector2 TopLeftScreen()
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
	}
	public Vector2 TopRightScreen()
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.nearClipPlane));
	}

	public Vector2 BottomLeftScreen()
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
	}
	public Vector2 BottomRightScreen()
	{
		return new Vector2(TopRightScreen().x, BottomLeftScreen().y);
	}
}