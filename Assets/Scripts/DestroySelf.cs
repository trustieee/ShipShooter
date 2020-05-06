using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public void AnimationEndDestroy() => Destroy(gameObject);
}