using UnityEngine;
using System.Collections;

public class Shadowmap : MonoBehaviour {

	public Light _light = null;
	public Shader _depthShader = null;
	public int _depthTextureScale = 1;

	private Camera _camera = null;
	private RenderTexture _renderTexture = null;

	// Use this for initialization
	void Start () {
	
		InitCamera ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPreCull() {

		_camera.RenderWithShader (_depthShader, "RenderType");
		Shader.SetGlobalTexture ("_shadowMap", _renderTexture);
		Shader.SetGlobalMatrix ("_shadowMapMatrix", _camera.projectionMatrix * _camera.worldToCameraMatrix);
	}

	private void InitCamera () {

		_camera = _light.gameObject.AddComponent<Camera> ();
		_camera.cullingMask = 1 << LayerMask.NameToLayer ("Shadow");
		_camera.clearFlags = CameraClearFlags.SolidColor;
		_camera.backgroundColor = new Color(1,1,1,1);
		_camera.fieldOfView = 80;
		_camera.useOcclusionCulling = false;
		_camera.enabled = false;

		_renderTexture = new RenderTexture (Screen.width * _depthTextureScale, Screen.height * _depthTextureScale, 0);
		_renderTexture.format = RenderTextureFormat.R8;
		_camera.targetTexture = _renderTexture;
		_camera.depthTextureMode = DepthTextureMode.Depth;
	}
}
