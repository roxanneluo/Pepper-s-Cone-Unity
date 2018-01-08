using UnityEngine;
using System.Collections;

public class WarpStereo : MonoBehaviour {
	public Texture2D[] encodedMaps;
	public float mapDiv = 0x3FFF;
	public bool flipTexture = true;
	public float power = 1, alpha = 1;
	public float addAngle = 0;
	static int LOAD_TEX_COLOR_BIT_DEPTH = 8;
	Material material;
	Texture2D[] maps;
	Transform rotate;
	Vector3 iPadScale = new Vector3 (4f, 3f,  1f);
	// Use this for initialization
	void Start () {
		const int kNumEye = 2;
		Debug.Assert(encodedMaps.Length == kNumEye);
		maps = new Texture2D[kNumEye];
		material = GetComponent<Renderer>().material;
		for (int e = 0; e < kNumEye; ++e)
			cvtRGBATexture2Map(encodedMaps[e], mapDiv, out maps[e]);
		material.SetTexture("_MapTexL", maps[0]);
		material.SetTexture("_MapTexR", maps[1]);

		//		rotate = GameObject.Find ("ModelController").transform;
		//		print ("rotate = " + rotate);
	}

	void cvtRGBATexture2Map(Texture2D encodedMap, float mapDiv, out Texture2D map) {
		map = new Texture2D(encodedMap.width, encodedMap.height, TextureFormat.RGFloat, false);
		map.wrapMode = TextureWrapMode.Clamp;


		Color32[] encodedColor32 = encodedMap.GetPixels32();
		Color[] mapColor = new Color[encodedColor32.Length];

		print("length = " + encodedColor32.Length);
		Color32 ec;
		for (int i = 0; i < mapColor.Length; ++i)
		{
			ec = encodedColor32[i];
			mapColor[i].r = ((ec.r << LOAD_TEX_COLOR_BIT_DEPTH) + ec.g) / mapDiv;
			//!! IMPORTANT: origin of opencv image is at top left while the origin of 
			// textures in unity is at bottom left. So map_y needs to be flipped
			mapColor[i].g = ((ec.b << LOAD_TEX_COLOR_BIT_DEPTH) + ec.a) / mapDiv; 
			if (flipTexture)
				mapColor [i].g = 1 - mapColor [i].g;
			//print(i + ": " + mapColor[i]);
		}
		print("done.");

		map.SetPixels(mapColor);
		map.Apply();
	}

	// Update is called once per frame
	// LateUpdate is called after update but before rendering
	void LateUpdate () {
		// although the texture's rotating eulerZ degree, the uv needs to rotate -eulerZ
		Quaternion rot = Quaternion.Euler (0, 0, -RotationManager.RotationAngle+addAngle);
		Matrix4x4 m = Matrix4x4.Scale(new Vector3(1.0f/iPadScale.x, 1.0f/iPadScale.y, 1f)) 
			* Matrix4x4.TRS (Vector3.zero, rot, iPadScale);
		//material.SetMatrix ("_TextureRotation", m);
		material.SetVector("_TexRotationVec", new Vector4(m[0,0], m[0,1], m[1,0], m[1,1]));
		material.SetFloat ("_power", power);
		material.SetFloat ("_alpha", alpha);
	}
}