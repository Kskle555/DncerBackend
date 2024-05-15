using DncerBackend.AppContext;

using DncerBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DncerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


  
    public class CarController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CarController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCars()
        {
            try
            {
                var cars = _context.Cars.ToList(); // Tüm ürünleri getir
                return Ok(cars);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            try
            {
                var car = _context.Cars.Find(id); // Id'ye göre ürün getir
                return Ok(car);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddCar(Cars car)
        {
            if (car == null)
            {
                return BadRequest("Araba bilgileri geçersiz.");
            }

            try
            {
                // Parametreleri oluşturma
                SqlParameter carNameParam = new SqlParameter("@CarName", car.CarName);
                SqlParameter brandParam = new SqlParameter("@Brand", car.Brand);
                SqlParameter modelParam = new SqlParameter("@Model", car.Model);
                SqlParameter carYearParam = new SqlParameter("@CarYear", car.CarYear);
                SqlParameter colorParam = new SqlParameter("@Color", car.Color);
                SqlParameter engineTypeParam = new SqlParameter("@EngineType", car.EngineType);

                // ExecuteSqlRaw kullanarak saklı prosedürü çağırma
                _context.Database.ExecuteSqlRaw("EXEC InsertCars @CarName, @Brand, @Model, @CarYear, @Color, @EngineType",
                    carNameParam, brandParam, modelParam, carYearParam, colorParam, engineTypeParam);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Beklenmeyen bir hata oluştu: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Beklenmeyen bir hata oluştu.");
            }
        }
        //[HttpPost]
        //public IActionResult AddCar(Cars car)
        //{
        //    if (car == null)
        //    {
        //        return BadRequest("Araba bilgileri geçersiz.");
        //    }

        //    try
        //    {
        //        car.Id = null;
        //        _context.Cars.Add(car);
        //        _context.SaveChanges();
        //        return StatusCode(StatusCodes.Status201Created);
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        Console.WriteLine("Veritabanı hatası: " + ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Veritabanı hatası oluştu.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Beklenmeyen bir hata oluştu: " + ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Beklenmeyen bir hata oluştu.");
        //    }
        //}



        [HttpDelete]
        public IActionResult DeleteCar(int id)
        {
            try
            {
                // Parametre oluşturma
                SqlParameter carIDParam = new SqlParameter("@CarID", id);

                // ExecuteSqlRaw kullanarak saklı prosedürü çağırma
                _context.Database.ExecuteSqlRaw("EXEC DeleteCars @CarID", carIDParam);

                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }




        //[HttpDelete]
        //public IActionResult DeleteCar(int id)
        //{
        //    try
        //    {
        //        var car = _context.Cars.Find(id); //get car by id
        //        _context.Cars.Remove(car);   // delete car from db
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    catch (System.Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpPut]
        public IActionResult UpdateCar(int id, [FromBody] Cars car)
        {
            try
            {
                // Parametreler oluşturma
                SqlParameter carIDParam = new SqlParameter("@CarID", id);
                SqlParameter carNameParam = new SqlParameter("@CarName", car.CarName);
                SqlParameter brandParam = new SqlParameter("@Brand", car.Brand);
                SqlParameter modelParam = new SqlParameter("@Model", car.Model);
                SqlParameter carYearParam = new SqlParameter("@CarYear", car.CarYear);
                SqlParameter colorParam = new SqlParameter("@Color", car.Color);
                SqlParameter engineTypeParam = new SqlParameter("@EngineType", car.EngineType);

                // ExecuteSqlRaw kullanarak saklı prosedürü çağırma
                _context.Database.ExecuteSqlRaw("EXEC UpdateCars @CarID, @CarName, @Brand, @Model, @CarYear, @Color, @EngineType",
                    carIDParam, carNameParam, brandParam, modelParam, carYearParam, colorParam, engineTypeParam);

                // Güncellenmiş arabayı döndürme
                var updatedCar = _context.Cars.Find(id);
                return Ok(updatedCar);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //[HttpPut]
        //public IActionResult UpdateCar (int id, [FromBody] Cars car)
        //{
        //    try
        //    {
        //        var existingCar = _context.Cars.Find(id);
        //        if (existingCar != null)
        //        {
        //            existingCar.CarName = car.CarName;
        //            existingCar.Brand = car.Brand;
        //            existingCar.Model = car.Model;
        //            existingCar.CarYear = car.CarYear;
        //            existingCar.Color = car.Color;
        //            existingCar.EngineType = car.EngineType;

        //            _context.Cars.Update(existingCar);
        //            _context.SaveChanges();
        //            return Ok(existingCar);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status404NotFound);
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
    }
}
