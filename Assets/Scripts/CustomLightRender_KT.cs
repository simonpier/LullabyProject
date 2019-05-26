using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

//this script need to keep only one on scene and be contained renderer script(need to call "void OnWillRenderObject" Event)

public class CustomLightSystem_KT
{
    static CustomLightSystem_KT m_Instance;
    static public CustomLightSystem_KT instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new CustomLightSystem_KT();
            return m_Instance;
        }
    }

    internal HashSet<CandleLight_KT> m_Lights = new HashSet<CandleLight_KT>();

    public void Add(CandleLight_KT o)
    {
        Remove(o);
        m_Lights.Add(o);
    }
    public void Remove(CandleLight_KT o)
    {
        m_Lights.Remove(o);
    }
}

[ExecuteInEditMode]
public class CustomLightRender_KT : MonoBehaviour
{
    public Shader m_LightShader;
    private Material m_LightMaterial;

    public Mesh m_SphereMesh;

    private Dictionary<Camera, CommandBuffer> m_Cameras = new Dictionary<Camera, CommandBuffer>();


    public void OnDisable()
    {
        foreach (var cam in m_Cameras)
        {
            if (cam.Key)
            {
                cam.Key.RemoveCommandBuffer(CameraEvent.AfterLighting, cam.Value);
            }
        }
        Object.DestroyImmediate(m_LightMaterial);
    }


    public void OnWillRenderObject()
    {
        var act = gameObject.activeInHierarchy && enabled;
        if (!act)
        {
            OnDisable();
            return;
        }

        var cam = Camera.current;
        if (!cam)
            return;

        if (!m_LightMaterial)
        {
            m_LightMaterial = new Material(m_LightShader);
            m_LightMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        CommandBuffer buf;
        if (m_Cameras.ContainsKey(cam))
        {
            buf = m_Cameras[cam];
            buf.Clear();
        }
        else
        {
            buf = new CommandBuffer();
            buf.name = "Deferred custom lights";
            m_Cameras[cam] = buf;

            cam.AddCommandBuffer(CameraEvent.AfterLighting, buf);
        }

        var system = CustomLightSystem_KT.instance;

        var propParams = Shader.PropertyToID("_CustomLightParams");
        var propColor = Shader.PropertyToID("_CustomLightColor");
        Vector4 param = Vector4.zero;
        Matrix4x4 trs = Matrix4x4.identity;

        foreach (var o in system.m_Lights)
        {
            param.x = o.firstAttenuation;
            param.y = o.bufferRange;
            param.z = 1.0f / (o.attenuationRange * o.attenuationRange);
            param.w = o.colliderRange;
            buf.SetGlobalVector(propParams, param);
            buf.SetGlobalColor(propColor, o.GetLinearColor());

            trs = Matrix4x4.TRS(o.transform.position, o.transform.rotation, new Vector3(o.attenuationRange * 2, o.attenuationRange * 2, o.attenuationRange * 2));
            buf.DrawMesh(m_SphereMesh, trs, m_LightMaterial, 0, 0);
        }
    }
}
