using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    // Start is called before the first frame update

    public Tentacle tentacle;

    Tentacle[] tentacles;

    private void Awake()
    {
        tentacles = new Tentacle[Random.Range(10, (int)(GameManager.dificulty * 5))];

        for (int i = 0; i < tentacles.Length; i++)
        {
            tentacles[i] = Instantiate(tentacle);
        }

        StartCoroutine(Octopusing());
    }

    IEnumerator Octopusing()
    {
        //while ()
        {
            PositionTentacles();

            float time = 3f / GameManager.gSpeed;
            while (time > 0)
            {
                Color color = new Color(1, 1, 1, 1 - time);
                for (int i = 0; i < tentacles.Length; i++) tentacles[i].srendender.color = color;
                time -= Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < tentacles.Length; i++) tentacles[i].srendender.color = Color.white;

            yield return new WaitForSeconds(3f / GameManager.gSpeed);

            for (int i = 0; i < tentacles.Length; i++) tentacles[i].Rise();

        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < tentacles.Length; i++)
        {
            if (tentacles[i] == null) continue;
            tentacles[i].animator.SetTrigger("Hide");
            tentacles[i].WTF(1.5f);
        }
    }

    void PositionTentacles()
    {
        for (int i = 0; i < tentacles.Length; i++)
        {
            int x = Random.Range(-8, 8);
            int y = Random.Range(-4, 2);

            tentacles[i].transform.position = new Vector3(x, y);
        }
    }

}
