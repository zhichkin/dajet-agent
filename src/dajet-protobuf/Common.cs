using ProtoBuf;
using System;
using System.Text.Json.Serialization;

namespace DaJet.ProtoBuf
{
    [ProtoContract] public enum RecordType
    {
        Receipt = 0,
        Expense = 1
    }
    [ProtoContract] public sealed class Entity
    {
        [ProtoMember(1, Name = "type")] [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
        [ProtoMember(2, Name = "uuid")] [JsonPropertyName("value")] public string Uuid { get; set; } = string.Empty;
    }
    [ProtoContract] public sealed class ObjectDeletion
    {
        [ProtoMember(1, Name = "entity")] public Entity Entity { get; set; } = null!;
    }

    // *************************************
    // * https://github.com/protobuf-net/  *
    // * https://protogen.marcgravell.com/ *
    // *************************************
    [ProtoContract()] public sealed class Union // : IExtensible
    {
        //private IExtension __pbn__extensionData;
        //IExtension IExtensible.GetExtensionObject(bool createIfMissing) => Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [ProtoMember(1, Name = @"undefined")]
        public Google.Protobuf.WellKnownTypes.NullValue Undefined
        {
            get => __pbn__value.Is(1) ? ((Google.Protobuf.WellKnownTypes.NullValue)__pbn__value.Int32) : default;
            set => __pbn__value = new DiscriminatedUnion64Object(1, (int)value);
        }
        public bool ShouldSerializeUndefined() => __pbn__value.Is(1);
        public void ResetUndefined() => DiscriminatedUnion64Object.Reset(ref __pbn__value, 1);

        private DiscriminatedUnion64Object __pbn__value;

        [ProtoMember(2, Name = @"boolean")]
        public bool Boolean
        {
            get => __pbn__value.Is(2) ? __pbn__value.Boolean : default;
            set => __pbn__value = new DiscriminatedUnion64Object(2, value);
        }
        public bool ShouldSerializeBoolean() => __pbn__value.Is(2);
        public void ResetBoolean() => DiscriminatedUnion64Object.Reset(ref __pbn__value, 2);

        [ProtoMember(3, Name = @"numeric")]
        public double Numeric
        {
            get => __pbn__value.Is(3) ? __pbn__value.Double : default;
            set => __pbn__value = new DiscriminatedUnion64Object(3, value);
        }
        public bool ShouldSerializeNumeric() => __pbn__value.Is(3);
        public void ResetNumeric() => DiscriminatedUnion64Object.Reset(ref __pbn__value, 3);

        [ProtoMember(4, Name = @"string")]
        [System.ComponentModel.DefaultValue("")]
        public string String
        {
            get => __pbn__value.Is(4) ? ((string)__pbn__value.Object) : "";
            set => __pbn__value = new DiscriminatedUnion64Object(4, value);
        }
        public bool ShouldSerializeString() => __pbn__value.Is(4);
        public void ResetString() => DiscriminatedUnion64Object.Reset(ref __pbn__value, 4);

        [ProtoMember(5, Name = @"entity")]
        public Entity Entity
        {
            get => __pbn__value.Is(5) ? ((Entity)__pbn__value.Object) : default;
            set => __pbn__value = new DiscriminatedUnion64Object(5, value);
        }
        public bool ShouldSerializeEntity() => __pbn__value.Is(5);
        public void ResetEntity() => DiscriminatedUnion64Object.Reset(ref __pbn__value, 5);

        [ProtoMember(6, Name = @"datetime", DataFormat = DataFormat.WellKnown)]
        public DateTime? Datetime
        {
            get => __pbn__value.Is(6) ? ((DateTime?)__pbn__value.DateTime) : default;
            set => __pbn__value = new DiscriminatedUnion64Object(6, value);
        }
        public bool ShouldSerializeDatetime() => __pbn__value.Is(6);
        public void ResetDatetime() => DiscriminatedUnion64Object.Reset(ref __pbn__value, 6);

        public ValueOneofCase ValueCase => (ValueOneofCase)__pbn__value.Discriminator;

        public enum ValueOneofCase
        {
            None = 0,
            Undefined = 1,
            Boolean = 2,
            Numeric = 3,
            String = 4,
            Entity = 5,
            Datetime = 6,
        }
    }
}