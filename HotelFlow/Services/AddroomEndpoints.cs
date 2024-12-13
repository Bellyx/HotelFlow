using Microsoft.EntityFrameworkCore;
using HotelFlow.Data;
using HotelFlow.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace HotelFlow.Services
{
    public static class AddroomEndpoints
    {
        public static void MapAddroomEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Addroom").WithTags(nameof(Addroom));

            group.MapGet("/", async (HotelFlowContext db) =>
            {
                return await db.Addroom.ToListAsync();
            })
            .WithName("GetAllAddrooms")
            .WithOpenApi();


            group.MapGet("/{id}", async Task<Results<Ok<Addroom>, NotFound>> (int roomid, HotelFlowContext db) =>
            {
                return await db.Addroom.AsNoTracking()
                    .FirstOrDefaultAsync(model => model.RoomId == roomid)
                    is Addroom model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
            .WithName("GetAddroomById")
            .WithOpenApi();

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int roomid, Addroom addroom, HotelFlowContext db) =>
            {
                var affected = await db.Addroom
                    .Where(model => model.RoomId == roomid)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(m => m.RoomId, addroom.RoomId)
                        .SetProperty(m => m.RoomName, addroom.RoomName)
                        .SetProperty(m => m.Capacity, addroom.Capacity)
                        .SetProperty(m => m.RoomType, addroom.RoomType)
                        .SetProperty(m => m.Price, addroom.Price)
                        .SetProperty(m => m.ImgUrl, addroom.ImgUrl)
                        );
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateAddroom")
            .WithOpenApi();

            group.MapPost("/", async (Addroom addroom, HotelFlowContext db) =>
            {
                if (addroom == null)
                {
                    return Results.BadRequest("ข้อมูลห้องพักไม่ถูกต้อง");
                }
                // Ensure RoomName is not null or empty
                if (string.IsNullOrWhiteSpace(addroom.RoomName))
                {
                    return Results.BadRequest("RoomName is required");
                }
                db.Addroom.Add(addroom);
                int v = await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Addroom/{addroom.RoomId}", addroom);
            })
            .WithName("CreateAddroom")
            .WithOpenApi();

            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int roomid, HotelFlowContext db) =>
            {
                var affected = await db.Addroom
                    .Where(model => model.RoomId == roomid)
                    .ExecuteDeleteAsync();
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeleteAddroom")
            .WithOpenApi();
        }
    }

}



//public class Manageroom
//{ 


//}