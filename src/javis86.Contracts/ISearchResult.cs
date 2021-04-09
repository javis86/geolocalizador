using System;

namespace javis86.Contracts
{
    public interface ISearchResult
    {
        Guid Id { get; set; }
        bool Found { get; set; }
        double? Latitud { get; set; }
        double? Longitud { get; set; }
    }
}