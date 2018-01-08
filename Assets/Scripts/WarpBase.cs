using UnityEngine;
using System.Collections;

/*
 * Warp the image of the model (the render texture of RenderCamera that looks at the models)
 * using the precomputed warp map so that after reflection from curved surface, 
 * the image looks undistorted.
 */
public class WarpBase : MonoBehaviour {
	public float mapDiv = 4095; //0x3FFF;
	// flip texture in y direction
	public bool flipTexture = true;
	// to increase brightness. final_intensity = alpha*original_intensity^power
	public float power = 1, alpha = 1;
	// aspect ratio of the ipad or other tablet. When rotating the coordinate in shader, I need to first
	// convert the 0-1 uv value into pixel position and then rotate
	protected Vector3 tabletScreenScale = new Vector3 (4f, 3f,  1f);
	protected Material material;

	static int LOAD_TEX_COLOR_BIT_DEPTH = 8;

	protected void ConvertRGBATexture2Map(Texture2D encodedMap, float mapDiv, out Texture2D decodedMapResult) {
		decodedMapResult = new Texture2D(encodedMap.width, encodedMap.height, TextureFormat.RGFloat, false);
		decodedMapResult.wrapMode = TextureWrapMode.Clamp;


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
		}
		print("done.");

		decodedMapResult.SetPixels(mapColor);
		decodedMapResult.Apply();
	}

	// Update is called once per frame
	// LateUpdate is called after update but before rendering
	protected virtual void LateUpdate () {
		// although the texture's rotating eulerZ degree, the uv needs to rotate -eulerZ
		Quaternion rot = Quaternion.Euler (0, 0, -RotationManager.RotationAngle);
		Matrix4x4 m = Matrix4x4.Scale(new Vector3(1.0f/tabletScreenScale.x, 1.0f/tabletScreenScale.y, 1f)) 
			* Matrix4x4.TRS (Vector3.zero, rot, tabletScreenScale);
		material.SetVector("_TexRotationVec", new Vector4(m[0,0], m[0,1], m[1,0], m[1,1]));
		material.SetFloat ("_power", power);
		material.SetFloat ("_alpha", alpha);
	}
}
