
using SegundaWebAPI.Entities;
using System.Collections.Generic;

namespace SegundaWebAPI.entities
{
    public class Autor
    {
        /// <summary>
        /// Id del autor
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del autor
        /// </summary>
        public string nombre { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
