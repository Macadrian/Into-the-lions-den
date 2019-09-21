using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameRendering : MonoBehaviour
{
    [Header("Maps")]
    public RenderTexture texturaShadowMapInicial;   // This one is from 540 degrees worth (to handle wraparound). Needs two lookups to sample a given angle.
    public RenderTexture texturaShadowMapFinal;     // This one is reduced to 360 degrees from the above. Only needs one lookup to sample an angle.

    [Header("Materials")]
    public Material shadowMapMaterial;     
    public Material shadowMapOptimiseMaterial;     

    private CommandBuffer commandBuffer;
    private Mesh mallaDinamicaDeOclusion = null;
    private Mesh mShadowMapOptimiseMesh = null;



    public static GameRendering Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //Falta algo aquí que no entiendo.
    }

    private void OnPreRender()
    {
        if (commandBuffer == null)
        {
            commandBuffer = new CommandBuffer();

            var camera = GetComponent<Camera>();
            camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, commandBuffer);
        }

        //Rehacer la malla oclusora dinámica
        mallaDinamicaDeOclusion = GameManager.Instance.GetMallaOclusores();

        if (mallaDinamicaDeOclusion != null)
        {
            /*
             * Antes de utilizar el commandBuffer debemos limpiarlo, 
             * y lo mismo ocurre con la textura del shadowMap inicial.
             */

            int clearDepthValue = 1;

            commandBuffer.Clear();
            commandBuffer.SetRenderTarget(texturaShadowMapInicial);
            commandBuffer.ClearRenderTarget(true, true, Color.black, clearDepthValue);

            /* 
             * Render the shadow maps for everything which casts its own shadows. The shadow blockers
             * are pre-rendered for each light source in their polar space, writing to a different
             * row in the shadow map each time (rows are allocated by ShadowCaster.ShadowMapAlloc)
             */

            foreach (ShadowCaster caster in ShadowCaster.shadowCasters)
            {
                MaterialPropertyBlock properties = caster.BindShadowMap(texturaShadowMapFinal);
                if (properties != null)
                {
                    //commandBuffer.DrawMesh(shadow)
                }
            }
            /*
             * Reduce the shadow map to a texture which we can take a single sample from,
             * eliminating the extra 180 degress wraparound region.
             */

            //Falta algo aquí que no entiendo.
        }
    }
}
