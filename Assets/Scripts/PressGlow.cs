using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap.Unity;
using Leap.Unity.Interaction;

[RequireComponent(typeof(InteractionBehaviour))]
public class PressGlow : MonoBehaviour
{
    Color defaultColor;
  public Color pressedColor = Color.white;

  private Material[] _materials;
  private InteractionBehaviour _intObj;

  void Start() {
    _intObj = GetComponent<InteractionBehaviour>();

    Renderer[] renderers;
    renderers = GetComponentsInChildren<Renderer>();

    _materials = new Material[renderers.Length];
    for (int i = 0; i < renderers.Length; ++ i) {
        _materials[i] = renderers[i].material;
        defaultColor = _materials[i].color;
    }
  }

  void Update() {
    // The target color for the Interaction object will be determined by various simple state checks.
    Color targetColor = defaultColor;

    // We can also check the depressed-or-not-depressed state of InteractionButton objects
    // and assign them a unique color in that case.
    if (_intObj is InteractionButton && (_intObj as InteractionButton).isPressed) {
        targetColor = pressedColor;
    }

    // Lerp actual material color to the target color.
    foreach (var _material in _materials) {
        _material.color = Color.Lerp(_material.color, targetColor, 30F * Time.deltaTime);
    }
  }

}
