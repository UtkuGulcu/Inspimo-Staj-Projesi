using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private RenderTexture videoRenderTexture;
    
    private void Awake()
    {
        videoRenderTexture.Release();
        videoRenderTexture.width = Screen.width;
        videoRenderTexture.height = Screen.height - 80;
    }
}
