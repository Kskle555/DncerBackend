using DncerBackend.AppContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DncerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPartsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CarPartsController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCarParts()
        {
            try
            {
                var carParts = _context.CarParts.ToList(); // Tüm ürünleri getir
                return Ok(carParts);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCarPartById(int id)
        {
            try
            {
                var carPart = _context.CarParts.Find(id); // Id'ye göre ürün getir
                return Ok(carPart);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult AddCarPart([FromBody] Models.CarParts carPart)
        {
            try
            {
                if (carPart == null)
                {
                    return BadRequest("Ürün bilgileri geçersiz.");
                }

                // Parametreleri oluşturma
                SqlParameter partNameParam = new SqlParameter("@PartName", carPart.PartName);
                SqlParameter descriptionParam = new SqlParameter("@Description", carPart.Description);
                SqlParameter priceParam = new SqlParameter("@Price", carPart.Price);
                SqlParameter brandParam = new SqlParameter("@Brand", carPart.Brand);
                SqlParameter modelParam = new SqlParameter("@Model", carPart.Model);
                SqlParameter currencyParam = new SqlParameter("@Currency", carPart.Currency);
                SqlParameter vehicleBrandParam = new SqlParameter("@VehicleBrand", carPart.Brand);
                SqlParameter carIdParam = new SqlParameter("@CarId", carPart.CarId);

                // ExecuteSqlRaw kullanarak saklı prosedürü çağırma
                _context.Database.ExecuteSqlRaw("EXEC InsertCarParts @PartName, @Description, @Price, @Brand, @Model, @Currency, @VehicleBrand, @CarId",
                    partNameParam, descriptionParam, priceParam, brandParam, modelParam, currencyParam, vehicleBrandParam, carIdParam);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpPost]
        //public IActionResult AddCarPart([FromBody] Models.CarParts carPart)
        //{
        //    try
        //    {
        //        if (carPart == null)
        //        {
        //            return BadRequest("Ürün bilgileri geçersiz.");
        //        }
        //        var carParts = _context.CarParts.
        //            Where(cp => cp.PartName == carPart.PartName && cp.Brand == carPart.Brand && cp.Model == carPart.Model).
        //            ToList();
        //        //if (carParts.Count > 0)
        //        //{
        //        //    return BadRequest("Bu ürün zaten mevcut.");
        //        //}


        //        _context.CarParts.Add(carPart); // Ürün ekle
        //        _context.SaveChanges();
        //        return StatusCode(StatusCodes.Status201Created);
        //    }
        //    catch (System.Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}


        [HttpPut]
        public IActionResult UpdateCarPart(int id, [FromBody] Models.CarParts carPart)
        {
            try
            {
                // Parametreleri oluşturma
                SqlParameter partIDParam = new SqlParameter("@PartID", id);
                SqlParameter partNameParam = new SqlParameter("@PartName", carPart.PartName);
                SqlParameter descriptionParam = new SqlParameter("@Description", carPart.Description);
                SqlParameter priceParam = new SqlParameter("@Price", carPart.Price);
                SqlParameter brandParam = new SqlParameter("@Brand", carPart.Brand);
                SqlParameter modelParam = new SqlParameter("@Model", carPart.Model);
                SqlParameter currencyParam = new SqlParameter("@Currency", carPart.Currency);
                SqlParameter carIdParam = new SqlParameter("@CarId", carPart.CarId);

                // ExecuteSqlRaw kullanarak saklı prosedürü çağırma
                _context.Database.ExecuteSqlRaw("EXEC UpdateCarParts @PartID, @PartName, @Description, @Price, @Brand, @Model, @Currency,@CarId",
                    partIDParam, partNameParam, descriptionParam, priceParam, brandParam, modelParam, currencyParam, carIdParam);

                // Güncellenmiş parçayı döndürme
                var updatedCarPart = _context.CarParts.Find(id);
                return Ok(updatedCarPart);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        //[HttpPut]
        //public IActionResult UpdateCarPart(int id, [FromBody] Models.CarParts carPart)
        //{
        //    try
        //    {
        //        var existingCarPart = _context.CarParts.Find(id);
        //        if (existingCarPart != null)
        //        {
        //            existingCarPart.PartName = carPart.PartName;
        //            existingCarPart.Description = carPart.Description;
        //            existingCarPart.Price = carPart.Price;
        //            existingCarPart.Brand = carPart.Brand;
        //            existingCarPart.Model = carPart.Model;
        //            //existingCarPart.CarId = carPart.CarId;
        //            _context.CarParts.Update(existingCarPart);
        //            _context.SaveChanges();
        //            return Ok(existingCarPart);
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



        [HttpDelete("{id}")]
        public IActionResult DeleteCarPart(int id)
        {
            try
            {
                // Parametre oluşturma
                SqlParameter partIDParam = new SqlParameter("@PartID", id);

                // ExecuteSqlRaw kullanarak saklı prosedürü çağırma
                _context.Database.ExecuteSqlRaw("EXEC DeleteCarParts @PartID", partIDParam);

                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //[HttpDelete("{id}")]
        //public IActionResult DeleteCarPart(int id)
        //{
        //    try
        //    {
        //        var carPart = _context.CarParts.Find(id); //get car by id
        //        _context.CarParts.Remove(carPart);   // delete car from db
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    catch (System.Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}


        [HttpGet("car/{carId}")]
        public IActionResult GetCarNameByCarId(int carId)
        {
            try
            {
                // Fetch CarName directly using a single query
                var carName = _context.Cars.Where(car => car.Id == carId)
                                          .Select(car => car.CarName)
                                          .FirstOrDefault();

                if (carName == null)
                {
                    return NotFound("Car with the specified ID not found.");
                }

                return Ok(carName);
            }
            catch (System.Exception ex)
            {
                // Log the exception for debugging purposes
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
