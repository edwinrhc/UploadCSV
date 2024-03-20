using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UploadCSV.Models
{
    public class Direccion
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Sector{ get; set; }
        public int Grupo { get; set; }
        public string Manzana{ get; set; }
        public string Lote   { get; set; }
        public string Avenida{ get; set; }  


    }
}
