﻿using System.Text;
using Nino.Serialization;

namespace Nino.Benchmark.Serializers
{
	public class NinoSerializer : SerializerBase
    {
        public override T Deserialize<T>(object input)
        {
            return Deserializer.Deserialize<T>((byte[])input, Encoding.UTF8);
        }

        public override object Serialize<T>(T input)
        {
            return Serializer.Serialize(input);
        }

        public override string ToString()
        {
            return "Nino";
        }
    }
}

