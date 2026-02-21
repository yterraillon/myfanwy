namespace Thermo.Application.Models;

public static class Rooms
{
    public static string[] AllRooms => [LivingRoom, Room1, Room2, Outside];
    
    public const string LivingRoom = "Salon";
    public const string Room1 = "Chambre";
    public const string Room2 = "Chambre Mael";
    public const string Outside = "Extérieur";
}