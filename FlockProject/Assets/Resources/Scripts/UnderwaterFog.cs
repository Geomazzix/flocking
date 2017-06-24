using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterFog : MonoBehaviour
{
    private Material _NoSkyBox;

    private void Update()
    {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
            RenderSettings.fogDensity = 0.04f;
            RenderSettings.skybox = _NoSkyBox;
    }
}
