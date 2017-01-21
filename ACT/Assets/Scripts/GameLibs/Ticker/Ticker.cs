using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ACTBase
{
    public class Ticker
    {
        private List<ITick> m_vITickList;
        private Dictionary<ITick, int> m_mITickHash;

        //当前帧加入的ITick 会放到下一帧更新
        private List<ITick> m_vDelayAddList;
        //当前帧删除的ITick 会先更新逻辑在删除
        private List<ITick> m_vDelayRemoveList;

        //每一个Ticker都有自己的更新周期(和TickerManager不一样)
        private int m_iTickCount;
        private bool m_bRunning;

        //是当前的更新逻辑按照对应的模更新（整除不更新）
        private int m_iDurationTick;
        private int m_iIntervalTick;

        public Ticker()
        {
            m_vITickList = new List<ITick>();
            m_mITickHash = new Dictionary<ITick, int>();

            m_vDelayAddList = new List<ITick>();
            m_vDelayRemoveList = new List<ITick>();

            m_iTickCount = 0;
            m_bRunning = false;
        }

        public virtual void Init()
        {
            m_iTickCount = 0;
            m_bRunning = true;
        }

        public virtual void Clear()
        {
            m_iTickCount = 0;
            m_bRunning = true;

            m_vITickList.Clear();
            m_mITickHash.Clear();
            m_vDelayAddList.Clear();
            m_vDelayRemoveList.Clear();
        }

        public virtual void AddITick(ITick itick)
        {
            if (m_mITickHash.ContainsKey(itick))
            {
                Debug.LogAssertion("the itick repeat");
                return;
            }
            m_vDelayAddList.Add(itick);
        }

        public virtual void RemoveITick(ITick itick)
        {
            if (!m_mITickHash.ContainsKey(itick))
            {
                Debug.LogAssertion("the itick is empty");
                return;
            }
            m_vDelayRemoveList.Add(itick);
        }

        public virtual bool HasITick(ITick itick)
        {
            return m_mITickHash.ContainsKey(itick);
        }

        public void Run()
        {
            m_bRunning = true;
        }

        public void Pause()
        {
            m_bRunning = false;
        }

        public void SlowRun(int iDurationTick, int iIntervalTick)
        {
            m_iDurationTick = iDurationTick;
            m_iIntervalTick = iIntervalTick;
        }

        public int GetTickCount()
        {
            return m_iTickCount;
        }

        public virtual void DoTick()
        {
            if (!m_bRunning)
                return;

            if (m_iDurationTick > 0)
            {
                m_iDurationTick --;
                if (m_iDurationTick % m_iIntervalTick != 0)
                    return;
            }

            foreach (var itick in m_vITickList)
            {
                itick.DoTick(m_iTickCount);
            }

            //优先删除
            DelayRemoveITick();
            DelayAddITick();

            m_iTickCount ++;
        }

        protected virtual void DelayAddITick()
        {
            foreach (var itick in m_vDelayAddList)
            {
                if (!m_mITickHash.ContainsKey(itick))
                {
                    m_mITickHash[itick] = m_vITickList.Count;
                    m_vITickList.Add(itick);
                }
            }

            m_vDelayAddList.Clear();
        }

        protected virtual void DelayRemoveITick()
        {
            foreach (var itick in m_vDelayRemoveList)
            {
                if (!m_mITickHash.ContainsKey(itick))
                    continue;

                int pos = m_mITickHash[itick];
                int last = m_vITickList.Count - 1;

                m_vITickList[pos] = m_vITickList[last];
                m_vITickList.RemoveAt(last);

                m_mITickHash.Remove(itick);
            }

            m_vDelayRemoveList.Clear();
        }

    }
}
