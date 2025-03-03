/* this is generated by nino */
namespace Nino.UnitTests
{
    public partial class C
    {
        public static C.SerializationHelper NinoSerializationHelper = new C.SerializationHelper();
        public class SerializationHelper: Nino.Serialization.NinoWrapperBase<C>
        {
            #region NINO_CODEGEN
            public override void Serialize(C value, Nino.Serialization.Writer writer)
            {
                writer.Write(value.Name);
                if(value.As != null)
                {
                    writer.CompressAndWrite(value.As.Count);
                    foreach (var entry in value.As)
                    {
                        Nino.UnitTests.A.NinoSerializationHelper.Serialize(entry, writer);
                    }
                }
                else
                {
                    writer.CompressAndWrite(0);
                }
            }

            public override Nino.Serialization.Box<C> Deserialize(Nino.Serialization.Reader reader)
            {
                C value = new C();
                value.Name = reader.ReadString();
                value.As = new System.Collections.Generic.List<Nino.UnitTests.A>(reader.ReadLength());
                for(int i = 0, cnt = value.As.Capacity; i < cnt; i++)
                {
                    var value_As_i = Nino.UnitTests.A.NinoSerializationHelper.Deserialize(reader).RetrieveValueAndReturn();
                    value.As.Add(value_As_i);
                }
                var ret = Nino.Shared.IO.ObjectPool<Nino.Serialization.Box<Nino.UnitTests.C>>.Request();
                ret.Value = value;
                return ret;
            }
            #endregion
        }
    }
}