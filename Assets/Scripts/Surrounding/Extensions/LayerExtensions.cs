﻿using UnityEngine;

namespace Surrounding.Extensions
{
    public static class LayerExtensions
    {
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}
