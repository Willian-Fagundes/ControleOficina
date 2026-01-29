using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ControleOficina.Models;

public class Carro
{
    public int Id {get;set;}
    public string? Marca {get;set;}
    public string? Modelo {get;set;}
    [Display(Name = "Ano de Fabrição")]
    [DataType(DataType.Date)]
    public DateTime AnoFarbrica {get;set;}
    public string? Descricao {get;set;}
}
public class MarcaFipe {
    public string Nome { get; set; }
    public string Valor { get; set; } // O código da marca usado na URL
}
public class FipeModelosResponse
{
    public List<MarcaFipe> Modelos { get; set; }
}