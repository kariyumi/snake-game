using System.Collections;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text pressAnyKeyToRestartText;

    public void ActivateGameOver()
    {
        StartCoroutine(GameOverFlickerRoutine());
    }

    public void DeactivateGameOver()
    {
        StopAllCoroutines();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.gameObject.SetActive(true);
            pressAnyKeyToRestartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameOverText.gameObject.SetActive(false);
            pressAnyKeyToRestartText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
