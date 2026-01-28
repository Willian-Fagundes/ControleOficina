using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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