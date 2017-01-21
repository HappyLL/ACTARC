using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ACTBase
{
    public class TickerMgr
    {
        private static TickerMgr m_pInst = null;
        public static TickerMgr Get()
        {
            if (m_pInst == null)
                m_pInst = new TickerMgr();
            return m_pInst;
        }

        private List<Ticker> m_vTickerList; //ticker管理器
        private List<ITick> m_vITikcList; //
        private int m_iTickCount;
        private bool m_bRunning;

        private TickerMgr()
        {
            m_vTickerList = new List<Ticker>();
            m_vITikcList = new List<ITick>();

            Clear();
        }

        public void AddTicker(Ticker ticker)
        {
            ticker.Clear();
            m_vTickerList.Add(ticker);
        }

        public void AddITick(ITick itick)
        {
            m_vITikcList.Add(itick);
        }

        public int GetCountTick()
        {
            return m_iTickCount;
        }

        public void Start()
        {
            m_bRunning = true;
            m_iTickCount = 0;
        }

        public void Run()
        {
            m_bRunning = true;
        }

        public void Stop()
        {
            m_bRunning = false;
        }

        public void Clear()
        {
            m_iTickCount = 0;
            m_bRunning = false;
        }

        public void DoTick()
        {
            if (!m_bRunning)
                return;

            foreach (var ticker in m_vTickerList)
                ticker.DoTick();

            foreach (var itick in m_vITikcList)
                itick.DoTick(m_iTickCount);
        }

    }
}
