using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public bool hit = false;

    public IEnumerator Shake(float duration, float magnitude)
    {

        if (hit)
        {
            yield return null;
        }
        else
        {

            Vector3 originalPos = transform.position;
            float elapsed = 0.0f;
            hit = true;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.position = originalPos;
            hit = false;
        }
    }


}
