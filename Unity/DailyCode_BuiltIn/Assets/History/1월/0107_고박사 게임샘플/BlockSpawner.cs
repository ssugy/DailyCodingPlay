using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField][Range(2, 5)] private int blockCount = 2; //��ϰ���

    private void Awake()
    {
        int cellSize = 300 - 50 * (blockCount - 2);
        gridLayout.cellSize = new Vector2(cellSize, cellSize);  // ���ο� ��ġ�Ǵ� �� ����

        // blockCount * blockCount ������ŭ ��� ����
        for (int i = 0; i < blockCount; i++)
        {
            for (int j = 0; j < blockCount; j++)
            {
                Instantiate<GameObject>(blockPrefab, gridLayout.transform);
            }
        }
    }
}
