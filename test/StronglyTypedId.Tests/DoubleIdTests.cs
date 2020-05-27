using StronglyTypedId.Tests.Types;
using Xunit;
using NewtonsoftJsonSerializer = Newtonsoft.Json.JsonConvert;
using SystemTextJsonSerializer = System.Text.Json.JsonSerializer;

namespace StronglyTypedId.Tests
{
    public class DoubleIdTests
    {
        [Fact]
        public void SameValuesAreEqual()
        {
            var id = 123d;
            var foo1 = new DoubleId(id);
            var foo2 = new DoubleId(id);

            Assert.Equal(foo1, foo2);
        }

        [Fact]
        public void EmptyValueIsEmpty()
        {
            Assert.Equal(0d, DoubleId.Empty.Value);
        }


        [Fact]
        public void DifferentValuesAreUnequal()
        {
            var foo1 = new DoubleId(1d);
            var foo2 = new DoubleId(2d);

            Assert.NotEqual(foo1, foo2);
        }

        [Fact]
        public void OverloadsWorkCorrectly()
        {
            var id = 12d;
            var same1 = new DoubleId(id);
            var same2 = new DoubleId(id);
            var different = new DoubleId(3);

            Assert.True(same1 == same2);
            Assert.False(same1 == different);
            Assert.False(same1 != same2);
            Assert.True(same1 != different);
        }

        [Fact]
        public void DifferentTypesAreUnequal()
        {
            var bar = GuidId2.New();
            var foo = new DoubleId(23d);

            //Assert.NotEqual(bar, foo); // does not compile
            Assert.NotEqual((object)bar, (object)foo);
        }

        [Fact]
        public void CanSerializeToDouble_WithNewtonsoftJsonProvider()
        {
            var foo = new NewtonsoftJsonDoubleId(123d);

            var serializedFoo = NewtonsoftJsonSerializer.SerializeObject(foo);
            var serializedDouble = NewtonsoftJsonSerializer.SerializeObject(foo.Value);

            Assert.Equal(serializedFoo, serializedDouble);
        }

        [Fact]
        public void CanSerializeToDouble_WithSystemTextJsonProvider()
        {
            var foo = new SystemTextJsonDoubleId(123d);

            var serializedFoo = SystemTextJsonSerializer.Serialize(foo);
            var serializedDouble = SystemTextJsonSerializer.Serialize(foo.Value);

            Assert.Equal(serializedFoo, serializedDouble);
        }

        [Fact]
        public void CanDeserializeFromDouble_WithNewtonsoftJsonProvider()
        {
            var value = 123d;
            var foo = new NewtonsoftJsonDoubleId(value);
            var serializedDouble = NewtonsoftJsonSerializer.SerializeObject(value);

            var deserializedFoo = NewtonsoftJsonSerializer.DeserializeObject<NewtonsoftJsonDoubleId>(serializedDouble);

            Assert.Equal(foo, deserializedFoo);
        }

        [Fact]
        public void CanDeserializeFromDouble_WithSystemTextJsonProvider()
        {
            var value = 123d;
            var foo = new SystemTextJsonDoubleId(value);
            var serializedDouble = SystemTextJsonSerializer.Serialize(value);

            var deserializedFoo = SystemTextJsonSerializer.Deserialize<SystemTextJsonDoubleId>(serializedDouble);

            Assert.Equal(foo, deserializedFoo);
        }

        [Fact]
        public void CanSerializeToDouble_WithBothJsonConverters()
        {
            var foo = new BothJsonDoubleId(123d);

            var serializedFoo1 = NewtonsoftJsonSerializer.SerializeObject(foo);
            var serializedDouble1 = NewtonsoftJsonSerializer.SerializeObject(foo.Value);

            var serializedFoo2 = SystemTextJsonSerializer.Serialize(foo);
            var serializedDouble2 = SystemTextJsonSerializer.Serialize(foo.Value);

            Assert.Equal(serializedFoo1, serializedDouble1);
            Assert.Equal(serializedFoo2, serializedDouble2);
        }

        [Fact]
        public void WhenNoJsonConverter_SerializesWithValueProperty()
        {
            var foo = new NoJsonDoubleId(123.4d);

            var serialized1 = NewtonsoftJsonSerializer.SerializeObject(foo);
            var serialized2 = SystemTextJsonSerializer.Serialize(foo);

            var expected = "{\"Value\":" + foo.Value + "}";

            Assert.Equal(expected, serialized1);
            Assert.Equal(expected, serialized2);
        }
    }
}