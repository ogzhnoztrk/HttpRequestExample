using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestExample.ExampleLibrary
{
    public static class ResultHelper
    {
        public static IActionResult Result(string message)
        {
            var result = new Result(400, message);
            return new BadRequestObjectResult(result);
        }
        public static IActionResult Result(string message, object data)
        {
            var result = new Result(200, message, data);
            return new OkObjectResult(result);
        }
        public static IActionResult Result(int code, string message, object data)
        {
            var result = new Result(code, message, data);

            switch (code)
            {
                case 200:
                    return new OkObjectResult(result);
                case 400:
                    return new BadRequestObjectResult(result);
                default:
                    return new BadRequestObjectResult(result);
            }
        }
    }
}
