using UnityEngine;
using System.Collections;

/*
 * Warp the image of the model (the render texture of RenderCamera that looks at the models)
 * using the precomputed warp map so that after reflection from curved surface, 
 * the image looks undistorted.
 */
public class WarpMono : WarpBase {
	// encoded warp map. (the warp map: tablet's pixel position -> camera's pixel position)
	// Because the number of pixels exceed 256, when saving as picture, I encoded the x,y pixel
	// coordinates as RGBA channels.
    public Texture2D encodedMap;
	Texture2D decodedMap;

	protected virtual void Start () {
        material = GetComponent<Renderer>().material;
		ConvertRGBATexture2Map(encodedMap, mapDiv, out decodedMap);
        material.SetTexture("MapTex", decodedMap);
    }

	// Update is called once per frame
    // LateUpdate is called after update but before rendering
	protected void LateUpdate () {
		// although the texture's rotating eulerZ degree, the uv needs to rotate -eulerZ
		Quaternion rot = Quaternion.Euler (0, 0, -RotationManager.RotationAngle);
		Matrix4x4 m = Matrix4x4.Scale(new Vector3(1.0f/tabletScreenScale.x, 1.0f/tabletScreenScale.y, 1f)) 
			* Matrix4x4.TRS (Vector3.zero, rot, tabletScreenScale);
		material.SetVector("_TexRotationVec", new Vector4(m[0,0], m[0,1], m[1,0], m[1,1]));
		material.SetFloat ("_power", power);
		material.SetFloat ("_alpha", alpha);
	}
}
