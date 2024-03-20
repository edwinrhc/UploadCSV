using CsvHelper.Configuration;
using UploadCSV.Models;
namespace UploadCSV.Map
{
    public sealed class PersonaMap : ClassMap<Persona>
    {
        public PersonaMap() { 
            
            Map(m => m.Nombre).Name("Nombre");
            Map(m => m.Edad).Name("Edad");
            Map(m => m.Id).Ignore();
        
        }
    }
    
}
