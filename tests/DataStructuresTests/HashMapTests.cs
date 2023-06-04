namespace DataStructuresTests
{
    public class HashMapTests
    {
        [Fact]
        public void Put_Get_ReturnsCorrectValue()
        {
            // Arrange
            var map = new SimpleHashMap<string, int>(10);
            string key = "test";
            int value = 123;

            // Act
            map.Put(key, value);
            int returnedValue = map.Get(key);

            // Assert
            Assert.Equal(value, returnedValue);
        }

        [Fact]
        public void Put_UpdateValue_ReturnsUpdatedValue()
        {
            // Arrange
            var map = new SimpleHashMap<string, int>(10);
            string key = "test";
            int value = 123;
            int updatedValue = 456;

            // Act
            map.Put(key, value);
            map.Put(key, updatedValue); // Update the value
            int returnedValue = map.Get(key);

            // Assert
            Assert.Equal(updatedValue, returnedValue);
        }

        [Fact]
        public void ContainsKey_KeyExists_ReturnsTrue()
        {
            // Arrange
            var map = new SimpleHashMap<string, int>(10);
            string key = "test";
            int value = 123;

            // Act
            map.Put(key, value);
            bool containsKey = map.ContainsKey(key);

            // Assert
            Assert.True(containsKey);
        }

        [Fact]
        public void ContainsKey_KeyDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var map = new SimpleHashMap<string, int>(10);
            string key = "test";

            // Act
            bool containsKey = map.ContainsKey(key);

            // Assert
            Assert.False(containsKey);
        }

        [Fact]
        public void Remove_Key_RemovesKey()
        {
            // Arrange
            var map = new SimpleHashMap<string, int>(10);
            string key = "test";
            int value = 123;
            map.Put(key, value);

            // Act
            map.Remove(key);
            bool containsKey = map.ContainsKey(key);

            // Assert
            Assert.False(containsKey);
        }

        [Fact]
        public void Size_ReturnsCorrectCount()
        {
            // Arrange
            var map = new SimpleHashMap<string, int>(10);
            string key1 = "test1";
            string key2 = "test2";
            int value = 123;

            // Act
            map.Put(key1, value);
            map.Put(key2, value);
            int size = map.Size();

            // Assert
            Assert.Equal(2, size);
        }

        [Fact]
        public void Clear_EmptiesMap()
        {
            // Arrange
            var map = new SimpleHashMap<string, int>(10);
            string key1 = "test1";
            string key2 = "test2";
            int value = 123;

            // Act
            map.Put(key1, value);
            map.Put(key2, value);
            map.Clear();
            bool containsKey1 = map.ContainsKey(key1);
            bool containsKey2 = map.ContainsKey(key2);

            // Assert
            Assert.False(containsKey1);
            Assert.False(containsKey2);
        }

        [Fact]
        public void Put_CollidingKeys_StoresAndRetrievesCorrectly()
        {
            var map = new SimpleHashMap<string, int>(10);
            string key1 = "test1";
            string key2 = "test2";
            int value1 = 123;
            int value2 = 456;

            // Act
            map.Put(key1, value1);
            map.Put(key2, value2);

            // Assert
            Assert.Equal(value1, map.Get(key1));
            Assert.Equal(value2, map.Get(key2));
        }

        [Fact]
        public void Remove_CollidingKeys_RemovesCorrectly()
        {
            var map = new SimpleHashMap<string, int>(10);
            string key1 = "test1";
            string key2 = "test2";
            int value1 = 123;
            int value2 = 456;
            map.Put(key1, value1);
            map.Put(key2, value2);

            // Act
            map.Remove(key1);

            // Assert
            Assert.False(map.ContainsKey(key1));
            Assert.Equal(value2, map.Get(key2));
        }

        [Fact]
        public void Put_MultipleKeys_AddsAndRetrievesCorrectly()
        {
            var map = new SimpleHashMap<string, int>(10);
            map.Put("test1", 123);
            map.Put("test2", 456);
            map.Put("test3", 789);

            Assert.Equal(123, map.Get("test1"));
            Assert.Equal(456, map.Get("test2"));
            Assert.Equal(789, map.Get("test3"));
        }

        [Fact]
        public void Put_NullValue_AddsCorrectly()
        {
            var map = new SimpleHashMap<string, string>(10);
            map.Put("test", null);

            Assert.Null(map.Get("test"));
        }

        [Fact]
        public void Clear_Map_ClearsCorrectly()
        {
            var map = new SimpleHashMap<string, int>(10);
            map.Put("test", 123);
            map.Clear();

            Assert.Equal(0, map.Size());
        }

        [Fact]
        public void IsEmpty_EmptyMap_ReturnsTrue()
        {
            var map = new SimpleHashMap<string, int>(10);

            Assert.True(map.IsEmpty());
        }

        [Fact]
        public void IsEmpty_NonEmptyMap_ReturnsFalse()
        {
            var map = new SimpleHashMap<string, int>(10);
            map.Put("test", 123);

            Assert.False(map.IsEmpty());
        }

        [Fact]
        public void HashMap_Iteration_WorksCorrectly()
        {
            // Arrange
            var data = new SimpleHashMap<string, int>(10);
            data.Put("test1", 1);
            data.Put("test2", 2);
            data.Put("test3", 3);

            // Act
            var enumeratedData = new List<Node<string, int>>();
            foreach (var node in data)
            {
                enumeratedData.Add(node);
            }

            // Assert
            Assert.Equal(data.Size(), enumeratedData.Count);
            foreach (var node in data)
            {
                Assert.Contains(node, enumeratedData);
            }
        }

        [Fact]
        public void HashMap_Iteration_EmptyHashMap()
        {
            // Arrange
            var data = new SimpleHashMap<string, int>(10);

            // Act
            var enumeratedData = new List<Node<string, int>>();
            foreach (var node in data)
            {
                enumeratedData.Add(node);
            }

            // Assert
            Assert.Empty(enumeratedData);
        }

        [Fact]
        public void CollisionHashMap_GetHash_LinkingNodes()
        {
            // Arrange
            var data = new CollisionHashMap<int>(10);
            data.Put(1, 1);
            data.Put(11, 11);
            data.Put(101, 101);
            // Act
            var hash1 = data.GetHashCode(1);
            var hash11 = data.GetHashCode(11);

            // Assert
            Assert.Equal(hash1, hash11);
            Assert.Equal(1, data.Get(1));
            Assert.Equal(11, data.Get(11));
            Assert.Equal(101, data.Get(101));
        }
    }
}