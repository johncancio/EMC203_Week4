using UnityEditor.Build.Content;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 start;
    public Vector3 control1;
    public Vector3 control2;
    public Vector3 end;

    public float speed = 1f;
    private float t = 0f;
    private bool isCubic = false;

    public void SetPath(Vector3 start, Vector3 control1, Vector3 end, bool cubic = false, Vector3 control2 = default)
    {
        this.start = start;
        this.control1 = control1;
        this.end = end;
        this.isCubic = cubic;
        this.control2 = control2;
    }

    void Update()
    {
        t += speed * Time.deltaTime;

        if (t >= 1f)
        {
            GameManager.Instance.LoseHP();
            Destroy(gameObject);
        }
        else
        {
            if (isCubic)
            {
                transform.position = CubicBezier(start, control1, control2, end, t);
            }
            else
            {
                transform.position = QuadraticBezier(start, control1, end, t);
            }
        }
    }
    
    private Vector3 QuadraticBezier(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        return Mathf.Pow(1 - t, 2) * start + 2 * (1 - t) * t * control + t * t * end;
    }

    private Vector3 CubicBezier(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end, float t)
    {
        return Mathf.Pow(1 - t, 3) * start + 3 * Mathf.Pow(1 - t, 2) * t * control1 +
               3 * (1 - t) * t * t * control2 + t * t * t * end;
    }
}
