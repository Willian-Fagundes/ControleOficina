using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleOficina.Models;

public class OficinaContext : DbContext
{
    public DbSet<Carro> Carros {get;set;}
    public string DbPath {get; }

    public OficinaContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "oficina.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"DataSource={DbPath}");
    
}

public class Carro
{
    public int Id {get;set;}
    public string? Marca {get;set;}
    public string? Modelo {get;set;}
    [DataType(DataType.Date)]
    public DateTime AnoFarbrica {get;set;}
    public string? Descricao {get;set;}
}