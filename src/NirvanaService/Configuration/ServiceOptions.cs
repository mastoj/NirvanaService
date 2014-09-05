using System.Collections.Generic;
using System.Linq;

namespace NirvanaService.Configuration
{
    public class ServiceOptions
    {
        public string ArgSeparator { get; set; }
        public string ArgPrefix { get; set; }
        public Dictionary<string, string> Arguments { get; set; }

        public override string ToString()
        {
            return Arguments.Aggregate("",
                (aggr, next) => string.IsNullOrWhiteSpace(next.Key) || string.IsNullOrWhiteSpace(next.Value) ? 
                    string.Format("{0} {1}{2}{3}", aggr, ArgPrefix, next.Key, next.Value) : // is key or value is white space don't write one of them as arg
                    string.Format("{0} {1}{2}{3}{4}", aggr, ArgPrefix, next.Key, ArgSeparator, next.Value)); // otherwise write <key> <argSep> <value>
        }
    }
}