🎫 Customer Ticket View

This module provides the ticket viewing and interaction interface for customers.
It allows customers to view their submitted tickets, track responses from admins, and reply directly within the platform.

🚀 Features

📋 My Tickets → View all tickets submitted by the customer.

👁️ Ticket Details → Show conversation history with the admin.

💬 Reply → Customers can reply to admin messages.

📎 Attachments → Upload and download files related to the ticket.

🔔 Status Tracking → See if the ticket is Unread, Replied, or Closed.

🛠️ Technologies Used

ASP.NET MVC 5 (C#)

Entity Framework (Database ORM)

Razor View Engine

jQuery / AJAX (for dynamic updates)

CSS / Bootstrap (UI styling)

📂 File Structure
/Views/Customer/Ticket/
│── TicketList.cshtml        # Show list of all tickets
│── TicketShow.cshtml        # Show full ticket conversation
│── TicketReply.cshtml       # Reply form (partial view)
│── TicketSuccess.cshtml     # Confirmation after submission

Example Status Labels

✅ Unread → "Status: Unread" (highlighted in green).

📖 Read → "Status: Read" (shown in gray).

✍️ Author: Melika Mehranpour
📌 Project: Translation Order Management Platform
