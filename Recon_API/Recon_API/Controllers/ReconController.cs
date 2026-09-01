using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReconModels;
using ReconServiceLayer;
using System.Data;
using System.Data.Common;
using static ReconModels.DatasetModel;
using static ReconModels.ReconModel;
using static ReconModels.UserManagementModel;


namespace Recon_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReconController : ControllerBase
    {
		private IConfiguration _configuration;
		public ReconController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		string constring = "";
		[HttpGet("recontype")]
        public IActionResult ReconType()
        {
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
            try
            {
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.getReconType(header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("reconlist")]
        public IActionResult ReconList([FromBody] Reconlist objreconlist)
        {
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
            try
            {
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.getReconList(objreconlist, header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }


        [HttpPost("fetchrecondetails")]
        public IActionResult fetchReconDetails([FromBody]  fetchRecon objfetch)
        {
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
            DataSet ds = new DataSet();
            try
            {
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				ds = ReconService.fetchReconDetails(objfetch, header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(ds, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("recondatamapping")]
        public IActionResult recondatamapping(datamapping objdatamapping)
        {
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
            try
            {
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.recondatamapping(objdatamapping, header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

		[HttpPost("recondatafield")]
		public IActionResult recondatafield(datafieldmodel objdatafieldmodel)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.recondatafieldsrv(objdatafieldmodel, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		[HttpPost("recondatamappingdelete")]
		public IActionResult recondatamappingdel(datamapping objdatamapping)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.recondatamappingdelete(objdatamapping, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}
		
		[HttpPost("Recon")]
        public IActionResult Recon([FromBody] Recon recon)
        {
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
            try
            {
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.Recon(recon, header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
                return Ok(serializedProduct);
            }
            catch (Exception ex)
            {
                return Problem(title: ex.Message);
            }
        }


        [HttpPost("Recondataset")]
        public IActionResult Recondataset([FromBody] Recondataset objrecondataset)
        {
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
            try
            {
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.Recondataset(objrecondataset, header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
                return Ok(serializedProduct);
            }
            catch (Exception ex)
            {
                return Problem(title: ex.Message);
            }
        }

        [HttpPost("getReconDataMappingList")]
        public IActionResult getReconDataMappingList([FromBody] getReconDataMappingList objdatamappinglist)
        {
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
            try
            {
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.Recondatamappinglist(objdatamappinglist, header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
                return Ok(serializedProduct);
            }
            catch (Exception ex)
            {
                return Problem(title: ex.Message);
            }
        }

		[HttpPost("getFieldAgainstRecon")]
		public IActionResult getFieldAgainstRecon([FromBody] getFieldAgainstReconList objfieldlist)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.ReconFieldAgainstReconlist(objfieldlist, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
				return Ok(serializedProduct);
			}
			catch (Exception ex)
			{
				return Problem(title: ex.Message);
			}
		}

        [HttpPost("getreconknockofflist")]
        public IActionResult getreconknockofflist()
        {
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
            DataTable response = new DataTable();
            try
            {
                var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
                var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
                var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
                header_value.user_code = getvalue;
                header_value.lang_code = getlangCode;
                header_value.role_code = getRoleCode;
                response = ReconService.reconlistknockoffService(header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
                return Ok(serializedProduct);
            }
            catch (Exception ex)
            {
                return Problem(title: ex.Message);
            }
        }


        [HttpPost("getReconAgainstTypecode")]
		public IActionResult getReconAgainstTypecode(Reconagainsttypecode objreconlist)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.getReconagainsttypecode(objreconlist, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		
		[HttpPost("Datasetfield")]
		public IActionResult DatasetReaddetail(Datasetfieldlist Datasetfieldlist)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataSet response = new DataSet();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.Datasetfield(Datasetfieldlist, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		[HttpPost("cloneRecon")]
		public IActionResult cloneRecon(cloneReconModel objcloneRecon)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.cloneReconService(objcloneRecon, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		[HttpPost("cloneReconDataset")]
		public IActionResult cloneReconDataset(cloneReconDatasetModel objcloneReconDataset)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.cloneReconDatasetService(objcloneReconDataset, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		[HttpPost("fetchclonerecondetail")]
		public IActionResult fetchCloneReconDetails(fetchRecon objfetch)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			DataSet ds = new DataSet();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				ds = ReconService.fetchCloneReconDetails(objfetch, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(ds, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}
		[HttpPost("ReconDatasetlist")]
		public IActionResult ReconDatasetlist()
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.ReconDatasetlist(header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		//getReconforOpeningBalance
		[HttpGet("getReconforOpeningBalance")]
		public IActionResult getReconforOpeningBalance()
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.getReconforOpeningBalanceService(header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		//getdatasetagainstRecon
		[HttpPost("getdatasetagainstRecon")]
		public IActionResult getdatasetagainstRecon(openingbalanceDatasetModel objReconDataset)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.getdatasetagainstReconService(objReconDataset, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}
		[HttpPost("ReconDatasetmaplist")]
		public IActionResult ReconDatasetmaplist(fetchRecon objDatasetmap)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = ReconService.ReconDatasetmaplist_srv(objDatasetmap,header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

        [HttpPost("ArcheiveRecon")]
        public IActionResult ArcheiveRecon(ArcheiveReconobj objArcheiveRecon)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            headerValue header_value = new headerValue();
            DataTable response = new DataTable();
            try
            {
                var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
                var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
                var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
                header_value.user_code = getvalue;
                header_value.lang_code = getlangCode;
                header_value.role_code = getRoleCode;
                response = ReconService.ArcheiveReconService(objArcheiveRecon, header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("ArcheiveReconlist")]
        public IActionResult ArcheiveReconlist(ArcheiveReconobj objArcheiveRecon)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            headerValue header_value = new headerValue();
            DataTable response = new DataTable();
            try
            {
                var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
                var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
                var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
                header_value.user_code = getvalue;
                header_value.lang_code = getlangCode;
                header_value.role_code = getRoleCode;
                response = ReconService.ArcheiveReconlistService(objArcheiveRecon, header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }
    }
}
