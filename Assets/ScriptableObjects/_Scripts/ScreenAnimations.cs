using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationList", menuName = "MSO/Animations/NewAnimationList")]
public class ScreenAnimations : ScriptableObject
{
    public GameObject[] Animations;
    public GameObject[] Effects;
}
