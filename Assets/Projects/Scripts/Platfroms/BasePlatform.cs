using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class BasePlatform : MonoBehaviour
{
    public LayerMask mask;
    public Color initColor;
    private MeshRenderer meshRenderer;
    public Transform route;
    public List<Transform> points;
    [SerializeField] private bool[] isPointOccupied;
    public virtual void InitializePlatForm()
    {
        meshRenderer = this.GetComponentInChildren<MeshRenderer>();
        isPointOccupied = new bool[points.Count];
        SetColorPlatform(initColor);
    }
    public abstract void HandleLandingPlatform(Player player);
    protected void HandleSetPositionPlayerInPlatform(Player player)
    {
        bool pointOccupied = false;
        for (int i = 0; i < points.Count; i++)
        {
            pointOccupied = false;
            Collider[] colliders = Physics.OverlapSphere(points[i].position, 0.1f);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<Player>(out Player getPlayer))
                {
                    pointOccupied = getPlayer ? true : false;
                }
            }
            isPointOccupied[i] = pointOccupied;
        }
        MovePlayerToAvailablePoint(player.transform);
    }
    private int GetAvailablePointIndex()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!isPointOccupied[i])
            {
                return i;
            }
        }
        return -1;
    }
    private void MovePlayerToAvailablePoint(Transform playerTransform)
    {
        int pointIndex = GetAvailablePointIndex();
        if (pointIndex != -1)
        {
            playerTransform.DOMove(points[pointIndex].position,0.2f,false).SetEase(Ease.Linear);
            isPointOccupied[pointIndex] = true;
        }
    }
    protected void SetColorPlatform(Color color)
    {
        meshRenderer.material.SetColor("_Color", color);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (Transform point in points)
        {
            Gizmos.DrawWireSphere(point.position, 0.1f);
        }
    }
}
