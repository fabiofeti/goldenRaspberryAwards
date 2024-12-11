﻿namespace FFeti.GoldenRaspberryAwards.Domain;

public class Movie
{
    
    public Movie() { }
    public int Id { get;  set; }
    public string Title { get;  set; }
    public int Year { get;  set; }
    public string Studios { get;  set; }
    public string Producers { get;  set; }
    public bool Winner { get;  set; }
}
