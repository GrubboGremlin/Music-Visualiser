using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncScale : AudioSyncer
{

    /// <summary>
    /// The Maxium scale of the object this Audio scaler is applied too. 
    /// </summary>
    [SerializeField] public Vector3 BeatScale;

    /// <summary>
    /// the resting scale this object will return to when no beat is hit. 
    /// </summary>
    [SerializeField] public Vector3 RestScale;


    private IEnumerator MoveToScale(Vector3 target) 
    {
        Vector3 curr = transform.localScale;
        Vector3 initial = curr;
        float timer = 0;


        while (curr != target) 
        {
            curr = Vector3.Lerp(initial, target, timer / TimeToBeat);
            timer += Time.deltaTime;

            transform.localScale = curr;

            yield return null;
        }


        _IsBeat = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", BeatScale);

    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_IsBeat) return;

        transform.localScale = Vector3.Lerp(transform.localScale, RestScale, RestTime * Time.deltaTime);

    }
}
