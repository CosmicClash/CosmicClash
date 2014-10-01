using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public float maxSize = 1.0f;
	public float minSize = 0.3f;
	public float speed = 0.001f;
	private float currentSize;

	// Use this for initialization
	void Start ()
	{
		currentSize = (maxSize + minSize) / 2.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if(scroll != 0.0f)
		{
			Camera.main.orthographicSize -= Mathf.Lerp(Camera.main.orthographicSize, scroll, Time.time * speed);
			Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSize, maxSize);
		}
	}
}

/*
 float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            camera.fieldOfView -= Mathf.Lerp(currentFOV, scroll, Time.time * ZoomSpeed);
 
        }
 */