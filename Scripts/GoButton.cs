using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoButton : MonoBehaviour
{
    // 호출 버튼에 연결된 함수
    public void OnClickGo()
    {
        StartCoroutine(PlaySoundThenGo());
    }

    private IEnumerator PlaySoundThenGo()
    {
        // 효과음 재생
        AudioManager.Instance.PlayComplete();

        // 효과음 클립 길이만큼 대기
        float clipLength = AudioManager.Instance.complete.length;
        yield return new WaitForSeconds(clipLength);

        // 디버그
        Debug.Log($"[GoButton] before GoToResultScene: schedule={GameManager.Instance.schedule}, " +
                  $"top={GameManager.Instance.selectedTop}, bottom={GameManager.Instance.selectedBottom}");

        // 결과 씬으로 이동
        GameManager.Instance.GoToResultScene();
    }
}