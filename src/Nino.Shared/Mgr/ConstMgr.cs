using System;
using System.Collections.Generic;

namespace Nino.Shared.Mgr
{
    public static class ConstMgr
    {
#if UNITY_2017_1_OR_NEWER
        /// <summary>
        /// Asset path
        /// </summary>
        public static string AssetPath => UnityEngine.Application.dataPath;
#else
        /// <summary>
        /// Asset path
        /// </summary>
        public static string AssetPath => System.IO.Directory.GetCurrentDirectory();
#endif
        /// <summary>
        /// Whether is mono or not
        /// </summary>
        public static readonly bool IsMono = Type.GetType("Mono.Runtime") != null;

        /// <summary>
        /// Null value
        /// </summary>
        public static readonly byte[] Null = Array.Empty<byte>();

        /// <summary>
        /// Empty param
        /// </summary>
        public static readonly object[] EmptyParam = Array.Empty<object>();

        #region basic types

        public static readonly Type ObjectType = typeof(object);
        public static readonly Type ByteType = typeof(byte);
        public static readonly Type SByteType = typeof(sbyte);
        public static readonly Type ShortType = typeof(short);
        public static readonly Type UShortType = typeof(ushort);
        public static readonly Type IntType = typeof(int);
        public static readonly Type UIntType = typeof(uint);
        public static readonly Type LongType = typeof(long);
        public static readonly Type ULongType = typeof(ulong);
        public static readonly Type StringType = typeof(string);
        public static readonly Type BoolType = typeof(bool);
        public static readonly Type DecimalType = typeof(decimal);
        public static readonly Type DoubleType = typeof(double);
        public static readonly Type FloatType = typeof(float);
        public static readonly Type CharType = typeof(char);
        public static readonly Type DateTimeType = typeof(DateTime);
        public static readonly Type ByteArrType = typeof(byte[]);
        public static readonly Type ByteListType = typeof(List<byte>);
        public static readonly Type ListDefType = typeof(List<>);
        public static readonly Type DictDefType = typeof(Dictionary<,>);
        public static readonly Type NullableDefType = typeof(Nullable<>);

        #endregion

        public static readonly byte SizeOfUInt = sizeof(uint);
        public static readonly byte SizeOfInt = sizeof(int);
        public static readonly byte SizeOfUShort = sizeof(ushort);
        public static readonly byte SizeOfShort = sizeof(short);
        public static readonly byte SizeOfULong = sizeof(ulong);
        public static readonly byte SizeOfLong = sizeof(long);
        public static readonly byte SizeOfDecimal = sizeof(decimal);
    }
}