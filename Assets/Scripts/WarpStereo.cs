using UnityEngine;
using System.Collections;

public class WarpStereo : Warp {
	// encoded warp map. (the warp map: tablet's pixel position -> camera's pixel position)
	// Because the number of pixels exceed 256, when saving as picture, I encoded the x,y pixel
	// coordinates as RGBA channels.
	public Texture2D[] encodedMaps;
	Texture2D[] decodedMaps;

	// Use this for initialization
	protected override void Start () {
		const int kNumEye = 2;
		Debug.Assert(encodedMaps.Length == kNumEye);
		decodedMaps = new Texture2D[kNumEye];
		material = GetComponent<Renderer>().material;
		for (int e = 0; e < kNumEye; ++e)
			ConvertRGBATexture2Map(encodedMaps[e], mapDiv, out decodedMaps[e]);
		material.SetTexture("_MapTexL", decodedMaps[0]);
		material.SetTexture("_MapTexR", decodedMaps[1]);
	}
		
	// Update is called once per frame
	// LateUpdate is called after update but before rendering
	protected virtual void LateUpdate () {
		// although the texture's rotating eulerZ degree, the uv needs to rotate -eulerZ
		Quaternion rot = Quaternion.Euler (0, 0,  -RotationManager.RotationAngle);
		Matrix4x4 m = Matrix4x4.Scale(new Vector3(1.0f/tabletScreenScale.x, 1.0f/tabletScreenScale.y, 1f)) 
			* Matrix4x4.TRS (Vector3.zero, rot, tabletScreenScale);
		//material.SetMatrix ("_TextureRotation", m);
		material.SetVector("_TexRotationVec", new Vector4(m[0,0], m[0,1], m[1,0], m[1,1]));
		material.SetFloat ("_power", power);
		material.SetFloat ("_alpha", alpha);
	}
}