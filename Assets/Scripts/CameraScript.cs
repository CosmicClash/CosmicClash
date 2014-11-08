using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public float maxSize = 1.0f;
	public float minSize = 0.3f;
	public float speed = 0.001f;
	private float currentSize;
	
	public float dragSpeed = 2;
	private Vector3 dragOrigin;


	// Use this for initialization
	void Start ()
	{
		currentSize = (maxSize + minSize) / 2.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		float scroll = Input.GetAxis ("Mouse ScrollWheel");
//		if(scroll != 0.0f)
//		{
//			Camera.main.orthographicSize -= Mathf.Lerp(Camera.main.orthographicSize, scroll, Time.time * speed);
//			Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSize, maxSize);
//		}
//		if (Input.GetMouseButtonDown(0))
//		{
//			dragOrigin = Input.mousePosition;
//			return;
//		}
//		
//		if (!Input.GetMouseButton(0)) return;
//		
//		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
//		Vector3 move = new Vector3(pos.x * -dragSpeed, 0, pos.y * dragSpeed);
//		
//		transform.Translate(move, Space.World);  
	}
}
/*
 float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            camera.fieldOfView -= Mathf.Lerp(currentFOV, scroll, Time.time * ZoomSpeed);
 
        }
 */