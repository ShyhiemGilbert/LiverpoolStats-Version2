using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LiverpoolStats.Models;

namespace LiverpoolStats.Controllers
{
    public class FirstTeamController : Controller
    {
        private readonly LiverpoolDbEntities db = new LiverpoolDbEntities();

        // GET: FirstTeam
        public ActionResult Index(string searchString, string positionOptions)
        {
			//Creating the position selection list for dropdown list
			List<string> positionList = new List<string> ();
			var positionQuery = from p in db.LiverpoolFirstTeamStatsTbls
								orderby p.playerPosition
								select p.playerPosition;
			positionList.AddRange(positionQuery.Distinct());
			ViewBag.positionOptions = new SelectList(positionList);
			//Getting all players from db
			var players = from p in db.LiverpoolFirstTeamStatsTbls
						  select p;

			//Filter by position
			if (!String.IsNullOrEmpty(positionOptions))
			{
				players = players.Where(x => x.playerPosition == positionOptions);
			}


			//Searching by title
			if (!String.IsNullOrEmpty(searchString))
			{
				players = players.Where(x => x.playerName.Contains(searchString));
			}
            return View(players);
        }

        // GET: FirstTeam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiverpoolFirstTeamStatsTbl liverpoolFirstTeamStatsTbl = db.LiverpoolFirstTeamStatsTbls.Find(id);
            if (liverpoolFirstTeamStatsTbl == null)
            {
                return HttpNotFound();
            }
            return View(liverpoolFirstTeamStatsTbl);
        }

        // GET: FirstTeam/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FirstTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LiverpoolFirstTeamStatsTbl liverpoolFirstTeamStatsTbl)
        {
            if (ModelState.IsValid)
            {
                db.LiverpoolFirstTeamStatsTbls.Add(liverpoolFirstTeamStatsTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(liverpoolFirstTeamStatsTbl);
        }

        // GET: FirstTeam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiverpoolFirstTeamStatsTbl liverpoolFirstTeamStatsTbl = db.LiverpoolFirstTeamStatsTbls.Find(id);
            if (liverpoolFirstTeamStatsTbl == null)
            {
                return HttpNotFound();
            }
            return View(liverpoolFirstTeamStatsTbl);
        }

        // POST: FirstTeam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "playerId,playerName,playerPosition,playerAge,playerApps,playerGoals,playerYellowCards,playerRedCards,playerImg")] LiverpoolFirstTeamStatsTbl liverpoolFirstTeamStatsTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liverpoolFirstTeamStatsTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(liverpoolFirstTeamStatsTbl);
        }

        // GET: FirstTeam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiverpoolFirstTeamStatsTbl liverpoolFirstTeamStatsTbl = db.LiverpoolFirstTeamStatsTbls.Find(id);
            if (liverpoolFirstTeamStatsTbl == null)
            {
                return HttpNotFound();
            }
            return View(liverpoolFirstTeamStatsTbl);
        }

        // POST: FirstTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LiverpoolFirstTeamStatsTbl liverpoolFirstTeamStatsTbl = db.LiverpoolFirstTeamStatsTbls.Find(id);
            db.LiverpoolFirstTeamStatsTbls.Remove(liverpoolFirstTeamStatsTbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
