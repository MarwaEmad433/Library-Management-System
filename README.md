# Library Management System 📚

**Project Description**  
The Library Management System project is designed to provide an efficient way to manage books, track requests, and help users borrow books in an organized manner. This system was developed using ASP.NET MVC, and includes an admin panel to manage content and user functions to view book details and place borrowing requests.

---

## Table of Contents

1. [Introduction](#Introduction)
2. [Objectives](#Objectives)
3. [Project Structure](#Project-Structure)
4. [Main Features](#Main-Features)
5. [Technologies Used](#Technologies-Used)
6. [Acknowledgements](#Acknowledgements)

---

## Introduction
The **Library Management System** is a project developed as part of the summer training at ITI. This system manages books, allows users to view book details, and request to borrow books. It also provides features like user management and tracking transactions for borrowed books.

---

## Objectives
- **Efficient Book Management**: Add books, categorize them, and update their information and availability.
- **Request Tracking**: Allow users to request books and track the status of their requests.
- **Improved User Experience**: Provide a simple, easy-to-navigate interface with quick access to data.

---

## Project Structure

### Main Folders:

- **Models**: Contains definitions of the core entities like `Admin`, `Book`, `User`, and `Transaction`, which represent the data and models in the system.
- **Controllers**: Contains the controllers responsible for managing the interaction between the user and the interface.
- **Views**: Contains the Razor pages that display the data and take user input.
- **wwwroot**: Resources such as CSS, JavaScript, and images.

---

## Main Features

### 1. Book Management
- **Add a Book**: Admins can add new books with details such as title, category, author, and number of copies available.
- **Update Book Information**: Admins can update the details of any book.
- **Delete a Book**: Books that are no longer available can be deleted from the system.

### 2. User Management
- **Register Users**: Admins can add new users and update their information.
- **Login**: Users and admins can log in using their credentials.
- **Request Book Borrowing**: Users can place a request to borrow available books.

### 3. Transaction Tracking
- **Add Borrowing Request**: Users can request to borrow books, and the system logs the request.
- **Update Transaction Status**: Admins can change the status of a transaction (pending, completed, rejected).
- **View Transaction History**: Users can view their borrowing history.

### 4. Admin Permissions
- **Full Access to System Data**: Admins have access to all system features, allowing them to manage books, users, and transactions.

---

## Technologies Used

- **ASP.NET Core MVC**: Framework for building the application and developing the user interface.
- **Entity Framework Core**: For interacting with the database using object-oriented programming.
- **SQL Server**: Database used for storing and managing books, users, and transactions.
- **HTML/CSS**: For designing and styling the web pages.
- **JavaScript/jQuery**: For improving user interactions and enhancing the user experience.

---
