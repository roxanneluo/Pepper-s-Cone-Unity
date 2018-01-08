using UnityEngine;
using System.Collections;

public class WarpStereo : WarpBase {
	const int kNumEyes = 2;
	// encoded warp maps, [0] for left eye and [1] for right eye. 
	// (the warp map: tablet's pixel position -> camera's pixel position)
	// Because the number of pixels exceed 256, when saving as picture, I encoded the x,y pixel
	// coordinates as RGBA channels.
	public Texture2D[] encodedMaps = new Texture2D[kNumEyes];
	Texture2D[] decodedMaps;

	// Use this for initialization
	protected void Start () {
		Debug.Assert(encodedMaps.Length == kNumEyes);
		decodedMaps = new Texture2D[kNumEyes];
		material = GetComponent<Renderer>().material;
		for (int e = 0; e < kNumEyes; ++e)
			ConvertRGBATexture2Map(encodedMaps[e], mapDiv, out decodedMaps[e]);
		material.SetTexture("_MapTexL", decodedMaps[0]);
		material.SetTexture("_MapTexR", decodedMaps[1]);
	}
		
	// Update is called once per frame
	// LateUpdate is called after update but before rendering
	protected void LateUpdate () {
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