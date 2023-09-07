using RevvyTasks.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RevvyTasks.Services;

public class SumRepresentationFinder : ISumRepresentationFinder
{
    public bool SumCanBeRepresentedFromArray(int[] arr, int sum)
    {
        var n = arr.Length;

        return isSubsetSum(arr, n, sum);
    }

    private static bool isSubsetSum(int[] arr, int n, int sum)
    {
        if (sum == 0)
            return true;
        if (n == 0)
            return false;

        if (arr[n - 1] > sum)
            return isSubsetSum(arr, n - 1, sum);

        return isSubsetSum(arr, n - 1, sum)
            || isSubsetSum(arr, n - 1, sum - arr[n - 1]);
    }
}


public static class SumFinderEndpoints
{
	public static void MapSumFinderEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/SumFinder");

        group.MapGet("/", () =>
        {
            return new [] { new SumRepresentationFinder() };
        })
        .WithName("GetAllSumFinders");

        group.MapGet("/{id}", (int id) =>
        {
            //return new SumFinder { ID = id };
        })
        .WithName("GetSumFinderById");

        group.MapPut("/{id}", (int id, SumRepresentationFinder input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateSumFinder");

        group.MapPost("/", (SumRepresentationFinder model) =>
        {
            //return TypedResults.Created($"/api/SumFinders/{model.ID}", model);
        })
        .WithName("CreateSumFinder");

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new SumFinder { ID = id });
        })
        .WithName("DeleteSumFinder");
    }
}