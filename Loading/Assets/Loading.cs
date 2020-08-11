using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loading : MonoBehaviour
{
    #region 要控制的元件
    [Header("載入遊戲物件、標題、提示與吧條")]
    public GameObject objLoading;
    public Text textLoading;
    public Text textTip;
    public Image imgLoading;

    [Header("上面跑的小人")]
    public RectTransform rectCaracter;
    [Header("小人的範圍")]
    public Vector2 posRange = new Vector2(-500, 500);

    [Header("要載入的場景名稱")]
    public string nameScene;
    #endregion

    #region 方法
    public void StartLoading()
    {
        StartCoroutine(LoadingEffect());
    }

    /// <summary>
    /// 載入特效處理：進度 0 - 0.9 - 1
    /// </summary>
    private IEnumerator LoadingEffect()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(nameScene);
        ao.allowSceneActivation = false;
        objLoading.SetActive(true);

        while (ao.progress <= 0.9f)
        {
            print(ao.progress);

            textLoading.text = "載入進度：" + (ao.progress / 0.9f * 100).ToString("F2") + "%";
            float x = Mathf.Lerp(posRange.x, posRange.y, ao.progress / 0.9f);
            rectCaracter.anchoredPosition = new Vector2(x, 75);

            yield return null;

            if (ao.progress == 0.9f)
            {
                textTip.text = "請按任意鍵開始遊戲";

                if (Input.anyKeyDown) ao.allowSceneActivation = true;
            }
        }
    }
    #endregion
}
