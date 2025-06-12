using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScheduleButton : MonoBehaviour
{
    public int scheduleType;               // Inspector 에 1/2/3 설정
    public float delayBeforeLoad = 0.5f;   // 씬 전환 전 지연 시간

    // Button → On Click() 에 연결
    public void OnClickSchedule()
    {
        Debug.Log($"[ScheduleButton] 클릭! scheduleType={scheduleType}");
        StartCoroutine(DelayedLoad());
    }

    private IEnumerator DelayedLoad()
    {
        Debug.Log("[ScheduleButton] 코루틴 시작, 효과음 재생");
        AudioManager.Instance.PlayScheduleSelect();

        Debug.Log($"[ScheduleButton] {delayBeforeLoad}초 대기");
        yield return new WaitForSeconds(delayBeforeLoad);

        Debug.Log("[ScheduleButton] 값 저장 및 씬 전환");
        GameManager.Instance.schedule = scheduleType;
        SceneManager.LoadScene("DressUpScene");
    }
}
