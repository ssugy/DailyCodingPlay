using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField][Range(2, 5)] private int blockCount = 2; //블록갯수

    private void Awake()
    {
        int cellSize = 300 - 50 * (blockCount - 2);
        gridLayout.cellSize = new Vector2(cellSize, cellSize);  // 가로에 배치되는 셀 갯수

        // blockCount * blockCount 갯수만큼 블록 생성
        for (int i = 0; i < blockCount; i++)
        {
            for (int j = 0; j < blockCount; j++)
            {
                Instantiate<GameObject>(blockPrefab, gridLayout.transform);
            }
        }
    }
}
