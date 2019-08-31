using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{

    public Image fill1;
    public Image fill2;


    public void SetFill(int val, int max)
    {
        fill1.fillAmount = (float)val / max;
        StopAllCoroutines();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);

        float time = 1f;
        while(time > 0)
        {
            fill2.fillAmount = Mathf.Lerp(fill2.fillAmount, fill1.fillAmount, 5f * Time.deltaTime);
            time -= Time.deltaTime;
            yield return null;
        }
    }

}
