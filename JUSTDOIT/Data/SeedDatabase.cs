using viacinema.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using viacinema.Models;

namespace viacinema.Data
{
    public static class SeedDatabase
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            // comment
            if (!context.Movies.Any())
            {
                AddMovies(context);
                context.SaveChanges();
            }

            if (!context.Rooms.Any())
            {
                AddRooms(context);
                context.SaveChanges();
            }

            if (!context.Seats.Any())
            {
                AddSeats(context);
                context.SaveChanges();
            }
            if (!context.Screenings.Any())
            {
                List<Movie> movies = context.Movies.Take(2).ToList();
                AddScreening(context, movies[0].Id, 1, DateTime.Now, "3D");
                AddScreening(context, movies[1].Id, 2, new DateTime(2018, 5, 22, 15, 0, 0), "2D");
                context.SaveChanges();
            }
            if (!context.SeatScreeningMediator.Any())
            {
                AddSeatScreening(context, 2);
                AddSeatScreening(context, 1);
                context.SaveChanges();
            }
            context.SaveChanges();
        }

        private static void AddMovies(ApplicationDbContext context)
        {
            var movies = new Movie[]
            {
                new Movie { Title = "Spider man", Genre = "Action", DurationInSeconds = 7980,  Description = "Spiderman is cool!", ReleaseDate = new DateTime(), Rating = 5, ImageUrl = "https://cdn.vox-cdn.com/thumbor/K4OmrMLjcqBK10FOUiky7DYuSc4=/0x0:1012x675/1280x854/cdn.vox-cdn.com/uploads/chorus_image/image/45661554/amazing-spider-man-poster-battle-damage.0.0.jpg" },
                new Movie { Title = "Frozen", Genre = "Cartoon", DurationInSeconds = 6540, Description = "Frozen is cool!", ReleaseDate = new DateTime(), Rating = 3, ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61Vnrpp7IJL.jpg" }
            };

            context.Movies.AddRange(movies);
        }

        private static void AddRooms(ApplicationDbContext context)
        {
            var rooms = new Room[]
            {
                new Room { availableSeats = 40, roomNo = 1, totalSeats = 40 },
                new Room { availableSeats = 50, roomNo = 2, totalSeats = 50 }
            };

            context.Rooms.AddRange(rooms);
        }

        private static int GetRowNo(int seatNo)
        {
            return (seatNo / 10) + 1;
        }

        private static void AddSeats(ApplicationDbContext context)
        {
            var seats = new List<Seat>();
            int totalSeats = 40;

            for (int i = 0; i < totalSeats; i ++)
            {
                seats.Add(new Seat() { Price = 100, RoomNo = 1, SeatNo = i+1, RowNo = GetRowNo(i)});
            }

            totalSeats = 50;
            for (int i = 0; i < totalSeats; i++)
            {
                seats.Add(new Seat() { Price = 90, RoomNo = 2, SeatNo = i+1, RowNo = GetRowNo(i) });
            }
            context.Seats.AddRange(seats);
        }

        private static void AddScreening(ApplicationDbContext context, int movieId, int roomNo, DateTime startTime, String screenType)
        {
            var screening = new Screening() { MovieId = movieId, RoomNo = roomNo, StartTime = startTime, ScreenType = screenType };
            context.Screenings.Add(screening);
        }

        private static void AddSeatScreening(ApplicationDbContext context, int roomNo)
        {
            List<int> screeningIds = new List<int>();
            screeningIds = context.Screenings.Select(i => i.Id).ToList();

            var seats = new List<Seat>();
            seats = context.Seats.Where(s => s.RoomNo == roomNo).ToList();

            foreach (Seat seat in seats)
            {
                foreach (int id in screeningIds)
                {
                    context.SeatScreeningMediator.Add(new SeatScreening() { Occupied = false, RoomNo = roomNo, ScreeningId = id, SeatNo = seat.SeatNo, SeatId = seat.Id});
                }
            }
        }
    }
}
