using UnityEngine;
using System.Collections;

public class TextureTileScaling : MonoBehaviour
{

    // Give us the texture so that we can scale proportianally the width according to the height variable below
    // We will grab it from the meshRenderer
    public Texture texture;
    public float textureToMeshZ = 2f; // Use this to contrain texture to a certain size

    // Use this for initialization
    void Start()
    {
        UpdateTiling();
    }

    [ContextMenu("UpdateTiling")]
    void UpdateTiling()
    {
        // A Unity plane is 10 units x 10 units
        float planeSizeX = 10f;
        float planeSizeZ = 10f;

        // Figure out texture-to-mesh width based on user set texture-to-mesh height
        float textureToMeshX = ((float)this.texture.width / this.texture.height) * this.textureToMeshZ;

        gameObject.GetComponent<Renderer>().sharedMaterial.mainTextureScale = new Vector2(planeSizeX * gameObject.transform.lossyScale.x / textureToMeshX, planeSizeZ * gameObject.transform.lossyScale.y / textureToMeshZ);
    }
}
