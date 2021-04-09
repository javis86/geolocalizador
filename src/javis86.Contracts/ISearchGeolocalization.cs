using System;

namespace javis86.Contracts
{
    public interface ISearchGeolocalization
    {
        Guid Id { get; set; }
        string Calle { get; set; } 
        string Numero { get; set; }
        string Ciudad { get; set; }
        string CodigoPostal { get; set; }
        string Provincia { get; set; } 
        string Pais { get; set; }
    }
}