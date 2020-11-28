using UnityEngine;

public class ScreenBounds
{
	public Vector2 ScreenSize()
	{
		float height = UnityEngine.Camera.main.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;

		return new Vector2(width, height);
	}

	public Vector2 TopLeftScreen()
	{
		return UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(0, UnityEngine.Camera.main.pixelHeight, UnityEngine.Camera.main.nearClipPlane));
	}
	public Vector2 TopRightScreen()
	{
		return UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Camera.main.pixelWidth, UnityEngine.Camera.main.pixelHeight, UnityEngine.Camera.main.nearClipPlane));
	}

	public Vector2 BottomLeftScreen()
	{
		return UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(0, 0, UnityEngine.Camera.main.nearClipPlane));
	}
	public Vector2 BottomRightScreen()
	{
		return new Vector2(TopRightScreen().x, BottomLeftScreen().y);
	}
}