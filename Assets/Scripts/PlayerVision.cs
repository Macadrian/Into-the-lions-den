using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PlayerVision : ShadowCaster
{
    [Header("Range Properties")]
    [SerializeField] [Min(0)] private float radius = 5;
    [SerializeField] [Range(0, 360)] private float spread = 360;

    [Header("Vision Properties")]
    [Range(0, 1)] public float fullBrightRadius = 0;
    [SerializeField] [Min(0)] private float fallOffExponent = 2;
    [SerializeField] [Min(0)] private float angleFalloffExponent = 1.0f;

    private Mesh mesh;

    protected override void Awake()
    {
        base.Awake();
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        RebuildQuad();
    }

    private void RebuildQuad()
    {
        List<Vector3> verts = new List<Vector3>();

        verts.Add(new Vector3(-radius, -radius));
        verts.Add(new Vector3(+radius, -radius));
        verts.Add(new Vector3(-radius, +radius));
        verts.Add(new Vector3(+radius, +radius));

        mesh.SetVertices(verts);
    }

    public override MaterialPropertyBlock BindShadowMap(Texture shadowMapTexture)
    {
        Vector4 shadowMapParams = GetShadowMapParams(shadowMapSlot);

        materialPropertyBlock.SetVector("_LightPosition", new Vector4(transform.position.x, transform.position.y, transform.localRotation.z * Mathf.Deg2Rad, spread * Mathf.Deg2Rad * 0.5f));
        materialPropertyBlock.SetVector("_ShadowMapParams", shadowMapParams);

        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.materials[0];

        //mat.SetVector("_LightPosition", new Vector4(transform.position.x, transform.position.y, fallOffExponent, angleFalloffExponent));
        //mat.SetVector("_Params2", new Vector4(transform.localRotation.z * Mathf.Deg2Rad, spread * Mathf.Deg2Rad * 0.5f, 1.0f / ((1.0f - fullBrightRadius) * radius), fullBrightRadius * radius));
        //mat.SetVector("_ShadowMapParams", shadowMapParams);
        //mat.SetTexture("_ShadowTex", shadowMapTexture);

        return materialPropertyBlock;
    }
}
