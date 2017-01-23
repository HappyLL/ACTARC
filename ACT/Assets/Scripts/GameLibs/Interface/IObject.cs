using System;
using System.Collections;

namespace ACTBase
{
    public interface IObject<R>
    {
        void Init(R data);
        void Destroy();
        void DoTick();
    }

    public interface IObjectUnit<R> : IObject<R>
    {
        int UniqueNo { get; set; }
    }

}