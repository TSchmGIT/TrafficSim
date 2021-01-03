using UnityEngine;

/// <summary>
/// Reprents a street in the world that can be connected to other streets
/// </summary>
public class Street : MonoBehaviour
{
    #region Public Properties
    /// <summary>
    /// The in connection where this street is connected to a previous street
    /// </summary>
    public Transform connectionIn => m_ConnectionIn;
    /// <summary>
    /// The out connection where a street can be added 
    /// </summary>
    public Transform connectionOut => m_ConnectionOut;
    #endregion

    #region Unity References
    [SerializeField] private Transform m_ConnectionIn = default;
    [SerializeField] private Transform m_ConnectionOut = default;
    #endregion
}
