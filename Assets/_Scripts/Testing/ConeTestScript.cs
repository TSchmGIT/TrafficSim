using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConeTestScript : MonoBehaviour
{
    [SerializeField] private Transform m_Origin = default;
    [SerializeField] private Transform m_Target = default;
    [SerializeField] private Transform[] m_PotentialTargets = default;

    [SerializeField, Range(0.0f, 100.0f)] private float m_Distance = 50.0f;
    [SerializeField, Range(0.0f, 90.0f)] private float m_Angle = 15.0f;

    public List<Vector3> hitList { get; private set; } = new List<Vector3>();

    public void DoCheck()
    {
        hitList = TPhysics.ConeCast(m_PotentialTargets.Select(transform => transform.position), m_Origin.position, m_Target.position - m_Origin.position, m_Distance, m_Angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_Origin.position, m_Origin.position + (m_Target.position - m_Origin.position).normalized * m_Distance);

        Gizmos.color = Color.white;
        foreach (Vector3 point in hitList)
        {
            Gizmos.DrawLine(m_Origin.position, point);
        }
    }
}
