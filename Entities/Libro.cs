﻿using SegundaWebAPI.entities;

namespace SegundaWebAPI.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }

    }
}