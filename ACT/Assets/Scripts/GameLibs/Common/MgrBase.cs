using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace ACTBase
{
    class MgrBaseUniqueNoMake
    {
        private static int m_iUniqueNoIncrease = 1;

        public static int GetOneUniqueNo()
        {
            return m_iUniqueNoIncrease++;
        }
    }

    public class MgrBase<T, R> : ITick where T : IObjectUnit<R>, new()
    {
        protected Dictionary<int, T> m_dUnitList;

        protected List<T> m_unitPool;
        protected List<T> m_delayDestroyList;
        protected List<T> m_delayAddList;

        public MgrBase()
        {
            m_dUnitList = new Dictionary<int, T>();

            m_unitPool = new List<T>();
            m_delayAddList = new List<T>();
            m_delayDestroyList = new List<T>();
        }

        //uniId?
        public virtual T CreateUnit(int uniId)
        {
            T unit;
            int count = m_unitPool.Count - 1;
            if (count >= 0)
            {
                unit = m_unitPool[count];
                m_unitPool.RemoveAt(count);
            }
            else
            {
                unit = new T();
            }

            unit.UniqueNo = MgrBaseUniqueNoMake.GetOneUniqueNo();
            m_delayAddList.Add(unit);

            return unit;
        }

        public virtual bool HasUnit(int uniqueNo)
        {
            foreach (var item in m_delayAddList)
            {
                if (item.UniqueNo == uniqueNo)
                    return true;
            }

            if (m_dUnitList.ContainsKey(uniqueNo))
                return true;

            return false;
        }

        public virtual T GetUnit(int uniqueNo)
        {
            foreach (var item in m_delayAddList)
            {
                if (item.UniqueNo == uniqueNo)
                    return item;
            }

            if (m_dUnitList.ContainsKey(uniqueNo))
                return m_dUnitList[uniqueNo];

            return default(T);
        }

        public virtual bool HasUnitInDestory(int uniqueNo)
        {
            foreach (var item in m_delayDestroyList)
            {
                if (item.UniqueNo == uniqueNo)
                    return true;
            }

            return false;
        }

        public virtual void DestroyUnit(T unit)
        {
            if (m_dUnitList.ContainsKey(unit.UniqueNo) && !HasUnitInDestory(unit.UniqueNo))
            {
                m_delayDestroyList.Add(unit);
            }
        }

        public virtual void Init()
        {

        }

        public virtual void Clear()
        {
            m_delayDestroyList.Clear();

            foreach (var item in m_delayAddList)
            {
                item.Destroy();
                m_unitPool.Add(item);
            }

            m_delayAddList.Clear();

            foreach (var item in m_dUnitList.Values)
            {
                item.Destroy();
                m_unitPool.Add(item);
            }

            m_dUnitList.Clear();
        }

        public virtual void ClearPool()
        {
            Clear();

            m_unitPool.Clear();
        }

        public void DoTick(int iTickCount)
        {
            if (m_delayDestroyList.Count > 0)
            {
                foreach (var item in m_delayDestroyList)
                {
                    item.Destroy();
                    m_dUnitList.Remove(item.UniqueNo);
                    m_unitPool.Add(item);
                }
                m_delayDestroyList.Clear();
            }

            if (m_delayAddList.Count > 0)
            {
                foreach (var item in m_delayAddList)
                {
                    m_dUnitList.Add(item.UniqueNo , item);
                }
                m_delayAddList.Clear();
            }

        }
    }
}
