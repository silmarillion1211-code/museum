using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("所有拼图碎片")]
    public List<PuzzlePiece> allPieces;
    [Header("拼图完成弹窗UI面板")]
    public GameObject completePanel;

    void Awake()
    {
        Instance = this;
    }

    public void CheckPuzzleComplete()
    {
        foreach (var piece in allPieces)
        {
            if (!piece.IsPlaced())
                return;
        }
        ShowCompleteUI();
    }

    void ShowCompleteUI()
    {
        if (completePanel != null)
        {
            completePanel.SetActive(true);
        }
        Debug.Log("拼图全部完成！");
    }
}