using System;
using System.Collections.Generic;

namespace Dal.DataBase.models;

public  class Station
{
    public int Id { get; set; }

    public string Name { get; set; } 

    public string Adress { get; set; } 

    public string Phone { get; set; } 

    public string Principal { get; set; } 

    public Station(int id, string? name, string? adress, string? phone, string? principal)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Adress = adress;
        Principal = principal;

    }
}
