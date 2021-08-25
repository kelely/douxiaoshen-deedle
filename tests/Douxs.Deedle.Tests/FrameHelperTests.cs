using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Deedle
{
    public class FrameHelperTests
    {
        [Fact]
        public void Test()
        {
            var array = JArray.Parse(@"[{'key1':123, 'key2':456, 'key3':654},{'key1':789, 'key2':562, 'key3':518}]");
            var df = FrameHelper.FromJsonArray(array);
            df.RowCount.ShouldBe(2);
            df.ColumnCount.ShouldBe(4);
        }
    }
}