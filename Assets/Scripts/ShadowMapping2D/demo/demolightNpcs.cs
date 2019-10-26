using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demolightNpcs : GameLight
{
    public Vector3 mVel;

    public Vector3 GetRandomVel()
    {
        Vector3 vel = transform.position.normalized;
        vel.x = vel.x * Random.Range(0.5f, 1.0f);
        vel.y = vel.y * Random.Range(0.5f, 1.0f);
        vel.z = 0.0f;
        return vel;
    }

    // Use this for initialization
    void Start()
    {
        Spread = 360f;//Random.Range(45.0f, 360f);

        mColour = new Color(Random.Range(0.25f, 1.0f), Random.Range(0.25f, 1.0f), Random.Range(0.25f, 1.0f), 1.0f);

        base.Start();
    }

    public override MaterialPropertyBlock BindShadowMap(Texture shadowMapTexture)
    {
        Vector4 shadowMapParams = GetShadowMapParams(shadowMapSlot);

        Vector3 position = GameManager.Instance.playerTransform.position;

        Debug.Log(transform.position);

        materialPropertyBlock.SetVector("_LightPosition", new Vector4(position.x, position.y, mAngle * Mathf.Deg2Rad, mSpread * Mathf.Deg2Rad * 0.5f));
        materialPropertyBlock.SetVector("_ShadowMapParams", shadowMapParams);

        SpriteRenderer mr = GetComponent<SpriteRenderer>();

        Material mat = mr.materials[0];

        float radius = mRadius;

        mat.SetVector("_Color", mColour);
        mat.SetVector("_LightPosition", new Vector4(position.x, position.y, mFalloffExponent, mAngleFalloffExponent));
        mat.SetVector("_Params2", new Vector4(mAngle * Mathf.Deg2Rad, mSpread * Mathf.Deg2Rad * 0.5f, 1.0f / ((1.0f - mFullBrightRadius) * radius), mFullBrightRadius * radius));
        mat.SetVector("_ShadowMapParams", shadowMapParams);
        mat.SetTexture("_ShadowTex", shadowMapTexture);

        return materialPropertyBlock;
    }
}
