using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    public Renderer TintRenderer;
    public int TintMaterialSlot;

    public void SetColor(Color c)
    {
        var prop = new MaterialPropertyBlock();
        prop.SetColor("_BaseColor", c);
        TintRenderer.SetPropertyBlock(prop, TintMaterialSlot);
    }
}
