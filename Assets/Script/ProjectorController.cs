using UnityEngine;
using System.Collections;

public class ProjectorController : MonoBehaviour {

	private Camera projectorCamera = null;
	private Projector projector = null;
	private RenderTexture renderTexture = null;

	// Use this for initialization
	void Start () {
	
		projector = this.GetComponent<Projector> ();
		InitCamera ();
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	private void InitCamera ()
	{
		projectorCamera = this.gameObject.AddComponent<Camera> ();

		projectorCamera.clearFlags = CameraClearFlags.SolidColor;
		projectorCamera.backgroundColor = new Color (1, 1, 1, 0);
		projectorCamera.fieldOfView = projector.fieldOfView;
		projectorCamera.orthographic = projector.orthographic;
		projectorCamera.farClipPlane = projector.farClipPlane;
		projectorCamera.nearClipPlane = projector.nearClipPlane;
		projectorCamera.aspect = projector.aspectRatio;
		projectorCamera.orthographicSize = projector.orthographicSize;
		projectorCamera.cullingMask = 1 << LayerMask.NameToLayer ("Shadow");
		projectorCamera.depthTextureMode = DepthTextureMode.None;

		renderTexture = new RenderTexture (Screen.width, Screen.height, 0);
		renderTexture.format = RenderTextureFormat.ARGB4444;
		renderTexture.anisoLevel = 4;

		projectorCamera.targetTexture = renderTexture;

		projector.material.SetTexture ("_ShadowTex", renderTexture);
		Shader.SetGlobalMatrix ("_Projector", projectorCamera.projectionMatrix * projectorCamera.worldToCameraMatrix);
	}
}
