using UnityEngine;
using System.Collections;

public class BillboardScript : MonoBehaviour
{
	public GameObject thisObj;
	public Transform cameraTransform;

	void Start ()
	{
		this.thisObj = this.gameObject;
		this.cameraTransform = Camera.main.transform;
		this.thisObj.transform.right = this.cameraTransform.right;
		this.thisObj.transform.up = this.cameraTransform.up;
		this.thisObj.transform.forward = this.cameraTransform.forward * -1.0f;
	}
	void Update ()
	{

	}
}
