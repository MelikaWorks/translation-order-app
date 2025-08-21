#region [ticket]
        public ActionResult TicketList()
        {
            if (Session["admin_Email"] == null)
                return RedirectToAction("Cms", "Account");

            string email = Session["admin_Email"].ToString();
            var qemail = db.Admins.Where(a => a.admin_Email == email).SingleOrDefault();
            if (qemail == null)
            {
                return RedirectToAction("Cms", "Account");
            }
            else
            {
                var qlist = db.Tickets
                              .OrderByDescending(a => a.Tick_Dte)
                              .ToList();

                if (!qlist.Any())
                {
                    ViewBag.error = "No files found!";
                    return View(qlist);
                }
                else
                {
                    // Display files
                    ViewBag.error = TempData["OK"];
                    ViewBag.State = TempData["Result"];
                    return View(qlist);
                }
            }
        }

        public ActionResult TicketShow(int id)
        {
            if (Session["admin_Email"] == null)
                return RedirectToAction("Cms", "Account");

            string email = Session["admin_Email"].ToString();
            var qemail = db.Admins.Where(a => a.admin_Email == email).SingleOrDefault();
            if (qemail == null)
            {
                return RedirectToAction("Cms", "Account");
            }
            else
            {
                var idcs = (from a in db.Tickets where (a.Tick_Id == id) select a.Cstmr_Id).FirstOrDefault();
                var t = db.Tickets.Where(a => a.Tick_Id == id).SingleOrDefault();

                var t1 = db.Tickets
                           .Where(b => (b.Tick_Id == id) || (b.Tick_ParentId == id && b.Cstmr_Id == idcs))
                           .OrderByDescending(c => c.Tick_Id)
                           .ToList();

                if (t == null)
                {
                    return RedirectToAction("TicketList");
                }
                else
                {
                    t.Tick_UpAdmin = false;
                    db.Tickets.Attach(t);
                    db.Entry(t).State = EntityState.Modified;
                    db.SaveChanges();
                    return View(t1);
                }
            }
        }

        public ActionResult TicketReply(int id)
        {
            if (Session["admin_Email"] == null)
                return RedirectToAction("Cms", "Account");

            string email = Session["admin_Email"].ToString();
            var qemail = db.Admins.Where(a => a.admin_Email == email).SingleOrDefault();

            if (qemail == null)
            {
                return RedirectToAction("Cms", "Account");
            }
            else
            {
                Session["idadmin"] = qemail.admin_Id;

                var idcs = (from a in db.Tickets where (a.Tick_Id == id) select a.Cstmr_Id).FirstOrDefault();
                var j = (from a in db.Tickets where (a.Cstmr_Id == idcs && a.Tick_ParentId == id && a.Tick_ParentPrim != null && a.admin_Id == null) select a.Tick_Id).OrderByDescending(a => a).FirstOrDefault();

                if (j == 0)
                {
                    var i = (from a in db.Tickets where (a.Cstmr_Id == idcs && a.Tick_ParentId == null && a.Tick_ParentPrim == null && a.admin_Id == null) select a.Tick_Id).OrderByDescending(a => a).FirstOrDefault();
                    return PartialView(new Ticket()
                    {
                        Tick_Id = id,
                        Cstmr_Id = idcs,
                        Tick_ParentPrim = i,
                    });
                }
                else
                {
                    return PartialView(new Ticket()
                    {
                        Tick_Id = id,
                        Cstmr_Id = idcs,
                        Tick_ParentPrim = j,
                    });
                }
            }
        }

        [HttpPost]
        public ActionResult TicketReply(Ticket tick, string Btxt, int id)
        {
            if (Session["admin_Email"] == null)
                return RedirectToAction("Cms", "Account");

            string email = Session["admin_Email"].ToString();
            var qemail = db.Admins.Where(a => a.admin_Email == email).SingleOrDefault();
            if (qemail == null)
            {
                return RedirectToAction("Cms", "Account");
            }
            else
            {
                var idcs = (from a in db.Tickets where (a.Tick_Id == id) select a.Cstmr_Id).FirstOrDefault();
                var j = (from a in db.Tickets where (a.Cstmr_Id == idcs && a.Tick_ParentId == id && a.Tick_ParentPrim != null && a.admin_Id == null) select a.Tick_Id).OrderByDescending(a => a).FirstOrDefault();

                if (j == 0)
                {
                    var i = (from a in db.Tickets where (a.Cstmr_Id == idcs && a.Tick_ParentId == null && a.Tick_ParentPrim == null && a.admin_Id == null) select a.Tick_Id).OrderByDescending(a => a).FirstOrDefault();
                    tick.Tick_ParentPrim = Convert.ToInt32(i);
                }
                else
                {
                    tick.Tick_ParentPrim = Convert.ToInt32(j);
                }
                tick.Tick_Dte = ((DateTimeNow).ToString("MM-dd-yyyy HH:mm:ss"));
                tick.Tick_ParentId = Convert.ToInt32(id);
                tick.Tick_Body = Btxt;
                tick.Cstmr_Id = idcs;
                tick.Tick_Title = null;
                tick.admin_Id = qemail.admin_Id;
                db.Tickets.Add(tick);
                ////////////////////////////////////////////////////////////
                var t = db.Tickets.Where(a => a.Tick_Id == id && a.Cstmr_Id == idcs).SingleOrDefault();
                t.Tick_UpAdmin = true;
                t.Tick_DatModif = ((DateTimeNow).ToString("MM-dd-yyyy HH:mm:ss"));
                t.Tick_Status = "Replied";
                db.Tickets.Attach(t);
                db.Entry(t).State = EntityState.Modified;
                ///////////////////////////////////////////////////////////     
                db.SaveChanges();
                long idtick = tick.Tick_Id;
                Session["tickid"] = idtick;
                return RedirectToAction("TicketList");
            }
        }

        public ActionResult TicketClose(int id)
        {
            if (Session["admin_Email"] == null)
                return RedirectToAction("Cms", "Account");

            string email = Session["admin_Email"].ToString();
            var qemail = db.Admins.Where(a => a.admin_Email == email).SingleOrDefault();

            if (qemail == null)
            {
                return RedirectToAction("Cms", "Account");
            }
            else
            {
                Session["idadmin"] = qemail.admin_Id;

                var idcs = (from a in db.Tickets where (a.Tick_Id == id) select a.Cstmr_Id).FirstOrDefault();
                var j = (from a in db.Tickets where (a.Cstmr_Id == idcs && a.Tick_ParentId == id && a.Tick_ParentPrim != null && a.admin_Id == null) select a.Tick_Id).OrderByDescending(a => a).FirstOrDefault();
                if (j == 0)
                {
                    var i = (from a in db.Tickets where (a.Cstmr_Id == idcs && a.Tick_ParentId == null && a.Tick_ParentPrim == null && a.admin_Id == null) select a.Tick_Id).OrderByDescending(a => a).FirstOrDefault();
                    return PartialView(new Ticket()
                    {
                        Tick_Id = id,
                        Cstmr_Id = idcs,
                        Tick_ParentPrim = i,
                    });
                }
                else
                {
                    return PartialView(new Ticket()
                    {
                        Tick_Id = id,
                        Cstmr_Id = idcs,
                        Tick_ParentPrim = j,
                    });
                }
            }
        }

        [HttpPost]
        public ActionResult TicketClose(Ticket tick, bool? ReRead, int id)
        {
            if (Session["admin_Email"] == null)
                return RedirectToAction("Cms", "Account");

            string email = Session["admin_Email"].ToString();
            var qemail = db.Admins.Where(a => a.admin_Email == email).SingleOrDefault();
            if (qemail == null)
            {
                return RedirectToAction("Cms", "Account");
            }
            else
            {
                var idcs = (from a in db.Tickets where (a.Tick_Id == id) select a.Cstmr_Id).FirstOrDefault();
                var j = (from a in db.Tickets where (a.Cstmr_Id == idcs && a.Tick_ParentId == id && a.Tick_ParentPrim != null && a.admin_Id == null) select a.Tick_Id).OrderByDescending(a => a).FirstOrDefault();

                if (j == 0)
                {
                    var i = (from a in db.Tickets where (a.Cstmr_Id == idcs && a.Tick_ParentId == null && a.Tick_ParentPrim == null && a.admin_Id == null) select a.Tick_Id).OrderByDescending(a => a).FirstOrDefault();
                    tick.Tick_ParentPrim = Convert.ToInt32(i);
                }
                else
                {
                    tick.Tick_ParentPrim = Convert.ToInt32(j);
                }
                //tick.Tick_Dte = ((DateTimeNow).ToString("MM-dd-yyyy HH:mm:ss"));
                //tick.Tick_ParentId = Convert.ToInt32(id);             
                //tick.Cstmr_Id = idcs;
                //tick.Tick_Title = null;
                //tick.admin_Id = qemail.admin_Id;
                //db.Tickets.Add(tick);
                ////////////////////////////////////////////////////////////
                var t = db.Tickets.Where(a => a.Tick_Id == id && a.Cstmr_Id == idcs).SingleOrDefault();
                t.Tick_UpAdmin = true;
                t.Tick_DatModif = ((DateTimeNow).ToString("MM-dd-yyyy HH:mm:ss"));
                t.Tick_Status = "Closed";
                db.Tickets.Attach(t);
                db.Entry(t).State = EntityState.Modified;
                ///////////////////////////////////////////////////////////     
                db.SaveChanges();
                long idtick = tick.Tick_Id;
                Session["tickid"] = idtick;
                return RedirectToAction("TicketList");
            }
        }
#endregion
