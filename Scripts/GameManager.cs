using System.Collections;
using System.Collections.Generic;
// Assets/Scripts/GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector] public int schedule;       // 1=학교,2=운동,3=데이트
    [HideInInspector] public int selectedTop;
    [HideInInspector] public int selectedBottom;
    public int score { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    /// ScheduleSelector 스크립트에서 호출: 선택된 스케줄(1,2,3)을 저장
    public void ScheduleSelector(int choice)
    {
        // 재생할 사운드
        AudioManager.Instance.PlayScheduleSelect();

        // 기존 로직
        schedule = choice;
        Debug.Log($"[GameManager] schedule = {schedule}");
    }
    // ────────────────────

    public void CalculateScore()
    {
        bool topOK = (schedule == 1 && selectedTop == 2)
                  || (schedule == 2 && selectedTop == 1)
                  || (schedule == 3 && selectedTop == 3);
        bool bottomOK = (schedule == 1 && selectedBottom == 1)
                     || (schedule == 2 && selectedBottom == 2)
                     || (schedule == 3 && selectedBottom == 3);

        if (topOK && bottomOK) score = 30;
        else if (topOK || bottomOK) score = 20;
        else score = 10;
    }

    public void GoToResultScene()
    {
        Debug.Log($"[GameManager] Calculating: schedule={schedule}, top={selectedTop}, bottom={selectedBottom}");
        CalculateScore();
        Debug.Log($"[GameManager] Calculated score = {score}");
        SceneManager.LoadScene("ResultScene");
    }

}