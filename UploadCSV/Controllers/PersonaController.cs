using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadCSV.Context;
using System;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using UploadCSV.Models;
using UploadCSV.Map;

namespace UploadCSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly AppDbContext context;

        public PersonaController(AppDbContext context)
        {
            this.context = context;
        }

        // Metodo POST - Metodo funciona para la tabla Persona solo una tabla
        //[HttpPost("UploadCsv")]
        //public async Task<IActionResult> UploadCsv()
        //{
        //    try
        //    {
        //        var file = Request.Form.Files[0]; // Obtener el archivo CSV desde la solicitud

        //        using (var reader = new StreamReader(file.OpenReadStream()))
        //        using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
        //        {
        //            Delimiter = "|", // Establecer el delimitador de encabezados como coma
        //            HasHeaderRecord = true // Indicar que el archivo CSV tiene una fila de encabezado
        //        }))
        //        {
        //            csv.Context.RegisterClassMap<PersonaMap>(); // Registrar el mapeo personalizado de CsvHelper

        //            var records = csv.GetRecords<Persona>().ToList();
        //            await context.AddRangeAsync(records);
        //            await context.SaveChangesAsync();
        //        }

        //        return Ok("Datos insertados con éxito");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error al procesar el archivo CSV: " + ex.Message);
        //    }
        //}

        [HttpPost("UploadCsv")]
        public async Task<IActionResult> UploadCsv()
        {
            try
            {
                var file = Request.Form.Files[0]; // Obtenemos el archivo CSV desde la solicitud
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    Delimiter = "|",
                    HasHeaderRecord = false // No hay encabezado en el archivo CSV
                }))
                {
                    var personas = new List<Persona>(); // Lista para almacenar las personas
                    var direcciones = new List<Direccion>(); // Lista para almacenar las direcciones

                    // Leer datos de la persona de la primera fila
                    if (csv.Read())
                    {
                        var persona = new Persona
                        {
                            DNI = csv.GetField<string>(0), // El DNI está en la primera columna
                            Nombre = csv.GetField<string>(1), // El Nombre está en la segunda columna
                            Apellido = csv.GetField<string>(2), // El Apellido está en la tercera columna
                            Edad = csv.GetField<int>(3) // La Edad está en la cuarta columna
                        };
                        personas.Add(persona);
                    }
                    else
                    {
                        return BadRequest("El archivo CSV está vacío");
                    }

                    // Leer datos de dirección de las filas siguientes
                    while (csv.Read())
                    {
                        var direccion = new Direccion
                        {
                            Sector = csv.GetField<string>(1), // El Sector está en la primera columna
                            Grupo = csv.GetField<int>(2), // El Grupo está en la segunda columna
                            Manzana = csv.GetField<string>(3), // La Manzana está en la tercera columna
                            Lote = csv.GetField<string>(4), // El Lote está en la cuarta columna
                            Avenida = csv.GetField<string>(5) // La Avenida está en la quinta columna
                        };
                        direcciones.Add(direccion);
                    }

                    // Guardar datos en la BD
                    await context.AddRangeAsync(personas);
                    await context.AddRangeAsync(direcciones);
                    await context.SaveChangesAsync();
                }

                return Ok("Datos insertados con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al procesar el archivo CSV: " + ex.Message);
            }
        }


    }



}

