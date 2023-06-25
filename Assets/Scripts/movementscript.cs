using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class movementscript : MonoBehaviour
{
    private Sequence _sequence;

    private TweenCallback _callback;

    // Start is called before the first frame update
    void Start()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOPunchRotation(new Vector3(0, 120, 0), 2, 10, 1))
            .PrependInterval(Random.Range(1f, 10f)).Append(transform.DOMoveY(.2f, 1)).SetEase(Ease.InBounce)
            .SetLoops(-1, LoopType.Yoyo).SetRelative(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}