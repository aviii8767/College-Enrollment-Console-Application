# College-Enrollment-Console-Application

## 📌 Project Overview
The College Enrollment Console Application is a .NET-based C# console application integrated with a MySQL database. It simulates the academic and administrative processes of a college, with structured role-based access. The system includes four distinct user roles: Student, Teacher, Admin, and Super-Admin, each having specific menus and permissions.

All data operations are handled using ADO.NET, making it a strong backend system to understand database-driven application development.

Sure, here's the **final updated `README.md`** with the addition of **“.NET”** usage clearly highlighted throughout. I've emphasized it's a **.NET-based C# console application**, which adds credibility and clarity to your tech stack.

---

## 🚀 Technologies Used

* **Programming Language**: C# (.NET Framework / .NET Core)
* **Framework**: .NET (Console Application)
* **Database**: MySQL
* **Data Access Layer**: ADO.NET
* **Development Environment**: Visual Studio / JetBrains Rider

---

## 🧩 System Architecture

On startup, the user is prompted to select their entity:

```
Who are you?
1. Student
2. Teacher
3. Admin
4. Super Admin
```

Based on the input, the application navigates to the corresponding section with access to specific features.

---

## 👤 1. Student Section

* Prompts: New or Existing Student
* Existing Student enters `StudentID` which is verified against the database.

  * If found: Student menu is shown.
  * If not: Prompt to retry or exit.
* New Student can register via form inputs.

### 📋 Student Features:

* View personal student info
* Update student data
* View enrolled courses
* Enroll in new courses (from list)
* Remove own student account

---

## 👨‍🏫 2. Teacher Section

* Prompts: New or Existing Teacher
* Existing Teacher enters `TeacherID` for verification
* New Teacher can register

### 📋 Teacher Features:

* View and update teacher profile
* View allocated courses
* View students enrolled in their courses
* Add results for students in their courses

---

## 🧑‍💼 3. Admin Section

* Only existing Admins can log in via `AdminID`
* Admin registration is **only allowed by Super Admin**

### 📋 Admin Features:

* View own admin profile
* Manage Students:

  * View all, search by ID, update, remove
* Manage Teachers:

  * View all, search by ID, update, remove
* View Courses, Semesters, Departments

---

## 🧑‍⚖️ 4. Super Admin Section

Super Admin has full privileges across the system and extended admin capabilities.

### 📋 Super Admin Features:

* All Admin-level operations
* Add and remove Admins
* Add new Courses, Departments, Semesters

---

## 💾 Database Design

The backend uses a structured **MySQL relational database** with tables such as:

* `students`, `teachers`, `admins`, `super_admins`
* `courses`, `enrollments`, `results`
* `departments`, `semesters`, etc.

### 🧮 ER Diagram

> ✅ The ER Diagram is included in the project files for better schema understanding.
> *(Example: `![ER Diagram](./assets/er_diagram.png)`)*

---

## 🔌 ADO.NET Integration

All database operations are handled using **ADO.NET**, with:

* `MySqlConnection`
* `MySqlCommand`
* `MySqlDataReader`
* Parameterized queries to avoid SQL injection

This helps in understanding how .NET interacts with relational databases securely.

---

## 🎯 Project Objectives

* Learn .NET backend development with C#
* Practice role-based access logic
* Implement full CRUD operations via ADO.NET
* Understand real-world use cases like enrollment systems
* Prepare for deployment using **AWS EC2** and API integration

---

## 🔜 Upcoming Features

* REST API implementation using **ASP.NET Core**
* Deploy backend APIs on **AWS EC2**
* Add login/authentication flow
* Export data (PDF/CSV)

---

## 📂 How to Run

1. Clone the repo:

   ```bash
   git clone https://github.com/your-username/college-enrollment-console-app.git
   ```

2. Import the MySQL schema using the provided `.sql` file.

3. Update the connection string in `dbConn.cs`:

   ```csharp
   string connString = "server=localhost;user=root;password=yourpassword;database=college_db;";
   ```

4. Open the project in **Visual Studio** and run the application.

---

## 📧 Contact

**Developer**: Avishkar Purushottam Gaware
📫 [avishkargaware.sit.entc.2026@gmail.com](mailto:avishkargaware.sit.entc.2026@gmail.com)
🌐 [Portfolio](https://aviii8767.github.io/portfolio-website/)
🔗 [GitHub](https://github.com/aviii8767)
