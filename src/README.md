# 🎫 Translation Order Management - Ticket System (Sample)

This repository contains a **sample Ticketing System module** from the *Translation Order Management Platform*.  
The focus here is on the **Customer–Admin support ticket workflow** as a standalone showcase.

---

## 📌 Why all files are in one folder?

To keep this repository **simple and focused**, all files (Controllers, Views, and documentation) are placed together inside the `/src` folder.  
This is not a full project structure — it is only the **Ticket System** extracted from a larger platform to demonstrate clean code and documentation style.

---

## 🚀 Features

- 👤 **Customers** can:
  - Create and view tickets  
  - Reply to admin messages  
  - Upload attachments  

- 🛠️ **Admins** can:
  - View all tickets  
  - Reply to customers  
  - Close resolved tickets  

- 🔔 **Status Tracking** for every ticket:
  - Unread / Read  
  - Customer Message  
  - Replied  
  - Closed  

---

## 🛠️ Technologies Used

- **ASP.NET MVC 5** (C#)  
- **Entity Framework**  
- **Razor View Engine**  
- **jQuery + Bootstrap**  
- **Persian Date Extension**  

---

## Example Status Labels

- ✅ **Unread** → `"Status: Unread"`  
- 📖 **Read** → `"Status: Read"`  
- 📨 **Customer Message** → `"Customer replied"`  
- 🛠️ **Replied** → `"Admin replied"`  
- 🔒 **Closed** → `"Status: Closed"`  

---

## 📊 Ticket Workflow Diagram

Customer        → System  : Submit Ticket (Title + Body + File)
System          → Admin   : Notify (New Ticket: Unread)
Admin           → System  : Reply to Ticket
System          → Customer: Notify (Replied)
Customer        → System  : Customer Reply
System          → Admin   : Update Status (Customer Message)
Admin           → System  : Close Ticket
System          → Customer: Status = Closed


✍️ **Author:** Melika Mehranpour  
📌 Project: *Translation Order Management Platform*  
