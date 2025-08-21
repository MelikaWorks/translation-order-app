 #region[ticket]
        public ActionResult TicketList()
        {
            if (Session["Cstmr_Email"] == null)
                return RedirectToAction("CustomerLogin", "Account");

            string idq = Session["Cstmr_Id"].ToString();
            int id = Convert.ToInt32(idq);
            string email = Session["Cstmr_Email"].ToString();

            var qemail = db.Customers.SingleOrDefault(a => a.Cstmr_Email == email);
            if (qemail == null)
            {
                return RedirectToAction("CustomerLogin", "Account");
            }
            else
            {
                var tickets = db.Tickets
                                .Where(a => a.Cstmr_Id == qemail.Cstmr_Id)
                                .OrderByDescending(a => a.Tick_Dte)
                                .ToList();

                // Optional: you can show an empty state in the view if !tickets.Any()
                return View(tickets);
            }
        }

        public ActionResult Ticket()
        {
            if (Session["Cstmr_Email"] == null)
                return RedirectToAction("CustomerLogin", "Account");

            string idq = Session["Cstmr_Id"].ToString();
            int id = Convert.ToInt32(idq);
            string email = Session["Cstmr_Email"].ToString();

            var qemail = db.Customers.SingleOrDefault(a => a.Cstmr_Email == email);
            if (qemail == null)
            {
                return RedirectToAction("CustomerLogin", "Account");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Ticket(TicketView model, HttpPostedFileBase file)
        {
            if (Session["Cstmr_Email"] == null)
                return RedirectToAction("CustomerLogin", "Account");

            string idq = Session["Cstmr_Id"].ToString();
            int id = Convert.ToInt32(idq);
            string email = Session["Cstmr_Email"].ToString();

            var qemail = db.Customers.SingleOrDefault(a => a.Cstmr_Email == email);
            if (qemail == null)
            {
                return RedirectToAction("CustomerLogin", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var tik = new Ticket
                    {
                        Tick_Title = model.Tick_Title,
                        Tick_Body = model.Tick_Desc,
                        Cstmr_Id = id,
                        Tick_Dte = (DateTimeNow).ToString("MM-dd-yyyy HH:mm:ss"),
                        Tick_UpCust = true,
                        Tick_Status = "New Ticket",
                    };

                    string stamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");

                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = stamp + email + "-" + System.IO.Path.GetFileName(file.FileName);
                        string path = "~/ticket/" + fileName;
                        tik.Tick_FilePath = path;
                        file.SaveAs(Server.MapPath(path));
                    }
                    else
                    {
                        throw new Exception("Error while uploading");
                    }

                    db.Tickets.Add(tik);
                    db.SaveChanges();

                    Session["tickid"] = tik.Tick_Id;
                    ModelState.Clear();

                    //ViewBag.message = "Your ticket has been submitted. Our support team will contact you soon.";
                    return RedirectToAction("TicketSuccess");
                }
                else
                {
                    ViewBag.message = "Please fill in all required fields.";
                    return View(new TicketView());
                }
            }
        }

        public ActionResult TicketOrder(int id)
        {
            if (Session["Cstmr_Email"] == null)
                return RedirectToAction("CustomerLogin", "Account");

            string idq = Session["Cstmr_Id"].ToString();
            int ids = Convert.ToInt32(idq);
            string email = Session["Cstmr_Email"].ToString();

            var qemail = db.Customers.SingleOrDefault(a => a.Cstmr_Email == email);
            if (qemail == null)
            {
                return RedirectToAction("CustomerLogin", "Account");
            }
            else
            {
                var model = new TicketView();
                if (id > 0)
                {
                    model.File_Id = id;
                }

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult TicketOrder(TicketView model, HttpPostedFileBase file)
        {
            if (Session["Cstmr_Email"] == null)
                return RedirectToAction("CustomerLogin", "Account");

            string idq = Session["Cstmr_Id"].ToString();
            int ids = Convert.ToInt32(idq);
            string email = Session["Cstmr_Email"].ToString();

            var qemail = db.Customers.SingleOrDefault(a => a.Cstmr_Email == email);
            if (qemail == null)
            {
                return RedirectToAction("CustomerLogin", "Account");
            }
            else
            {
                var tik = new Ticket
                {
                    Tick_Title = model.Tick_Title,
                    Tick_Body = model.Tick_Desc,
                    Cstmr_Id = ids,
                    File_Id = model.File_Id,
                    Tick_Dte = (DateTimeNow).ToString("MM-dd-yyyy HH:mm:ss"),
                    Tick_UpCust = true,
                    Tick_Status = "New Ticket",
                };

                string stamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");

                if (file != null && file.ContentLength > 0)
                {
                    string fileName = stamp + email + "-" + System.IO.Path.GetFileName(file.FileName);
                    string path = "~/ticket/" + fileName;
                    tik.Tick_FilePath = path;
                    file.SaveAs(Server.MapPath(path));
                }
                else
                {
                    throw new Exception("Error while uploading");
                }

                db.Tickets.Add(tik);
                db.SaveChanges();

                Session["tickid"] = tik.Tick_Id;
                ModelState.Clear();

                //ViewBag.message = "Your ticket has been submitted. Our support team will contact you soon.";
                return RedirectToAction("TicketSuccess");
            }
        }

        public ActionResult TicketShow(int id)
        {
            if (Session["Cstmr_Email"] == null)
                return RedirectToAction("CustomerLogin", "Account");

            string idq = Session["Cstmr_Id"].ToString();
            int ids = Convert.ToInt32(idq);
            string email = Session["Cstmr_Email"].ToString();

            var qemail = db.Customers.SingleOrDefault(a => a.Cstmr_Email == email);
            if (qemail == null)
            {
                return RedirectToAction("CustomerLogin", "Account");
            }
            else
            {
                var t = db.Tickets.SingleOrDefault(a => a.Tick_Id == id && a.Cstmr_Id == ids);

                var t1 = db.Tickets
                           .Where(b => (b.Tick_ParentId == id && b.Cstmr_Id == ids) || (b.Tick_Id == id))
                           .OrderByDescending(c => c.Tick_Id)
                           .ToList();

                if (t == null)
                {
                    return RedirectToAction("TicketList");
                }
                else
                {
                    t.Tick_UpCust = false;
                    db.Tickets.Attach(t);
                    db.Entry(t).State = EntityState.Modified;
                    db.SaveChanges();
                    return View(t1);
                }
            }
        }

        public ActionResult TicketReply(int id)
        {
            if (Session["Cstmr_Email"] == null)
                return RedirectToAction("CustomerLogin", "Account");

            string idq = Session["Cstmr_Id"].ToString();
            int ids = Convert.ToInt32(idq);
            string email = Session["Cstmr_Email"].ToString();

            var qemail = db.Customers.SingleOrDefault(a => a.Cstmr_Email == email);
            if (qemail == null)
            {
                return RedirectToAction("CustomerLogin", "Account");
            }
            else
            {
                Session["nameCust"] = qemail.PCstmr_Name ?? qemail.Cstmr_Email;

                var j = (from a in db.Tickets
                         where (a.Cstmr_Id == ids && a.Tick_ParentId == id && a.Tick_ParentPrim != null && a.admin_Id != null)
                         orderby a.Tick_Id descending
                         select a.Tick_Id).FirstOrDefault();

                if (j == 0)
                {
                    var i = (from a in db.Tickets
                             where (a.Cstmr_Id == ids && a.Tick_ParentId == null && a.Tick_ParentPrim == null && a.admin_Id == null)
                             orderby a.Tick_Id descending
                             select a.Tick_Id).FirstOrDefault();

                    Session["prime"] = i;
                    Session["tickid"] = id;

                    return PartialView(new Ticket()
                    {
                        Tick_Id = id,
                        Cstmr_Id = ids,
                        Tick_ParentPrim = i,
                    });
                }
                else
                {
                    Session["prime"] = j;
                    Session["tickid"] = id;

                    return PartialView(new Ticket()
                    {
                        Tick_Id = id,
                        Cstmr_Id = ids,
                        Tick_ParentPrim = j,
                    });
                }
            }
        }

        [HttpPost]
        public ActionResult TicketReply(Ticket tick, string Btxt, int id)
        {
            if (Session["Cstmr_Email"] == null)
                return RedirectToAction("CustomerLogin", "Account");

            string idq = Session["Cstmr_Id"].ToString();
            int ids = Convert.ToInt32(idq);
            string email = Session["Cstmr_Email"].ToString();

            var qemail = db.Customers.SingleOrDefault(a => a.Cstmr_Email == email);
            if (qemail == null)
            {
                return RedirectToAction("CustomerLogin", "Account");
            }
            else
            {
                Session["nameCust"] = qemail.PCstmr_Name;

                var j = (from a in db.Tickets
                         where (a.Cstmr_Id == ids && a.Tick_ParentId == id && a.Tick_ParentPrim != null && a.admin_Id != null)
                         orderby a.Tick_Id descending
                         select a.Tick_Id).FirstOrDefault();

                if (j == 0)
                {
                    var i = (from a in db.Tickets
                             where (a.Cstmr_Id == ids && a.Tick_ParentId == null && a.Tick_ParentPrim == null && a.admin_Id == null)
                             orderby a.Tick_Id descending
                             select a.Tick_Id).FirstOrDefault();
                    tick.Tick_ParentPrim = Convert.ToInt32(i);
                }
                else
                {
                    tick.Tick_ParentPrim = Convert.ToInt32(j);
                }

                tick.Tick_Dte = (DateTimeNow).ToString("MM-dd-yyyy HH:mm:ss");
                tick.Tick_ParentId = id;
                tick.Tick_Body = Btxt;
                tick.Cstmr_Id = ids;
                tick.Tick_Title = null;
                tick.admin_Id = null;

                db.Tickets.Add(tick);

                var t = db.Tickets.SingleOrDefault(a => a.Tick_Id == id && a.Cstmr_Id == ids);
                t.Tick_UpCust = true;
                t.Tick_DatModif = (DateTimeNow).ToString("MM-dd-yyyy HH:mm:ss");
                t.Tick_Status = "Customer Message";

                db.Tickets.Attach(t);
                db.Entry(t).State = EntityState.Modified;

                db.SaveChanges();

                Session["tickid"] = tick.Tick_Id;
                return RedirectToAction("TicketSuccess");
            }
        }

        public ActionResult TicketSuccess()
        {
            if (Session["Cstmr_Email"] == null)
                return RedirectToAction("CustomerLogin", "Account");
            return View();
        }
#endregion
