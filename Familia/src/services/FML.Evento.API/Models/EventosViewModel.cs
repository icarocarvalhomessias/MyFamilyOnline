﻿namespace FML.Evento.API.Models
{
    public class EventoModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Theme { get; set; }
    }
}
