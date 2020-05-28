using StronglyTypedId.Tests.Types;
using Xunit;
using NewtonsoftJsonSerializer = Newtonsoft.Json.JsonConvert;
using SystemTextJsonSerializer = System.Text.Json.JsonSerializer;

namespace StronglyTypedId.Tests
{
    public class DecimalIdTests
    {
        [Fact]
        public void SameValuesAreEqual()
        {
            var id = 123m;
            var foo1 = new DecimalId(id);
            var foo2 = new DecimalId(id);

            Assert.Equal(foo1, foo2);
        }

        [Fact]
        public void EmptyValueIsEmpty()
        {
            Assert.Equal(0, DecimalId.Empty.Value);
        }


        [Fact]
        public void DifferentValuesAreUnequal()
        {
            var foo1 = new DecimalId(1m);
            var foo2 = new DecimalId(2m);

            Assert.NotEqual(foo1, foo2);
        }

        [Fact]
        public void OverloadsWorkCorrectly()
        {
            var id = 12m;
            var same1 = new DecimalId(id);
            var same2 = new DecimalId(id);
            var different = new DecimalId(3m);

            Assert.True(same1 == same2);
            Assert.False(same1 == different);
            Assert.False(same1 != same2);
            Assert.True(same1 != different);
        }

        [Fact]
        public void DifferentTypesAreUnequal()
        {
            var bar = GuidId2.New();
            var foo = new DecimalId(23m);

            //Assert.NotEqual(bar, foo); // does not compile
            Assert.NotEqual((object)bar, (object)foo);
        }

        [Fact]
        public void CanSerializeToDecimal_WithNewtonsoftJsonProvider()
        {
            var foo = new NewtonsoftJsonDecimalId(123m);

            var serializedFoo = NewtonsoftJsonSerializer.SerializeObject(foo);
            var serializedDecimal = NewtonsoftJsonSerializer.SerializeObject(foo.Value);

            Assert.Equal(serializedFoo, serializedDecimal);
        }

        [Fact]
        public void CanSerializeToDecimal_WithSystemTextJsonProvider()
        {
            var foo = new SystemTextJsonDecimalId(123m);

            var serializedFoo = SystemTextJsonSerializer.Serialize(foo);
            var serializedDecimal = SystemTextJsonSerializer.Serialize(foo.Value);

            Assert.Equal(serializedFoo, serializedDecimal);
        }

        [Fact]
        public void CanDeserializeFromDecimal_WithNewtonsoftJsonProvider()
        {
            var value = 123m;
            var foo = new NewtonsoftJsonDecimalId(value);
            var serializedDecimal = NewtonsoftJsonSerializer.SerializeObject(value);

            var deserializedFoo = NewtonsoftJsonSerializer.DeserializeObject<NewtonsoftJsonDecimalId>(serializedDecimal);

            Assert.Equal(foo, deserializedFoo);
        }

        [Fact]
        public void CanDeserializeFromDecimal_WithSystemTextJsonProvider()
        {
            var value = 123m;
            var foo = new SystemTextJsonDecimalId(value);
            var serializedDecimal = SystemTextJsonSerializer.Serialize(value);

            var deserializedFoo = SystemTextJsonSerializer.Deserialize<SystemTextJsonDecimalId>(serializedDecimal);

            Assert.Equal(foo, deserializedFoo);
        }

        [Fact]
        public void CanSerializeToDecimal_WithBothJsonConverters()
        {
            var foo = new BothJsonDecimalId(123m);

            var serializedFoo1 = NewtonsoftJsonSerializer.SerializeObject(foo);
            var serializedDecimal1 = NewtonsoftJsonSerializer.SerializeObject(foo.Value);

            var serializedFoo2 = SystemTextJsonSerializer.Serialize(foo);
            var serializedDecimal2 = SystemTextJsonSerializer.Serialize(foo.Value);

            Assert.Equal(serializedFoo1, serializedDecimal1);
            Assert.Equal(serializedFoo2, serializedDecimal2);
        }

        [Fact]
        public void WhenNoJsonConverter_SerializesWithValueProperty()
        {
            var foo = new NoJsonDecimalId(123.4m);

            var serialized1 = NewtonsoftJsonSerializer.SerializeObject(foo);
            var serialized2 = SystemTextJsonSerializer.Serialize(foo);

            var expected = "{\"Value\":" + foo.Value + "}";

            Assert.Equal(expected, serialized1);
            Assert.Equal(expected, serialized2);
        }
    }
}