using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// ResultScene에서 화면을 클릭/터치하면
/// ScheduleScene으로 전환
public class ResultClickToSchedule : MonoBehaviour
{
    void Update()
    {
        // 마우스 좌클릭이나 첫 번째 터치가 시작될 때
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭음 재생
            AudioManager.Instance.PlayPick();

            // 씬 전환
            SceneManager.LoadScene("ScheduleScene");
        }
    }
}

