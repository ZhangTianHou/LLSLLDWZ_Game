using System;
using UnityEngine;

namespace ZTH.UTool
{
    public static class NotNull
    {
        /// <summary>
        /// 获取非空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="get"></param>
        /// <returns></returns>
        public static T Get<T>(ref T value, Func<T> get)
        {
            return value != null ? value : value = get();
        }
    }

    public static class TransformHelp
    {
        public static T Get<T>(this Transform t, ref T value, params string[] nodes) where T : Component
        {
            return NotNull.Get(ref value, () => t.FindComponent<T>(nodes));
        }

        public static T FindComponent<T>(this Transform t, params string[] nodes) where T : Component
        {
            var node = t.FindNode(nodes);
            if (node.TryGetComponent<T>(out var component)) return component;
            $"The node '{node}' has't component '{typeof(T)}'".ThrowException();
            return default;
        }

        public static Transform FindNode(this Transform t, params string[] nodes)
        {
            var curt = t;
            foreach (var node in nodes)
            {
                curt = curt.Find(node);
                if (curt == null) $"The node '{node}' is not found".ThrowException();
            }
            return curt;
        }
    }

    public static class ExceptionHelp
    {
        /// <summary>
        /// 自定义报错
        /// </summary>
        /// <param name="reason"></param>
        public static void ThrowException(this string reason)
        {
            throw new Exception("ET stop running because " + reason);
        }
    }
}