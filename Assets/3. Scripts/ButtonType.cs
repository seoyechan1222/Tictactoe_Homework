using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  public ButtonTypes currentType;
  public Transform buttonScale;
  Vector3 defaultScale;

  private void Start()
  {
    defaultScale = buttonScale.localScale;
  }
  public void OnButtonTypesClick()
  {
    switch (currentType)
    {
      case ButtonTypes.New:
        Debug.Log("새 게임 시작.");
        SceneManager.LoadScene("GameScene");
        break;
      case ButtonTypes.Back:
        Debug.Log("뒤로가기 실행.");
        SceneManager.LoadScene("GameMenu");
        break;
      case ButtonTypes.Quit:
        Debug.Log("종료 실행.");
        ExitGame();
        break;
      case ButtonTypes.Option:
        Debug.Log("옵션 실행.");
        SceneManager.LoadScene("GameOption");
        break;
      
    }
  }
  
  public void ExitGame()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    buttonScale.localScale = defaultScale * 1.2f;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    buttonScale.localScale = defaultScale;
  }
}
