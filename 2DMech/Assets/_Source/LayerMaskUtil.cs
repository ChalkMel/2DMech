using UnityEngine;

public static class LayerMaskUtil
{
  public static bool ContainsLayer(LayerMask mask, GameObject gameObject) => 
    (mask.value & 1 << gameObject.layer) > 0;
}