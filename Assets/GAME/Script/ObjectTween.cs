using UnityEngine;
using DG.Tweening;

public class ObjectTween : MonoBehaviour
{
    [SerializeField] private GameObject dashBox;


    private void Update()
    {
        dashBox.transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
