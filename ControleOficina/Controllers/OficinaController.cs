using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleOficina.Models;
using Oficina.Data;
using System.Net.Http;
using System.Text.Json;


namespace ControleOficina.Controllers
{
    public class OficinaController : Controller
    {
        private readonly OficinaContext _context;
        private readonly HttpClient _httpClient;
        public OficinaController(HttpClient client, OficinaContext context)
        {
            _context = context;
            _httpClient = client;
        }

        // GET: Oficina

        [HttpGet]
        public async Task<IActionResult> GetMarcas(string tipoVeiculo)
        {
            // A Brasil API espera: carros, motos ou caminhoes
            if (string.IsNullOrEmpty(tipoVeiculo)) tipoVeiculo = "carros";

            string url = $"https://brasilapi.com.br/api/fipe/marcas/v1/{tipoVeiculo}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    // Usando System.Text.Json para desserializar
                    var marcas = JsonSerializer.Deserialize<List<MarcaFipe>>(jsonString, new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true 
                    });

                    return Json(marcas);
                }
                
                return BadRequest("Erro ao buscar marcas na API externa.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetModelos(string tipoVeiculo, long codigoMarca)
        {
            if (string.IsNullOrEmpty(tipoVeiculo)) tipoVeiculo = "carros";
            string url = $"https://brasilapi.com.br/api/fipe/veiculos/v1/{tipoVeiculo}/{codigoMarca}";
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    // The Brasil API returns an object containing a "modelos" list
                    var data = JsonSerializer.Deserialize<FipeModelosResponse>(jsonString, new JsonSerializerOptions 
                    { 
                        PropertyNameCaseInsensitive = true 
                    });

                    return Json(data.Modelos);
                }
                return BadRequest("Erro ao buscar modelos.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carro.ToListAsync());
        }

        // GET: Oficina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // GET: Oficina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Oficina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marca,Modelo,AnoFarbrica,Descricao")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carro);
        }

        // GET: Oficina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carro.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }
            return View(carro);
        }

        // POST: Oficina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marca,Modelo,AnoFarbrica,Descricao")] Carro carro)
        {
            if (id != carro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarroExists(carro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carro);
        }

        // GET: Oficina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // POST: Oficina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carro = await _context.Carro.FindAsync(id);
            if (carro != null)
            {
                _context.Carro.Remove(carro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarroExists(int id)
        {
            return _context.Carro.Any(e => e.Id == id);
        }
    }
}