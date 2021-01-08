using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;
using Newtonsoft.Json.Serialization;
using Domain;
using Api.ViewModels;
using Newtonsoft.Json;
using AutoMapper;

namespace Api.Controllers
{
    public class ArticulosController : ApiController
    {
        private readonly IArticulosService service;
        private readonly IMapper mapper;

        public ArticulosController(IArticulosService _service, IMapper _mapper)
        {
            this.service = _service;
            this.mapper = _mapper;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            List<Articulo> lista = service.GetAll().ToList();
            return Ok(lista);

        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Articulo articulo = service.GetById(id);
            if (articulo != null)
                return Ok(articulo);
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] PostArticuloViewModel newArticulo)
        {
            Articulo articulo = mapper.Map<Articulo>(newArticulo);

            articulo = service.Add(articulo);
            if (articulo != null)
                return Ok(articulo);
            return BadRequest("No se pudo agregar el articulo");
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] PostArticuloViewModel updatedArticulo)
        {
            Articulo articulo = mapper.Map<Articulo>(updatedArticulo);

            articulo = service.Update(id, articulo);
            if (articulo != null)
                return Ok(articulo);
            return BadRequest("No se pudo actualizar el articulo");
        }

        public IHttpActionResult Delete(int id)
        {
            if (service.Delete(id))
                return Ok();
            return BadRequest();
        }
       
    }
}