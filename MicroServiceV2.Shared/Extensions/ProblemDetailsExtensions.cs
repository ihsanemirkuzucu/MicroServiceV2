using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroServiceV2.Shared.Extensions
{
    public static class ProblemDetailsExtensions
    {
        // Extension method for logging ProblemDetails
        public static void LogProblemDetails(this ILogger logger, ApiException? apiException)
        {
            if (string.IsNullOrEmpty(apiException!.Content))
            {
                logger.LogError(apiException.Message);
                return;
            }


            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content!);

            if (problemDetails is null) return;

            logger.LogError(
                "Problem Details: Title: {Title}, Detail: {Detail}, Status: {Status}",
                problemDetails.Title,
                problemDetails.Detail,
                problemDetails.Status);
        }
    }
}
