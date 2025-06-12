using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [Header("다인이 표정")]
    public Image faceImage;          // Canvas ▶ DainCharacter ▶ ResultFace(Image)
    public Sprite happy;             // happy.png (30점)
    public Sprite soso;              // soso.png  (20점)
    public Sprite sad;               // sad.png   (10점)

    [Header("점수 이미지")]
    public Image scoreImage;         // Canvas ▶ ResultScore(Image)
    public Sprite score10;           // 10.png
    public Sprite score20;           // 20.png
    public Sprite score30;           // 30.png

    [Header("입힌 옷 복원")]
    public Image equippedTopImage;    // Canvas ▶ DainCharacter ▶ EquippedTop(Image)
    public Image equippedBottomImage; // Canvas ▶ DainCharacter ▶ EquippedBottom(Image)
    public List<Sprite> topSprites;   // Inspector에 3개 할당: top1, top2, top3
    public List<Sprite> bottomSprites;// Inspector에 3개 할당: bottom1, bottom2, bottom3

    // ────────────────────────────────────────────────────────────────────────────
    // 하트 연출용 필드
    [Header("하트 연출")]
    public Image heartsImage;         // 빈 상태부터 풀까지 바꿀 UI Image
    public Sprite heartZero;          // 0개
    public Sprite heartOne;           // 1개
    public Sprite heartTwo;           // 2개
    public Sprite heartFull;          // 3개
    [Tooltip("첫 하트 뜨기 전 잠깐 대기(초)")]
    public float firstHeartDelay = 0.2f;
    [Tooltip("두 번째 이후 하트 간격(초)")]
    public float nextHeartDelay = 1f;
    // ────────────────────────────────────────────────────────────────────────────

    // ────────────────────────────────────────────────────────────────────────────
    // 2) 태열이 이미지 필드
    [Header("태열이 연출")]
    public Image taeyeolImage;        // Canvas ▶ … ▶ TaeyeolImage(Image)
    public Sprite taeyeol1;           // 10점일 때
    public Sprite taeyeol2;           // 20점일 때
    public Sprite taeyeol3;           // 30점일 때
    // ────────────────────────────────────────────────────────────────────────────

    void Start()
    {
        // ──────────── 기존 로직 (건드리지 마세요!!!) ────────────
        int score = GameManager.Instance.score;
        int topIndex = GameManager.Instance.selectedTop - 1;
        int bottomIndex = GameManager.Instance.selectedBottom - 1;

        // 표정 & 점수
        if (score == 30) { faceImage.sprite = happy; scoreImage.sprite = score30; }
        else if (score == 20) { faceImage.sprite = soso; scoreImage.sprite = score20; }
        else { faceImage.sprite = sad; scoreImage.sprite = score10; }
        faceImage.color = Color.white;
        scoreImage.color = Color.white;

        // 옷 복원
        if (topIndex >= 0 && topIndex < topSprites.Count)
        {
            equippedTopImage.sprite = topSprites[topIndex];
            equippedTopImage.color = Color.white;
        }
        if (bottomIndex >= 0 && bottomIndex < bottomSprites.Count)
        {
            equippedBottomImage.sprite = bottomSprites[bottomIndex];
            equippedBottomImage.color = Color.white;
        }
        // ────────────────────────────────────────────────────────────

        // ─────────────── 태열이 이미지 설정 ───────────────
        // Score 에 따라 미리 만들어둔 taeyeol1~3 중 하나로 교체
        if (score >= 30) taeyeolImage.sprite = taeyeol3;
        else if (score >= 20) taeyeolImage.sprite = taeyeol2;
        else taeyeolImage.sprite = taeyeol1;
        taeyeolImage.color = Color.white;
        // ────────────────────────────────────────────────────────

        // ─────────────── 하트 연출 시작 ───────────────
        heartsImage.sprite = heartZero;                   // 빈 상태
        StartCoroutine(ShowHeartsSequentially(score));    // 순차 연출
        // ────────────────────────────────────────────────────

        AudioManager.Instance.PlaySuccess();  // 성공 효과음(30/20/10점 공통)
        StartCoroutine(ShowHeartsSequentially(score));
    }

    // 화면 아무 곳이나 클릭하면 스케줄 씬으로 돌아가기
    public void OnClickAnywhere()
    {
        SceneManager.LoadScene("ScheduleScene");
    }

    // ──────────── 하트 순차 연출 코루틴 ────────────
    private IEnumerator ShowHeartsSequentially(int score)
    {
        int count = Mathf.Clamp(score / 10, 0, 3);
        for (int i = 1; i <= count; i++)
        {
            // 첫 하트 전과 이후 간격 제어
            yield return new WaitForSeconds(i == 1 ? firstHeartDelay : nextHeartDelay);
            AudioManager.Instance.PlayHeart();  // 하트 채우는 소리

            switch (i)
            {
                case 1: heartsImage.sprite = heartOne; break;
                case 2: heartsImage.sprite = heartTwo; break;
                case 3: heartsImage.sprite = heartFull; break;
            }
        }
    }
    // ────────────────────────────────────────────────────
}
