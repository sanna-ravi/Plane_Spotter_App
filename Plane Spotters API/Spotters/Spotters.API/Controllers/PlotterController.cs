using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotters.Service.Interface;
using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Spotters.API.Controllers
{
   
    public class PlotterController : BaseAPIController
    {
        protected ISpotterService Service { get; set; }
        public PlotterController(ISpotterService service)
        {
            this.Service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlaneSpotterViewModel>> Get()
        {
            return Ok(Service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<PlaneSpotterViewModel>> CreateNew([FromForm] PlaneSpotterViewModel spotterViewModel)
        {
            var image = spotterViewModel.SpotterImage;

            String imageFile = SaveImageFile(image);
            if (!String.IsNullOrEmpty(imageFile))
            {
                spotterViewModel.SpotterImageUrl = imageFile;
                return Ok(await Service.AddAsync(spotterViewModel));
            }
            else
            {
                return BadRequest("There must be atleast a image");
            }
        }

        protected String SaveImageFile(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                String fileName = $"{Guid.NewGuid()}{image.FileName.Substring(image.FileName.LastIndexOf('.'))}";
                using (var fileStream = new FileStream(Path.Combine("images", fileName), FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                return $"images/{fileName}";
            }
            else
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<IEnumerable<PlaneSpotterViewModel>>> GetById(Guid id)
        {
            PlaneSpotterViewModel result = await Service.GetEntityById(id).ConfigureAwait(true);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        public async Task<ActionResult<PlaneSpotterViewModel>> Update(Guid id, [FromForm] PlaneSpotterViewModel spotterViewModel)
        {
            if (!spotterViewModel.InternalId.Equals(id))
            {
                return NotFound();
            }

            String imageFile = "";
            if (spotterViewModel.SpotterImage != null && spotterViewModel.SpotterImage.Length > 0)
            {
                imageFile = SaveImageFile(spotterViewModel.SpotterImage);
            }

            if (!String.IsNullOrEmpty(imageFile))
            {
                spotterViewModel.SpotterImageUrl = imageFile;
            }

            if (String.IsNullOrEmpty(spotterViewModel.SpotterImageUrl))
            {
                return BadRequest("There must be atleast a image");
            }

            PlaneSpotterViewModel result = await Service.UpdateAsync(id, spotterViewModel);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            await Service.DeleteAsync(id).ConfigureAwait(true);
            return Ok();
        }
    }
}
