// 
// HOTweenManager.cs
//  
// Author: Daniele Giardini
// 
// Copyright (c) 2012 Daniele Giardini - Holoville - http://www.holoville.com
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Collections.Generic;
using Holoville.HOTween;
using UnityEngine;

/// <summary>
/// A component that, when attached to a GameObject,
/// allows the setting of tweens on its Components/MonoBehaviours.
/// Relies on <see cref="HOTweenManager"/> and other stuff.
/// </summary>
[AddComponentMenu("HOTween/HOTweenComponent")]
public class HOTweenComponent : ABSHOTweenEditorElement
{
    /// <summary>
    /// Destroys this Component after the tween has been setup.
    /// </summary>
    public bool destroyAfterSetup = false;

    [System.NonSerialized]
    public List<Holoville.HOTween.Tweener> generatedTweeners;

    // ===================================================================================
    // UNITY METHODS ---------------------------------------------------------------------

    void Awake()
    {
        HOTween.Init(true, true, true);
        
        // Store Tweeners generated by this component
        generatedTweeners = new List<Holoville.HOTween.Tweener>();
        foreach (HOTweenManager.HOTweenData twData in tweenDatas) {
            generatedTweeners.Add(HOTweenManager.CreateTween(twData, globalDelay, globalTimeScale));
        }

        if (destroyAfterSetup) DoDestroy();
    }

    void OnDestroy()
    {
        DoDestroy();
    }

    // ===================================================================================
    // METHODS ---------------------------------------------------------------------------

    override protected void DoDestroy()
    {
        if (destroyed) return;

        base.DoDestroy();
        generatedTweeners = null;
        Destroy(this);
    }
}