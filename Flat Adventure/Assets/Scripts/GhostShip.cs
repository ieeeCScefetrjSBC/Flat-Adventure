using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostShip : MonoBehaviour
{
    public CannonBall ball;
    public UnityEngine.Experimental.Rendering.LWRP.Light2D boatLight;

    private void Start()
    {
        StartCoroutine(GhostShipping());
    }

    IEnumerator GhostShipping()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 8f) / GameManager.gSpeed);

            Vector3 target = Player.P.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Vector3 dir = (target - transform.position).normalized;
            CannonBall obj = Instantiate(ball, transform.position + .5f * dir, Quaternion.identity);

            obj.SetVelocity(dir * 4f);
        }
    }

    private void Update()
    {
        boatLight.intensity = 1f - DayCicle.y;
    }

}
