using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobsUtils : MonoBehaviour
{
    public static void SetupGridLayoutGroup(GameObject _gameObject)
    {
        GridLayoutGroup gridLayoutGroup = _gameObject.AddComponent<GridLayoutGroup>();
        gridLayoutGroup.padding.top = JobConstants.gridTopPadding;
        gridLayoutGroup.padding.left = JobConstants.gridLeftPadding;
        gridLayoutGroup.cellSize = JobConstants.gridCellSize;
        gridLayoutGroup.spacing = JobConstants.gridSpacing; 
    }
}
