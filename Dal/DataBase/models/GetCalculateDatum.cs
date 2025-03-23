using System;
using System.Collections.Generic;

namespace Dal.DataBase.models;



public class GetCalculateDatum
{
    public int Id { get; set; }

    public string NameStation { get; set; } = null!;

    public double MaxTemp { get; set; }

    public double MinTemp { get; set; }

    public double MaxRain { get; set; }

    public double MinRain { get; set; }
}
