using System;
using System.Collections.Generic;

class Program
{
    static RentalStore rentalStore;
    static void Main()
    {
        Console.WriteLine("   *** Witaj w wypożyczalni DVD Krzysimir's Company ***   ");

        rentalStore = new RentalStore();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine(" ** Witaj! Jestem automatycznym asystentem klienta, w czym mogę Ci pomóc? **");
            Console.WriteLine("1. Chciałbym przejrzeć katalog filmów.");
            Console.WriteLine("2. Chciałbym zobaczyć cennik.");
            Console.WriteLine("3. Chciałbym wypożyczyć film.");
            Console.WriteLine("4. Chciałbym oddać film.");
            Console.WriteLine("5. Chciałbym zakończyć.");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Opcja: Katalog filmów");
                    rentalStore.DisplayCatalog();
                    break;
                case "2":
                    Console.WriteLine("Opcja: Cennik");
                    Pricing();
                    break;
                case "3":
                    Console.WriteLine("Opcja: Wypożycz film");
                    RentMovie();
                    break;
                case "4":
                    Console.WriteLine("Opcja: Oddaj film");
                    ReturnMovie();
                    break;
                case "5":
                    Console.WriteLine("Opcja: Zakończ");
                    Console.WriteLine("Dziękujemy za korzystanie z Krzysimir's Company!");
                    Console.WriteLine("   *** Zapraszamy Ponownie ***   ");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Bardzo przepraszam ale nie rozpoznaję tej opcji. Wybierz ponownie proszę.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void Pricing()
    {
        Console.WriteLine("Cennik wypożyczenia:");
        Console.WriteLine("1. Od 1 do 3 dni - 10zł.");
        Console.WriteLine("2. Od 3 do 7 dni - 15zł.");
        Console.WriteLine("3. Powyżej 7 dni - 30zł (nie dłużej niż miesiąc)");
    }

    static void RentMovie()
    {
        Console.WriteLine("Podaj numer pozycji filmu do wypożyczenia:");
        int itemNumber = int.Parse(Console.ReadLine());

        Movie movie = rentalStore.GetMovieByItemNumber(itemNumber);

        if (movie != null)
        {
            if (movie.IsAvailable)
            {
                Console.WriteLine($"Wybrano film: {movie.Title}");

                Console.WriteLine("Wybierz opcję czasu wypożyczenia:");
                Console.WriteLine("1. Od 1 do 3 dni");
                Console.WriteLine("2. Od 3 do 7 dni");
                Console.WriteLine("3. Powyżej 7 dni");

                string durationChoice = Console.ReadLine();

                switch (durationChoice)
                {
                    case "1":
                        RentMovieForDuration(movie, 3);
                        break;
                    case "2":
                        RentMovieForDuration(movie, 7);
                        break;
                    case "3":
                        RentMovieForDuration(movie, 30);
                        break;
                    default:
                        Console.WriteLine("Niepoprawny wybór czasu wypożyczenia.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ten film jest już wypożyczony. Spróbuj ponownie później.");
            }
        }
        else
        {
            Console.WriteLine("Nie znaleziono filmu o podanym numerze pozycji.");
        }
    }

    static void RentMovieForDuration(Movie movie, int days)
    {
        decimal rentalPrice = CalculateRentalPrice(movie, days);

        movie.IsAvailable = false;
        Console.WriteLine($"Wypożyczono film: {movie.Title} na {days} dni. Kwota do zapłacenia: {rentalPrice} zł");
    }

    static decimal CalculateRentalPrice(Movie movie, int days)
    {
        decimal rentalPrice = 0;

        if (days >= 1 && days <= 3)
        {
            rentalPrice = 10;
        }
        else if (days > 3 && days <= 7)
        {
            rentalPrice = 15;
        }
        else if (days > 7 && days <= 30)
        {
            rentalPrice = 30;
        }

        return rentalPrice;
    }

    static void ReturnMovie()
    {
        Console.WriteLine("Podaj numer pozycji filmu do oddania:");
        int itemNumber = int.Parse(Console.ReadLine());

        Movie movie = rentalStore.GetMovieByItemNumber(itemNumber);

        if (movie != null)
        {
            if (!movie.IsAvailable)
            {
                movie.IsAvailable = true;
                Console.WriteLine($"Oddano film: {movie.Title}");
            }
            else
            {
                Console.WriteLine("Ten film nie jest obecnie wypożyczony.");
            }
        }
        else
        {
            Console.WriteLine("Nie znaleziono filmu o podanym numerze pozycji.");
        }
    }
}

class Movie
{
    public int ItemNumber { get; set; }
    public string? Category { get; set; }
    public string? Title { get; set; }
    public string? Director { get; set; }
    public int Year { get; set; }
    public bool IsAvailable { get; set; }
}

class RentalStore
{
    private List<Movie> movies;

    public RentalStore()
    {
        movies = new List<Movie>();

        movies.Add(new Movie { ItemNumber = 1, Category = "Rodzinny", Title = "Księżniczka i żaba", Director = "Ron Clements, John Musker", Year = 2009, IsAvailable = true });
        movies.Add(new Movie { ItemNumber = 2, Category = "Komedia", Title = "Ace Ventura: Poszukiwacz zaginionych zwierząt", Director = "Tom Shadyac", Year = 1994, IsAvailable = true });
        movies.Add(new Movie { ItemNumber = 3, Category = "Romantyczna komedia", Title = "Notting Hill", Director = "Roger Michell", Year = 1999, IsAvailable = true });
        movies.Add(new Movie { ItemNumber = 4, Category = "Przygodowy", Title = "Indiana Jones i poszukiwacze zaginionej arki", Director = "Steven Spielberg", Year = 1981, IsAvailable = true });
        movies.Add(new Movie { ItemNumber = 5, Category = "Dramat", Title = "Skazani na Shawshank", Director = "Frank Darabont", Year = 1994, IsAvailable = true });
        movies.Add(new Movie { ItemNumber = 6, Category = "Akcja", Title = "Szklana pułapka", Director = "John McTiernan", Year = 1988, IsAvailable = true });
        movies.Add(new Movie { ItemNumber = 7, Category = "Science Fiction", Title = "Łowca androidów", Director = "Ridley Scott", Year = 1982, IsAvailable = true });
        movies.Add(new Movie { ItemNumber = 8, Category = "Thriller", Title = "Milczenie owiec", Director = "Jonathan Demme", Year = 1991, IsAvailable = true });
        movies.Add(new Movie { ItemNumber = 9, Category = "Komedia romantyczna", Title = "Kiedy Harry poznał Sally", Director = "Rob Reiner", Year = 1989, IsAvailable = true });
        movies.Add(new Movie { ItemNumber = 10, Category = "Fantasy", Title = "Harry Potter i Kamień Filozoficzny", Director = "Chris Columbus", Year = 2001, IsAvailable = true });
    }

    public void DisplayCatalog()
    {
        Console.WriteLine("Katalog Filmów:");
        Console.WriteLine();

        foreach (Movie movie in movies)
        {
            Console.WriteLine($"Numer Pozycji: {movie.ItemNumber}");
            Console.WriteLine($"Kategoria: {movie.Category}");
            Console.WriteLine($"Tytuł: {movie.Title}");
            Console.WriteLine($"Reżyser: {movie.Director}");
            Console.WriteLine($"Rok Produkcji: {movie.Year}");
            Console.WriteLine($"Dostępny: {(movie.IsAvailable ? "Tak" : "Nie")}");
            Console.WriteLine();
        }
    }

    public Movie GetMovieByItemNumber(int itemNumber)
    {
        return movies.Find(movie => movie.ItemNumber == itemNumber);
    }
}