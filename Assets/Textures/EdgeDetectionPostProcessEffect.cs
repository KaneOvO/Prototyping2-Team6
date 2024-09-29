using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EdgeDetectionPostProcessEffect : MonoBehaviour
{
    public Material postProcessMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (postProcessMaterial != null)
        {
            // 使用我们创建的材质处理屏幕图像
            Graphics.Blit(source, destination, postProcessMaterial);
        }
        else
        {
            // 如果没有指定材质，直接显示原图像
            Graphics.Blit(source, destination);
        }
    }
}