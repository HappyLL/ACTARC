using UnityEngine;
using System.Collections;
namespace ACTBase
{
    public interface ITick
    {
        void DoTick(int iTickCount);
    }
}
