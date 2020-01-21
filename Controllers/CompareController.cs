using HappyPets_v1._1.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HappyPets_v1._1.Models
{
    public class CompareController : ApiController
    {
        private TestEntitiesnew db = new TestEntitiesnew();

        public object Get(int petId)
        {
            var Pet = db.Pets.Find(petId);
            var Caracacteristic = Pet.Caracteristics.OrderByDescending(x => x.Date).Select(x => new CompareDTO {
                Id = x.Id,
                Weight = x.Weight,
                Height = x.Height,
                Date = x.Date
            }).ToList();

            var References = db.References.Where(x => x.BreedId == Pet.BreedId).Select(x => new CompareDTO {
                Id = x.Id,
                Height = x.RefHeight,
                Weight = x.RefWeight
            }).ToList();
            var result = new ChartDTO();
            result.Caracteristics = Caracacteristic;
            result.References = References;

            //result.Caracteristic = Caracacteristic.Height;
            //result.Caracteristic = Caracacteristic.Weight;

            //result.Reference = References.RefHeight;
            //result.Reference = References.RefWeight;

            //result.CompareHeight = (result.Caracteristic >= result.Reference && result.Caracteristic <= result.Reference);
            //result.CompareWeight = (result.Caracteristic >= result.Reference && result.Caracteristic <= result.Reference);

            //return result;

            return (result);
        }
       
        }
    }

