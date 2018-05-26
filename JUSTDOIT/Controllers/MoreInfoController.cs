﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using viacinema.Data;
using Microsoft.AspNetCore.Mvc;
using viacinema.Data;
using viacinema.Models;
using viacinema.ViewModels;

namespace viacinema.Controllers
{
    [Route("moreinfo")]
    public class MoreInfoController : Controller
    {
        public ApplicationDbContext context;

        public MoreInfoController(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        [Route("{id}")]
        public IActionResult Index(int id)
        {
            var movie = context.Movies
                .Single(m => m.Id == id);
            var screenings = context.Screenings
                .Where(s => s.MovieId == id && DateTime.Now < s.StartTime)
                .OrderByDescending(s => s.StartTime)
                .ToList();

            List<SeatScreening> seatScreenings = new List<SeatScreening>();
            foreach (var screening in screenings)
            {
                seatScreenings.AddRange(context.SeatScreeningMediator.Where(s => s.ScreeningId == screening.Id).ToList());
            }

            return View(new MoreInfoViewModel(movie, screenings, seatScreenings));
        }

        [HttpGet]
        public IActionResult GetScreenings(int movieId, int screeningId, int roomNo)
        {
            var movie = context.Movies
                .Single(m => m.Id == movieId);
            var screenings = context.Screenings
                .Where(s => s.MovieId == movieId && DateTime.Now < s.StartTime)
                .OrderByDescending(s => s.StartTime)
                .ToList();


            var seatScreenings = context.SeatScreeningMediator.Select(s => s).Where(s => s.ScreeningId == screeningId && s.RoomNo == roomNo).ToList();

            return View(new MoreInfoViewModel(movie, screenings, seatScreenings));
        }
    }
}
