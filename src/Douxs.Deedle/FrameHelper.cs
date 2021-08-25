using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EnsureThat;
using Newtonsoft.Json.Linq;

namespace Deedle
{
    public static class FrameHelper
    {
        public static Frame<int, string> FromJsonArray([NotNull] JArray objects)
        {
            Ensure.That(objects).IsNotNull();
            Ensure.That(objects).HasItems();

            // ReSharper disable once EnforceIfStatementBraces
            if (objects.First is not JObject o)
                throw new FormatException($"{nameof(objects)} 必须是一个包含 JObject 对象的数组!");

            var rows = new List<KeyValuePair<int, Series<string, object>>>();
            var properties = o.Properties().Select(p => p.Name).ToArray();

            for (var i = 0; i < objects.Count; i++)
            {
                var sb = new SeriesBuilder<string> {{"Index", i}};

                foreach (var property in properties)
                {
                    sb.Add(property, objects[i][property]!.Value<string>());
                }

                rows.Add(KeyValue.Create(i, sb.Series));
            }

            var df = Frame.FromRows(rows);
            return df;
        }
    }
}