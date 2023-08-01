using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float _magnitude)
    {
        Vector3 orginalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed< duration)
        {
            float x = Random.Range(-.5f, .5f) * _magnitude;
            float y = Random.Range(-.5f, .5f) * _magnitude;

            transform.localPosition = new Vector3(x, y, orginalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = orginalPos;
    }
}
