using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WearItem : MonoBehaviour
{
    public enum ItemType { Top, Bottom }
    public ItemType itemType;

    [Tooltip("top1=1, top2=2, top3=3 / bottom1=1, bottom2=2, bottom3=3")]
    public int itemIndex;

    [Header("이 버튼이 보여줄 스프라이트")]
    public Sprite itemSprite;      // Inspector에 할당할 스프라이트

    [Header("다인 캐릭터에 입힐 Image 컴포넌트")]
    public Image targetImage;      // EquippedTop 또는 EquippedBottom의 Image

    // 버튼 클릭 시 호출할 함수
    public void OnClickWear()
    {
        AudioManager.Instance.PlayPick();   // 클릭음
        // 필수 할당이 안 되어 있으면 아무것도 안 함
        if (targetImage == null || itemSprite == null) return;

        // 화면에 옷 표시
        targetImage.sprite = itemSprite;
        targetImage.color = Color.white;

        // GameManager에 선택값 저장
        if (itemType == ItemType.Top)
        {
            GameManager.Instance.selectedTop = itemIndex;
            Debug.Log($"[WearItem] Top selected = {itemIndex}");
        }
        else
        {
            GameManager.Instance.selectedBottom = itemIndex;
            Debug.Log($"[WearItem] Bottom selected = {itemIndex}");
        }

        // 현재 스케줄과 선택된 상의/하의 상태 모두 확인
        Debug.Log($"[WearItem] schedule = {GameManager.Instance.schedule}, " +
                  $"top = {GameManager.Instance.selectedTop}, " +
                  $"bottom = {GameManager.Instance.selectedBottom}");
    }
}
