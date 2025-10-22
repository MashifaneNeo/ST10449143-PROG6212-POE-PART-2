# PROG6212 POE PART 2

# Contract Monthly Claim System (CMCS)

Project Overview
The Contract Monthly Claim System (CMCS) is a modern ASP.NET Core web application built to simplify how contract lecturers submit, review, and track their monthly payment claims. Instead of relying on slow, paper-based processes, CMCS offers a secure, easy-to-use online platform for educational institutions to manage lecturer payments. Through CMCS, the entire claim process is digital — from the initial submission to approval and final payment. Lecturers can see the real-time status of their claims, upload supporting documents, and receive updates at every stage. The result is a faster, more transparent, and organized workflow for both lecturers and administrators.

Core Functionality
Lecturers can submit their monthly claims by entering details such as hours worked, hourly rates, and attaching relevant supporting documents. The system supports a wide range of file formats including PDFs, Word documents, Excel spreadsheets, and image files. Once submitted, claims automatically move into an “Under Review” stage for coordinators to verify. Administrators can then approve or reject claims with explanations, ensuring fairness and accountability. Every claim is timestamped and logged, providing a complete audit trail for tracking and compliance.

Technical Architecture
The system is built on the ASP.NET Core MVC framework and uses Entity Framework Core for managing data within a SQL Server database. Its design follows a clear separation of concerns, using:
Service layers for business logic,
Repository patterns for clean data access, and
ViewModels for organizing user interface data.
Robust error handling ensures stability and provides helpful feedback to users when something goes wrong. The user interface is designed with Bootstrap and Font Awesome, making it both responsive and visually appealing across devices.

Security and Validation
Security is a core part of the CMCS design. 
The system includes:
Anti-forgery token validation to prevent unauthorized actions,
Strict file upload controls to allow only safe document types and sizes.

Testing and Reliability
The system undergoes thorough testing using the xUnit framework. These tests verify the accuracy of financial calculations, file handling, and data consistency. Edge cases are carefully handled to maintain smooth operation even under unusual conditions.

Friendly error messages help users recover from issues without exposing system details. Thanks to its modular architecture, CMCS is easy to maintain, extend, and improve as future needs arise.
