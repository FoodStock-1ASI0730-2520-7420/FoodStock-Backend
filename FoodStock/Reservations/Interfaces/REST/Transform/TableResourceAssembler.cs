// path: src/Reservations/Interfaces/REST/Transform/TableResourceAssembler.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using FoodStock.Reservations.Interfaces.REST.Resources;

namespace FoodStock.Reservations.Interfaces.REST.Transform;

public static class TableResourceAssembler
{
    public static TableResource ToResource(Table e) => new(e.Id, e.Number, e.Capacity);
}