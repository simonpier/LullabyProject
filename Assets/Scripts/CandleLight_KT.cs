using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(CircleCollider2D))]
public class CandleLight_KT : MonoBehaviour
{
    //player attack range and first light range
    [Header("Lighting and Collider"), Range(0.001f, 100)]
    public float colliderRange;

    //the range between first light range and second light range
    [Header("Lighting only"), Range(0.001f, 10)]
    public float bufferRange;
    //the range outside collider range
    [Range(0.001f, 100)]
    public float attenuationRange;

    //this parameter is storonger blar as nearly as 1;
    [SerializeField, Range(0, 1)]
    public float firstAttenuation;

    //under here, the member variable same as normal light 
    public Color lightColor;
    [Range(0.001f, 100)]
    public float lightIntencsity;

    private CircleCollider2D _circle;

    public void OnEnable()
    {
        CustomLightSystem_KT.instance.Add(this);
    }

    public void Start()
    {
        CustomLightSystem_KT.instance.Add(this);
        _circle = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (!Application.isPlaying) _circle.radius = colliderRange;
    }

    public void OnDisable()
    {
        CustomLightSystem_KT.instance.Remove(this);
    }

    public Color GetLinearColor()
    {
        return new Color(
            Mathf.GammaToLinearSpace(lightColor.r * lightIntencsity),
            Mathf.GammaToLinearSpace(lightColor.g * lightIntencsity),
            Mathf.GammaToLinearSpace(lightColor.b * lightIntencsity),
            1.0f
        );
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "PointLight Gizmo", true);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(lightColor.r, lightColor.g, lightColor.b, 0.5f);
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.DrawWireSphere(transform.position, colliderRange + bufferRange);
        Gizmos.color = new Color(lightColor.r, lightColor.g, lightColor.b, 0.2f);
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.DrawWireSphere(transform.position, attenuationRange);
    }
}