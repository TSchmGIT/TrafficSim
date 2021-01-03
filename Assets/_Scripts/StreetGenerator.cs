using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "StreetGenerator", menuName = "Create Street Generator")]
public class StreetGenerator : ScriptableObject
{
    #region Public Properties
    /// <summary>
    /// Time taken for generating the current street map
    /// </summary>
    public float lastGenerationDuration { get; private set; }
    #endregion

    #region Unity References
    [SerializeField] private Street m_PrefabStraight = default;
    [SerializeField] private Street m_PrefabTurnR = default;
    [SerializeField] private Street m_PrefabTurnL = default;
    [SerializeField] private Street m_PrefabCrossT = default;
    [SerializeField] private Street m_PrefabCrossFull = default;

    [SerializeField] private Transform m_Spawn = default;
    [SerializeField, Range(1, 100)] private int m_StreetAmount = 4;
    [SerializeField, Range(0f, 1f)] private float m_TurnProbability = 0.5f;
    #endregion

    #region Private Variables
    private List<Street> m_StreetList = new List<Street>();
    #endregion

    /// <summary>
    /// Generates a network of streets in the world
    /// </summary>
    public void GenerateStreets()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Remove all old streets first, so we can spawn new ones
        RemoveStreets();

        // Generate a straight line of streets
        GenerateStreetMap();

        sw.Stop();
        lastGenerationDuration = (float)sw.Elapsed.TotalMilliseconds;
    }

    private void GenerateStreetMap()
    {
        for (int i = 0; i < m_StreetAmount; ++i)
        {
            // Calculate the position
            Vector3 position = default;
            Quaternion rotation = default;
            if (i == 0) // The first street
            {
                position = m_Spawn != null ? m_Spawn.position : Vector3.zero;
                rotation = m_Spawn != null ? m_Spawn.rotation : Quaternion.identity; 
            }
            else
            {
                Street prevStreet = m_StreetList[i - 1];
                Transform connectionOutTransform = prevStreet.connectionOut;

                position = connectionOutTransform.position;
                rotation = connectionOutTransform.rotation;
            }

            // Spawn the street
            Street prefab = Random.value < m_TurnProbability ? m_PrefabTurnR : m_PrefabStraight; // Get a random prefab

            Street newStreet = Instantiate(prefab, m_Spawn);
            newStreet.gameObject.name = string.Format("Street {0}", i + 1);
            m_StreetList.Add(newStreet);

            // Calculate and apply offset to in connection position
            position -= rotation * newStreet.connectionIn.localPosition;
            newStreet.transform.position = position;
            newStreet.transform.rotation = rotation;
        }
    }

    /// <summary>
    /// Remove all streets from the world
    /// </summary>
    public void RemoveStreets()
    {
        foreach (Street street in m_StreetList)
        {
            if (street != null)
                DestroyImmediate(street.gameObject);
        }

        m_StreetList.Clear();
    }
}
