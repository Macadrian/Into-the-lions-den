﻿using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class GameRendering : MonoBehaviour
{
	private CommandBuffer mCommandBuffer1;
	private Mesh mShadowBlockerDynamicMesh = null;
	public RenderTexture mShadowMapInitialTexture;  // This one is from 540 degrees worth (to handle wraparound). Needs two lookups to sample a given angle.
    public RenderTexture mShadowMapFinalTexture;    // This one is reduced to 360 degrees from the above. Only needs one lookup to sample an angle.
    public Material mShadowMapMaterial;
    private Mesh mShadowMapOptimiseMesh = null;
    public Material mShadowMapOptimiseMaterial;

    public static GameRendering Instance
    {
        get;
        private set;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        mShadowMapOptimiseMesh = MakeFullscreenRenderMesh();

        //Debug.Assert(mShadowMapInitialTexture.height == ShadowCaster.MAX_SHADOW_MAPS);
    }

    public void OnPreRender()
    {
        if (mCommandBuffer1 == null)
        {
            mCommandBuffer1 = new CommandBuffer();

            Camera camera = GetComponent<Camera>();
            camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, mCommandBuffer1);
        }

        // Regenerate dynamic blocker mesh
        mShadowBlockerDynamicMesh = GameManager.Instance.GetMallaOclusores(); 

        if (mShadowBlockerDynamicMesh != null)
        {
            mCommandBuffer1.Clear();

            mCommandBuffer1.SetRenderTarget(mShadowMapInitialTexture);

            mCommandBuffer1.ClearRenderTarget(true, true, Color.white, 1.0f);

            // Render the shadow maps for everything which casts its own shadows. The shadow blockers
            // are pre-rendered for each light source in their polar space, writing to a different
            // row in the shadow map each time (rows are allocated by ShadowCaster.ShadowMapAlloc)
            {
				foreach (ShadowCaster caster in ShadowCaster.shadowCasters)
                {
                    MaterialPropertyBlock properties = caster.BindShadowMap(mShadowMapFinalTexture);
                    if (properties != null)
                    {
                        mCommandBuffer1.DrawMesh(mShadowBlockerDynamicMesh, Matrix4x4.identity, mShadowMapMaterial, 0, -1, properties);
                    }
                }
            }
        }

        // Reduce the shadow map to a texture which we can take a single sample from,
        // eliminating the extra 180 degress wraparound region.
        {
            mShadowMapOptimiseMaterial.SetTexture("_ShadowMap", mShadowMapInitialTexture);

            mCommandBuffer1.SetRenderTarget(mShadowMapFinalTexture);

            mCommandBuffer1.DrawMesh(mShadowMapOptimiseMesh, Matrix4x4.identity, mShadowMapOptimiseMaterial);
        }
    }

    // Make a simple mesh suitable for doing a fullscreen shader
    // pass, e.g. fills the screen with uvs going from (0,0) to (1,1)
    public static Mesh MakeFullscreenRenderMesh()
    {
        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs0 = new List<Vector2>();
        int[] indices = new int[6];

        verts.Add(new Vector3(-1.0f, +1.0f, 0.0f));
        verts.Add(new Vector3(+1.0f, +1.0f, 0.0f));
        verts.Add(new Vector3(+1.0f, -1.0f, 0.0f));
        verts.Add(new Vector3(-1.0f, -1.0f, 0.0f));

        uvs0.Add(new Vector2(0.0f, 0.0f));
        uvs0.Add(new Vector2(1.0f, 0.0f));
        uvs0.Add(new Vector2(1.0f, 1.0f));
        uvs0.Add(new Vector2(0.0f, 1.0f));

        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;
        indices[3] = 0;
        indices[4] = 2;
        indices[5] = 3;

        Mesh mesh = new Mesh();
        mesh.SetVertices(verts);
        mesh.SetUVs(0, uvs0);
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        return mesh;
    }
}
