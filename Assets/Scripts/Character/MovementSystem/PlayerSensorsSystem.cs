using UnityEngine;


public class PlayerSensorsSystem : MonoBehaviour
{

    [Space(5)]
    [Header("Сенсоры:")]

    [Space(5)]
    [SerializeField] private float _checkDistance = 1f;
    [SerializeField] private float _minSafeDistance = 1f;

    [Space(5)]
    [SerializeField] private LayerMask _obstacleLayer;

    [Space(5)]
    public float LeftBlockBorderX;
    public float RightBlockBorderX;
    public float TopBlockBorderY;
    public float BottomBlockBorderY;




    private void FixedUpdate()
    {

        UpdateObstacleInfo();

    }



    private void UpdateObstacleInfo()
    {

        UpdateBorder(Vector2.left, new Vector3[] {
        Vector3.zero,
        new(0, 0.5f, 0),
        new(0, -0.5f, 0)
    }, isHorizontal: true, isLeftOrDown: true);

        UpdateBorder(Vector2.right, new Vector3[] {
        Vector3.zero,
        new(0, 0.5f, 0),
        new(0, -0.5f, 0)
    }, isHorizontal: true, isLeftOrDown: false);

        UpdateBorder(Vector2.up, new Vector3[] {
        Vector3.zero,
        new(0.5f, 0, 0),
        new(-0.5f, 0, 0)
    }, isHorizontal: false, isLeftOrDown: false);

        UpdateBorder(Vector2.down, new Vector3[] {
        Vector3.zero,
        new(0.5f, 0, 0),
        new(-0.5f, 0, 0)
    }, isHorizontal: false, isLeftOrDown: true);

    }



    private void UpdateBorder(Vector2 dir, Vector3[] offsets, bool isHorizontal, bool isLeftOrDown)
    {

        float borderValue = isLeftOrDown ? float.MinValue : float.MaxValue;

        foreach (var offset in offsets)
        {
            Vector3 origin = transform.position + offset;
            RaycastHit2D hit = Physics2D.Raycast(origin, dir, _checkDistance, _obstacleLayer);

            Debug.DrawRay(origin, dir * _checkDistance, Color.red);

            if (hit.collider != null)
            {
                if (isHorizontal)
                {
                    float x = hit.point.x + (isLeftOrDown ? 1 : -1) * _minSafeDistance;
                    if (isLeftOrDown) borderValue = Mathf.Max(borderValue, x);
                    else borderValue = Mathf.Min(borderValue, x);
                }
                else
                {
                    float y = hit.point.y + (isLeftOrDown ? 1 : -1) * _minSafeDistance;
                    if (isLeftOrDown) borderValue = Mathf.Max(borderValue, y);
                    else borderValue = Mathf.Min(borderValue, y);
                }
            }
        }

        if (isHorizontal)
        {
            if (isLeftOrDown) LeftBlockBorderX = borderValue;
            else RightBlockBorderX = borderValue;
        }
        else
        {
            if (isLeftOrDown) BottomBlockBorderY = borderValue;
            else TopBlockBorderY = borderValue;
        }

    }

}
