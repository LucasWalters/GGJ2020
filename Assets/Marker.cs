using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public int markerSize = 10;
    public Color32 markerColor = Color.black;
    public float drawCooldown = 0.1f;
    private Color32[] colors;
    private int lastMarkerSize;
    private Dictionary<Material, Texture> oldTextures = new Dictionary<Material, Texture>();

    private Vector3 pointOrig;
    private Vector3 pointDest;

    private float lastDraw;



    private void Start()
    {
        lastMarkerSize = markerSize;
        RebuildColorArray();
    }

    private void Update()
    {
        if (lastMarkerSize != markerSize)
        {
            lastMarkerSize = markerSize;
            RebuildColorArray();
        }
    }

    private void RebuildColorArray()
    {
        colors = new Color32[markerSize * markerSize];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = markerColor;
        }
    }

    private void OnDestroy()
    {
        foreach (Material mat in oldTextures.Keys)
        {
            mat.mainTexture = oldTextures[mat];
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Customer" || Time.time - lastDraw < drawCooldown)
        {
            return;
        }
        MeshRenderer rend = collision.gameObject.GetComponent<MeshRenderer>();
        if (rend == null || rend.material == null || rend.material.mainTexture == null)
            return;
        ContactPoint cp = collision.contacts[0];
        RaycastHit hit;
        Ray ray = new Ray(cp.point + cp.normal * 0.5f, -cp.normal);
        pointOrig = cp.point + cp.normal * 0.5f;
        pointDest = cp.point + cp.normal * 0.5f - cp.normal;

        if (cp.otherCollider.Raycast(ray, out hit, 1))
        {

            Material mat = rend.material;

            if (!oldTextures.ContainsKey(mat))
            {
                Texture2D oldText = (Texture2D)mat.mainTexture;
                Texture2D newText = new Texture2D(oldText.width, oldText.height);
                newText.SetPixels(oldText.GetPixels());
                oldTextures[mat] = oldText;
                mat.mainTexture = newText;
            }
            Texture2D texture = mat.mainTexture as Texture2D;
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= texture.width;
            pixelUV.y *= texture.height;


            bool changed;
            Color32[] checkColors = texture.GetPixels32();
            checkColors = AddCircle(checkColors, texture.width, texture.height, pixelUV, markerSize, markerColor, out changed);
            if (changed)
            {
                texture.SetPixels32(checkColors);
                texture.Apply();
                lastDraw = Time.time;
            }
        }
    }

    private Color32[] AddCircle(Color32[] original, int width, int height, Vector2 center, int radius, Color32 color, out bool changed)
    {
        changed = false;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Vector2.Distance(new Vector2(x, y), center) < radius)
                {
                    int currI = y * width + x;
                    if (!original[currI].Equals(color))
                    {
                        changed = true;
                        original[currI] = color;
                    }
                }
            }
        }
        return original;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pointOrig, pointDest);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointOrig, 0.1f);
    }
}
