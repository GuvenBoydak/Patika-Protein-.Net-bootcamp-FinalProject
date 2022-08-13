using FinalProject.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalProject.Api
{
    //FluentValidation default filter yerine kendi filterimizi olusturduk.
    public class ValidatorFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //ModelState.Values dan errorundan errorMesajı alıp List<string> tipindeki errors a atıyoruz.
                List<string> errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                //Contextden Gelen hataları CustomResponseDto ile  clienta dto olarak döndüruyoruz.
                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(404, errors, "Hatalı işlem"));

            }
        }
    }
}
