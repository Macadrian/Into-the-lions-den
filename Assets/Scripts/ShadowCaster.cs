using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShadowCaster : MonoBehaviour
{
    public int shadowMapSlot = -1;
    public MaterialPropertyBlock materialPropertyBlock;
    public static List<ShadowCaster> shadowCasters = new List<ShadowCaster>();

    public const int MAX_SHADOW_MAPS = 64; //Debe coincidir con el alto de la textura del mapa de sombras

    private static ulong shadowMapAllocator = 0; 

    protected virtual void Awake()
    {
        materialPropertyBlock = new MaterialPropertyBlock();   
    }

    protected virtual void OnEnable()
    {
        shadowMapSlot = ShadowMapAlloc();
        shadowCasters.Add(this);
    }

    protected virtual void OnDisable()
    {
        shadowCasters.Remove(this);
        shadowMapSlot = ShadowMapFree(shadowMapSlot);
    }

    public virtual MaterialPropertyBlock BindShadowMap(Texture shadowMapTexture)
    {
        return null;
    }

    protected static int ShadowMapFree(int shadowMapSlot)
    {
        if (shadowMapSlot >= 0)
        {
            ulong maskSlotPosition = (ulong)1 << shadowMapSlot;
            shadowMapAllocator &= ~maskSlotPosition;
        }

        return -1;
    }

    protected static int ShadowMapAlloc()
    {
        for (int slot = 0; slot < MAX_SHADOW_MAPS; slot++)
        {
            ulong maskSlotPosition = (ulong)1 << slot;
            if ((shadowMapAllocator & maskSlotPosition) == 0)
            {
                shadowMapAllocator |= maskSlotPosition;
                return slot;
            }
        }

        return -1;
    }

    /// <summary>
    /// calculate the parameters used to read and write the 1D shadow map.
    /// x = parameter for reading shadow map (uv space (0,1))
    /// y = parameter for writing shadow map (clip space (-1,+1))
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public static Vector4 GetShadowMapParams(int slot)
    {
        float u1 = ((float)slot + 0.5f) / MAX_SHADOW_MAPS;
        float u2 = (u1 - 0.5f) * 2.0f;

        if (
               (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore)
            || (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2)
            || (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3)
            )
        {
            return new Vector4(u1, u2, 0.0f, 0.0f);
        }
        else
        {
            return new Vector4(1.0f - u1, u2, 0.0f, 0.0f);
        }
    }
}
