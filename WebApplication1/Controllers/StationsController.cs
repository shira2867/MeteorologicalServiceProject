using Bl.Api;
using Bl.Models;
using Bl.Service;
using Dal.DataBase.models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class StationController:ControllerBase
    {


        IBl bl;
        public StationController(IBl b)
        {
            bl = b;
        }


        // Endpoint for GetFullData
        [HttpGet("station-data")]
        public IActionResult GetStationData()
        {
            try
            {
                var data = bl.StationBl.GetAllStations();
                return Ok(data); // מחזיר את הנתונים בצורה תקינה
            }
            catch (Exception ex)
            {
                return BadRequest($"Error fetching stations: {ex.Message}"); // מחזיר שגיאה אם יש בעיה
            }
        }

        [HttpPost("create")]
        public ActionResult CreateStation([FromBody] Station station)
        {
            try
            {
                bl.StationBl.CreateStation(station);
                return Ok("Station created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating station: {ex.Message}");
            }
        }

        //    // Endpoint for UpdateStation
        [HttpPut("update/{name}")]
        public async Task< ActionResult >UpdateStation([FromBody] Station station, string name)
        {
            try
            {
                bool result = await bl.StationBl.UpdateStation(station, name);
                if (result)
                    return Ok("Station updated successfully.");
                return NotFound("Station not found.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error updating station: {ex.Message}");
            }
        }

        //    // Endpoint for DeleteStation
           [HttpDelete("delete")]
        public async Task< ActionResult> DeleteStation([FromBody] Station station)
        {
            try
            {
                bool result =  await bl.StationBl.DeleteStation(station);
                if (result)
                    return Ok("Station deleted successfully.");
                return NotFound("Station not found.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error deleting station: {ex.Message}");
            }
        }



        //    // Endpoint for GetCalculate
        [HttpGet("GetCalculateFullData")]
        public async Task< ActionResult<List<GetCalculateData>>> GetCalculate()
        {
            try
            {
                // קבלת רשימת תחנות מסוג BLstation
                var stations = await bl.StationBl.GetFullData(); // קבלת נתוני תחנות מהשירות
                var data =await bl.StationBl.GetCalculate(stations); // העברת רשימת תחנות ל-GetCalculate
                return Ok(data); // מחזיר את התוצאה
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error calculating data: {ex.Message}");
            }
        }

    }
}




