/* this is generated by nino */
namespace Nino.Test
{
    public partial class NestedData2
    {
        public static NestedData2.SerializationHelper NinoSerializationHelper = new NestedData2.SerializationHelper();
        public class SerializationHelper: Nino.Serialization.NinoWrapperBase<NestedData2>
        {
            #region NINO_CODEGEN
            public override void Serialize(NestedData2 value, Nino.Serialization.Writer writer)
            {
                writer.Write(value.name);
                if(value.ps != null)
                {
                    writer.CompressAndWrite(value.ps.Length);
                    foreach (var entry in value.ps)
                    {
                        Nino.Test.Data.NinoSerializationHelper.Serialize(entry, writer);
                    }
                }
                else
                {
                    writer.CompressAndWrite(0);
                }
                if(value.vs != null)
                {
                    writer.CompressAndWrite(value.vs.Count);
                    foreach (var entry in value.vs)
                    {
                        writer.CompressAndWrite(entry);
                    }
                }
                else
                {
                    writer.CompressAndWrite(0);
                }
            }

            public override Nino.Serialization.Box<NestedData2> Deserialize(Nino.Serialization.Reader reader)
            {
                NestedData2 value = new NestedData2();
                value.name = reader.ReadString();
                value.ps = new Nino.Test.Data[reader.ReadLength()];
                for(int i = 0, cnt = value.ps.Length; i < cnt; i++)
                {
                    var value_ps_i = Nino.Test.Data.NinoSerializationHelper.Deserialize(reader).RetrieveValueAndReturn();
                    value.ps[i] = value_ps_i;
                }
                value.vs = new System.Collections.Generic.List<System.Int32>(reader.ReadLength());
                for(int i = 0, cnt = value.vs.Capacity; i < cnt; i++)
                {
                    var value_vs_i =  (System.Int32)reader.DecompressAndReadNumber();
                    value.vs.Add(value_vs_i);
                }
                var ret = Nino.Shared.IO.ObjectPool<Nino.Serialization.Box<Nino.Test.NestedData2>>.Request();
                ret.Value = value;
                return ret;
            }
            #endregion
        }
    }
}