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

```mermaid
sequenceDiagram
    participant C as Customer
    participant S as System
    participant A as Admin

    C->>S: Submit Ticket (Title + Body + File)
    S->>A: Notify Admin (New Ticket: Unread)
    A->>S: Reply to Ticket
    S->>C: Notify Customer (Replied)
    C->>S: Customer Reply
    S->>A: Update Status (Customer Message)
    A->>S: Close Ticket
    S->>C: Status = Closed 


✍️ **Author:** Melika Mehranpour  
📌 Project: *Translation Order Management Platform*  
