﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    public AnimationClip[] animations;

    public AnimationClip GetAnimation(string name) {

        AnimationClip r = null;

        foreach(AnimationClip animation in animations) {
            if(name == animation.name)
                r = animation;
        }

        return r;

    }

    public AnimationClip GetRandomAnimation() {

        int index = Random.Range(0, animations.Length);
        return animations[index];

    }
}
