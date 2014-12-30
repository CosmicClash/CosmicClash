using UnityEngine;
using System.Collections;

public class GraphicsCoreScript : MonoBehaviour
{

	void Start ()
	{
	
	}
	void Update ()
	{
	
	}
	public static void _SetMaterial (GameObject obj, string materialName)
	{
		Material mat = Resources.Load( materialName, typeof(Material)) as Material;
		obj.renderer.material = mat;
	}
}
